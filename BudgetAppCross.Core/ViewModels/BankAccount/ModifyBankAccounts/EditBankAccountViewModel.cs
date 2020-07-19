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
using MvvmCross;

namespace BudgetAppCross.Core.ViewModels
{
    public class EditBankAccountViewModel : BaseViewModel<BankAccount>// MvxViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        //private IDataManager DataManager = Mvx.IoCProvider.Resolve<IDataManager>();
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







        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Constructors
        public EditBankAccountViewModel(IMvxNavigationService nav)
        {
            navigationService = nav;
            SaveCommand = new Command(async () => await OnSave());
            CancelCommand = new Command(async () => await OnCancel());
            
        }

        public override void Prepare(BankAccount parameter)
        {
            BankAccount = parameter;
            Nickname = parameter.Nickname;
        }
        #endregion

        #region Methods
        private async Task OnSave()
        {
            BankAccount.Nickname = Nickname;
            if (string.IsNullOrWhiteSpace(BankAccount.Nickname))
            {
                var config = new AlertConfig().SetMessage("Invalid Company Name");//.SetOkText(ConfirmConfig.DefaultOkText);
                Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(config);
                return;
            }
            await BudgetDatabase.SaveBankAccount(BankAccount);

            Messenger.Send(new ChangeBalanceMessage());
            await navigationService.Close(this);
        }

        private async Task OnCancel()
        {
            await navigationService.Close(this);
        }

        #endregion

    }
}
