using BudgetAppCross.Core.ViewModels;
using BudgetAppCross.Core.ViewModels.Pages;
using BudgetAppCross.Core.ViewModels.Root;
using BudgetAppCrossNew.Mobile.Views;
using BudgetAppCrossNew.Mobile.Views.Pages;
using BudgetAppCrossNew.Mobile.Views.Root;
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

            //NAv Pages
            {typeof(AgendaViewModel), typeof(AgendaPage) },
            {typeof(SettingsViewModel), typeof(SettingsPage) },
            {typeof(WelcomeViewModel), typeof(WelcomePage) },

            //Pages
            {typeof(NewBudgetViewModel), typeof(NewBudgetPage) },
            {typeof(NewBillsViewModel), typeof(NewBillsPage) },

            //Views
            {typeof(AgendaBillView), typeof(AgendaBillView) },
            {typeof(NewMultiBillViewModel), typeof(NewMultiBillView) },
            {typeof(BudgetQuickViewModel), typeof(BudgetQuickStatusView) },

        };

        public static Dictionary<Type, Type> LookupDictionary => lookup;
    }
}
