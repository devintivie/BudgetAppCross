using BudgetAppCross.Core.Services;
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
    public class AgendaEntryViewModel : MvxViewModel
    {
        #region Fields
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

        private ObservableCollection<AgendaBillViewModel> bills = new ObservableCollection<AgendaBillViewModel>();
        public ObservableCollection<AgendaBillViewModel> Bills
        {
            get { return bills; }
            set
            {
                SetProperty(ref bills, value);
            }
        }

        #endregion

        #region Commands
        #endregion

        #region Constructors
        public AgendaEntryViewModel(Grouping<DateTime, AgendaBill> datagroup )
        {
            Date = datagroup.Key;
            foreach (var item in datagroup)
            {
                Bills.Add(new AgendaBillViewModel(item));
            }
        }


        #endregion

        #region Methods

        public override void ViewAppeared()
        {
            base.ViewAppeared();


        }


        #endregion

    }
}