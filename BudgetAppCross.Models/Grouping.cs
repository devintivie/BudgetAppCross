using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BudgetAppCross.Models
{
    public class Grouping<TKey, T> : ObservableCollection<T>
    {
        public TKey Key { get; private set; }

        public Grouping(TKey key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
    }
}
