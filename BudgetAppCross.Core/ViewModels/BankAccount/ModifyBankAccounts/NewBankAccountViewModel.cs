using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//using Acr.UserDialogs;
using System.Linq;
using MvvmCross;
using BaseViewModels;
using MvvmCross.Commands;
using BaseClasses;
using BudgetAppCross.DataAccess;

namespace BudgetAppCross.Core.ViewModels
{
    public class NewBankAccountViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        //private IDataManager DataManager = Mvx.IoCProvider.Resolve<IDataManager>();
        private IDataManager _dataManager;
        #endregion

        #region Properties
        private BankAccount bankAccount;
        public BankAccount BankAccount
        {
            get { return bankAccount; }
            set
            {
                SetProperty(ref bankAccount, value);
            }
        }

        //SelectionLength="{Binding BalanceSelectedLength}"
        //           CursorPosition="{Binding BalanceCursorPosition}"
        private int balanceSelectedLength;
        public int BalanceSelectedLength
        {
            get { return balanceSelectedLength; }
            set
            {
                SetProperty(ref balanceSelectedLength, value);
            }
        }

        private int balanceCursorPosition;
        public int BalanceCursorPosition
        {
            get { return balanceCursorPosition; }
            set
            {
                SetProperty(ref balanceCursorPosition, value);
            }
        }

        private DateTime date = DateTime.Today;
        public DateTime Date
        {
            get { return date; }
            set
            {
                SetProperty(ref date, value);
            }
        }

        private decimal balance;
        public decimal Balance
        {
            get { return balance; }
            set
            {
                SetProperty(ref balance, value);
            }
        }






        #endregion

        #region Commands
        public IMvxCommand SaveCommand { get; }
        public IMvxCommand CancelCommand { get; }
        public IMvxCommand BalanceFocusedCommand { get; }
        public IMvxCommand BalanceUnfocusedCommand { get; }
        #endregion

        #region Constructors
        public NewBankAccountViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            BankAccount = new BankAccount
            {
                Nickname = ""
            };

            SaveCommand = new MvxAsyncCommand(OnSave);
            CancelCommand = new MvxAsyncCommand(OnCancel);
            BalanceFocusedCommand = new MvxCommand(BalanceFocused);
            BalanceUnfocusedCommand = new MvxCommand(BalanceUnfocused);

            
        }
        #endregion

        #region Methods
        private async Task OnSave()
        {
            if (string.IsNullOrWhiteSpace(BankAccount.Nickname))
            {
                _backgroundHandler.Notify("Invalid Company Name");//.SetOkText(ConfirmConfig.DefaultOkText);
                return;
            }

            //BankAccount.History.Add(new Balance(Balance, Date));
            //var bal = new Balance
            //{
            //    Amount = Balance,
            //    Timestamp = Date,
            //    //AccountID = BankAccount.AccountID
            //};
            var bal = new Balance(Balance, Date);
            BankAccount.History.Add(bal);
            await _dataManager.SaveBankAccount(BankAccount);
            //await BudgetDatabase.Instance.SaveBankAccount(BankAccount);


            //bal.AccountID = BankAccount.AccountID;
            //await BudgetDatabase.Instance.SaveBalance(bal);
            //await BudgetDatabase.Instance.SaveBalanceAsync(BankAccount.History.First());
            //BankAccountManager.Instance.AddAccount(BankAccount);
            _backgroundHandler.SendMessage(new ChangeBalanceMessage());
            await _navService.Close(this);
        }

        private async Task OnCancel()
        {
            await _navService.Close(this);
        }

        private void BalanceFocused()
        {
            BalanceCursorPosition = 0;
            BalanceSelectedLength = Balance.ToString().Length;
        }

        private void BalanceUnfocused()
        {
            if (string.IsNullOrWhiteSpace(Balance.ToString()))
            {
                Balance = 0;
            }
        }
        #endregion

    }
}
