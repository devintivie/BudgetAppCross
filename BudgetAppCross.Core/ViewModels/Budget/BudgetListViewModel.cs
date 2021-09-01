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

namespace BudgetAppCross.Core.ViewModels
{
    public class BudgetListViewModel : MvxNavigationBaseViewModel// MvxViewModel
    {
        #region Fields
        private IDataManager _dataManager;
        #endregion

        #region Properties
        //public ObservableCollection<BillTracker> Trackers { get; set; } = new ObservableCollection<BillTracker>();

        private ObservableCollection<BillTrackerQuickViewModel> trackers = new ObservableCollection<BillTrackerQuickViewModel>();
        public ObservableCollection<BillTrackerQuickViewModel> Trackers
        {
            get { return trackers; }
            set
            {
                SetProperty(ref trackers, value);
            }
        }

        private BillTrackerQuickViewModel selectedTracker;
        public BillTrackerQuickViewModel SelectedTracker
        {
            get { return selectedTracker; }
            set
            {
                SetProperty(ref selectedTracker, value);
            }
        }


        #endregion

        #region Commands
        public IMvxCommand AddBTCommand { get; }
        public IMvxCommand EditCommand { get; }
        public IMvxCommand DeleteCommand { get; }
        public IMvxCommand SaveBudgetCommand { get; }
        public IMvxCommand LoadBudgetCommand { get; }
        public IMvxCommand RefreshItemsCommand { get; }
        #endregion

        #region Constructors
        public BudgetListViewModel(IMvxNavigationService navigation, IBackgroundHandler backgroundHandler,
            IDataManager dataManager) : base(navigation, backgroundHandler)
        {
            //Title = "Bill List";
            _dataManager = dataManager;
            AddBTCommand = new MvxAsyncCommand(OnAddBT);
            //EditCommand = new MvxAsyncCommand(OnEdit);
            //DeleteCommand = new MvxAsyncCommand(OnDelete);
            //SaveBudgetCommand = new Command(async() => await OnSaveBudget());
            //LoadBudgetCommand = new Command(async () => await OnLoadBudget());
            //RefreshItemsCommand = new MvxAsyncCommand(OnRefresh);

            _backgroundHandler.RegisterMessage<ChangeBillMessage>(this, async x => await OnChangeBillMessage());

            var _ = LoadBills();

        }

        private async Task OnAddBT()
        {
            await _navService.Navigate<NewBillsViewModel, string>(string.Empty);
        }

        #endregion

        #region Methods
        public override void ViewAppeared()
        {
            base.ViewAppeared();
            SelectedTracker = null;
            //LoadBills();
        }

        private async Task LoadBills()
        {
            var names = await _dataManager.GetBillPayees();
            //List<Grouping<string, Bill>> data = (bills.GroupBy(x => x.Payee, StringComparer.OrdinalIgnoreCase)
            //            .Select(groupedTable => new Grouping<string, Bill>(groupedTable.Key, groupedTable))).ToList();

            names.Sort();
            Trackers.Clear();
            foreach (var name in names)
            {
                //var bt = new BillTracker(item.Key, item.Grouped);
                Trackers.Add(new BillTrackerQuickViewModel(_navService, _backgroundHandler, _dataManager, name));
            }
        }

        private async Task OnChangeBillMessage()
        {
            await LoadBills();
        }


        //public Task ShowBillTracker(BillTracker bt)
        //{
        //    return navigationService.Navigate<BillTrackerViewModel, string>(bt.CompanyName);
        //}

        //public Task ShowBillTracker(string payee)
        //{
        //    return navigationService.Navigate<BillTrackerViewModel, string>(payee);
        //}

        //private async Task OnSaveBudget()
        //{
        //    await StateManager.SaveToFile();
        //}

        //private async Task OnLoadBudget()
        //{
        //    await StateManager.LoadFromFile();
        //    UpdateTrackers();

        //}

        //private async Task OnEdit()
        //{
        //    //var config = new ActionSheetConfig().SetTitle("Test Title").SetMessage("hi nerd").
        //    //config.Options.Add(new ActionSheetOption("test1", () => Test()));
        //    //Mvx.IoCProvider.Resolve<IUserDialogs>().ActionSheet(config);
        //    ////Mvx.Resolve<IUserDialogs>().Alert("it is not valid");
        //}
        //private async Task OnDelete()
        //{
        //    UpdateTrackers();
        //}

        //private async Task OnRefresh()
        //{
        //    await Task.Delay(2500);
        //    UpdateTrackers();
        //    IsBusy = false;
        //}

        //private void UpdateTrackers()
        //{
        //    //Trackers = new ObservableCollection<BillTracker>(BillManager.AllTrackers);
        //}

        private void Test()
        {
            Console.WriteLine("Test");
        }
        #endregion


    }
}
