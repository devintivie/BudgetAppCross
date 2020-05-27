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
        public BankAccountViewModel()
        {
            AddBalanceCommand = new Command(async () => await OnAddBalance());
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
            UpdateBalances();
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
            await navigationService.Navigate<NewBalanceViewModel, Balance, bool>(new Balance(BankAccount.AccountID));
            UpdateBalances();
        }
        #endregion


    }
}
