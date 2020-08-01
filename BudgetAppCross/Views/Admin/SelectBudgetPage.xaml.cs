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
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = false, Title = "Select Budget", WrapInNavigationPage =true)]
    public partial class SelectBudgetPage
    {
        public SelectBudgetPage()
        {
            InitializeComponent();
        }

        private void mainListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            mainListView.SelectedItem = null;
        }
    }
}