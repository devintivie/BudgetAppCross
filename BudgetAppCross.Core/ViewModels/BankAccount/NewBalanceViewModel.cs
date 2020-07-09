using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross;
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
    public class NewBalanceViewModel : MvxViewModel<Balance, bool>//<string, Bill>
    {
        #region Fields
        private IMvxNavigationService navigationService;
        private IDataManager DataManager = Mvx.IoCProvider.Resolve<IDataManager>();
        #endregion

        #region Properties

        public Balance Balance { get; private set; }

        public decimal Amount
        {
            get { return Balance.Amount; }
            set
            {
                var amount = Balance.Amount;
                Balance.Amount = value;
                SetProperty(ref amount, value);
            }
        }

        public DateTime Timestamp
        {
            get { return Balance.Timestamp; }
            set
            {
                var timestamp = Balance.Timestamp;
                Balance.Timestamp = value;
                SetProperty(ref timestamp, value);
            }
        }

        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Constructors
        public NewBalanceViewModel(IMvxNavigationService nav)
        {
            navigationService = nav;

            SaveCommand = new Command(async () => await OnSave());
            CancelCommand = new Command(async () => await OnCancel());
        }

        public override void Prepare(Balance parameter)
        {
            Balance = parameter;
        }
        #endregion

        #region Methods
        private async Task OnSave()
        {
            await DataManager.SaveBalance(Balance);
            await navigationService.Close(this, true);
        }

        private async Task OnCancel()
        {
            await navigationService.Close(this);
        }
        #endregion

    }
}
