﻿using BudgetAppCross.iOS.CustomControls;
using BudgetAppCross.Views;
using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(CurrencyEntry), typeof(CurrencyEntryRenderer))]
namespace BudgetAppCross.iOS.CustomControls
{
    
    public class CurrencyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.Started += Control_Started;
                Control.ValueChanged += Control_ValueChanged;
                Control.EditingChanged += Control_EditingChanged;
                Control.Ended += Control_Ended;
                var text = Control.Text;
                if (text.Equals("0.00") || text.Equals("$0.00") || text.Equals("0"))
                {
                    Control.Text = "";
                }else 
                if (decimal.TryParse(text, out var result))
                {
                    var temp = Math.Truncate(100 * result) / 100;
                    Control.Text = temp.ToString("C");
                }
                if (Control.Placeholder == null)
                {
                    Control.Placeholder = $"$0.00";
                }
                Control.KeyboardType = UIKeyboardType.DecimalPad;
                Control.ClearButtonMode = UITextFieldViewMode.WhileEditing;
                
                //Control.AllEvents += Control_AllEvents;

                //Control.AllTouchEvents += Control_AllTouchEvents;
                //Control.AllEditingEvents += Control_AllEditingEvents;
            }
        }

        private void Control_EditingChanged(object sender, EventArgs e)
        {
            var entry = sender as UITextField;
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

        private void Control_Ended(object sender, EventArgs e)
        {
            var entry = sender as UITextField;
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

        //private void Control_AllEvents(object sender, EventArgs e)
        //{
        //    var entry = sender as UITextField;

        //    var text = entry.Text;
        //    if (text.Equals("0.00") || text.Equals("$0.00") || text.Equals("0"))
        //    {
        //        entry.Text = "";
        //    }
        //}

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            var entry = sender as UITextField;
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

        private void Control_Started(object sender, EventArgs e)
        {
            var entry = sender as UITextField;

            if (entry.Text.StartsWith("$"))
            {
                entry.Text = entry.Text.Replace("$", "");
            }
        }

        //private void Control_AllEditingEvents(object sender, System.EventArgs e)
        //{
        //    var entry = sender as UITextField;
        //}

        //private void Control_AllTouchEvents(object sender, System.EventArgs e)
        //{
        //    var entry = sender as UITextField;
        //}
    }


}