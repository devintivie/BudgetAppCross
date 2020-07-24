using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BudgetAppCross.Droid.CustomControls;
using BudgetAppCross.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CurrencyEntry), typeof(CurrencyEntryRenderer))]
namespace BudgetAppCross.Droid.CustomControls
{
    public class CurrencyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
        }

        public CurrencyEntryRenderer(Context ctx) : base(ctx)
        {

        }
    }
}