using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using InAppPurchasing.Core;
using InAppPurchasing.iOS;
using MvvmCross;
using MvvmCross.Platforms.Ios.Core;
using UIKit;

namespace BudgetAppCross.iOS
{
    public class Setup : MvxIosSetup<Core.App>
    {
        protected override void InitializeFirstChance()
        {

            Mvx.IoCProvider.RegisterSingleton<IStoreManager>(() => new StoreManager());
            base.InitializeFirstChance();
        }
    }
}