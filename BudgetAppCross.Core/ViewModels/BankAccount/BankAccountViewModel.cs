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
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class BankAccountViewModel : BaseViewModel<BankAccount>
    {
        #region Fields
        private IMvxNavigationService navigationService;

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
        public ICommand AddBalanceCommand { get; }
        public IMvxCommand EditCommand { get; }
        public IMvxCommand SaveEditCommand { get; }
        #endregion

        #region Constructors
        public BankAccountViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            AddBalanceCommand = new Command(async () => await OnAddBalance());
            EditCommand = new MvxAsyncCommand(async () => await OnEdit());
            SaveEditCommand = new MvxAsyncCommand(async () => await OnSaveEdit());
            //Messenger.Register<ChangeBalanceMessage>(this, async x => await OnUpdateBalanceMessage());
            Messenger.Register<ChangeBalanceMessage>(this, async x => await OnChangeBalanceMessage());
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);

            Messenger.Unregister(this);
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
            var temp = await BudgetDatabase.GetBalances();
            var tempBalances = temp.Where(x => x.AccountId == BankAccount.AccountId)
                .OrderBy(x => x.Timestamp).ToList();

            var vms = new List<BalanceViewModel>();
            foreach (var item in tempBalances)
            {
                vms.Add(new BalanceViewModel(item));
            }

            Balances = new ObservableCollection<BalanceViewModel>(vms);
        }

        private async Task UpdateAccountName()
        {
            Nickname = NewAccountNickname;
            BankAccount.Nickname = NewAccountNickname.Trim();
            await BudgetDatabase.SaveBankAccount(BankAccount);

        }

        private async Task OnAddBalance()
        {
            var newBalance = new Balance()
            {
                BankAccount = BankAccount
            };
            await navigationService.Navigate<NewBalanceViewModel, Balance, bool>(newBalance);
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
