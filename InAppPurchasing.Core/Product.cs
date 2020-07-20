using System;

namespace BudgetAppCross.Core
{
    public abstract class Product
    {
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ProductID { get; set; }
        public string Title { get; set; }
    }
}
