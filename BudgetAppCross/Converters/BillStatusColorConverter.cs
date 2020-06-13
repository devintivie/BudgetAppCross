using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetAppCross.Converters
{
    public class BillStatusColorConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((BillStatus)value)
            {
                case BillStatus.Paid:
                case BillStatus.AutoPayPast:
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
                case BillStatus.NoneDue:
                    return Color.LightGreen;
                case BillStatus.AutoPayUpcoming:
                    return Color.DarkBlue;
                case BillStatus.DueInOverOneMonth:
                default:
                    return Color.Gray;
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
