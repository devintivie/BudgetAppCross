//using Acr.UserDialogs;
using BaseClasses;
using BaseViewModels;
using BudgetAppCross.Core.Services;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//;

namespace BudgetAppCross.Core.ViewModels
{
    public class DateRangeViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        private IDataManager _dataManager;
        #endregion

        #region Properties

        private DateTime startDate = DateTime.Today;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    var delta = (int)(EndDate - StartDate).TotalDays;
                    
                    startDate = value;
                    EndDate = value.AddDays(delta);
                    RaisePropertyChanged();
                }
            }
        }


        private DateTime endDate = DateTime.Today.AddDays(13);
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    if (value >= startDate)
                    {
                        endDate = value;
                        RaisePropertyChanged();
                    }
                    else
                    {
                        _backgroundHandler.Notify("Ending date must be after start date");
                    }
                }
            }
        }

        public ObservableCollection<BillViewModel> Bills { get; private set; } = new ObservableCollection<BillViewModel>();
        public ObservableCollection<string> AccountOptions { get; private set; } = new ObservableCollection<string>();

        private string selectedAccount;
        public string SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                if (selectedAccount != value)
                {
                    selectedAccount = value;
                    RaisePropertyChanged();
                    SetProperty(ref selectedAccount, value);
                    _ = GetBills(); 
                }

            }
        }

        private decimal startingBalance;
        public decimal StartingBalance
        {
            get { return startingBalance; }
            set
            {

                if (startingBalance != value)
                {
                    startingBalance = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(Remainder));

                }
                
            }
        }

        private decimal billTotal;
        public decimal BillTotal
        {
            get { return billTotal; }
            set
            {
                if (billTotal != value)
                {
                    billTotal = value;
                    RaisePropertyChanged();
                }
            }
        }
        public decimal Remainder => StartingBalance - BillTotal;

        #endregion

        #region Commands
        public IMvxCommand AddBillCommand { get; }
        public IMvxCommand OnDateSelectedCommand { get; }
        #endregion

        #region Constructors
        public DateRangeViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager) : base(navService, backgroundHandler)
        {
            //Title = "Custom Dates";
            _dataManager = dataManager;
            var _accts = LoadAccountOptions();
            
            AddBillCommand = new MvxAsyncCommand(async () => await _navService.Navigate<NewBillsViewModel, string>(string.Empty));
            OnDateSelectedCommand = new MvxAsyncCommand(async () => await GetBills());

            _backgroundHandler.RegisterMessage<ChangeBillMessage>(this, async x => await OnChangeBillMessage(x.AccountId));
            _backgroundHandler.RegisterMessage<UpdateBillMessage>(this, async x => await OnUpdateBillMessage(x.AccountId));


        }
        #endregion

        #region Methods
        private async Task GetBills()
        {
            if(SelectedAccount != null)
            {
                var billData = await _dataManager.GetBillsDateRangeForAccount(StartDate, EndDate, SelectedAccount);

                billData = billData.OrderBy(x => x.Date).ToList();
                Bills.Clear();
                foreach (var bill in billData)
                {
                    Bills.Add(new BillViewModel(_backgroundHandler, _dataManager, bill));
                }

                await UpdateCalculations();
            }
        }

        private async Task LoadAccountOptions()
        {
            AccountOptions = new ObservableCollection<string>(_dataManager.BankAccountNicknames);

            var allAccts = await _dataManager.GetBankAccounts();
            var accts = allAccts.Where(x => x.AccountID != 1).Select(x => x.Nickname).ToList();
            AccountOptions = new ObservableCollection<string>(accts);
            SelectedAccount = AccountOptions.FirstOrDefault();
        }


        private async Task UpdateCalculations()
        {

            var billCall = await _dataManager.GetBillsDateRangeForAccount(StartDate, EndDate, SelectedAccount);

            var billData = billCall;

            //Doesnt work if there are no bills
            var bal = await _dataManager.GetLatestBalance(SelectedAccount, StartDate);
            if (bal == null)
            {
                StartingBalance = 0.0m;
            }
            else
            {
                StartingBalance = bal.Amount;
            }
            BillTotal = billData.Sum(x => x.Amount);
        }

        private async Task OnChangeBillMessage(int id)
        {
            await GetBills();
        }

        private async Task OnUpdateBillMessage(int id)
        {
            await UpdateCalculations();
        }

        #endregion

    }
}
