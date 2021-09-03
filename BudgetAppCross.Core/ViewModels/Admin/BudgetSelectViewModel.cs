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
using BudgetAppCross.Configurations;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Core.ViewModels.Pages;

namespace BudgetAppCross.Core.ViewModels
{
    public class BudgetSelectViewModel : MvxNavigationBaseViewModel//BaseViewModel// MvxViewModel
    {
        #region Fields
        private ISettingsManager _settings;
        IConfigManager<SQLiteConfiguration> _configManager;
        IDataManager _dataManager;
        #endregion

        #region Properties
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
                        _ = BudgetSelected();
                    }
                    RaisePropertyChanged();
                }
            }
        }



        #endregion

        #region Commands
        
        #endregion

        #region Constructors
        public BudgetSelectViewModel(IMvxNavigationService navigation, IBackgroundHandler backgroundHandler, ISettingsManager settings,
            IConfigManager<SQLiteConfiguration> configManager, IDataManager dataManager) : base(navigation, backgroundHandler)
        {
            _settings = settings;
            _configManager = configManager;
            _dataManager = dataManager;
            backgroundHandler.RegisterMessage<ChangeBudgetsMessage>(this, async x => await OnChangeBudgetsMessage());
            _ = GetRecentBudgets();
        }
        #endregion

        #region Methods
        private async Task GetRecentBudgets()
        {
            var files = await _settings.FindLoadableConfigFiles();
            //var files = await StateManager.FindBudgetFiles();

            Budgets.Clear();
            foreach (var file in files)
            {
                Budgets.Add(new BudgetQuickViewModel(_navService, _backgroundHandler, _settings, file));
            }
            
        }

        private async Task OnChangeBudgetsMessage()
        {
            //var files = await StateManager.FindBudgetFiles();
            var files = await _settings.FindLoadableConfigFiles();
            if(files.Count == 0)
            {
                await _navService.Navigate<WelcomeViewModel>();
            }
            else
            {
                await GetRecentBudgets();
            }
            
        }

        private async Task BudgetSelected()
        {
            _configManager.Configuration.DatabaseFilename = SelectedBudget.BudgetName;
            _settings.ConfigFile = SelectedBudget.BudgetName;
            await _settings.SaveSettings();

            await _dataManager.Initialize();
            //await _dataManager.Initialize();
            await _dataManager.GetBankAccounts();
            await _navService.Navigate<AgendaViewModel>();
            _backgroundHandler.SendMessage(new UpdateMenuMessage());

        }
        #endregion


    }
}
