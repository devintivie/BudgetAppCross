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

        public double Balance
        {
            get { return BankAccount.Balance; }
            set
            {
                var balance = BankAccount.Balance;
                BankAccount.Balance = value;
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
        }
        #endregion

        #region Methods

        #endregion

    }
}
