using BaseClasses;
using BaseViewModels;
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
        #endregion

        #region Properties
        public BankAccount BankAccount { get; private set; }

        private decimal balance;
        public decimal Balance
        {
            get { return balance; }
            set
            {
                SetProperty(ref balance, value);
            }
        }


        public string Nickname
        {
            get { return BankAccount.Nickname; }
            set
            {
                var nickname = BankAccount.Nickname;
                BankAccount.Nickname = value;
                SetProperty(ref nickname, value);
            }
        }




        #endregion
       
        #region Commands
        public IMvxCommand EditThisCommand { get; private set; }
        public IMvxCommand DeleteThisCommand { get; private set; }
        #endregion
        
        #region Constructors
        public BankAccountQuickViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager, BankAccount account) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            BankAccount = account;
            var _ = GetLatestBalance();
            _backgroundHandler.RegisterMessage<ChangeBalanceMessage>(this, async x => await OnChangeBalanceMessage());
            EditThisCommand = new MvxAsyncCommand(EditBankAccount);
            DeleteThisCommand = new MvxAsyncCommand(OnDeleteThis);

        }
        #endregion

        #region Methods
        private async Task GetLatestBalance()
        {
            var temp = await _dataManager.GetLatestBalance(BankAccount.AccountID, DateTime.Today);
            //var temp = 0.0;
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
            await _dataManager.DeleteBankAccount(BankAccount);
            _backgroundHandler.SendMessage(new ChangeBalanceMessage());
        }

        private async Task EditBankAccount()
        {
            await _navService.Navigate<EditBankAccountViewModel, BankAccount>(BankAccount);
        }

        private async Task OnChangeBalanceMessage()
        {
            await GetLatestBalance();
        }
        #endregion

    }
}
