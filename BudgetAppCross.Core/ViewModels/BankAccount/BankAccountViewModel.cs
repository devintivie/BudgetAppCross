using BudgetAppCross.Models;
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

        #endregion

        #region Commands
        public ICommand AddBalanceCommand { get; }
        #endregion

        #region Constructors
        public BankAccountViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            AddBalanceCommand = new Command(async () => await OnAddBalance());

            Messenger.Register<ChangeBalanceMessage>(this, async x => await OnChangeBalanceMessage());
        }

        
        #endregion

        #region Methods
        public override void Prepare(BankAccount parameter)
        {
            BankAccount = parameter;
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            var _ = UpdateBalances();
        }

        private async Task UpdateBalances()
        {
            var temp = await BudgetDatabase.GetBalances();
            var tempBalances = temp.Where(x => x.AccountID == BankAccount.AccountID)
                .OrderBy(x => x.Timestamp).ToList();

            var vms = new List<BalanceViewModel>();
            foreach (var item in tempBalances)
            {
                vms.Add(new BalanceViewModel(item));
            }

            Balances = new ObservableCollection<BalanceViewModel>(vms);
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

        private async Task OnChangeBalanceMessage()
        {
            await UpdateBalances();
        }
        #endregion


    }
}
