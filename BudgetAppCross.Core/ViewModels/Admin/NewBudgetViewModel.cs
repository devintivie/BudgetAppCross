using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using System.Linq;
using MvvmCross;
using BaseViewModels;
using MvvmCross.Commands;
using BaseClasses;
using BudgetAppCross.Configurations;
using BudgetAppCross.DataAccess;

namespace BudgetAppCross.Core.ViewModels
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
                SetProperty(ref budgetFilename, value);
            }
        }

        private bool isAddingBankAccount;
        public bool IsAddingBankAccount
        {
            get { return isAddingBankAccount; }
            set
            {
                SetProperty(ref isAddingBankAccount, value);
            }
        }

        private string firstBankAccountName;
        public string FirstBankAccountName
        {
            get { return firstBankAccountName; }
            set
            {
                SetProperty(ref firstBankAccountName, value);
            }
        }

        private decimal initialBalance;
        public decimal InitialBalance
        {
            get { return initialBalance; }
            set
            {
                SetProperty(ref initialBalance, value);
            }
        }

        private DateTime initialBalanceDate = DateTime.Today;

        

        public DateTime InitialBalanceDate
        {
            get { return initialBalanceDate; }
            set
            {
                SetProperty(ref initialBalanceDate, value);
            }
        }




        #endregion

        #region Commands
        public IMvxCommand SaveCommand { get; }
        public IMvxCommand CancelCommand { get; }
        #endregion

        #region Constructors
        public NewBudgetViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, ISettingsManager settings, IDataManager database) : base(navService, backgroundHandler)
        {
            _settings = settings;
            _database = database;
            SaveCommand = new MvxAsyncCommand(OnSave);
            CancelCommand = new MvxAsyncCommand(OnCancel);
        }
        #endregion

        #region Methods
        private async Task OnSave()
        {
            if (string.IsNullOrWhiteSpace(BudgetFilename))
            {
                var config = new AlertConfig().SetMessage("Invalid Budget Name");//.SetOkText(ConfirmConfig.DefaultOkText);
                Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(config);
                return;
            }

            _configManager.Configuration.DatabaseFilename = BudgetFilename;

            await _database.Initialize();
            await _database.CreateDefaultAccount();

            if (IsAddingBankAccount)
            {
                if (string.IsNullOrWhiteSpace(FirstBankAccountName))
                {
                    var config = new AlertConfig().SetMessage("Invalid Account Name");//.SetOkText(ConfirmConfig.DefaultOkText);
                    Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(config);
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
                //await StateManager.SaveState();
            }

            await _navService.Navigate<DateRangeViewModel>();
        }

        private async Task OnCancel()
        {
            await _navService.Navigate<SelectBudgetViewModel>();
        }


        #endregion

    }
}
