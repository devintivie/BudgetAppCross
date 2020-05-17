using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Linq;

namespace BudgetAppCross.Core.ViewModels
{
    public class NewBankAccountViewModel : MvxViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
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

        private double balance;
        public double Balance
        {
            get { return balance; }
            set
            {
                SetProperty(ref balance, value);
            }
        }






        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand BalanceFocusedCommand { get; }
        public ICommand BalanceUnfocusedCommand { get; }
        #endregion

        #region Constructors
        public NewBankAccountViewModel(IMvxNavigationService nav)
        {
            navigationService = nav;
            BankAccount = new BankAccount
            {
                Nickname = ""
            };

            SaveCommand = new Command(async () => await OnSave());
            CancelCommand = new Command(async () => await OnCancel());
            BalanceFocusedCommand = new Command(() => BalanceFocused());
            BalanceUnfocusedCommand = new Command(() => BalanceUnfocused());
        }
        #endregion

        #region Methods
        private async Task OnSave()
        {
            if (string.IsNullOrWhiteSpace(BankAccount.Nickname))
            {
                var config = new AlertConfig().SetMessage("Invalid Company Name");//.SetOkText(ConfirmConfig.DefaultOkText);
                UserDialogs.Instance.Alert(config);
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
            await BudgetDatabase.Instance.SaveBankAccount(BankAccount);

            //bal.AccountID = BankAccount.AccountID;
            //await BudgetDatabase.Instance.SaveBalance(bal);
            //await BudgetDatabase.Instance.SaveBalanceAsync(BankAccount.History.First());
            //BankAccountManager.Instance.AddAccount(BankAccount);
            await navigationService.Close(this);
        }

        private async Task OnCancel()
        {
            await navigationService.Close(this);
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
