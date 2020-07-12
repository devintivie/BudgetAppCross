using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using StoreKit;
using UIKit;

namespace BudgetAppCross.iOS
{
	public static class SKProductExtension
	{
		public static string LocalizedPrice(this SKProduct product)
		{
			var formatter = new NSNumberFormatter
			{
				FormatterBehavior = NSNumberFormatterBehavior.Version_10_4,
				NumberStyle = NSNumberFormatterStyle.Currency,
				Locale = product.PriceLocale,
			};

			string formattedString = formatter.StringFromNumber(product.Price);
			return formattedString;
		}
	}
}