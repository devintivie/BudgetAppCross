using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models
{
    public interface ITransaction : IAccountInfo
    {
        string SourceDest { get; set; }
        DateTime Date { get; set; }
        decimal Amount { get; set; }
        string Confirmation { get; set; }
    }

}
