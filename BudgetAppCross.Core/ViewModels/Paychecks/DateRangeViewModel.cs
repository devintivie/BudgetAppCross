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
        private List<Grouping<DateTime, Bill>> billGroups = new List<Grouping<DateTime, Bill>>();
        private bool initialized = false;
        #endregion

        #region Properties
        private DateTime startDate = DateTime.Today;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                SetProperty(ref startDate, value);
                if (initialized) { var _ = LoadData(); }
            }
        }

        private DateTime endDate = DateTime.Today.AddDays(14);
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                SetProperty(ref endDate, value);
                if (initialized) { var _ = LoadData(); }
            }
        }

        //private ObservableCollection<BillViewModel> transactions = new ObservableCollection<BillViewModel>();
        //public ObservableCollection<BillViewModel> Transactions
        //{
        //    get { return transactions; }
        //    set
        //    {
        //        SetProperty(ref transactions, value);
        //    }
        //}

        private ObservableCollection<DateRangeEntryViewModel> accounts = new ObservableCollection<DateRangeEntryViewModel>();
        public ObservableCollection<DateRangeEntryViewModel> Accounts
        {
            get { return accounts; }
            set
            {
                SetProperty(ref accounts, value);
            }
        }



        //private double dateRangeTotal;
        //public double DateRangeTotal
        //{
        //    get { return dateRangeTotal; }
        //    set
        //    {
        //        SetProperty(ref dateRangeTotal, value);
        //    }
        //}

        //private double startingBalance;
        //public double StartingBalance
        //{
        //    get { return startingBalance; }
        //    set
        //    {
        //        SetProperty(ref startingBalance, value);
        //    }
        //}

        //private double endingBalance;
        //public double EndingBalance
        //{
        //    get { return endingBalance; }
        //    set
        //    {
        //        SetProperty(ref endingBalance, value);
        //    }
        //}

        //private double dateRangeTotal2;
        //public double DateRangeTotal2
        //{
        //    get { return dateRangeTotal2; }
        //    set
        //    {
        //        SetProperty(ref dateRangeTotal2, value);
        //    }
        //}

        //private double startingBalance2;
        //public double StartingBalance2
        //{
        //    get { return startingBalance2; }
        //    set
        //    {
        //        SetProperty(ref startingBalance2, value);
        //    }
        //}

        //private double endingBalance2;
        //public double EndingBalance2
        //{
        //    get { return endingBalance2; }
        //    set
        //    {
        //        SetProperty(ref endingBalance2, value);
        //    }
        //}

        #endregion

        #region Commands
        public ICommand AddBillCommand { get; }
        public ICommand DeleteBillCommand { get; }
        #endregion

        #region Constructors
        public DateRangeViewModel(IMvxNavigationService navigation, IMvxMessenger messenger)
        {
            navigationService = navigation;
            Title = "Paycheck View";

            var _ = LoadAccounts();
            //var _ = LoadData();
            //Messenger.Register<UpdateBillMessage>(this, x => OnUpdateBillMessage());
            //token = messenger.Subscribe<UpdateBillMessage>(OnUpdateBillMessage);

            AddBillCommand = new Command(async () => await navigationService.Navigate<NewBillViewModel, Bill>(new Bill()));



        }
        #endregion

        #region Methods
        private async Task LoadData()
        {
            //await BudgetDatabase.UpdateBankAccountNames();
            //var bills = await BudgetDatabase.GetBills();
            //var data = (bills.Where(x => x.Date >= StartDate && x.Date <= EndDate)
            //            .OrderBy(x => x.Date)
            //            .Select(bill => bill)).ToList();

            //Transactions.Clear();

            //foreach (var item in data)
            //{
            //    Transactions.Add(new BillViewModel(item));
            //}

            //await UpdateCalculations();
            
        }

        private async Task LoadAccounts()
        {
            var accts = await BudgetDatabase.GetBankAccounts();

            Accounts.Clear();
            foreach (var acct in accts)
            {
                Accounts.Add(new DateRangeEntryViewModel(StartDate, EndDate, acct));
            }
        }

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

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            initialized = true;
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            //SaveBills();
            base.ViewDestroy(viewFinishing);
            
        }

        //public async Task SaveBills()
        //{
        //    foreach (var bill in Transactions)
        //    {
        //        await BudgetDatabase.SaveBill(bill.Bill);
        //    }
        //}

        private void OnUpdateBillMessage()
        {
            //UpdateCalculations();
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
