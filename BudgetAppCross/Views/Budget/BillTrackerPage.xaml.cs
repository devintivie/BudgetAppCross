﻿using MvvmCross.Forms.Presenters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetAppCross.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = false, Title = "Bill Tracker")]
    public partial class BillTrackerPage
    {
        public BillTrackerPage()
        {
            InitializeComponent();
        }

        private void BTItemTapped(object sender, ItemTappedEventArgs e)
        {
            //var payee = (string)e.Item.GetType().GetProperty("Payee").GetValue(e.Item);
            ViewModel.ShowBill();
        }

        //private void BTItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    var id = e.Item.GetType().GetProperty("ID").GetValue(e.Item);
        //    ViewModel.ShowBill(id);
        //}
    }
}