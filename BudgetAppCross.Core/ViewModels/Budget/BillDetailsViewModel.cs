using BudgetAppCross.Models;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core.ViewModels
{
    public class BillDetailsViewModel : BaseViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public BillDetailsViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
        }
        #endregion

        #region Methods

        #endregion



    }
}
