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
        private ObservableCollection<string> budgets = new ObservableCollection<string>();
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
                if (SelectedBudget != null)
                {
                    var _ = BudgetSelected();
                }
            }
        }


        #endregion

        #region Commands
        public ICommand AddBudgetCommand { get; }
        #endregion

        #region Constructors
        public SelectBudgetViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            AddBudgetCommand = new Command(async () => await navigationService.Navigate<NewBudgetViewModel>());

            var _ = GetRecentBudgets();
        }

        #endregion

        #region Methods
        private async Task GetRecentBudgets()
        {
            var files = await StateManager.FindBudgetFiles();

            if(files.Count > 0)
            {
                Budgets = new ObservableCollection<string>(files);
            }
        }

        private async Task BudgetSelected()
        {

            if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsGestureEnabled = true;
            }
            StateManager.DatabaseFilename = SelectedBudget;
            //await StateManager.SaveState();
            await StateManager.SaveState();
            await BudgetDatabase.Initialize();
            await BudgetDatabase.GetBankAccounts();
            await navigationService.Navigate<DateRangeViewModel>();

        }
        #endregion


    }
}
