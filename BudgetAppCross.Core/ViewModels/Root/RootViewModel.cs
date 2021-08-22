using BaseClasses;
using BudgetAppCross.Configurations;
using BudgetAppCross.Core.ViewModels.Pages;
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
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public RootViewModel(IMvxNavigationService navService, ISettingsManager settings, IConfigManager<SQLiteConfiguration> configManager)
        {
            _settings = settings;
            _navigationService = navService;
            _configManager = configManager;
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
                //Remove later after debug over
                //await _navigationService.Navigate<MenuViewModel>();
                await NavigateToSettingsScreen();
            }
        }

        private async Task NavigateInitial()
        {
            await _navigationService.Navigate<MenuViewModel>();
            await _navigationService.Navigate<AgendaViewModel>();
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
            //await _navigationService.Navigate<SettingsViewModel>();
        }
        #endregion

    }
}
