using BudgetAppCross.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class BudgetListViewModel : MvxViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        public ObservableCollection<BillTracker> Trackers { get; set; } = new ObservableCollection<BillTracker>();
        private BillTracker selectedTracker;
        public BillTracker SelectedTracker
        {
            get { return selectedTracker; }
            set
            {
                SetProperty(ref selectedTracker, value);
            }
        }


        #endregion

        #region Commands
        public ICommand AddBTCommand { get; }
        #endregion

        #region Constructors
        public BudgetListViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            var firstBill = new Bill(100, new DateTime(2020, 5, 10));
            var firstBt = new BillTracker("AT&T", firstBill);
            Trackers.Add(firstBt);

            //AddBTCommand = new Command(async () => await navigationService.Navigate<NewBillTrackerViewModel>());
        }
        #endregion

        #region Methods

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            SelectedTracker = null;

            
        }
        public Task ShowBillTracker(BillTracker bt)
        {
            return navigationService.Navigate<BillTrackerViewModel, BillTracker>(bt);
        }
        #endregion


    }
}
