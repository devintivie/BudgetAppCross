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
        //[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }

        //[ForeignKey(typeof(BankAccount))]
        public int AccountID { get; set; }
        //[ManyToOne(CascadeOperations =CascadeOperation.CascadeRead)]
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
