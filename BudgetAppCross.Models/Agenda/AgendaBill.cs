﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models
{
    public class AgendaBill : Bill
    {
        public string Company { get; set; }

        public AgendaBill() { }
    }

    public class AgendaEntry
    {
        public DateTime Date { get; set; }
        public List<AgendaBill> Bills { get; set; } = new List<AgendaBill>();

        public AgendaEntry(DateTime iDate)
        {
            Date = iDate;
        }
    }

}
