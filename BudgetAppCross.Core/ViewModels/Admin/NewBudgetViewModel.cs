using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Linq;
using MvvmCross;

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

        private double initialBalance;
        public double InitialBalance
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
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Constructors
        public NewBudgetViewModel(IMvxNavigationService nav)
        {
            navigationService = nav;
            SaveCommand = new Command(async () => await OnSave());
            CancelCommand = new Command(async () => await OnCancel());

            
        }
        #endregion

        #region Methods
        private async Task OnSave()
        {
            StateManager.DatabaseFilename = BudgetFilename;
            await BudgetDatabase.Initialize();

            var defaultAccount = new BankAccount(0, "Undecided");
            await BudgetDatabase.SaveBankAccount(defaultAccount);
            if (IsAddingBankAccount)
            {
                var bal = new Balance(InitialBalance, InitialBalanceDate);
                var ba = new BankAccount()
                {
                    Nickname = FirstBankAccountName,
                    History = new List<Balance> { bal }
                };

                await BudgetDatabase.SaveBankAccount(ba);
            }
            
            await navigationService.Navigate<DateRangeViewModel>();
        }

        private async Task OnCancel()
        {
            await navigationService.Navigate<SelectBudgetViewModel>();
        }


        #endregion

    }
}
