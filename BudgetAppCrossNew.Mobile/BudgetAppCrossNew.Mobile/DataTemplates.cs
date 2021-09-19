using BudgetAppCross.Core.ViewModels;
using BudgetAppCross.Core.ViewModels.Pages;
using BudgetAppCross.Core.ViewModels.Root;
using BudgetAppCrossNew.Mobile.Views;
using BudgetAppCrossNew.Mobile.Pages;
using BudgetAppCrossNew.Mobile.Pages.Root;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCrossNew.Mobile
{
    public static class DataTemplates
    {
        private static readonly Dictionary<Type, Type> lookup = new Dictionary<Type, Type>
        {
            //Root
            {typeof(RootViewModel), typeof(RootPage) },
            {typeof(MenuViewModel), typeof(MenuPage) },

            //Nav Pages
            {typeof(WelcomeViewModel), typeof(WelcomePage) },
            {typeof(SettingsViewModel), typeof(SettingsPage) },
            {typeof(DateRangeViewModel), typeof(DateRangePage) },
            {typeof(BudgetListViewModel), typeof(PayeeListPage) },
            {typeof(AgendaViewModel), typeof(AgendaPage) },
            {typeof(BankOverviewViewModel), typeof(BankOverviewPage) },

            /*******Pages ****/
            //Add Item Pages
            {typeof(NewBudgetViewModel), typeof(NewBudgetPage) },
            {typeof(NewBillsViewModel), typeof(NewBillsPage) },
            {typeof(NavTestViewModel), typeof(NavTestPage  ) },
            {typeof(NewBankAccountViewModel), typeof(NewBankAccountPage) },
            {typeof(NewBalanceViewModel), typeof(NewBalancePage) },

            //Info Pages
            {typeof(BillTrackerViewModel), typeof(BillTrackerInfoPage) },
            {typeof(EditBillTrackerViewModel), typeof(BillTrackerDetailsPage) },
            {typeof(BankAccountViewModel), typeof(BankAccountPage) },

            {typeof(BudgetSelectViewModel), typeof(BudgetSelectPage) },


            //Views
            {typeof(AgendaBillView), typeof(AgendaBillView) },
            {typeof(NewMultiBillViewModel), typeof(NewMultiBillView) },
            {typeof(BudgetQuickViewModel), typeof(BudgetQuickStatusView) },
            {typeof(BillTrackerQuickViewModel), typeof(BillTrackerQuickView) },
            {typeof(BalanceViewModel), typeof(BalanceView) },
            {typeof(BillDetailsViewModel), typeof(BillDetailsPage) },

        };

        public static Dictionary<Type, Type> LookupDictionary => lookup;
    }
}
