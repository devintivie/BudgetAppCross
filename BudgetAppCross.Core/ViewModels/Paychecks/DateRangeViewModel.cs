using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace BudgetAppCross.Core.ViewModels
{
    public class DateRangeViewModel : BaseViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        private List<Grouping<DateTime, AgendaBill>> billGroups = new List<Grouping<DateTime, AgendaBill>>();
        #endregion

        #region Properties
        private DateTime startDate = DateTime.Today;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                SetProperty(ref startDate, value);
                LoadData();
            }
        }

        private DateTime endDate = DateTime.Today.AddDays(14);
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                SetProperty(ref endDate, value);
                LoadData();
            }
        }

        private ObservableCollection<BillViewModel> transactions = new ObservableCollection<BillViewModel>();
        public ObservableCollection<BillViewModel> Transactions
        {
            get { return transactions; }
            set
            {
                SetProperty(ref transactions, value);
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



        #endregion

        #region Commands

        #endregion

        #region Constructors
        public DateRangeViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            Title = "Paycheck View";

            LoadData();

            //var data = (from bts in BillManager.AllTrackers
            //            from bill in bts.Bills
            //            select new AgendaBill
            //            {
            //                Company = bts.CompanyName,
            //                Amount = bill.Amount,
            //                Date = bill.Date,
            //                IsPaid = bill.IsPaid,
            //                Confirmation = bill.Confirmation
            //            }).ToList();

            //foreach (var item in data)
            //{

            //}

            //foreach (var group in billGroups)
            //{

            //    if (group.Key >= startDate && group.Key <= EndDate)
            //    {
            //        BillsGrouped.Add(new PaycheckEntryViewModel(group));
            //    }

            //}
        }
        #endregion

        #region Methods
        private async void LoadData()
        {
            var bills = await BudgetDatabase.GetBills();
            var data = (bills.Where(x => x.Date >= StartDate && x.Date <= EndDate)
                        .OrderBy(x => x.Date)
                        .Select(bill => bill)).ToList();
            Transactions.Clear();

            foreach (var item in data)
            {
                Transactions.Add(new BillViewModel(item));
            }

            //BillsGrouped.Clear();
            //foreach (var item in data)
            //{
            //    BillsGrouped.Add(new AgendaEntryViewModel(item));
            //}
        }
        #endregion

    }
}
