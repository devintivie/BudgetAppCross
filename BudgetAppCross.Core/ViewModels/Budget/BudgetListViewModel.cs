using Acr.UserDialogs;
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
using Xamarin.Forms;
using System.Linq;

namespace BudgetAppCross.Core.ViewModels
{
    public class BudgetListViewModel : BaseViewModel// MvxViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
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
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveBudgetCommand { get; }
        public ICommand LoadBudgetCommand { get; }
        public ICommand RefreshItemsCommand { get; }
        #endregion

        #region Constructors
        public BudgetListViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            Title = "Bill List";

            AddBTCommand = new Command(async () => await navigationService.Navigate<NewBillViewModel>());
            EditCommand = new Command(async () => await OnEdit());
            DeleteCommand = new Command(async () => await OnDelete());
            //SaveBudgetCommand = new Command(async() => await OnSaveBudget());
            //LoadBudgetCommand = new Command(async () => await OnLoadBudget());
            RefreshItemsCommand = new Command(async () => await OnRefresh());

            

            



            //    var countGroupQuery = from table in dataTable.AsEnumerable()
            //                          group table by table.Field<string>(Column1) into groupedTable
            //                          select new
            //                          {
            //                              value = groupedTable.Key,
            //                              count = groupedTable.Count()
            //                          };

            //var query = dataTable.AsEnumerable()
            //         .GroupBy(x => table.Field<string>(Column1),
            //                  StringComparer.InvariantCultureIgnoreCase)
            //         .Select(groupedTable => new
            //         {
            //             value = groupedTable.Key,
            //             count = groupedTable.Count()
            //         });

        }

        
        #endregion

        #region Methods

        private async void LoadBills()
        {
            try
            {
                var bills = await BudgetDatabase.GetBills();
                var data = (bills.GroupBy(x => x.Payee, StringComparer.OrdinalIgnoreCase)
                            .Select(groupedTable => new Grouping<string, Bill>(groupedTable.Key, groupedTable))).ToList();
                Trackers.Clear();
                foreach (var item in data)
                {
                    Trackers.Add(new BillTracker(item.Key, item.Grouped));
                }
            }
            catch (InvalidOperationException ex)
            {
                Trackers.Clear();
            }
            
            
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            SelectedTracker = null;
            LoadBills();
            //Trackers = new ObservableCollection<BillTracker>(BillManager.AllTrackers);
        }

        public Task ShowBillTracker(BillTracker bt)
        {
            return navigationService.Navigate<BillTrackerViewModel, BillTracker>(bt);
        }

        //private async Task OnSaveBudget()
        //{
        //    await StateManager.SaveToFile();
        //}

        //private async Task OnLoadBudget()
        //{
        //    await StateManager.LoadFromFile();
        //    UpdateTrackers();

        //}

        private async Task OnEdit()
        {
            //var config = new ActionSheetConfig().SetTitle("Test Title").SetMessage("hi nerd").
            //config.Options.Add(new ActionSheetOption("test1", () => Test()));
            //Mvx.IoCProvider.Resolve<IUserDialogs>().ActionSheet(config);
            ////Mvx.Resolve<IUserDialogs>().Alert("it is not valid");
        }
        private async Task OnDelete()
        {
            UpdateTrackers();
        }

        private async Task OnRefresh()
        {
            await Task.Delay(2500);
            UpdateTrackers();
            IsBusy = false;
        }

        private void UpdateTrackers()
        {
            //Trackers = new ObservableCollection<BillTracker>(BillManager.AllTrackers);
        }

        private void Test()
        {
            Console.WriteLine("Test");
        }
        #endregion


    }
}
