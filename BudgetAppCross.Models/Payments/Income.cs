using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models
{
    public class Income : ITransaction
    {

        #region Fields

        #endregion

        #region Properties

        public string Source { get; set; }
        #region ITransaction
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Confirmation { get; set; }
        #endregion
        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion

       
    }
}
