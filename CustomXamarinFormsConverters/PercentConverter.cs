using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomXamarinFormsConverters
{
    public class PercentConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dollarAmount = (decimal)value;
            return $"{dollarAmount}%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = Regex.Replace(value.ToString(), @"\D", "");

            if (strValue.Length <= 0)
            {
                return 0m;
            }

            if (!long.TryParse(strValue, out var valueLong))
            {
                return 0m;
            }
            if (valueLong <= 0)
            {
                return 0m;
            }

            if (valueLong > 1)
            {
                return valueLong / 10000m;
            }

            return valueLong / 100m;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
