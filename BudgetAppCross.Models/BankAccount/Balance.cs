using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using SQLite;
//using SQLiteNetExtensions.Attributes;

namespace BudgetAppCross.Models
{
    public class Balance
    {

        #region Fields

        #endregion

        #region Properties
        public int BalanceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public int AccountId { get; set; }
        public BankAccount BankAccount { get; set; }
        #endregion

        #region Constructors
        public Balance(decimal amount, DateTime time, int acctId)
        {
            Amount = amount;
            Timestamp = time;
            AccountId = acctId;
        }
        public Balance(decimal amount, DateTime time) : this(amount, time, 0) { }

        public Balance() : this(0, DateTime.Today, 0) { }
        #endregion

        #region Methods
        #endregion

    }
}
