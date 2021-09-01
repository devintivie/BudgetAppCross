using BaseClasses;
using BaseViewModels;
using BudgetAppCross.Core.Services;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BudgetAppCross.Core.ViewModels
{
    public class BankOverviewViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        private IDataManager _dataManager;
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
        public IMvxCommand AddAccountCommand { get; }
        //public ICommand DeleteAccountCommand { get; }
        #endregion

        #region Constructors
        public BankOverviewViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            AddAccountCommand = new MvxAsyncCommand(async () => await _navService.Navigate<NewBankAccountViewModel>());

            var _ = LoadAccounts();
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);
            _backgroundHandler.UnregisterMessages(this);
        }

        #endregion

        #region Methods

        public async override void ViewAppeared()
        {
            base.ViewAppeared();
            SelectedAccount = null;
            _ = LoadAccounts();
        }

        private async Task LoadAccounts()
        {
            var allAccts = await _dataManager.GetBankAccounts();
            var accts = allAccts.Where(x => x.AccountID != 1).OrderBy(x => x.Nickname);

            Accounts.Clear();
            foreach (var item in accts)
            {
                Accounts.Add(new BankAccountQuickViewModel(_navService, _backgroundHandler, _dataManager, item));
            }
        } 

        public Task ShowBankAccount(BankAccount account)
        {
            return _navService.Navigate<BankAccountViewModel, BankAccount>(account);

        }

        //public override async void ViewDestroy(bool viewFinishing = true)
        //{
        //    base.ViewDestroy(viewFinishing);
        //    //await StateManager.SaveToFile();
        //}

        private async void OnDeleteAccount()
        {
            await _dataManager.DeleteBankAccount(SelectedAccount.BankAccount);
            //Console.WriteLine(count); 
            //BankAccountManager.DeleteAccount(SelectedAccount);
            Accounts.Remove(selectedAccount);
        }
        #endregion

    }
}
