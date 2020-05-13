using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BudgetAppCross.Core.Services
{
    public class IncomeManager
    {
        #region Singleton
        private static readonly Lazy<IncomeManager> instance = new Lazy<IncomeManager>();
        public static IncomeManager Instance => instance.Value;
        #endregion
        #region Fields

        #endregion

        #region Properties
        public Dictionary<int, Debit> IncomeByID { get; set; } = new Dictionary<int, Debit>();
        public List<Debit> AllIncomes { get; set; } = new List<Debit>();
        #endregion

        #region Methods 
        public void Clear()
        {
            IncomeByID.Clear();
            AllIncomes.Clear();
        }

        public void AddIncome(Debit debit)
        {
            if(AllIncomes.Count != 0)
            {
                IncomeByID.Add(1, debit);
                AllIncomes.Add(debit);
            }
            else
            {
                var nextId = IncomeByID.Keys.Last() + 1;
                IncomeByID.Add(nextId, debit);
                AllIncomes.Add(debit);
            }
        }

        public void DeleteIncome(int key)
        {
            var debit = IncomeByID[key];
            IncomeByID.Remove(key);
            AllIncomes.Remove(debit);

        }
        public void DeleteIncome(Debit debit)
        {
            var key = IncomeByID.First(x => x.Value.Equals(debit)).Key;
            IncomeByID.Remove(key);
            AllIncomes.Remove(debit);
        }

        #endregion

    }
}

