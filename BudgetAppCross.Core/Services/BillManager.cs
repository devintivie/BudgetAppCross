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

        private BillManager()
        {
            var firstBill = new Bill(100, new DateTime(2020, 5, 10));
            var firstBt = new BillTracker("AT&T", firstBill);
            AddTracker(firstBt);
        }
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
            AllTrackers.Sort();
        }

        public void AddBill(string company, Bill iBill)
        {
            if (TrackersByCompany.ContainsKey(company))
            {
                TrackersByCompany[company].Bills.Add(iBill);
                TrackersByCompany[company].Bills.Sort();
            }
            else
            {
                var bt = new BillTracker(company, iBill);
                bt.Bills.Add(iBill);

                AddTracker(bt);
            }
        }
        #endregion

    }
}
