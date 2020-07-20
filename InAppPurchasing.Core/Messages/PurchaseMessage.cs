using System;
using System.Collections.Generic;
using System.Text;

namespace InAppPurchasing.Core
{
    public class PurchaseMessage
    {
    }

    public class GetPriceMessage
    {
        public string ProductID { get; set; }

        public GetPriceMessage(string product)
        {
            ProductID = product;
        }
    }

    public class SetPriceMessage
    {
        public decimal ProductPrice { get; set; }

        public SetPriceMessage(decimal price)
        {
            ProductPrice = price;
        }
    }
}
