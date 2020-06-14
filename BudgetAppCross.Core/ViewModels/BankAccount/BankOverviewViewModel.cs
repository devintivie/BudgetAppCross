using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private ObservableCollection<BankAccountQuickViewModel> accounts = new ObservableCollection<BankAccountQuickViewModel>();
        public ObservableCollection<BankAccountQuickViewModel> Accounts
        {
            get { return accounts; }
            set
            {
                SetProperty(ref accounts, value);
            }
        }

        private BankAccountQuickViewModel selectedAccount;
        public BankAccountQuickViewModel SelectedAccount
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
        //public ICommand DeleteAccountCommand { get; }
        #endregion

        #region Constructors
        public BankOverviewViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            Title = "Bank Accounts";

            AddAccountCommand = new Command(async () => await navigationService.Navigate<NewBankAccountViewModel>());
            //DeleteAccountCommand = new Command(() => OnDeleteAccount());
            Messenger.Register<ChangeBalanceMessage>(this, async x => await OnChangeBalanceMessage());
        }

        #endregion

        #region Methods

        public async override void ViewAppeared()
        {
            base.ViewAppeared();
            SelectedAccount = null;

            LoadAccounts();
        }

        private async Task LoadAccounts()
        {
            var allAccts = await BudgetDatabase.GetBankAccounts();
            var accts = allAccts.Where(x => x.AccountID != 1).OrderBy(x => x.Nickname);

            Accounts.Clear();
            foreach (var item in accts)
            {
                Accounts.Add(new BankAccountQuickViewModel(item));
            }
        } 

        public Task ShowBankAccount(BankAccount account)
        {
            return navigationService.Navigate<BankAccountViewModel, BankAccount>(account);

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

        private async Task OnChangeBalanceMessage()
        {
            await LoadAccounts();
        }
        #endregion

    }
}
