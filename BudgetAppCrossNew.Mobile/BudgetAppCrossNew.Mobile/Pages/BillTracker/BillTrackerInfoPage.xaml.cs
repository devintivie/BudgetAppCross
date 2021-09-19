using MvvmCross.Forms.Presenters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetAppCrossNew.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = false)]

    public partial class BillTrackerInfoPage// : ContentPage
    {
        public BillTrackerInfoPage()
        {
            InitializeComponent();
        }

        //private void BTItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    var m = ViewModel.GetType().GetMethod("ShowBill");
        //    m.Invoke(ViewModel, null);

        //    //ViewModel.ShowBillTracker(payee);
        //}

        //private void BudgetCompanyList_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    var payee = (string)e.Item.GetType().GetProperty("Payee").GetValue(e.Item);
        //    var m = ViewModel.GetType().GetMethod("ShowBillTracker");
        //    m.Invoke(ViewModel, new object[] { payee });

        //    //ViewModel.ShowBillTracker(payee);
        //}
    }
}