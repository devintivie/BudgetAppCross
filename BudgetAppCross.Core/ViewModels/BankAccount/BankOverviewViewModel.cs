using BudgetAppCross.Models;
using MvvmCross.Navigation;
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
        private ObservableCollection<BankAccount> accounts;
        public ObservableCollection<BankAccount> Accounts
        {
            get { return accounts; }
            set
            {
                SetProperty(ref accounts, value);
            }
        }

        private BankAccount selectedAccount;
        public BankAccount SelectedAccount
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
            Title = "Bank Accounts";

            AddAccountCommand = new Command(async () => await navigationService.Navigate<NewBankAccountViewModel>());
            DeleteAccountCommand = new Command(() => OnDeleteAccount());
        }

        #endregion

        #region Methods

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            SelectedAccount = null;
            Accounts = new ObservableCollection<BankAccount>(BankAccountManager.AllAccounts);
        }

        public override async void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);
            await StateManager.SaveToFile();
        }

        private void OnDeleteAccount()
        {
            BankAccountManager.DeleteAccount(SelectedAccount);
            Accounts.Remove(selectedAccount);
        }
        #endregion

    }
}
