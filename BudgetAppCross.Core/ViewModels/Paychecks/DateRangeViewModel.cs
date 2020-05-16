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
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                SetProperty(ref startDate, value);
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                SetProperty(ref endDate, value);
            }
        }

        //private ObservableCollection<TransactionViewModel> transactions = new ObservableCollection<TransactionViewModel>();
        //public ObservableCollection<TransactionViewModel> Transactions
        //{
        //    get { return transactions; }
        //    set
        //    {
        //        SetProperty(ref transactions, value);
        //    }
        //}


        #endregion

        #region Commands

        #endregion

        #region Constructors
        public DateRangeViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            Title = "Paycheck View";

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

        #endregion

    }
}
