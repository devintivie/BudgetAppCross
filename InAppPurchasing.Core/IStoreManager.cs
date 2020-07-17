using System;
using System.Collections.Generic;
using System.Text;

namespace InAppPurchasing.Core
{
    public interface IStoreManager
    {
        List<string> Products { get; }

        decimal GetPrice(string product);
        bool PurchaseProduct(string product);
    }
}
