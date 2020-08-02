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
    public class SelectBudgetViewModel : WelcomeViewModel//BaseViewModel// MvxViewModel
    {
        #region Fields
        //private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        //private ObservableCollection<string> budgets = new ObservableCollection<string>();
        //public ObservableCollection<string> Budgets
        //{
        //    get { return budgets; }
        //    set
        //    {
        //        SetProperty(ref budgets, value);
        //    }
        //}

        private ObservableCollection<BudgetQuickViewModel> budgets = new ObservableCollection<BudgetQuickViewModel>();
        public ObservableCollection<BudgetQuickViewModel> Budgets
        {
            get { return budgets; }
            set
            {
                if (budgets != value)
                {
                    budgets = value;
                    RaisePropertyChanged();
                }
            }
        }


        //private string selectedBudget;
        //public string SelectedBudget
        //{
        //    get { return selectedBudget; }
        //    set
        //    {
        //        SetProperty(ref selectedBudget, value);
        //        if (SelectedBudget != null)
        //        {
        //            var _ = BudgetSelected();
        //        }
        //    }
        //}

        private BudgetQuickViewModel selectedBudget;
        public BudgetQuickViewModel SelectedBudget
        {
            get { return selectedBudget; }
            set
            {
                if (selectedBudget != value)
                {
                    selectedBudget = value;
                    if (SelectedBudget != null)
                    {
                        var _ = BudgetSelected();
                    }
                    RaisePropertyChanged();
                }
            }
        }



        #endregion

        #region Commands
        
        #endregion

        #region Constructors
        public SelectBudgetViewModel(IMvxNavigationService navigation) : base(navigation)
        {
            Messenger.Register<ChangeBudgetsMessage>(this, async x => await OnChangeBudgetsMessage());
            var _ = GetRecentBudgets();
        }

        #endregion

        #region Methods
        private async Task GetRecentBudgets()
        {
            var files = await StateManager.FindBudgetFiles();

            Budgets.Clear();
            foreach (var file in files)
            {
                Budgets.Add(new BudgetQuickViewModel(navigationService, file));
            }
            //Budgets = new ObservableCollection<string>(files);
            
        }

        private async Task OnChangeBudgetsMessage()
        {
            var files = await StateManager.FindBudgetFiles();
            if(files.Count == 0)
            {
                await navigationService.Navigate<WelcomeViewModel>();
            }
            else
            {
                await GetRecentBudgets();
            }
            
        }

        private async Task BudgetSelected()
        {
            //Enables access to swipe right main menu after this page closes
            if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsGestureEnabled = true;
            }
            StateManager.DatabaseFilename = SelectedBudget.BudgetName;
            //await StateManager.SaveState();
            await StateManager.SaveState();
            await BudgetDatabase.Initialize();
            await BudgetDatabase.GetBankAccounts();
            await navigationService.Navigate<BudgetListViewModel>();
            Messenger.Send(new UpdateMenuMessage());

        }
        #endregion


    }
}
