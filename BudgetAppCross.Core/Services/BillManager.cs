using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core.Services
{
    public class BillManager
    {
        #region Singleton
        private static BillManager instance;
        public static BillManager Instance
        {
            get { return instance ?? (instance = new BillManager()); }
        }

        private BillManager() { }
        #endregion Singleton

        #region Fields

        #endregion

        #region Properties
        public Dictionary<string, BillTracker> TrackersByCompany { get; set; } = new Dictionary<string, BillTracker>(StringComparer.OrdinalIgnoreCase);
        public List<BillTracker> AllTrackers { get; set; } = new List<BillTracker>();
        #endregion

        #region Methods
        public void AddTracker(BillTracker bt)
        {
            TrackersByCompany.Add(bt.CompanyName, bt);
            AllTrackers.Add(bt);
        }

        public void AddBill(string company, Bill iBill)
        {
            if (TrackersByCompany.ContainsKey(company))
            {
                TrackersByCompany[company].Bills.Add(iBill);
            }
            else
            {
                var bt = new BillTracker(company);
                bt.Bills.Add(iBill);

                AddTracker(bt);
            }
        }
        #endregion

    }
}
