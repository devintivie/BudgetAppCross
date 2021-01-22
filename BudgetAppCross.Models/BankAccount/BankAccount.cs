using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models
{
    public class BankAccount
    {
        #region Fields

        #endregion

        #region Properties
        public int AccountID { get; set; }

        //Useful name i.e. Main Account, Savings, College etc.
        //Unique
        public string Nickname { get; set; }
        //Bank account number
        //Not required
        public string AccountNumber { get; set; }
        //Bank name who holds account i.e. Chase, Wells Fargo etc.
        //Does not need to be unique
        public string BankName { get; set; }

        #endregion

        #region Constructors
        public BankAccount() { }// : this(0, "-", "-", "My Account") { }

        public BankAccount(decimal iBalance, string account, string bank, string nickname)
        {
            //CurrentBalance = iBalance;
            AccountNumber = account;
            BankName = bank;
            Nickname = nickname;
        }

        public BankAccount(decimal iBalance, string nickname) : this(iBalance, "-", "-", nickname) { }
        #endregion

        #region Methods

        #endregion





    }
}
