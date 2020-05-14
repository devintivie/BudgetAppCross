﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models
{
    public interface ITransaction
    {
        string SourceDest { get; set; }
        DateTime Date { get; set; }
        double Amount { get; set; }
        string Confirmation { get; set; }
    }
}
