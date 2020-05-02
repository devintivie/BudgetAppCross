using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class BillTrackerViewModel : MvxViewModel<BillTracker>
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        public BillTracker BillTracker { get; private set; }
        #endregion

        #region Commands
        public ICommand AddBillCommand { get; }
        #endregion

        #region Constructors
        public BillTrackerViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            AddBillCommand = new Command(async () => await navigationService.Navigate<NewBillViewModel>());
        }
        #endregion

        #region Methods
        public override void Prepare(BillTracker parameter)
        {
            BillTracker = parameter;
        }
        #endregion

        

        


        
    }
}
