using BaseClasses;
using BaseViewModels;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BudgetAppCross.Core.ViewModels.Pages
{
    public class AgendaViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        IDataManager _dataManager;
        #endregion

        #region Properties

        private ObservableCollection<Grouping<DateTime, BillViewModel>> bills = new ObservableCollection<Grouping<DateTime, BillViewModel>>();
        public ObservableCollection<Grouping<DateTime, BillViewModel>> Bills
        {
            get { return bills; }
            set
            {
                if (bills != value)
                {
                    bills = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Commands
        public IMvxCommand AddBillCommand { get; }
        //public IMvxCommand SwipeTestCommand { get; }
        #endregion

        #region Constructors
        public AgendaViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            _ = GetGroups();
            AddBillCommand = new MvxAsyncCommand(OnAddBill);//, CanAddBill);
            //SwipeTestCommand = new MvxCommand(OnTest);

            _backgroundHandler.RegisterMessage<ChangeBillMessage>(this, async x => await OnChangeBillMessage());
        }

        //private bool CanAddBill()
        //{
        //    return true;
        //}

        //public override void ViewAppearing()
        //{
        //    _ = UpdateUIControlAccess();
        //}

        private void OnTest()
        {
            throw new NotImplementedException();
        }

        private async Task OnAddBill()
        {
            //await _navService.Navigate<NavTestViewModel>();
            await _navService.Navigate<NewBillsViewModel, string>(string.Empty);
        }

        #endregion

        #region Methods
        private async Task GetGroups()
        {
            var billData = await _dataManager.GetUnpaidAndFutureBills(DateTime.Today, DateTime.Today.AddMonths(6));

            var data = billData.GroupBy(x => x.Date)
                        .OrderBy(x => x.Key)
                        .Select(grouped => new Grouping<DateTime, Bill>(grouped.Key, grouped)).ToList();

            var groupVM = new List<Grouping<DateTime, BillViewModel>>();
            foreach (var group in data)
            {
                var key = group.Key;
                var bvms = new List<BillViewModel>();
                foreach (var item in group.Grouped)
                {
                    bvms.Add(new BillViewModel(_navService, _backgroundHandler, _dataManager, item));
                }

                groupVM.Add(new Grouping<DateTime, BillViewModel>(key, bvms));
            }

            try
            {
                Bills = new ObservableCollection<Grouping<DateTime, BillViewModel>>(groupVM);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            

        }

        private async Task OnChangeBillMessage()
        {
            await GetGroups();

        }

        #endregion

    }
}

