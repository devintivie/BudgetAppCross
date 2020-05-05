using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models
{
    public class BillTracker : ObservableRangeCollection<Bill>, IComparable<BillTracker>
    {
        #region Properties
        public List<Bill> Bills { get; set; } = new List<Bill>();
        public string CompanyName { get; set; }
        public bool Autopay { get; set; }
        #endregion

        #region Constructors
        public BillTracker() : this("No Name") { }

        public BillTracker(string company, bool autopay = false)
        {
            CompanyName = company;
            Autopay = autopay;
        }

        public BillTracker(string name, IEnumerable<Bill> list, bool autopay = false)
        {
            CompanyName = name;
            Bills = new List<Bill>(list);
            Autopay = autopay;
        }
        public BillTracker(string name, Bill firstBill, bool autopay = false)
        {
            CompanyName = name;
            Bills = new List<Bill> { firstBill };
            Autopay = autopay;
        }


        #endregion

        #region IComparable
        public int CompareTo(BillTracker other)
        {
            if (other == null) return 1;
            else
            {
                return CompanyName.CompareTo(other.CompanyName);
            }
        }
        #endregion

    }
}
