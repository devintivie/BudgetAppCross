using BudgetAppCross.Core.Services;
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
        private BillManager BillManager => BillManager.Instance;
        #endregion

        #region Properties
        //public ObservableCollection<BillTracker> Trackers { get; set; } = new ObservableCollection<BillTracker>();

        private ObservableCollection<BillTracker> trackers = new ObservableCollection<BillTracker>();
        public ObservableCollection<BillTracker> Trackers
        {
            get { return trackers; }
            set
            {
                SetProperty(ref trackers, value);
            }
        }

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

            AddBTCommand = new Command(async () => await navigationService.Navigate<NewBillTrackerViewModel>());
        }
        #endregion

        #region Methods

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            SelectedTracker = null;
            Trackers = new ObservableCollection<BillTracker>(BillManager.AllTrackers);


        }
        public Task ShowBillTracker(BillTracker bt)
        {
            return navigationService.Navigate<BillTrackerViewModel, BillTracker>(bt);
        }
        #endregion


    }
}
