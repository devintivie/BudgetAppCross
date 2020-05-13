using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Confirmation { get; set; }

        public Transaction()
        {
            Date = DateTime.Now;
            Confirmation = "-";
            Amount = 0;
        }
    }

    public class Debit : Transaction
    {

    }

    public class Credit : Transaction
    {

    }
}
