using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Models
{
    public class Balance
    {

        #region Fields

        #endregion

        #region Properties
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public int AccountID { get; set; }
        public BankAccount BankAccount { get; set; }
        #endregion

        #region Constructors
        public Balance(decimal amount, DateTime time)
        {
            Amount = amount;
            Timestamp = time;
        }

        public Balance() : this(0, DateTime.Today) { }
        #endregion

        #region Methods
        #endregion

    }
}
