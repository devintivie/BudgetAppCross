using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class BankOverviewViewModel : BaseViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        private ObservableCollection<BankAccountViewModel> accounts = new ObservableCollection<BankAccountViewModel>();
        public ObservableCollection<BankAccountViewModel> Accounts
        {
            get { return accounts; }
            set
            {
                SetProperty(ref accounts, value);
            }
        }

        private BankAccountViewModel selectedAccount;
        public BankAccountViewModel SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                SetProperty(ref selectedAccount, value);
            }
        }


        #endregion

        #region Commands
        public ICommand AddAccountCommand { get; }
        public ICommand DeleteAccountCommand { get; }
        #endregion

        #region Constructors
        public BankOverviewViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            //Title = "Bank Accounts";

            AddAccountCommand = new Command(async () => await navigationService.Navigate<NewBankAccountViewModel>());
            DeleteAccountCommand = new Command(() => OnDeleteAccount());
        }

        #endregion

        #region Methods

        public async override void ViewAppeared()
        {
            base.ViewAppeared();
            SelectedAccount = null;

            LoadAccounts();
        }

        private async void LoadAccounts()
        {
            var accts = await BudgetDatabase.GetBankAccounts();

            Accounts.Clear();
            foreach (var item in accts)
            {
                Accounts.Add(new BankAccountViewModel(item));
            }
        } 

        //public override async void ViewDestroy(bool viewFinishing = true)
        //{
        //    base.ViewDestroy(viewFinishing);
        //    //await StateManager.SaveToFile();
        //}

        private async void OnDeleteAccount()
        {
            await BudgetDatabase.DeleteBankAccount(SelectedAccount.BankAccount);
            //Console.WriteLine(count); 
            //BankAccountManager.DeleteAccount(SelectedAccount);
            Accounts.Remove(selectedAccount);
        }
        #endregion

    }
}
