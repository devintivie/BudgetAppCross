using BudgetAppCross.Models;
using MvvmCross.Forms.Presenters.Attributes;
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
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true, Title = "Bill List Page")]
    public partial class BudgetListPage
    {
        public BudgetListPage()
        {
            InitializeComponent();
            
        }
        private void BTItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //ViewModel.ShowBillTracker(e.SelectedItem as BillTracker);
        }

        private void BTItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.ShowBillTracker(e.Item as BillTracker);
        }
    }
}