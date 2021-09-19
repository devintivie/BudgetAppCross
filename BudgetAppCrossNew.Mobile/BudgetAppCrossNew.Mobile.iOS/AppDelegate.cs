using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;


using MvvmCross.Forms.Platforms.Ios.Core;

namespace BudgetAppCrossNew.Mobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<Setup, BudgetAppCross.Core.App, App>
    {
        public AppDelegate()
        {

        }
    }
}
