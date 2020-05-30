using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BudgetAppCross.Models
{
    public class Grouping<TKey, T> : Collection<T>//ObservableCollection<T>
    {
        #region Fields

        #endregion

        #region Properties
        public TKey Key { get; private set; }
        public List<T> Grouped { get; private set; } = new List<T>();
        #endregion

        #region Constructors
        public Grouping(TKey key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
            {
                Items.Add(item);
                Grouped.Add(item);
            }
        }
        #endregion

        #region Methods
        public IEnumerable<T> GetItems()
        {
            foreach (var item in Items)
            {
                yield return item;
            }
        }
        #endregion

    }

}
