//using Acr.UserDialogs;
using BudgetAppCross.Models;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using BaseViewModels;
using BaseClasses;

namespace BudgetAppCross.Core.ViewModels
{
    public class AccountViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        #endregion

        #region Properties

        #endregion

        #region Commands
        public IMvxCommand SelectBudgetCommand { get; }
        #endregion

        #region Constructors
        public AccountViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler) : base(navService, backgroundHandler)
        {
            SelectBudgetCommand = new MvxAsyncCommand(async () => await _navService.Navigate<BudgetSelectViewModel>());
        }

        #endregion

        #region Methods
        
        #endregion


    }
}
