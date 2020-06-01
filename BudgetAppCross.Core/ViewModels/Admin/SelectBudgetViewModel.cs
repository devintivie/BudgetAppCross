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
    public class SelectBudgetViewModel : BaseViewModel// MvxViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        private ObservableCollection<string> budgets;
        public ObservableCollection<string> Budgets
        {
            get { return budgets; }
            set
            {
                SetProperty(ref budgets, value);
            }
        }

        private string selectedBudget;
        public string SelectedBudget
        {
            get { return selectedBudget; }
            set
            {
                SetProperty(ref selectedBudget, value);
                //if(SelectedBudget != null)
                //{
                //    var _ = BudgetSelected();
                //}
                
            }
        }


        #endregion

        #region Commands

        #endregion

        #region Constructors
        public SelectBudgetViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;

            var _ = GetRecentBudgets();
        }

        #endregion

        #region Methods
        private async Task GetRecentBudgets()
        {
            var files = await StateManager.FindBudgetFiles();

            Budgets = new ObservableCollection<string>(files);
        }

        private async Task BudgetSelected()
        {
            StateManager.DatabaseFilename = SelectedBudget;
            await BudgetDatabase.GetBankAccounts();
            await navigationService.Navigate<DateRangeViewModel>();

        }
        #endregion


    }
}
