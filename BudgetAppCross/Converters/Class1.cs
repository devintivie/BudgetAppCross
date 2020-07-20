using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetAppCross.Converters
{
    public class Class1 : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((BillStatus)value)
            {
                case BillStatus.Paid:
                    return Color.LightGreen;
                case BillStatus.PastDue:
                    return Color.Red;
                case BillStatus.DueToday:
                    return Color.Red;
                case BillStatus.DueTomorrow:
                    return Color.Red;
                case BillStatus.DueWithinTwoWeeks:
                    return Color.Orange;
                case BillStatus.DueWithinOneMonth:
                    return Color.Yellow;
                case BillStatus.DueInOverOneMonth:
                    return Color.Transparent;
                case BillStatus.NoneDue:
                    return Color.LightGreen;
                //case BillStatus.AutoPay:
                //    return Color.DarkBlue;
                default:
                    return Color.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
