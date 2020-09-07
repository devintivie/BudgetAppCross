using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core
{
    public enum NavigablePage
    {
        Account,
        LoadBudget,
        DateRange,
        BillList,
        Agenda,
        BankOverview,
        About,
        Purchasing
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
