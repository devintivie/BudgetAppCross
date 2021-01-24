using BudgetAppCross.Models;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class BankAccountQuickViewModel : BaseViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
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
        public ICommand EditThisCommand { get; private set; }
        public ICommand DeleteThisCommand { get; private set; }
        #endregion
        
        #region Constructors
        public BankAccountQuickViewModel(IMvxNavigationService navService, BankAccount account)
        {
            navigationService = navService;
            BankAccount = account;
            GetLatestBalance();
            EditThisCommand = new Command(async () => await navigationService.Navigate<EditBankAccountViewModel, BankAccount>(BankAccount));
            DeleteThisCommand = new Command(async () => await OnDeleteThis());
        }
        #endregion

        #region Methods
        private async void GetLatestBalance()
        {
            var temp = await BudgetDatabase.GetLatestBalance(BankAccount.AccountId, DateTime.Today);
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
            await BudgetDatabase.DeleteBankAccount(BankAccount);
            Messenger.Send(new ChangeBalanceMessage());
        }
        #endregion

    }
}
