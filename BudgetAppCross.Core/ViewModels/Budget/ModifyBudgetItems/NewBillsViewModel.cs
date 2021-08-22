using Acr.UserDialogs;
using BaseClasses;
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

namespace BudgetAppCross.Core.ViewModels
{
    public class NewBillsViewModel : MvxViewModel<string>//, bool>//<string, Bill>
    {
        #region Fields
        //private IDataManager DataManager = Mvx.IoCProvider.Resolve<IDataManager>();
        #endregion

        #region Properties
        private string newPayee;
        public string NewPayee
        {
            get { return newPayee; }
            set
            {
                SetProperty(ref newPayee, value);
            }
        }

        private ObservableCollection<string> payeeOptions;
        public ObservableCollection<string> PayeeOptions
        {
            get { return payeeOptions; }
            set
            {
                SetProperty(ref payeeOptions, value);
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
                //amount = value;
                //RaisePropertyChanged();
                //if (amount != value || amount == 0.0m)
                //{
                //    var temp = Math.Truncate(100 * value) / 100;
                //    amount = temp;
                //    //amount = value;
                //    RaisePropertyChanged();
                //}
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


        public int BillCount => NewBills.Count;
        public int CursorPosition => Amount.ToString("C", CultureInfo.CurrentCulture).Length;

        public string BillDueString
        {
            get
            {
                if (AddMultiple)
                {
                    return "First Bill Due";
                }
                return "Due Date";
            }
        }

        private ObservableCollection<INewBillViewModel> newBills = new ObservableCollection<INewBillViewModel>();
        public ObservableCollection<INewBillViewModel> NewBills
        {
            get { return newBills; }
            set
            {
                if (newBills != value)
                {
                    newBills = value;
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
                SetProperty(ref isNewPayee, value);
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
                        CreateBillsForDateRange();
                    }
                }

                RaisePropertyChanged();
                RaisePropertyChanged(nameof(BillDueString));
            }
        }


        private string selectedPayee;
        public string SelectedPayee
        {
            get { return selectedPayee; }
            set
            {
                SetProperty(ref selectedPayee, value);
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
                    CreateBillsForDateRange();
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
                        CreateBillsForDateRange();
                    }
                }
                else
                {
                    var config = new AlertConfig().SetMessage("Ending date must be after start date");
                    Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(config);
                }

            }
        }

        private ObservableCollection<DueDateFrequencies> dueFrequencies;
        public ObservableCollection<DueDateFrequencies> DueFrequencies
        {
            get { return dueFrequencies; }
            set
            {
                SetProperty(ref dueFrequencies, value);
            }
        }

        private ObservableCollection<string> accountOptions = new ObservableCollection<string>();
        public ObservableCollection<string> AccountOptions
        {
            get { return accountOptions; }
            set
            {
                SetProperty(ref accountOptions, value);
            }
        }

        private string selectedAccount;
        public string SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                SetProperty(ref selectedAccount, value);
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
                    CreateBillsForDateRange();
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
        public NewBillsViewModel(IMvxNavigationService nav)
        {
            navigationService = nav;
            //Bill = new Bill();
            LoadAccountOptions();
            

            SaveCommand = new MvxAsyncCommand(async () => await OnSave());
            CancelCommand = new MvxAsyncCommand(async () => await OnCancel());

            //MultiBillOptions = new ObservableCollection<MultipleBillOptions>()
            //{
            //    MultipleBillOptions.ByDateRange,
            //    MultipleBillOptions.ByNumber
            //};
            //SelectedBillOption = MultipleBillOptions.ByDateRange;

            //DueFrequencies = new ObservableCollection<DueDateFrequencies>()
            //{
            //    DueDateFrequencies.OneWeek,
            //    DueDateFrequencies.TwoWeek,
            //    DueDateFrequencies.FourWeek,
            //    DueDateFrequencies.Monthly,
            //    DueDateFrequencies.Quarterly,
            //    DueDateFrequencies.Quarterly,
            //    DueDateFrequencies.Quarterly
            //};

            DueDateFrequency = DueDateFrequencies.Monthly;
            ResetNewBillsToSingle();

        }

        public override void Prepare(string parameter)
        {
            //if (parameter != "")
            //{
            //    SelectedPayee = parameter;
            //}
            var _ = PrepareAsync(parameter);
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
            NewBills.Add(new NewBillViewModel(StartDate));

        }

        private void GetBillsForDateRange()
        {
            var temp = NewBills.Where(b => b.Date >= StartDate && b.Date <= EndDate);

            if (temp.Count() == 0)
            {
                ResetNewBillsToSingle();
            }
            else
            {
                NewBills = new ObservableCollection<INewBillViewModel>(temp);
            }
        }

        private void CreateBillsForDateRange()
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
                        NewBills.Add(new NewMultiBillViewModel(i + 1, StartDate.AddDays(7 * i)));
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
                        NewBills.Add(new NewMultiBillViewModel(i + 1, StartDate.AddDays(14 * i)));
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
                        NewBills.Add(new NewMultiBillViewModel(i + 1, StartDate.AddDays(28 * i)));
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
                        NewBills.Add(new NewMultiBillViewModel(i + 1, StartDate.AddMonths(i)));
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
                        NewBills.Add(new NewMultiBillViewModel(i + 1, StartDate.AddMonths(3 * i)));
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
                        NewBills.Add(new NewMultiBillViewModel(i + 1, StartDate.AddMonths(6 * i)));
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
                        NewBills.Add(new NewMultiBillViewModel(i + 1, StartDate.AddYears(i)));
                    }
                    break;
                default:
                    break;


            }
            RaisePropertyChanged(nameof(BillCount));
        }

        //public override void Prepare(string parameter)
        //{
        //    companyName = parameter;
        //}
        #endregion

        #region Methods
        private async Task OnSave()
        {
            //var accts = await DataManager.GetBankAccounts();
            //var acct = accts.Where(x => x.Nickname.Equals(SelectedAccount)).Single();
            //var acct = SelectedAccount;
            var acctId = await DataManager.GetBankAccountID(SelectedAccount);
            var payee = IsNewPayee ? NewPayee : SelectedPayee;

            if (string.IsNullOrWhiteSpace(payee))
            {
                var config = new AlertConfig().SetMessage("Payee needs a name");
                Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(config);
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

                await DataManager.InsertBills(tempBills);
            }
            else
            {
                var newBill = new Bill(payee, Amount, StartDate)
                {
                    AccountID = acctId,
                    //BankAccount = acct,
                    IsPaid = IsPaid
                };
                await DataManager.SaveBill(newBill);
            }
            Messenger.Instance.Send(new ChangeBillMessage());
            await navigationService.Close(this);


            //if (IsNewPayee)
            //{
            //    Bill.Payee = NewPayee;
            //}
            //else
            //{
            //    Bill.Payee = SelectedPayee;
            //}

            ////await navigationService.Close(this, Bill);
            ////var accts = await BudgetDatabase.Instance.GetBankAccounts();
            //var accts = await DataManager.GetBankAccounts();
            //var acct = accts.Where(x => x.Nickname.Equals(SelectedAccount)).First();
            //Bill.BankAccount = acct;
            ////Bill.AccountID = acct.AccountID;
            ////await BudgetDatabase.Instance.SaveBill(Bill);
            //await DataManager.SaveBill(Bill);
            //Messenger.Instance.Send(new ChangeBillMessage(Bill.AccountID));
            //await DataManager.UpdatePayeeNames();
            //await navigationService.Close(this, true);
        }

        private async Task OnCancel()
        {
            await navigationService.Close(this);
        }

        //private void LoadAccountOptions()
        //{
        //    //var options = await BudgetDatabase.Instance.GetBankAccounts();
        //    AccountOptions = new ObservableCollection<string>(DataManager.BankAccountNicknames);// await DataManager.GetBankAccounts();
        //    //AccountOptions.Clear();
        //    //foreach (var item in options)
        //    //{
        //    //    AccountOptions.Add(item.Nickname);
        //    //}
        //    if(AccountOptions.Count > 1)
        //    {
        //        SelectedAccount = AccountOptions.ElementAt(1);
        //    }
        //    else
        //    {
        //        SelectedAccount = AccountOptions.FirstOrDefault();
        //    }


        //}

        private void LoadAccountOptions()
        {
            AccountOptions = new ObservableCollection<string>(DataManager.BankAccountNicknames);
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
            await DataManager.UpdatePayeeNames();
            PayeeOptions = new ObservableCollection<string>(DataManager.PayeeNames);

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
