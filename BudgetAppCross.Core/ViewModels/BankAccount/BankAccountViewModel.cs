using BaseClasses;
using BaseViewModels;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BudgetAppCross.Core.ViewModels
{
    public class BankAccountViewModel : XamarinBaseViewModel<BankAccount>
    {
        #region Fields
        private IDataManager _dataManager;

        #endregion

        #region Properties
        public BankAccount BankAccount { get; private set; }

        private ObservableCollection<BalanceViewModel> balances;
        public ObservableCollection<BalanceViewModel> Balances
        {
            get { return balances; }
            set
            {
                SetProperty(ref balances, value);
            }
        }

        private string editButtonText = "Edit";
        public string EditButtonText
        {
            get { return editButtonText; }
            set
            {
                if (editButtonText != value)
                {
                    editButtonText = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool isEditing = false;
        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                if (isEditing != value)
                {
                    isEditing = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string newAccountNickname;
        public string NewAccountNickname
        {
            get { return newAccountNickname; }
            set
            {
                if (newAccountNickname != value)
                {
                    newAccountNickname = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string nickname;
        public string Nickname
        {
            get { return BankAccount.Nickname; }
            set
            {
                if (nickname != value)
                {
                    nickname = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Commands
        public IMvxCommand AddBalanceCommand { get; }
        public IMvxCommand EditCommand { get; }
        public IMvxCommand SaveEditCommand { get; }
        #endregion

        #region Constructors
        public BankAccountViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            AddBalanceCommand = new MvxAsyncCommand(OnAddBalance);
            EditCommand = new MvxAsyncCommand(OnEdit);
            SaveEditCommand = new MvxAsyncCommand(OnSaveEdit);
            //Messenger.Register<ChangeBalanceMessage>(this, async x => await OnUpdateBalanceMessage());
            //Messenger.Register<ChangeBalanceMessage>(this, async x => await OnChangeBalanceMessage());
        }
        #endregion

        #region Methods
        public override void Prepare(BankAccount parameter)
        {
            BankAccount = parameter;
            NewAccountNickname = BankAccount.Nickname;
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            var _ = UpdateBalances();
        }

        private async Task UpdateBalances()
        {
            var balances = await _dataManager.GetBalancesForAccount(BankAccount.AccountID);
            var vms = new List<BalanceViewModel>();
            foreach (var item in balances)
            {
                vms.Add(new BalanceViewModel(_backgroundHandler, _dataManager, item));
            }

            Balances = new ObservableCollection<BalanceViewModel>(vms);
        }

        private async Task UpdateAccountName()
        {
            Nickname = NewAccountNickname;
            BankAccount.Nickname = NewAccountNickname.Trim();
            await _dataManager.SaveBankAccount(BankAccount);

        }

        private async Task OnAddBalance()
        {
            var newBalance = new Balance()
            {
                AccountID = BankAccount.AccountID,
                BankAccount = BankAccount
            };
            //INavigationResult success;
            var result = await _navService.Navigate<NewBalanceViewModel, Balance, INavigationResult>(newBalance);
            var _ = UpdateBalances();
        }

        private async Task OnEdit()
        {
            if (IsEditing)
            {
                EditButtonText = "Edit";
                IsEditing = false;
                NewAccountNickname = BankAccount.Nickname;
                
            }
            else
            {
                EditButtonText = "Cancel";
                IsEditing = true;
                
            }
        }

        private async Task OnSaveEdit()
        {
            IsEditing = false;
            EditButtonText = "Edit";

            await UpdateAccountName();
        }

        private async Task OnChangeBalanceMessage()
        {
            await UpdateBalances();
        }
        //private async Task OnUpdateBalanceMessage()
        //{
        //}
        
        #endregion


    }
}
