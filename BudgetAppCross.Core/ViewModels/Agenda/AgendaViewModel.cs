using BudgetAppCross.Models;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BudgetAppCross.Core.ViewModels
{
    public class AgendaViewModel : BaseViewModel// MvxViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        //private ObservableCollection<Grouping<DateTime, AgendaBill>> billsGrouped;
        //public ObservableCollection<Grouping<DateTime, AgendaBill>> BillsGrouped
        //{
        //    get { return billsGrouped; }
        //    set
        //    {
        //        SetProperty(ref billsGrouped, value);
        //    }
        //}

        //private ObservableCollection<AgendaBillViewModel> entries;
        //public ObservableCollection<AgendaBillViewModel> Entries
        //{
        //    get { return entries; }
        //    set
        //    {
        //        SetProperty(ref entries, value);
        //    }
        //}

        private ObservableCollection<AgendaEntryViewModel> billsGrouped = new ObservableCollection<AgendaEntryViewModel>();
        public ObservableCollection<AgendaEntryViewModel> BillsGrouped
        {
            get { return billsGrouped; }
            set
            {
                SetProperty(ref billsGrouped, value);
            }
        }

        private List<Grouping<DateTime, AgendaBill>> billGroups = new List<Grouping<DateTime, AgendaBill>>();

        #endregion

        #region Commands
        #endregion

        #region Constructors
        public AgendaViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            Title = "Agenda";

            //Entries = new ObservableCollection<AgendaBillViewModel>();
            var data = (from bts in BillManager.AllTrackers
                        from bill in bts.Bills
                        select new AgendaBill
                        {
                            Company = bts.CompanyName,
                            Amount = bill.Amount,
                            Date = bill.Date,
                            IsPaid = bill.IsPaid
                        }).ToList();

            //foreach (var bill in data)
            //{
            //    Entries.Add(new AgendaBillViewModel(bill));
            //}

            billGroups = (from entry in data
                          orderby entry.Date
                          group entry by entry.Date into agendaGroup
                          select new Grouping<DateTime, AgendaBill>(agendaGroup.Key, agendaGroup)).ToList();

            foreach (var group in billGroups)
            {
                var dt = DateTime.Today.AddDays(-4);
                var dt2 = DateTime.Today.AddMonths(2);
                if(group.Key >= dt && group.Key <= dt2)
                {
                    BillsGrouped.Add(new AgendaEntryViewModel(group));
                }
                
            }


            Console.WriteLine();

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

        public override void ViewAppeared()
        {
            base.ViewAppeared();


        }

        public override async void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);

            BillManager.Update(billGroups);

            await StateManager.SaveToFile();
        }


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