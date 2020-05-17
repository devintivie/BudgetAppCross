using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core.ViewModels
{
    public class BankAccountViewModel : BaseViewModel
    {
        #region Fields

        #endregion

        #region Properties
        public BankAccount BankAccount { get; private set; }

        private double balance;
        public double Balance
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

        #region Constructors
        public BankAccountViewModel(BankAccount account)
        {
            BankAccount = account;
            GetLatestBalance();
        }
        #endregion

        #region Methods
        private async void GetLatestBalance()
        {
            //var temp = (double?)(await BudgetDatabase.GetLatestBalanceAsync(DateTime.Today)).Amount;
            var temp = 0.0;
            if(temp == null)
            {
                Balance = 0.0;
            }
            else
            {
                Balance =(double) temp;
            }
        }
        #endregion

    }
}
