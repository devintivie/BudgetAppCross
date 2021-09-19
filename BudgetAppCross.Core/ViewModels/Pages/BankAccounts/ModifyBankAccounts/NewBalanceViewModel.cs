using BudgetAppCross.Core.Services;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using MvvmCross;
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
    public class NewBalanceViewModel : MvxViewModel<Balance>//, INavigationResult>
    {
        #region Fields
        private IMvxNavigationService _navService;
        private IDataManager _dataManager;
        #endregion

        #region Properties

        public Balance Balance { get; private set; }

        public decimal Amount
        {
            get { return Balance.Amount; }
            set
            {
                if (Balance.Amount != value)
                {
                    Balance.Amount = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DateTime Timestamp
        {
            get { return Balance.Timestamp; }
            set
            {
                if (Balance.Timestamp != value)
                {
                    Balance.Timestamp = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Commands
        public IMvxCommand SaveCommand { get; }
        public IMvxCommand CancelCommand { get; }
        #endregion

        #region Constructors
        public NewBalanceViewModel(IMvxNavigationService navService, IDataManager dataManager)
        {
            _navService = navService;
            _dataManager = dataManager;
            SaveCommand = new MvxAsyncCommand(OnSave);
            CancelCommand = new MvxAsyncCommand(OnCancel);
        }

        public override void Prepare(Balance parameter)
        {
            Balance = parameter;
        }
        #endregion

        #region Methods
        private async Task OnSave()
        {
            await _dataManager.SaveBalance(Balance);
            //var success = new NavigationResult(true);
            await _navService.Close(this);//, success);
        }

        private async Task OnCancel()
        {
            await _navService.Close(this);
        }
        #endregion

    }
}
