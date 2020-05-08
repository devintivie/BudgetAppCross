using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class AgendaEntryViewModel : MvxViewModel
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

        private ObservableCollection<AgendaBillViewModel> bills = new ObservableCollection<AgendaBillViewModel>();
        public ObservableCollection<AgendaBillViewModel> Bills
        {
            get { return bills; }
            set
            {
                SetProperty(ref bills, value);
            }
        }

        private double dateTotal;
        public double DateTotal
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
        public AgendaEntryViewModel(Grouping<DateTime, AgendaBill> datagroup )
        {
            Date = datagroup.Key;
            foreach (var item in datagroup)
            {
                Bills.Add(new AgendaBillViewModel(item));
            }

            MessagingCenter.Subscribe<AgendaBillViewModel>(this, "UpdateTotal", async (obj) => OnUpdateTotal());

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
            var total = 0.0;
            foreach (var item in Bills)
            {
                total += item.AmountDue;
            }

            DateTotal = total;
        }


        #endregion

    }
}