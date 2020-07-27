using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
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
            if(Control != null)
            {
                var view = (CurrencyEntry)Element;
                view.Keyboard = Keyboard.Numeric;
                var text = Control.Text;
                Console.WriteLine(Control.PaddingBottom);
                Console.WriteLine(Control.PaddingEnd);
                Console.WriteLine(Control.PaddingLeft);
                Console.WriteLine(Control.PaddingRight);
                Console.WriteLine(Control.PaddingStart);
                Console.WriteLine(Control.PaddingTop);
                //Control.SetPadding()

                Control.SetPadding(8, 8, 8, 8);

                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.White);
                Control.SetBackground(gd);

                if (text.Equals("0.00") || text.Equals("$0.00") || text.Equals("0"))
                {
                    Control.Text = "";
                }
                else if (decimal.TryParse(text, out var result))
                {
                    var temp = Math.Truncate(100 * result) / 100;
                    Control.Text = temp.ToString("C");
                }

                if (Control.Hint == null)
                {
                    Control.Hint = $"$0.00";
                }

                Control.FocusChange += Control_FocusChange;


            }
            //if (Control != null)
            //{
            //    GradientDrawable gd = new GradientDrawable();
            //    gd.SetColor(Android.Graphics.Color.White);
            //    Control.SetBackground(gd);

            //    var view = (CurrencyEntry)Element;
            //    ////Control.ViewAttachedToWindow += Control_ViewAttachedToWindow;
            //    //Control.TextChanged += Control_TextChanged;
            //    Control.FocusChange += Control_FocusChange;

            //    //view.Placeholder = Control.Hint;
            //    //var text = Control.Text;
            //    //if (text.Equals("0.00") || text.Equals("$0.00") || text.Equals("0"))
            //    //{
            //    //    Control.Text = "";
            //    //}
            //    //else if (decimal.TryParse(text, out var result))
            //    //{
            //    //    var temp = Math.Truncate(100 * result) / 100;
            //    //    Control.Text = temp.ToString("C");
            //    //}
            //    //if (Control.Hint == null)
            //    //{
            //    //    Control.Hint = $"$0.00";
            //    //}
            //    //Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
            //    //Control.SetHintTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Green));
            //    view.Keyboard = Keyboard.Numeric;
            //}
        }


        private void Control_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            //var entry = sender as EditText;
            //var text = entry.Text;
            //if (text.Equals("0.00") || text.Equals("$0.00") || text.Equals("0"))
            //{
            //    entry.Text = "";
            //}
            //var array = text.ToCharArray();
            //var dotCount = array.Count(c => c == '.');
            //var lastIndex = text.LastIndexOf('.');
            //if (dotCount > 1)
            //{

            //    entry.Text = text.Remove(lastIndex, text.Length - lastIndex);
            //}
            //else if (dotCount == 1)
            //{
            //    Console.WriteLine($"text.length = {text.Length}");
            //    Console.WriteLine($"lastIndex = {lastIndex}");
            //    Console.WriteLine();
            //    if ((text.Length - lastIndex) > 3)
            //    {
            //        entry.Text = text.Substring(0, lastIndex + 3);
            //    }
            //}
        }

      
        //private void Control_ViewAttachedToWindow(object sender, ViewAttachedToWindowEventArgs e)
        //{
        //    var entry = sender as EditText;

        //    if (entry.Text.StartsWith("$"))
        //    {
        //        entry.Text = entry.Text.Replace("$", "");
        //    }
        //}

        private void Control_FocusChange(object sender, FocusChangeEventArgs e)
        {
            if (!e.HasFocus)
            {
                var entry = sender as EditText;
                var text = entry.Text;
                if (string.IsNullOrWhiteSpace(text))
                {
                    entry.Text = "0";
                }
                else if (decimal.TryParse(text, out var result))
                {
                    var temp = Math.Truncate(100 * result) / 100;
                    entry.Text = temp.ToString("C");
                }
            }
            else
            {
                var entry = sender as EditText;
                var text = entry.Text;
                if (entry.Text.StartsWith("$"))
                {
                    entry.Text = entry.Text.Replace("$", "");
                }
                InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
                imm.ShowSoftInput(Control.FindFocus(), ShowFlags.Implicit);

                entry.SetSelection(entry.Text.Length);

            }
            //if (!e.HasFocus)
            //{
            //    var entry = sender as EditText;
            //    var text = entry.Text;
            //    if (entry.Text.StartsWith("$"))
            //    {
            //        entry.Text = entry.Text.Replace("$", "");
            //    }

            //    if (string.IsNullOrWhiteSpace(text))
            //    {
            //        entry.Text = "0";
            //    }
            //    else if (decimal.TryParse(text, out var result))
            //    {
            //        var temp = Math.Truncate(100 * result) / 100;
            //        entry.Text = temp.ToString("C");
            //    }
            //}
            //else
            //{
            //    var entry = sender as EditText;
            //    var text = entry.Text;
            //    if (entry.Text.StartsWith("$"))
            //    {
            //        entry.Text = entry.Text.Replace("$", "");
            //    }

            //    if (text.Equals("0.00") || text.Equals("$0.00") || text.Equals("0"))
            //    {
            //        entry.Text = "";
            //    }


            //    var array = text.ToCharArray();
            //    var dotCount = array.Count(c => c == '.');
            //    var lastIndex = text.LastIndexOf('.');
            //    if (dotCount > 1)
            //    {
            //        entry.Text = text.Remove(lastIndex, text.Length - lastIndex);
            //    }
            //    else if (dotCount == 1)
            //    {
            //        Console.WriteLine($"text.length = {text.Length}");
            //        Console.WriteLine($"lastIndex = {lastIndex}");
            //        Console.WriteLine();
            //        if ((text.Length - lastIndex) > 3)
            //        {
            //            entry.Text = text.Substring(0, lastIndex + 3);
            //        }
            //    }

            //    entry.SetSelection(entry.Text.Length);
            //}
        }
    }
}