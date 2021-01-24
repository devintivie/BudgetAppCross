using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using System.Linq;
using MvvmCross;
using MvvmCross.Commands;
using System.IO;

namespace BudgetAppCross.Core.ViewModels
{
    public class NewBudgetViewModel : BaseViewModel// MvxViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        //private IDataManager DataManager = Mvx.IoCProvider.Resolve<IDataManager>();
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
                if(initialBalanceDate != value)
                {
                    initialBalanceDate = value;
                }
            }
        }

        #endregion

        #region Commands
        public IMvxCommand SaveCommand { get; }
        public IMvxCommand CancelCommand { get; }
        #endregion

        #region Constructors
        public NewBudgetViewModel(IMvxNavigationService nav)
        {
            navigationService = nav;
            SaveCommand = new MvxAsyncCommand(async () => await OnSave());
            CancelCommand = new MvxAsyncCommand(async () => await OnCancel());
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

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "Write.txt");
            File.WriteAllText(filename, "Write this text into a file");

            StateManager.DatabaseFilename = BudgetFilename;
            await BudgetDatabase.Initialize();
            var defaultAccount = new BankAccount("Undecided"); //sarting balance = 0
            await BudgetDatabase.SaveBankAccount(defaultAccount);
            //if (IsAddingBankAccount)
            //{
            //    if (string.IsNullOrWhiteSpace(FirstBankAccountName))
            //    {
            //        var config = new AlertConfig().SetMessage("Invalid Account Name");//.SetOkText(ConfirmConfig.DefaultOkText);
            //        Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(config);
            //        return;
            //    }
            //    var ba = new BankAccount()
            //    {
            //        Nickname = FirstBankAccountName,
            //    };

            //    var acctId = await BudgetDatabase.SaveBankAccount(ba);
            //    var bal = new Balance(InitialBalance, InitialBalanceDate, acctId);
            //    await BudgetDatabase.SaveBalance(bal);
            //    StateManager.SaveState();
            //}
            //if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            //{
            //    masterDetailPage.IsGestureEnabled = true;
            //}

            await navigationService.Navigate<SelectBudgetViewModel>();
            //await navigationService.Navigate<DateRangeViewModel>();
        }

        private async Task OnCancel()
        {
            await navigationService.Navigate<SelectBudgetViewModel>();
        }


        #endregion

    }
}
