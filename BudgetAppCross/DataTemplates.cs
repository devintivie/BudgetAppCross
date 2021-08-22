using BudgetAppCross.Core.ViewModels;
using BudgetAppCross.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross
{
    public static class DataTemplates
    {
        private static readonly Dictionary<Type, Type> lookup = new Dictionary<Type, Type>
        {
            {typeof(MainViewModel), typeof(MainPage) },
            {typeof(MenuViewModel), typeof(MenuPage) },
            {typeof(AgendaViewModel), typeof(AgendaPage) },
        };

        public static Dictionary<Type, Type> LookupDictionary => lookup;
    }
}
