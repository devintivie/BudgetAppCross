using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core.ViewModels
{
    public class WelcomeViewModel : SelectBudgetViewModel
    {
        public WelcomeViewModel(IMvxNavigationService navigation) : base(navigation)
        {
        }
    }
}
