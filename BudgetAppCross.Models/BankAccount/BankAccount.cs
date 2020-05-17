﻿using SQLite;
using SQLiteNetExtensions.Attributes;
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
        //Account Balance
        
        [PrimaryKey, AutoIncrement]
        //Used in app only, no external reference
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

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Balance> History { get; set; } = new List<Balance>();
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Bill> Bills { get; set; } = new List<Bill>();


        #endregion

        #region Constructors
        public BankAccount() : this(0, "-", "-", "My Account") { }

        public BankAccount(double iBalance, string account, string bank, string nickname)
        {
            History.Add(new Balance(iBalance, DateTime.Now));
            //CurrentBalance = iBalance;
            AccountNumber = account;
            BankName = bank;
            Nickname = nickname;
        }
        #endregion

        #region Methods

        #endregion


        

        
    }
}