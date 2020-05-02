using MvvmCross.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BudgetAppCross.Core
{
    public sealed class PageNameToStringConverter : MvxValueConverter<NavigablePage, string>
    {
        protected override string Convert(NavigablePage value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case NavigablePage.BillList:
                    return "Bill list dude";
                case NavigablePage.About:
                    return value.ToString();
                default:
                    break;
            }
            throw new InvalidOperationException(string.Format("Can't convert Incident.Agent from {0}", value));
        }

        protected override NavigablePage ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return base.ConvertBack(value, targetType, parameter, culture);
        }

        //protected override Incident.Agent ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    switch (value)
        //    {
        //        case "Mine":
        //            return Incident.Agent.Mine;
        //        case "UXO":
        //            return Incident.Agent.Uxo;
        //        case "Other":
        //            return Incident.Agent.Other;
        //    }
        //    throw new InvalidOperationException(string.Format("Can't convert Incident.Agent from '{0}'", value));
        //}
    }
}
