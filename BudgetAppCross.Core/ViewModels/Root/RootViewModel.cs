using BaseClasses;
using BudgetAppCross.Configurations;
using BudgetAppCross.Core.ViewModels.Pages;
using BudgetAppCross.DataAccess;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Core.ViewModels.Root
{
    public class RootViewModel : MvxViewModel
    {
        #region Fields
        IMvxNavigationService _navigationService;
        ISettingsManager _settings;
        IConfigManager<SQLiteConfiguration> _configManager;
        IDataManager _dataManager;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public RootViewModel(IMvxNavigationService navService, ISettingsManager settings, IConfigManager<SQLiteConfiguration> configManager,
            IDataManager dataManager)
        {
            _settings = settings;
            _navigationService = navService;
            _configManager = configManager;
            _dataManager = dataManager;
        }
        public override void ViewAppearing()
        {
            base.ViewAppearing();

            _ = Startup();
        }
        #endregion

        #region Methods
        private async Task Startup()
        {
            var prevConfigFile = await _settings.GetPreviousConfiguration();

            var loaded = await LoadConfiguration(prevConfigFile);
            if (loaded)
            {
                await NavigateInitial();
            }
            else
            {
                var files = await _settings.FindLoadableConfigFiles();
                if (files.Count == 0)
                {
                    await NavigateToWelcomeScreen();
                }
                else
                {
                    //Remove later after debug over
                    //await NavigateToSettingsScreen();
                    await NavigateToSelectBudget();
                    //await NavigateInitial();
                }
            }
        }

        private async Task NavigateInitial()
        {
            await _dataManager.Initialize();
            await _dataManager.GetBankAccounts();
            await _navigationService.Navigate<MenuViewModel>();
            await _navigationService.Navigate<DateRangeViewModel>();
        }

        public async Task<bool> LoadConfiguration(string filename)
        {
            if (filename == null)
            {
                _configManager.CreateConfiguration();
                return false;
            }

            await _configManager.LoadConfiguration(filename);
            if (_configManager.Configuration == null)
            {
                _settings.ConfigFile = null;

                await _settings.SaveSettings();
                return false;
            }

            await _settings.SaveSettings();
            return true;
        }

        public async Task NavigateToSettingsScreen()
        {
            await _navigationService.Navigate<SettingsViewModel>();
        }

        public async Task NavigateToWelcomeScreen()
        {
            await _navigationService.Navigate<WelcomeViewModel>();
        }
        
        public async Task NavigateToSelectBudget()
        {
            await _navigationService.Navigate<BudgetSelectViewModel>();
        }
        #endregion

    }
}
