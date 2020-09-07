using Acr.UserDialogs;
using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
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
    public class DateRangeViewModel : BaseViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        //private DateTime startDate = DateTime.Today;
        //public DateTime StartDate
        //{
        //    get { return startDate; }
        //    set
        //    {
        //        var currStart = new DateTime(startDate.Ticks);
        //        SetProperty(ref startDate, value);

        //        EndDate = StartDate.AddDays(14);
        //        //if (initialized) { var _ = GetGroups(); }
        //        //var _ = GetBills();
        //    }
        //}

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
                if (value >= startDate)
                {
                    SetProperty(ref endDate, value);
                }
                else
                {
                    var config = new AlertConfig().SetMessage("Ending date must be after start date");
                    UserDialogs.Instance.Alert(config);
                }

                //if (initialized) { var _ = GetGroups(); }
                //var _ = GetBills();
            }
        }

        private ObservableCollection<BillViewModel> bills = new ObservableCollection<BillViewModel>();
        public ObservableCollection<BillViewModel> Bills
        {
            get { return bills; }
            set
            {
                SetProperty(ref bills, value);
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
                if (selectedAccount != value)
                {
                    SetProperty(ref selectedAccount, value);
                    var _bills = GetBills(); 
                }

            }
        }

        private decimal startingBalance;
        public decimal StartingBalance
        {
            get { return startingBalance; }
            set
            {
                SetProperty(ref startingBalance, value);
                RaisePropertyChanged(nameof(Remainder));
            }
        }

        private decimal billTotal;
        public decimal BillTotal
        {
            get { return billTotal; }
            set
            {
                SetProperty(ref billTotal, value);
                RaisePropertyChanged(nameof(Remainder));
            }
        }

        public decimal Remainder => StartingBalance - BillTotal;




        //private ObservableCollection<DateRangeGrouping<BankAccount, BillViewModel>> billsGrouped = new ObservableCollection<DateRangeGrouping<BankAccount, BillViewModel>>();
        //public ObservableCollection<DateRangeGrouping<BankAccount, BillViewModel>> BillsGrouped
        //{
        //    get { return billsGrouped; }
        //    set
        //    {
        //        SetProperty(ref billsGrouped, value);
        //    }
        //}

        #endregion

        #region Commands
        public ICommand AddBillCommand { get; }
        //public ICommand DeleteBillCommand { get; }
        public ICommand OnDateSelectedCommand { get; }
        #endregion

        #region Constructors
        public DateRangeViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            Title = "Custom Dates";

            //var _ = LoadAccounts();
            var _accts = LoadAccountOptions();

            //var _groups = GetGroups();
            //var _ = LoadData();
            //Messenger.Register<UpdateBillMessage>(this, x => OnUpdateBillMessage());
            //token = messenger.Subscribe<UpdateBillMessage>(OnUpdateBillMessage);

            AddBillCommand = new Command(async () => await navigationService.Navigate<NewBillsViewModel, string>(string.Empty));
            OnDateSelectedCommand = new Command(async () => await GetBills());

            Messenger.Register<ChangeBillMessage>(this, async x => await OnChangeBillMessage(x.AccountId));
            Messenger.Register<UpdateBillMessage>(this, async x => await OnUpdateBillMessage(x.AccountId));


        }

        public override void ViewDestroy(bool viewFinishing = true)
        {

            Messenger.Unregister(this);
            base.ViewDestroy(viewFinishing);
        }
        #endregion

        #region Methods
        private async Task GetBills()
        {
            if(SelectedAccount != null)
            {
                var billcall = await BudgetDatabase.GetBills();

                var billData = billcall
                    .Where(x => x.Date >= StartDate)
                    .Where(x => x.Date <= EndDate)
                    .Where(x => x.BankAccount.Nickname.Equals(SelectedAccount))
                    .OrderBy(x => x.Date).ToList();

                Bills.Clear();
                foreach (var bill in billData)
                {
                    Bills.Add(new BillViewModel(bill));
                }

                await UpdateCalculations();
            }
            



            //    var billData = (bills)
            //                .Where(x => x.Date >= StartDate)
            //                .Where(x => x.Date <= EndDate)
            //                .OrderBy(x => x.Date)
            //                .Select(bill => bill).ToList();

            //    //var data = billData.GroupBy(item => item.BankAccount.Nickname)
            //    //            .Select(x => new
            //    //            {
            //    //                x.Key,
            //    //                x,
            //    //                sum = x.Sum(i => i.Amount)
            //    //            });

            //    var moreData = billData.GroupBy(x => x.BankAccount)
            //                .OrderBy(x => x.Key.Nickname).ToList();
            //    var data = moreData.Select(grouped => new DateRangeGrouping<BankAccount, Bill>(grouped.Key, grouped)
            //    {
            //        Sum = grouped.Sum(i => i.Amount),

            //    }).ToList();


            //    var newGroup = new List<DateRangeGrouping<BankAccount, BillViewModel>>();
            //    foreach (var item in data)
            //    {
            //        var key = item.Key;
            //        var bvms = new List<BillViewModel>();
            //        foreach (var group in item.Grouped)
            //        {
            //            bvms.Add(new BillViewModel(group));
            //        }
            //        newGroup.Add(new DateRangeGrouping<BankAccount, BillViewModel>(key, bvms));
            //    }

            //    BillsGrouped = new ObservableCollection<DateRangeGrouping<BankAccount, BillViewModel>>(newGroup);









        }

        private async Task LoadAccountOptions()
        {
            AccountOptions = new ObservableCollection<string>(BudgetDatabase.BankAccountNicknames);

            var allAccts = await BudgetDatabase.GetBankAccounts();
            var accts = allAccts.Where(x => x.AccountID != 1).Select(x => x.Nickname).ToList();
            AccountOptions = new ObservableCollection<string>(accts);
            SelectedAccount = AccountOptions.FirstOrDefault();

            //await GetBills();
        }


        private async Task UpdateCalculations()
        {
            var billcall = await BudgetDatabase.GetBills();

            var billData = billcall
                .Where(x => x.Date >= StartDate)
                .Where(x => x.Date <= EndDate)
                .Where(x => x.BankAccount.Nickname.Equals(SelectedAccount))
                .OrderBy(x => x.Date).ToList();

            //Doesnt work if there are no bills
            var bal = await BudgetDatabase.GetLatestBalance(SelectedAccount, StartDate);
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


        //private async Task LoadData()
        //{
        //    //await BudgetDatabase.UpdateBankAccountNames();
        //    //var bills = await BudgetDatabase.GetBills();
        //    //var data = (bills.Where(x => x.Date >= StartDate && x.Date <= EndDate)
        //    //            .OrderBy(x => x.Date)
        //    //            .Select(bill => bill)).ToList();

        //    //Transactions.Clear();

        //    //foreach (var item in data)
        //    //{
        //    //    Transactions.Add(new BillViewModel(item));
        //    //}

        //    //await UpdateCalculations();

        //}

        //private async Task LoadAccounts()
        //{
        //    var accts = await BudgetDatabase.GetBankAccounts();

        //    Accounts.Clear();
        //    foreach (var acct in accts)
        //    {
        //        Accounts.Add(new DateRangeEntryViewModel(StartDate, EndDate, acct));
        //    }
        //}

        //private async Task GetGroups()
        //{
        //    var bills = await BudgetDatabase.GetBills();
        //    var billData = (bills)
        //                .Where(x => x.Date >= StartDate)
        //                .Where(x => x.Date <= EndDate)
        //                .OrderBy(x => x.Date)
        //                .Select(bill => bill).ToList();

        //    //var data = billData.GroupBy(item => item.BankAccount.Nickname)
        //    //            .Select(x => new
        //    //            {
        //    //                x.Key,
        //    //                x,
        //    //                sum = x.Sum(i => i.Amount)
        //    //            });

        //    var moreData = billData.GroupBy(x => x.BankAccount)
        //                .OrderBy(x => x.Key.Nickname).ToList();
        //    var data = moreData.Select(grouped => new DateRangeGrouping<BankAccount, Bill>(grouped.Key, grouped)
        //    {
        //        Sum = grouped.Sum(i => i.Amount),

        //    }).ToList();


        //    var newGroup = new List<DateRangeGrouping<BankAccount, BillViewModel>>();
        //    foreach (var item in data)
        //    {
        //        var key = item.Key;
        //        var bvms = new List<BillViewModel>();
        //        foreach (var group in item.Grouped)
        //        {
        //            bvms.Add(new BillViewModel(group));
        //        }
        //        newGroup.Add(new DateRangeGrouping<BankAccount, BillViewModel>(key, bvms));
        //    }

        //    BillsGrouped = new ObservableCollection<DateRangeGrouping<BankAccount, BillViewModel>>(newGroup);
        //}

        //private async Task UpdateAccounts()
        //{
        //    foreach (var acct in Accounts)
        //    {
        //        await acct.UpdateData(StartDate, EndDate);
        //    }
        //}

        //public override async Task Initialize()
        //{
        //    await base.Initialize();
        //    Console.WriteLine("Start DRVM Init");
        //    await LoadData();
        //    Console.WriteLine("Finished DRVM Init");
        //}

        //public override void Start()
        //{
        //    base.Start();
        //    initialized = true;
        //}

        //public override void ViewAppeared()
        //{
        //    base.ViewAppeared();
        //    //initialized = true;
        //}

        //public override void ViewDestroy(bool viewFinishing = true)
        //{
        //    //SaveBills();
        //    base.ViewDestroy(viewFinishing);
            
        //}

        //public async Task SaveBills()
        //{
        //    foreach (var bill in Transactions)
        //    {
        //        await BudgetDatabase.SaveBill(bill.Bill);
        //    }
        //}

        //private void OnUpdateBillMessage()
        //{
        //    //UpdateCalculations();
        //}

        private async Task OnChangeBillMessage(int id)
        {
            //await GetGroups();
            await GetBills();
        }

        private async Task OnUpdateBillMessage(int id)
        {
            await UpdateCalculations();
        }

        //private async Task UpdateCalculations()
        //{
        //    var bills = await BudgetDatabase.GetBills();
        //    var data = (bills.Where(x => x.Date >= StartDate && x.Date <= EndDate)
        //                .OrderBy(x => x.Date)
        //                .Select(bill => bill)).ToList();
        //    var accts = await BudgetDatabase.GetBankAccounts();
        //    var accountIDUsed = accts.First().AccountID;
        //    StartingBalance = (await BudgetDatabase.GetLatestBalance(accountIDUsed, startDate)).Amount;
        //    DateRangeTotal = 0;

        //    var billsForThisAccount = data.Where(x => x.AccountID == accountIDUsed);
        //    foreach (var item in billsForThisAccount)
        //    {
        //        DateRangeTotal += item.Amount;
        //    }

        //    EndingBalance = StartingBalance - DateRangeTotal;



        //    var accountIDUsed2 = accts.Last().AccountID;
        //    StartingBalance2 = (await BudgetDatabase.GetLatestBalance(accountIDUsed2, startDate)).Amount;
        //    DateRangeTotal2 = 0;

        //    var billsForThisAccount2 = data.Where(x => x.AccountID == accountIDUsed2);
        //    foreach (var item in billsForThisAccount2)
        //    {
        //        DateRangeTotal2 += item.Amount;
        //    }

        //    EndingBalance2 = StartingBalance2 - DateRangeTotal2;
        //}
        #endregion

    }
}
