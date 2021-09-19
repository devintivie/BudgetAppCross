using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//using Acr.UserDialogs;
using System.Linq;
using MvvmCross;
using BaseViewModels;
using MvvmCross.Commands;
using BaseClasses;
using BudgetAppCross.Configurations;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Core.ViewModels.Pages;
using BudgetAppCross.Core.ViewModels.Root;

namespace BudgetAppCross.Core.ViewModels.Pages
{
    public class NewBudgetViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        private IConfigManager<SQLiteConfiguration> _configManager;
        private IDataManager _database;
        private ISettingsManager _settings;
        #endregion

        #region Properties
        private string budgetFilename;
        public string BudgetFilename
        {
            get { return budgetFilename; }
            set
            {
                if (budgetFilename != value)
                {
                    budgetFilename = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool isAddingBankAccount;
        public bool IsAddingBankAccount
        {
            get { return isAddingBankAccount; }
            set
            {
                if (isAddingBankAccount != value)
                {
                    isAddingBankAccount = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string firstBankAccountName;
        public string FirstBankAccountName
        {
            get { return firstBankAccountName; }
            set
            {
                if (firstBankAccountName != value)
                {
                    firstBankAccountName = value;
                    RaisePropertyChanged();
                }
            }
        }

        private decimal initialBalance;
        public decimal InitialBalance
        {
            get { return initialBalance; }
            set
            {
                if (initialBalance != value)
                {
                    initialBalance = value;
                    RaisePropertyChanged();
                }
            }
        }

        private DateTime initialBalanceDate = DateTime.Today;
        public DateTime InitialBalanceDate
        {
            get { return initialBalanceDate; }
            set
            {
                if (initialBalanceDate != value)
                {
                    initialBalanceDate = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region Commands
        public IMvxCommand SaveCommand { get; }
        public IMvxCommand CancelCommand { get; }
        #endregion

        #region Constructors
        public NewBudgetViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, ISettingsManager settings, IConfigManager<SQLiteConfiguration> configManager, IDataManager database) : base(navService, backgroundHandler)
        {
            _settings = settings;
            _database = database;
            _configManager = configManager;

            SaveCommand = new MvxAsyncCommand(OnSave);
            CancelCommand = new MvxAsyncCommand(OnCancel);
        }
        #endregion

        #region Methods
        private async Task OnSave()
        {
            if (string.IsNullOrWhiteSpace(BudgetFilename))
            {
                _backgroundHandler.Notify("Invalid Budget Name");
                return;
            }

            _configManager.Configuration.DatabaseFilename = BudgetFilename;

            await _database.Initialize();
            await _database.CreateDefaultAccount();

            if (IsAddingBankAccount)
            {
                if (string.IsNullOrWhiteSpace(FirstBankAccountName))
                {
                    _backgroundHandler.Notify("Invalid Account Name");//.SetOkText(ConfirmConfig.DefaultOkText);
                    return;
                }
                var bal = new Balance(InitialBalance, InitialBalanceDate);
                var ba = new BankAccount()
                {
                    Nickname = FirstBankAccountName,
                    History = new List<Balance> { bal }
                };

                await _database.SaveBankAccount(ba);
                _settings.ConfigFile = BudgetFilename;
                await _settings.SaveSettings();
            }

            await _navService.Navigate<MenuViewModel>();
            await _navService.Navigate<AgendaViewModel>();
        }

        private async Task OnCancel()
        {
            var files = await _settings.FindLoadableConfigFiles();
            if (files.Count == 0)
            {
                await _navService.Navigate<WelcomeViewModel>();
            }
            else
            {
                //Remove later after debug over
                //await NavigateToSettingsScreen();
                await _navService.Navigate<BudgetSelectViewModel>();
                //await NavigateInitial();
            }
        }


        #endregion

    }
}
