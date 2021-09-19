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

namespace BudgetAppCross.Core.ViewModels.Pages
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
                if (accounts != value)
                {
                    accounts = value;
                    RaisePropertyChanged();
                }
            }
        }

        private BankAccountQuickViewModel selectedAccount;
        public BankAccountQuickViewModel SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                if (selectedAccount != value)
                {
                    selectedAccount = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Commands
        public IMvxCommand AddAccountCommand { get; }
        public IMvxCommand ShowBankAccountCommand { get; }
        #endregion

        #region Constructors
        public BankOverviewViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            AddAccountCommand = new MvxAsyncCommand(OnAddBankAccount);
            ShowBankAccountCommand = new MvxAsyncCommand(OnShowBankAccount);

            //_backgroundHandler.RegisterMessage<ChangeBalanceMessage>(this, async x => await OnChangeBalanceMessage());
            _ = LoadAccounts();
        }

        private async Task OnChangeBalanceMessage()
        {
            await LoadAccounts();
        }
        #endregion

        #region Methods

        public override void ViewAppeared()
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

        private async Task OnAddBankAccount()
        {
            await _navService.Navigate<NewBankAccountViewModel>();
        }

        private async Task OnShowBankAccount()
        {
            var account = SelectedAccount.BankAccount;
            await _navService.Navigate<BankAccountViewModel, BankAccount>(account);
        }
        #endregion

    }
}
