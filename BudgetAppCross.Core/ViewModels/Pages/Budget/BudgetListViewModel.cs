//using Acr.UserDialogs;
using BudgetAppCross.Models;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using BaseViewModels;
using BaseClasses;
using BudgetAppCross.DataAccess;

namespace BudgetAppCross.Core.ViewModels.Pages
{
    public class BudgetListViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        private IDataManager _dataManager;
        #endregion

        #region Properties
        private ObservableCollection<BillTrackerQuickViewModel> trackers = new ObservableCollection<BillTrackerQuickViewModel>();
        public ObservableCollection<BillTrackerQuickViewModel> Trackers
        {
            get { return trackers; }
            set
            {
                if (trackers != value)
                {
                    trackers = value;
                    RaisePropertyChanged();
                }
            }
        }

        private BillTrackerQuickViewModel selectedTracker;
        public BillTrackerQuickViewModel SelectedTracker
        {
            get { return selectedTracker; }
            set
            {
                if (selectedTracker != value)
                {
                    selectedTracker = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Commands
        public IMvxCommand AddBillCommand { get; }
        //public IMvxCommand EditCommand { get; }
        //public IMvxCommand DeleteCommand { get; }
        //public IMvxCommand SaveBudgetCommand { get; }
        //public IMvxCommand LoadBudgetCommand { get; }
        //public IMvxCommand RefreshItemsCommand { get; }
        public IMvxCommand ShowBillTrackerCommand { get; }
        #endregion

        #region Constructors
        public BudgetListViewModel(IMvxNavigationService navigation, IBackgroundHandler backgroundHandler,
            IDataManager dataManager) : base(navigation, backgroundHandler)
        {
            _dataManager = dataManager;
            AddBillCommand = new MvxAsyncCommand(OnAddBT);
            ShowBillTrackerCommand = new MvxAsyncCommand(OnShowBillTracker);

            _backgroundHandler.RegisterMessage<ChangeBillMessage>(this, async x => await OnChangeBillMessage());

            _ = LoadBills();
        }

        #endregion

        #region Methods

        private async Task LoadBills()
        {
            var names = await _dataManager.GetBillPayees();
            names.Sort();
            Trackers.Clear();
            foreach (var name in names)
            {
                Trackers.Add(new BillTrackerQuickViewModel(_navService, _backgroundHandler, _dataManager, name));
            }
        }

        private async Task OnChangeBillMessage()
        {
            await LoadBills();
        }

        private async Task OnAddBT()
        {
            await _navService.Navigate<NavTestViewModel>();
            //await _navService.Navigate<NewBillsViewModel, string>(string.Empty);
        }

        private async Task OnShowBillTracker()//string payee)
        {
            var payee = SelectedTracker.Payee;
            await _navService.Navigate<BillTrackerViewModel, string>(payee);
        }


        #endregion


    }
}
