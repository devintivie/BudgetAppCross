using MvvmCross.Forms.Presenters.Attributes;
using BudgetAppCross.Models;
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
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true)]

    public partial class PayeeListPage
    {
        public PayeeListPage()
        {
            InitializeComponent();
        }

        //private void BudgetCompanyList_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    var payee = (string)e.Item.GetType().GetProperty("Payee").GetValue(e.Item);
        //    var m = ViewModel.GetType().GetMethod("ShowBillTracker");
        //    m.Invoke(ViewModel, new object[] { payee });

        //    //ViewModel.ShowBillTracker(payee);
        //}
    }
}