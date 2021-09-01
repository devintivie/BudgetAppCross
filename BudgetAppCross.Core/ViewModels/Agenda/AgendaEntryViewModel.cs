using BaseClasses;
using BaseViewModels;
using BudgetAppCross.Core.Services;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BudgetAppCross.Core.ViewModels
{
    public class AgendaEntryViewModel : BaseViewModel
    {
        #region Fields
        private IDataManager _dataManager;
        #endregion

        #region Properties
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                SetProperty(ref date, value);
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

        private decimal dateTotal;
        public decimal DateTotal
        {
            get { return dateTotal; }
            set
            {
                SetProperty(ref dateTotal, value);
            }
        }


        #endregion

        #region Commands

        #endregion

        #region Constructors

        public AgendaEntryViewModel(IBackgroundHandler backgroundHandler, IDataManager dataManager, Grouping<DateTime, Bill> datagroup) : base(backgroundHandler)
        {
            _dataManager = dataManager;
            Date = datagroup.Key;
            foreach (var item in datagroup)
            {
                Bills.Add(new BillViewModel(_backgroundHandler, _dataManager, item));
            }
        }

        #endregion

        #region Methods

        private void OnUpdateTotal()
        {
            var total = 0.0m;
            foreach (var item in Bills)
            {
                total += item.Amount;
            }

            DateTotal = total;
        }


        #endregion

    }
}