using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models
{
    public enum BillStatus
    {
        Paid,
        PastDue,
        DueToday,
        DueTomorrow,
        DueWithinTwoWeeks,
        DueWithinOneMonth,
        DueInOverOneMonth,
        AutoPayUpcoming,
        AutoPayPast,
        NoneDue,
    }
}
