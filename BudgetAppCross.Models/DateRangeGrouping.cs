using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models
{
    public class DateRangeGrouping<TKey, T> : Grouping<TKey, T>
    {
        public decimal Sum { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal EndingBalance => StartingBalance - Sum;
        public BankAccount BankAccount { get; set; }
        public DateRangeGrouping(TKey key, IEnumerable<T> items) : base(key, items)
        {
           
        }
    }
}
