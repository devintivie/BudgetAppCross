using BudgetAppCross.Models;
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

        #region Commands
        public ICommand DeleteThisCommand { get; private set; }
        #endregion


        #endregion

        #region Constructors
        public BankAccountQuickViewModel(BankAccount account)
        {
            BankAccount = account;
            GetLatestBalance();

            DeleteThisCommand = new Command(async () => await OnDeleteThis());
        }
        #endregion

        #region Methods
        private async void GetLatestBalance()
        {
            var temp = await BudgetDatabase.GetLatestBalance(BankAccount.AccountID, DateTime.Today);
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
