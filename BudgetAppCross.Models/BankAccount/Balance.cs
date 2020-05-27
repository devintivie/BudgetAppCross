using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        [ManyToOne(CascadeOperations =CascadeOperation.CascadeRead)]
        public BankAccount BankAccount { get; set; }
        #endregion

        #region Constructors
        public Balance(int accountid, double amount, DateTime time)
        {
            Amount = amount;
            Timestamp = time;
            AccountID = accountid;
        }

        public Balance(int accountid) : this(accountid, 0, DateTime.Today) { }

        public Balance()
        {

        }
        #endregion

        #region Methods
        #endregion

    }
}
