using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    //public enum NavigablePage
    //{
    //    //Calendar,
    //    //[Description("Bills List")]
    //    //BillList,
    //    //[Description("Banking Overview")]
    //    //BankOverview,
    //    //[Description("Debt Overview")]
    //    //DebtOverview,


    //    Account,
    //    LoadBudget,
    //    [Description("Date Range")]
    //    DateRange,
    //    [Description("Bills List")]
    //    BillList,
    //    Agenda,
    //    [Description("Bank Overview")]
    //    BankOverview,
    //    About,
    //    Purchasing
    //}
}
