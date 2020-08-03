using BudgetAppCross.Models;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class AgendaViewModel : BaseViewModel// MvxViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        private ObservableCollection<Grouping<DateTime, BillViewModel>> bills = new ObservableCollection<Grouping<DateTime, BillViewModel>>();
        public ObservableCollection<Grouping<DateTime, BillViewModel>> Bills
        {
            get { return bills; }
            set
            {
                SetProperty(ref bills, value);
            }
        }

        #endregion

        #region Commands
        public ICommand AddBillCommand { get; }
        #endregion

        #region Constructors
        public AgendaViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            Title = "Agenda";
            var getgroup = GetGroups();

            AddBillCommand = new Command(async () => await navigationService.Navigate<NewBillsViewModel, string>(string.Empty));
            Messenger.Register<ChangeBillMessage>(this, async x => await OnChangeBillMessage(x.AccountId));

            ////Entries = new ObservableCollection<AgendaBillViewModel>();
            //var data = (from bts in BillManager.AllTrackers
            //            from bill in bts.Bills
            //            select new AgendaBill
            //            {
            //                Company = bts.CompanyName,
            //                Amount = bill.Amount,
            //                Date = bill.Date,
            //                IsPaid = bill.IsPaid
            //            }).ToList();

            ////foreach (var bill in data)
            ////{
            ////    Entries.Add(new AgendaBillViewModel(bill));
            ////}

            //billGroups = (from entry in data
            //              orderby entry.Date
            //              group entry by entry.Date into agendaGroup
            //              select new Grouping<DateTime, AgendaBill>(agendaGroup.Key, agendaGroup)).ToList();

            //foreach (var group in billGroups)
            //{
            //    var dt = DateTime.Today.AddDays(-4);
            //    var dt2 = DateTime.Today.AddMonths(2);
            //    if(group.Key >= dt && group.Key <= dt2)
            //    {
            //        BillsGrouped.Add(new AgendaEntryViewModel(group));
            //    }

            //}


            //Console.WriteLine();

            /*
            //Works well but can't see any property changed from item template, trying viewmodel approach
            var data = (from bts in BillManager.AllTrackers
                       from bill in bts.Bills
                       select new AgendaBill
                       {
                           Company = bts.CompanyName,
                           Amount = bill.Amount,
                           Date = bill.Date,
                           IsPaid = bill.IsPaid
                       }).ToList();

            var grouped = from entry in data
                          orderby entry.Date
                          group entry by entry.Date into agendaGroup
                          select new Grouping<DateTime, AgendaBill>(agendaGroup.Key, agendaGroup);

            BillsGrouped = new ObservableCollection<Grouping<DateTime, AgendaBill>>(grouped);*/


        }




        #endregion

        #region Methods

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            var _ = SendScrollMessage();
        }

        public async Task SendScrollMessage()
        {
            await Task.Delay(200);
            Messenger.Send(new UpdateViewMessage());
        }

        //public override void ViewAppeared()
        //{
        //    base.ViewAppeared();
        //    LoadAgenda();

        //}

        //public override void ViewDestroy(bool viewFinishing = true)
        //{
        //    SaveBills();
        //    base.ViewDestroy(viewFinishing);

        //}

        private async Task GetGroups()
        {
            //var dt = DateTime.Today.AddDays(-4);
            //var dt2 = DateTime.Today.AddMonths(2);
            var bills = await BudgetDatabase.GetBills();
            var billData = bills.ToList();

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
                    bvms.Add(new BillViewModel(item));
                }

                groupVM.Add(new Grouping<DateTime, BillViewModel>(key, bvms));
            }

            Bills = new ObservableCollection<Grouping<DateTime, BillViewModel>>(groupVM);

            //await BudgetDatabase.UpdateBankAccountNames();
            //var dt = DateTime.Today.AddDays(-4);
            //var dt2 = DateTime.Today.AddMonths(2);
            //var bills = await BudgetDatabase.GetBills();
            //var data = (bills.GroupBy(x => x.Date)
            //            .OrderBy(x => x.Key)
            //            .Where(x => x.Key >= dt && x.Key <= dt2)
            //            .Select(groupedTable => new Grouping<DateTime, Bill>(groupedTable.Key, groupedTable))).ToList();

            //Bills = new ObservableCollection<Grouping<DateTime, Bill>>(data);
            //BillsGrouped.Clear();
            //foreach (var item in data)
            //{
            //    BillsGrouped.Add(new AgendaEntryViewModel(item));
            //}
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





//public AgendaViewModel(IMvxNavigationService navigation)
//{
//    navigationService = navigation;
//    Title = "Agenda";

//    Entries = new ObservableCollection<AgendaBillViewModel>();
//    var data = (from bts in BillManager.AllTrackers
//                from bill in bts.Bills
//                select new AgendaBill
//                {
//                    Company = bts.CompanyName,
//                    Amount = bill.Amount,
//                    Date = bill.Date,
//                    IsPaid = bill.IsPaid
//                }).ToList();

//    //foreach (var bill in data)
//    //{
//    //    Entries.Add(new AgendaBillViewModel(bill));
//    //}

//    var grouped = from entry in data
//                  orderby entry.Date
//                  group entry by entry.Date into agendaGroup
//                  select new Grouping<DateTime, AgendaBill>(agendaGroup.Key, agendaGroup);


//    Console.WriteLine();