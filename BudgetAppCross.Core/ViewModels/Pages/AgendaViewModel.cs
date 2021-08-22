using BaseClasses;
using BaseViewModels;
using BudgetAppCross.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BudgetAppCross.Core.ViewModels.Pages
{
    public class AgendaViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        private bool scrolled = false;
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
        #endregion

        #region Constructors
        public AgendaViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, string name) : base(navService, backgroundHandler)
        {
            var getgroup = GetGroups();
            _backgroundHandler.RegisterMessage<ChangeBillMessage>(this, async x => await OnChangeBillMessage(x.AccountId));

            AddBillCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<NewBillsViewModel, string>(string.Empty));
        }
        #endregion

        #region Methods
        //public override void ViewDestroy(bool viewFinishing = true)
        //{
        //    base.ViewDestroy(viewFinishing);
        //    Messenger.Unregister(this);
        //}


        private async Task GetGroups()
        {
            //var tempBills = await BudgetDatabase.GetBills();
            //var allUnpaid = tempBills.Where(x => x.IsPaid == false || (x.Date >= DateTime.Today && x.Date <= DateTime.Today.AddMonths(1)));
            //var billData = allUnpaid.ToList();

            var billData = await BudgetDatabase.GetUnpaidAndFutureBills(DateTime.Today, DateTime.Today.AddMonths(6));

            var data = billData.GroupBy(x => x.Date)
                        .OrderBy(x => x.Key)
                        .Select(grouped => new Grouping<DateTime, Bill>(grouped.Key, grouped)).ToList();

            var groupVM = new List<Grouping<DateTime, BillViewModel>>();
            //var groupBefore = new List<Grouping<DateTime, BillViewModel>>();
            //var groupAfter = new List<Grouping<DateTime, BillViewModel>>();

            foreach (var group in data)
            {
                var key = group.Key;
                var bvms = new List<BillViewModel>();
                foreach (var item in group.Grouped)
                {
                    bvms.Add(new BillViewModel(item));
                }

                groupVM.Add(new Grouping<DateTime, BillViewModel>(key, bvms));
            }

            Bills = new ObservableCollection<Grouping<DateTime, BillViewModel>>(groupVM);

        }

        private async Task OnChangeBillMessage(int accountId)
        {
            Console.WriteLine("On change");
            await GetGroups();

        }

        //private async void SaveBills()
        //{
        //    foreach (var bill in BillsGrouped)
        //    {
        //        foreach (var item in bill.Bills)
        //        {
        //            await BudgetDatabase.SaveBill(item.Bill);
        //        }
        //    }
        //}


        //public override async void ViewDestroy(bool viewFinishing = true)
        //{
        //    base.ViewDestroy(viewFinishing);

        //    //BillManager.Update(billGroups);

        //    //await StateManager.SaveToFile();
        //}


        #endregion

    }
}

