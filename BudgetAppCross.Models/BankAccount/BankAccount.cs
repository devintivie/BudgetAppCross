//using SQLite;
//using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models
{
    public class BankAccount// : IAccountInfo
    {
        #region Fields

        #endregion

        #region Properties
        
        //Used in app only, no external reference
        public int AccountId { get; set; }

        //Useful name i.e. Main Account, Savings, College etc.
        //Unique
        //[Unique]
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

        public BankAccount(string nickname, string bank, string account)
        {
            AccountNumber = account;
            BankName = bank;
            Nickname = nickname;
        }

        public BankAccount(string nickname) : this(nickname, "-", "-") { }
        #endregion

        #region Methods

        #endregion


        

        
    }
}
