using BudgetAppCross.Core.ViewModels;
using BudgetAppCross.Core.ViewModels.Root;
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
            {typeof(RootViewModel), typeof(RootPage) },
            {typeof(MenuViewModel), typeof(MenuPage) },
            {typeof(AgendaViewModel), typeof(AgendaPage) },
        };

        public static Dictionary<Type, Type> LookupDictionary => lookup;
    }
}
