﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core
{
    public enum NavigablePage
    {
        Paycheck,
        BillList,
        Agenda,
        BankOverview,
        About
    }
    //[TypeConverter(typeof(EnumDescriptionTypeConverter))]
    //public enum Navigation
    //{
    //    Calendar,
    //    [Description("Bills List")]
    //    BillList,
    //    [Description("Banking Overview")]
    //    BankOverview,
    //    [Description("Debt Overview")]
    //    DebtOverview
    //}
}