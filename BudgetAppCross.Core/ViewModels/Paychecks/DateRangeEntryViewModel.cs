using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class DateRangeEntryViewModel : BaseViewModel// MvxViewModel
    {
        #region Fields
        #endregion

        #region Properties
        private DateTime StartDate; //{ get; private set; }
        private DateTime EndDate; //{ get; private set; }
        private List<Bill> billList;

        public BankAccount BankAccount { get; private set; }

        private ObservableCollection<BillViewModel> bills = new ObservableCollection<BillViewModel>();
        public ObservableCollection<BillViewModel> Bills
        {
            get { return bills; }
            set
            {
                SetProperty(ref bills, value);
            }
        }

        private double dateRangeTotal;
        public double DateRangeTotal
        {
            get { return dateRangeTotal; }
            set
            {
                SetProperty(ref dateRangeTotal, value);
            }
        }

        private double startingBalance;
        public double StartingBalance
        {
            get { return startingBalance; }
            set
            {
                SetProperty(ref startingBalance, value);
            }
        }

        private double endingBalance;
        public double EndingBalance
        {
            get { return endingBalance; }
            set
            {
                SetProperty(ref endingBalance, value);
            }
        }


        #endregion

        #region Commands

        #endregion

        #region Constructors
        public DateRangeEntryViewModel(DateTime start, DateTime end, BankAccount acct)
        {
            StartDate = start;
            EndDate = end;
            BankAccount = acct;
            Bills.Clear();

            var load = LoadData();
            Messenger.Register<UpdateBillMessage>(this, async x => await OnUpdateBillMessage(x.AccountId));
            Messenger.Register<ChangeBillMessage>(this, async x => await OnChangeBillMessage());

        }

        private async Task OnUpdateBillMessage(int id)
        {
            if(BankAccount.AccountID == id)
            {
                await UpdateCalculations();
            }
            
            //LoadData();
        }

        private async Task OnChangeBillMessage()
        {
            //if (BankAccount.AccountID == id)
            //{
            //    await LoadData();
            //}

            await LoadData();
            
        }
        #endregion

        #region Methods

        //private void OnUpdateTotal()
        //{
        //    var total = 0.0;
        //    foreach (var item in Bills)
        //    {
        //        total += item.Amount;
        //    }

        //    DateTotal = total;
        //}
        private async Task UpdateCalculations()
        {
            var bal = await BudgetDatabase.GetLatestBalance(BankAccount.AccountID, StartDate);
            StartingBalance = bal.Amount;
            var billTotal = 0.0;
            foreach (var bill in billList)
            {
                billTotal += bill.Amount;
            }

            DateRangeTotal = billTotal;
            EndingBalance = StartingBalance - DateRangeTotal;
        }

        private async Task LoadData()
        {
            var bills = await BudgetDatabase.GetBills();
            var data = (bills)
                        .Where(x => x.Date >= StartDate)
                        .Where(x => x.Date <= EndDate)
                        .Where(x => x.AccountID == BankAccount.AccountID)
                        .OrderBy(x => x.Date)
                        .Select(bill => bill).ToList();

            billList = new List<Bill>(data);
            Bills.Clear();
            foreach (var item in billList)
            {
                Bills.Add(new BillViewModel(item));
            }

            await UpdateCalculations();

            //var bal = await BudgetDatabase.GetLatestBalance(BankAccount.AccountID, StartDate);
            //StartingBalance = bal.Amount;
            //var billTotal = 0.0;
            //foreach (var bill in billList)
            //{
            //    billTotal += bill.Amount;
            //}

            //DateRangeTotal = billTotal;
            //EndingBalance = StartingBalance - DateRangeTotal;

        }

        public async Task UpdateData(DateTime start, DateTime end)
        {
            if(StartDate.Equals(start) && EndDate.Equals(end))
            {
                return;
            }

            StartDate = start;
            EndDate = end;

            await LoadData();
        }


        #endregion

    }
}