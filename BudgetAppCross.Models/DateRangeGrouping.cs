using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models
{
    public class DateRangeGrouping<TKey, T> : Grouping<TKey, T>
    {
        public double Sum { get; set; }
        public double StartingBalance { get; set; }
        public double EndingBalance => StartingBalance - Sum;
        public BankAccount BankAccount { get; set; }
        public DateRangeGrouping(TKey key, IEnumerable<T> items) : base(key, items)
        {
           
        }
    }
}
