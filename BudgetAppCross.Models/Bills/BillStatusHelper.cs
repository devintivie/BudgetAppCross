using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models
{
    public static class BillStatusHelper
    {
        public static BillStatus GetBillStatus(DateTime refDate, DateTime compDate, bool isPaid, bool isAuto = false)
        {
            if (isAuto)
            {
                if ((compDate - refDate).TotalDays < 0)
                {
                    return BillStatus.AutoPayPast;
                }
                else
                {
                    return BillStatus.AutoPayUpcoming;
                }
            }

            if (isPaid)
            {
                return BillStatus.Paid;
            }
            else
            {
                if ((compDate - refDate).TotalDays < 0)
                {
                    return BillStatus.PastDue;
                }
                else if ((compDate - refDate).TotalDays == 0)
                {
                    return BillStatus.DueToday;
                }
                else if ((compDate - refDate).TotalDays == 1)
                {
                    return BillStatus.DueTomorrow;
                }
                else if ((compDate - refDate).TotalDays < 14)
                {
                    return BillStatus.DueWithinTwoWeeks;
                }
                else if ((compDate - refDate.AddMonths(1)).TotalDays < 0)
                {
                    return BillStatus.DueWithinOneMonth;
                }
                else
                {
                    return BillStatus.DueInOverOneMonth;
                }
            }
        }
    }
}
