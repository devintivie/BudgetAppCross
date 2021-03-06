﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models
{
    public enum MultipleBillOptions
    {
        ByDateRange,
        ByNumber,
    }

    public enum DueDateFrequencies
    {
        OneWeek,
        TwoWeek,
        FourWeek,
        Monthly,
        Quarterly,
        SemiAnnually,
        Yearly
    }

    public enum PayDateFrequencies
    {
        Weekly,
        Biweekly,
        FirstAndMid,
    }
}
