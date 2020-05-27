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
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true)]
    public partial class BankOverviewPage
    {
        public BankOverviewPage()
        {
            InitializeComponent();
        }

        private void BankAccount_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var acct = (BankAccount)e.Item.GetType().GetProperty("BankAccount").GetValue(e.Item);
            ViewModel.ShowBankAccount(acct);
        }
    }
}