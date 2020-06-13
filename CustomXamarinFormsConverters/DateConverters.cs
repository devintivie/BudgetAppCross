using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomXamarinFormsConverters
{
    public class DateOnlyConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.ToShortDateString();
            //var dollarAmount = (double)value;
            //return dollarAmount.ToString("C", CultureInfo.CurrentCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            //var strValue = Regex.Replace(value.ToString(), @"\D", "");

            //if (strValue.Length <= 0)
            //{
            //    return 0m;
            //}

            //if(!long.TryParse(strValue, out var valueLong))
            //{
            //    return 0m;
            //}
            //if(valueLong <= 0)
            //{
            //    return 0m;
            //}

            //return valueLong / 100m;



        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
