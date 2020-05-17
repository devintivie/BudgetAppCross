using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BudgetAppCross.Models
{
    public class Balance
    {

        #region Fields

        #endregion

        #region Properties
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }

        [ForeignKey(typeof(BankAccount))]
        public int AccountID { get; set; }

        #endregion

        #region Constructors
        public Balance(double amount, DateTime time)
        {
            Amount = amount;
            Timestamp = time;
        }

        public Balance()
        {

        }
        #endregion

        #region Methods

        #endregion

    }
}
