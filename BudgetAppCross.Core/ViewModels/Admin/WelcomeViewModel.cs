﻿using BaseClasses;
using BaseViewModels;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core.ViewModels
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
            AddBudgetCommand = new MvxAsyncCommand(async () => await _navService.Navigate<NewBudgetViewModel>());
        }
        #endregion

        #region Commands
        public IMvxCommand AddBudgetCommand { get; }
        #endregion

        #region Methods

        #endregion


    }
}
