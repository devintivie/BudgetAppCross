using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        #region Fields
        public IMvxNavigationService navigationService;
        #endregion

        #region Properties

        #endregion

        #region Commands
        public IMvxCommand AddBudgetCommand { get; }
        #endregion

        #region Constructors
        public WelcomeViewModel(IMvxNavigationService navigation)
        {
            //Disables access to swipe right main menu while this page is open
            if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsGestureEnabled = false;
            }
            navigationService = navigation;
            AddBudgetCommand = new MvxAsyncCommand(async () => await OnAddBudget());
        }
        #endregion

        

        #region Methods
        private async Task OnAddBudget()
        {
            await navigationService.Navigate<NewBudgetViewModel>();
        } 
        #endregion


    }
}
