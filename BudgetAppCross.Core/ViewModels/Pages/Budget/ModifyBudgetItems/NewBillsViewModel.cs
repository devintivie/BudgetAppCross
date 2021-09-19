//using Acr.UserDialogs;
using BaseClasses;
using BaseViewModels;
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BudgetAppCross.Core.ViewModels.Pages
{
    public class NewBillsViewModel : XamarinBaseViewModel<string>//, bool>//<string, Bill>
    {
        #region Fields
        //private IDataManager DataManager = Mvx.IoCProvider.Resolve<IDataManager>();
        private IDataManager _dataManager;
        #endregion

        #region Properties
        private ObservableCollection<string> payeeOptions = new ObservableCollection<string>();
        public ObservableCollection<string> PayeeOptions
        {
            get { return payeeOptions; }
            set
            {
                if (payeeOptions != value)
                {
                    payeeOptions = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Title => AddMultiple ? "Add Bills" : "Add Bill";

        //public ObservableCollection<string> PayeeOptions { get; private set; } = new ObservableCollection<string>();
        public ObservableCollection<INewBillViewModel> NewBills { get; private set; } = new ObservableCollection<INewBillViewModel>();
        public int BillCount => NewBills.Count;
        public int CursorPosition => Amount.ToString("C", CultureInfo.CurrentCulture).Length;
        public string BillDueString => AddMultiple ? "First Bill Due" : "Due Date";

        private string newPayee;
        public string NewPayee
        {
            get { return newPayee; }
            set
            {
                if (newPayee != value)
                {
                    newPayee = value;
                    RaisePropertyChanged();
                }
            }
        }

        private decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    RaisePropertyChanged();
                }
            }
        }

        private decimal customAmount;
        public decimal CustomAmount
        {
            get { return customAmount; }
            set
            {
                if (customAmount != value)
                {
                    customAmount = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool isNewPayee;
        public bool IsNewPayee
        {
            get { return isNewPayee; }
            set
            {
                if (isNewPayee != value)
                {
                    isNewPayee = value;
                    RaisePropertyChanged();
                }
            }
        }


        private bool addMultiple;
        public bool AddMultiple
        {
            get { return addMultiple; }
            set
            {
                if (addMultiple != value)
                {
                    addMultiple = value;
                    if (!value)
                    {
                        ResetNewBillsToSingle();
                    }
                    else
                    {
                        _ = CreateBillsForDateRange();
                    }
                }

                RaisePropertyChanged();
                RaisePropertyChanged(nameof(BillDueString));
                RaisePropertyChanged(nameof(Title));
            }
        }

        private string selectedPayee;
        public string SelectedPayee
        {
            get { return selectedPayee; }
            set
            {
                if (selectedPayee != value)
                {
                    selectedPayee = value;
                    RaisePropertyChanged();
                }
            }
        }

        private DateTime startDate = DateTime.Today;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    RaisePropertyChanged();
                    _ = CreateBillsForDateRange();
                }
            }
        }
        private DateTime endDate = new DateTime(DateTime.Today.Year, 12, 31);
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (value >= StartDate)
                {
                    if (endDate != value)
                    {
                        endDate = value;
                        RaisePropertyChanged();
                        _ = CreateBillsForDateRange();
                    }
                }
                else
                {
                    _backgroundHandler.Notify("Ending date must be after start date");
                }

            }
        }

        private ObservableCollection<DueDateFrequencies> dueFrequencies;
        public ObservableCollection<DueDateFrequencies> DueFrequencies
        {
            get { return dueFrequencies; }
            set
            {
                if (dueFrequencies != value)
                {
                    dueFrequencies = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<string> accountOptions = new ObservableCollection<string>();
        public ObservableCollection<string> AccountOptions
        {
            get { return accountOptions; }
            set
            {
                if (accountOptions != value)
                {
                    accountOptions = value;
                    RaisePropertyChanged();
                }
            }
        }

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
                }
            }
        }


        private DueDateFrequencies dueDateFrequency;
        public DueDateFrequencies DueDateFrequency
        {
            get { return dueDateFrequency; }
            set
            {
                if (dueDateFrequency != value)
                {
                    dueDateFrequency = value;
                    RaisePropertyChanged();
                    _ = CreateBillsForDateRange();
                }
            }
        }

        private bool isPaid;
        public bool IsPaid
        {
            get { return isPaid; }
            set
            {
                if (isPaid != value)
                {
                    isPaid = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool onAutopay;
        public bool OnAutopay
        {
            get { return onAutopay; }
            set
            {
                if (onAutopay != value)
                {
                    onAutopay = value;
                    RaisePropertyChanged();
                }
            }
        }


        private bool payeeSet;
        public bool PayeeSet
        {
            get { return payeeSet; }
            set
            {
                if (payeeSet != value)
                {
                    payeeSet = value;
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
        public NewBillsViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            LoadAccountOptions();
            
            SaveCommand = new MvxAsyncCommand(OnSave);
            CancelCommand = new MvxAsyncCommand(OnCancel);
            DueDateFrequency = DueDateFrequencies.Monthly;
            ResetNewBillsToSingle();
        }

        public override void Prepare(string parameter)
        {
            _ = PrepareAsync(parameter);
        }

        private async Task PrepareAsync(string parameter)
        {
            await LoadPayeeOptions();
            if (parameter != string.Empty)
            {
                PayeeSet = true;
                SelectedPayee = parameter;
            }
        }

        private void ResetNewBillsToSingle()
        {
            NewBills.Clear();
            NewBills.Add(new NewBillViewModel(_backgroundHandler, _dataManager, StartDate));

        }

        //private void GetBillsForDateRange()
        //{
        //    var temp = NewBills.Where(b => b.Date >= StartDate && b.Date <= EndDate);

        //    if (temp.Count() == 0)
        //    {
        //        ResetNewBillsToSingle();
        //    }
        //    else
        //    {
        //        NewBills = new ObservableCollection<INewBillViewModel>(temp);
        //    }
        //}

        private async Task CreateBillsForDateRange()
        {
            var days = (EndDate - StartDate).TotalDays;
            double dayCount = 0.0;

            var spacings = 0;   //weeks, months, fortnights, 4weeks etc

            NewBills.Clear();
            switch (DueDateFrequency)
            {
                case DueDateFrequencies.OneWeek:
                    while (dayCount <= days)
                    {
                        dayCount = (StartDate.AddDays((spacings + 1) * 7) - StartDate).TotalDays;
                        spacings++;
                    }
                    for (int i = 0; i < spacings; i++)
                    {
                        var number = i + 1;
                        var dueDate = StartDate.AddDays(7 * i);
                        NewBills.Add(new NewMultiBillViewModel(_backgroundHandler, _dataManager, number, dueDate));
                    }
                    break;
                case DueDateFrequencies.TwoWeek:
                    while (dayCount <= days)
                    {
                        dayCount = (StartDate.AddDays((spacings + 1) * 14) - StartDate).TotalDays;
                        spacings++;
                    }
                    for (int i = 0; i < spacings; i++)
                    {
                        var number = i + 1;
                        var dueDate = StartDate.AddDays(14 * i);
                        NewBills.Add(new NewMultiBillViewModel(_backgroundHandler, _dataManager, number, dueDate));
                    }
                    break;
                case DueDateFrequencies.FourWeek:
                    while (dayCount <= days)
                    {
                        dayCount = (StartDate.AddDays((spacings + 1) * 28) - StartDate).TotalDays;
                        spacings++;
                    }
                    for (int i = 0; i < spacings; i++)
                    {
                        var number = i + 1;
                        var dueDate = StartDate.AddDays(28 * i);
                        NewBills.Add(new NewMultiBillViewModel(_backgroundHandler, _dataManager, number, dueDate));
                    }
                    break;

                case DueDateFrequencies.Monthly:
                    while (dayCount <= days)
                    {
                        dayCount = (StartDate.AddMonths(spacings + 1) - StartDate).TotalDays;
                        spacings++;
                    }
                    for (int i = 0; i < spacings; i++)
                    {
                        var number = i + 1;
                        var dueDate = StartDate.AddMonths(i);
                        NewBills.Add(new NewMultiBillViewModel(_backgroundHandler, _dataManager, number, dueDate));
                    }
                    break;
                case DueDateFrequencies.Quarterly:
                    while (dayCount <= days)
                    {
                        dayCount = (StartDate.AddMonths(3 * (spacings + 1)) - StartDate).TotalDays;
                        spacings++;
                    }
                    for (int i = 0; i < spacings; i++)
                    {
                        var number = i + 1;
                        var dueDate = StartDate.AddMonths(3 * i);
                        NewBills.Add(new NewMultiBillViewModel(_backgroundHandler, _dataManager, number, dueDate));
                    }
                    break;
                case DueDateFrequencies.SemiAnnually:
                    while (dayCount <= days)
                    {
                        dayCount = (StartDate.AddMonths(6 * (spacings + 1)) - StartDate).TotalDays;
                        spacings++;
                    }
                    for (int i = 0; i < spacings; i++)
                    {
                        var number = i + 1;
                        var dueDate = StartDate.AddMonths(6 * i);
                        NewBills.Add(new NewMultiBillViewModel(_backgroundHandler, _dataManager, number, dueDate));
                    }
                    break;
                case DueDateFrequencies.Yearly:
                    while (dayCount <= days)
                    {
                        dayCount = (StartDate.AddYears(spacings + 1) - StartDate).TotalDays;
                        spacings++;
                    }
                    for (int i = 0; i < spacings; i++)
                    {
                        var number = i + 1;
                        var dueDate = StartDate.AddYears(i);
                        NewBills.Add(new NewMultiBillViewModel(_backgroundHandler, _dataManager, number, dueDate));
                    }
                    break;
                default:
                    break;


            }
            await RaisePropertyChanged(nameof(BillCount));
        }
        #endregion

        #region Methods
        private async Task OnSave()
        {
            var acctId = await _dataManager.GetBankAccountID(SelectedAccount);
            var payee = IsNewPayee ? NewPayee : SelectedPayee;

            if (string.IsNullOrWhiteSpace(payee))
            {
                _backgroundHandler.Notify("Payee needs a name");
                return;
            }
            if (AddMultiple)
            {
                var tempBills = new List<Bill>();

                foreach (var item in NewBills)
                {
                    var bill = new Bill(payee, Amount, item.Date);
                    bill.AccountID = acctId;
                    bill.IsAuto = onAutopay;
                    //bill.BankAccount = acct;
                    tempBills.Add(bill);
                }

                await _dataManager.InsertBills(tempBills);
            }
            else
            {
                var newBill = new Bill(payee, Amount, StartDate)
                {
                    AccountID = acctId,
                    //BankAccount = acct,
                    IsPaid = IsPaid
                };
                await _dataManager.SaveBill(newBill);
            }

            _backgroundHandler.SendMessage(new ChangeBillMessage());
            await CloseAsync();
        }

        private async Task OnCancel()
        {
            await CloseAsync();
        }

        private async Task CloseAsync()
        {
            _backgroundHandler.UnregisterMessages(this);
            await _navService.Close(this);
        }

        private void LoadAccountOptions()
        {
            AccountOptions = new ObservableCollection<string>(_dataManager.BankAccountNicknames);
            if (AccountOptions.Count > 1)
            {
                SelectedAccount = AccountOptions.ElementAt(1);
            }
            else
            {
                SelectedAccount = AccountOptions.FirstOrDefault();
            }
        }

        private async Task LoadPayeeOptions()
        {
            await _dataManager.UpdatePayeeNames();
            PayeeOptions = new ObservableCollection<string>(_dataManager.PayeeNames);

            if (PayeeOptions.Count == 0)
            {
                IsNewPayee = true;
            }
            else if (SelectedPayee == null)
            {
                SelectedPayee = PayeeOptions.First();
            }
        }



        #endregion

    }
}
