using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
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
        public CurrencyEntryRenderer(Context ctx) : base(ctx)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.White);
                Control.SetBackground(gd);

                var view = (CurrencyEntry)Element;
                Control.ViewAttachedToWindow += Control_ViewAttachedToWindow;
                Control.FocusChange += Control_FocusChange;
                Control.LayoutChange += Control_LayoutChange;
                Control.EditorAction += Control_EditorAction;
                view.Placeholder = Control.Hint;
                if (Control.Hint == null)
                {
                    Control.Hint = $"$0.00";
                }
                //Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                //Control.SetHintTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Green));
            }
        }

        private void Control_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            Console.WriteLine("Control_EditorAction");
        }

        private void Control_LayoutChange(object sender, LayoutChangeEventArgs e)
        {
            Console.WriteLine("layout changed");
        }

        private void Control_ViewAttachedToWindow(object sender, ViewAttachedToWindowEventArgs e)
        {
            var entry = sender as EditText;

            if (entry.Text.StartsWith("$"))
            {
                entry.Text = entry.Text.Replace("$", "");
            }
        }

        private void Control_FocusChange(object sender, FocusChangeEventArgs e)
        {
            var entry = sender as EditText;
            var text = entry.Text;
            if (text.Equals("0.00") || text.Equals("$0.00") || text.Equals("0"))
            {
                entry.Text = "";
            }
            var array = text.ToCharArray();
            var dotCount = array.Count(c => c == '.');
            var lastIndex = text.LastIndexOf('.');
            if (dotCount > 1)
            {

                entry.Text = text.Remove(lastIndex, text.Length - lastIndex);
            }
            else if (dotCount == 1)
            {
                Console.WriteLine($"text.length = {text.Length}");
                Console.WriteLine($"lastIndex = {lastIndex}");
                Console.WriteLine();
                if ((text.Length - lastIndex) > 3)
                {
                    entry.Text = text.Substring(0, lastIndex + 3);
                }
            }
        }



    }
}