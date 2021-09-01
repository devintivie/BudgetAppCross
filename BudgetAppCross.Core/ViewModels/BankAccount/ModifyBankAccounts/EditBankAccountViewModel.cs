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
using BaseClasses;
using MvvmCross.Commands;
using BudgetAppCross.DataAccess;

namespace BudgetAppCross.Core.ViewModels
{
    public class EditBankAccountViewModel : XamarinBaseViewModel<BankAccount>// MvxViewModel
    {
        #region Fields
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
        public IMvxCommand SaveCommand { get; }
        public IMvxCommand CancelCommand { get; }
        #endregion

        #region Constructors
        public EditBankAccountViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            SaveCommand = new MvxAsyncCommand(OnSave);
            CancelCommand = new MvxAsyncCommand(OnCancel);
            
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
                _backgroundHandler.Notify("Invalid Company Name");
                return;
            }
            await _dataManager.SaveBankAccount(BankAccount);

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
