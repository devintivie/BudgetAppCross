using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BudgetAppCross.Core.ViewModels
{
    public class BudgetListViewModel : MvxViewModel
    {
        #region Fields

        #endregion

        #region Properties
        public ObservableCollection<BillTracker> Items { get; set; } = new ObservableCollection<BillTracker>();
        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion

        public BudgetListViewModel()
        {

        }
    }
}
