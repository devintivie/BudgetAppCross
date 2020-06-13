using Acr.UserDialogs;
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
using Xamarin.Forms;
using System.Linq;

namespace BudgetAppCross.Core.ViewModels
{
    public class AccountViewModel : BaseViewModel// MvxViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties

        #endregion

        #region Commands
        public ICommand SelectBudgetCommand { get; }
        #endregion

        #region Constructors
        public AccountViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;

            SelectBudgetCommand = new Command(async () => await navigationService.Navigate<SelectBudgetViewModel>());
        }

        #endregion

        #region Methods
        
        #endregion


    }
}
