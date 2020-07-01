using Acr.UserDialogs;
using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross;
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
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class NewBillsViewModel : MvxViewModel<string>//, bool>//<string, Bill>
    {
        #region Fields
        private IMvxNavigationService navigationService;
        private IDataManager DataManager = Mvx.IoCProvider.Resolve<IDataManager>();
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
                //amount = value;
                //RaisePropertyChanged();
                if (amount != value || amount == 0.0m)
                {
                    var temp = Math.Truncate(100 * value) / 100;
                    amount = temp;
                    //amount = value;
                    RaisePropertyChanged();
                    //RaisePropertyChanged(nameof(CursorPosition));
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
                if(value >= StartDate)
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


        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Constructors
        public NewBillsViewModel(IMvxNavigationService nav)
        {
            navigationService = nav;
            //Bill = new Bill();
            LoadAccountOptions();
            LoadPayeeOptions();

            SaveCommand = new Command(async () => await OnSave());
            CancelCommand = new Command(async () => await OnCancel());

            //MultiBillOptions = new ObservableCollection<MultipleBillOptions>()
            //{
            //    MultipleBillOptions.ByDateRange,
            //    MultipleBillOptions.ByNumber
            //};
            //SelectedBillOption = MultipleBillOptions.ByDateRange;

            DueFrequencies = new ObservableCollection<DueDateFrequencies>()
            {
                DueDateFrequencies.OneWeek,
                DueDateFrequencies.TwoWeek,
                DueDateFrequencies.FourWeek,
                DueDateFrequencies.Monthly,
                DueDateFrequencies.Quarterly
            };

            DueDateFrequency = DueDateFrequencies.Monthly;
            ResetNewBillsToSingle();

        }

        public override void Prepare(string parameter)
        {
            if(parameter != "")
            {
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

            if(temp.Count() == 0)
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
            var weeks = 0;
            var days = (EndDate - StartDate).TotalDays;
            var months = 0;
            double dayCount = 0.0;

            NewBills.Clear();
            switch (DueDateFrequency)
            {
                case DueDateFrequencies.OneWeek:
                    while(dayCount < days)
                    {

                    }
                    break;
                case DueDateFrequencies.TwoWeek:
                    weeks = 2;
                    break;
                case DueDateFrequencies.FourWeek:
                    break;

                case DueDateFrequencies.Monthly:
                    while(dayCount <= days)
                    {
                        dayCount = (StartDate.AddMonths(months + 1) - StartDate).TotalDays;
                        months++;
                    }
                    for (int i = 0; i < months; i++)
                    {
                        NewBills.Add(new NewMultiBillViewModel(StartDate.AddMonths(i)));
                    }
                    break;
                case DueDateFrequencies.Quarterly:
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
            var accts = await DataManager.GetBankAccounts();
            var acct = accts.Where(x => x.Nickname.Equals(SelectedAccount)).First();
            var payee = IsNewPayee ? NewPayee : SelectedPayee;
            var tempBills = new List<Bill>();

            foreach (var item in NewBills)
            {
                var bill = new Bill(payee, (double)Amount, item.Date);
                tempBills.Add(bill);
            }

            await DataManager.SaveBills(tempBills);
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
            PayeeOptions = new ObservableCollection<string>(DataManager.PayeeNames);

            if(PayeeOptions.Count == 0)
            {
                IsNewPayee = true;
            }
            else if(SelectedPayee == null)
            {
                SelectedPayee = PayeeOptions.First();
            }
        }

        

        #endregion

    }
}
