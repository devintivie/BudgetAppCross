using BaseClasses;
using BaseViewModels;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Core.ViewModels.Pages
{
    public class WelcomeViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public WelcomeViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler) : base(navService, backgroundHandler)
        {
            AddBudgetCommand = new MvxAsyncCommand(NavigateToNewBudgetPage);
        }
        #endregion

        #region Commands
        public IMvxCommand AddBudgetCommand { get; }
        #endregion

        #region Methods
        private async Task NavigateToNewBudgetPage()
        {
            await _navService.Navigate<NewBudgetViewModel>();
        }
        #endregion


    }
}
