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

namespace BudgetAppCross.Core.ViewModels.Pages
{
    public class NewBankAccountViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        private IDataManager _dataManager;
        //private BankAccount _bankAccount;
        #endregion

        #region Properties
        private string nickname;
        public string Nickname
        {
            get { return nickname; }
            set
            {
                if (nickname != value)
                {
                    nickname = value;
                    RaisePropertyChanged();
                }
            }
        }

        private DateTime date = DateTime.Today;
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    RaisePropertyChanged();
                }
            }
        }

        private decimal balance;
        public decimal Balance
        {
            get { return balance; }
            set
            {
                if (balance != value)
                {
                    balance = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Commands
        public IMvxCommand SaveCommand { get; }
        public IMvxCommand CancelCommand { get; }
        #endregion

        #region Constructors
        public NewBankAccountViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            

            SaveCommand = new MvxAsyncCommand(OnSave);
            CancelCommand = new MvxAsyncCommand(OnCancel);

            
        }
        #endregion

        #region Methods
        private async Task OnSave()
        {
            if (string.IsNullOrWhiteSpace(Nickname))
            {
                _backgroundHandler.Notify("Invalid Company Name");//.SetOkText(ConfirmConfig.DefaultOkText);
                return;
            }
            var bal = new Balance(Balance, Date);
            var bankAccount = new BankAccount
            {
                Nickname = Nickname
            };
            bankAccount.History.Add(bal);
            await _dataManager.SaveBankAccount(bankAccount);
            _backgroundHandler.SendMessage(new ChangeBalanceMessage());
            await _navService.Close(this);
        }

        private async Task OnCancel()
        {
            await _navService.Close(this);
        }
        #endregion

    }
}
