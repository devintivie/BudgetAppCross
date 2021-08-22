using BaseViewModels;
using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BudgetAppCross.Core.ViewModels
{
    public class AgendaEntryViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        #endregion

        #region Properties
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                SetProperty(ref date, value);
            }
        }

        private ObservableCollection<BillViewModel> bills = new ObservableCollection<BillViewModel>();
        public ObservableCollection<BillViewModel> Bills
        {
            get { return bills; }
            set
            {
                SetProperty(ref bills, value);
            }
        }

        private decimal dateTotal;
        public decimal DateTotal
        {
            get { return dateTotal; }
            set
            {
                SetProperty(ref dateTotal, value);
            }
        }


        #endregion

        #region Commands

        #endregion

        #region Constructors
        //public AgendaEntryViewModel(Grouping<DateTime, Bill> datagroup )
        //{
        //    Date = datagroup.Key;
        //    foreach (var item in datagroup)
        //    {
        //        Bills.Add(new BillViewModel(item));
        //    }

        //    MessagingCenter.Subscribe<AgendaBillViewModel>(this, "UpdateTotal", async (obj) => OnUpdateTotal());

        //    //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
        //    //{
        //    //    var newItem = item as Item;
        //    //    Items.Add(newItem);
        //    //    await DataStore.AddItemAsync(newItem);
        //    //});
        //}

        public AgendaEntryViewModel(Grouping<DateTime, Bill> datagroup)
        {
            Date = datagroup.Key;
            foreach (var item in datagroup)
            {
                Bills.Add(new BillViewModel(item));
            }

            //MessagingCenter.Subscribe<AgendaBillViewModel>(this, "UpdateTotal", async (obj) => OnUpdateTotal());

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Items.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        #endregion

        #region Methods

        public override void ViewAppeared()
        {
            base.ViewAppeared();
        }


        private void OnUpdateTotal()
        {
            var total = 0.0m;
            foreach (var item in Bills)
            {
                total += item.Amount;
            }

            DateTotal = total;
        }


        #endregion

    }
}