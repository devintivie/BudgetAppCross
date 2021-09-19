using BaseClasses;
using BaseViewModels;
using BudgetAppCross.Core.ViewModels.Pages;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BudgetAppCross.Core.ViewModels
{
    public class BankAccountQuickViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        private IDataManager _dataManager;
        public BankAccount BankAccount { get; private set; }
        //private BankAccount _bankAccount;
        #endregion

        #region Properties

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

        public string Nickname => BankAccount.Nickname; 


        #endregion
       
        #region Commands
        //public IMvxCommand EditThisCommand { get; private set; }
        public IMvxCommand DeleteThisCommand { get; private set; }
        #endregion
        
        #region Constructors
        public BankAccountQuickViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager, BankAccount account) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            BankAccount = account;

            //EditThisCommand = new MvxAsyncCommand(EditBankAccount);
            DeleteThisCommand = new MvxAsyncCommand(OnDeleteThis);

            _backgroundHandler.RegisterMessage<ChangeBalanceMessage>(this, async x => await OnChangeBalanceMessage());
            _ = GetLatestBalance();

        }
        #endregion

        #region Methods
        private async Task GetLatestBalance()
        {
            var temp = await _dataManager.GetLatestBalance(BankAccount.AccountID, DateTime.Today);
            if (temp == null)
            {
                Balance = 0.0m;
            }
            else
            {
                Balance = temp.Amount;
            }
        }

        private async Task OnDeleteThis()
        {
            var continueDelete = await _backgroundHandler.ConfirmAsync($"Are you you want to delete {BankAccount.Nickname}?");

            if (continueDelete)
            {
                var deleted = await _dataManager.DeleteBankAccount(BankAccount);
                if (deleted <= 0)
                {
                    _backgroundHandler.Notify("delete error");
                }
            }
            //await _dataManager.DeleteBankAccount(BankAccount);
            _backgroundHandler.SendMessage(new ChangeBalanceMessage());
        }

        private async Task TestConfirm(bool confirmed)
        {
            if (confirmed)
            {
                await _dataManager.DeleteBankAccount(BankAccount);
            }
        }

        //private async Task EditBankAccount()
        //{
        //    await _navService.Navigate<EditBankAccountViewModel, BankAccount>(BankAccount);
        //}

        private async Task OnChangeBalanceMessage()
        {
            await GetLatestBalance();
        }
        #endregion

    }
}
