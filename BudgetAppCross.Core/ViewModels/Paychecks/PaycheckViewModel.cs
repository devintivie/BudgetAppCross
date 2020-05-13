using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace BudgetAppCross.Core.ViewModels
{
    public class PaycheckViewModel : BaseViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties

        #endregion

        #region Commands

        #endregion

        #region Constructors
        public PaycheckViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            Title = "Paycheck View";
        }
        #endregion

        #region Methods

        #endregion

    }
}
