using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models.Bills
{
    public class BudgetModel
    {
        #region Fields

        #endregion

        #region Properties
        public List<BillTracker> BillData { get; set; } = new List<BillTracker>();
        public List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion

    }
}
