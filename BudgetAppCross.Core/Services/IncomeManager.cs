using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BudgetAppCross.Core.Services
{
    //public class IncomeManager
    //{
    //    #region Singleton
    //    private static readonly Lazy<IncomeManager> instance = new Lazy<IncomeManager>();
    //    public static IncomeManager Instance => instance.Value;
    //    #endregion
    //    #region Fields

    //    #endregion

    //    #region Properties
    //    public Dictionary<int, Income> IncomeByID { get; set; } = new Dictionary<int, Income>();
    //    public List<Income> AllIncomes { get; set; } = new List<Income>();
    //    #endregion

    //    #region Methods 
    //    public void Clear()
    //    {
    //        IncomeByID.Clear();
    //        AllIncomes.Clear();
    //    }

    //    public void AddIncome(Income income)
    //    {
    //        if(AllIncomes.Count != 0)
    //        {
    //            IncomeByID.Add(1, income);
    //            AllIncomes.Add(income);
    //        }
    //        else
    //        {
    //            var nextId = IncomeByID.Keys.Last() + 1;
    //            IncomeByID.Add(nextId, income);
    //            AllIncomes.Add(income);
    //        }
    //    }

    //    public void DeleteIncome(int key)
    //    {
    //        var debit = IncomeByID[key];
    //        IncomeByID.Remove(key);
    //        AllIncomes.Remove(debit);

    //    }
    //    public void DeleteIncome(Income income)
    //    {
    //        var key = IncomeByID.First(x => x.Value.Equals(income)).Key;
    //        IncomeByID.Remove(key);
    //        AllIncomes.Remove(income);
    //    }

    //    #endregion

    //}
}

