using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetAppCross.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewBillsPage
    {
        DateTime prevStart;
        DateTime prevEnd;
        public NewBillsPage()
        {
            InitializeComponent();
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            entry.CursorPosition = entry.Text.Length;
            if (entry.Text.Equals("0.00") || entry.Text.Equals("$0.00"))
            {
                entry.Text = "";
            }
            if (entry.Text.StartsWith("$"))
            {
                entry.Text = entry.Text.Replace("$", "");
            }

        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            var text = entry.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                entry.Text = "0";
            }
            else if(decimal.TryParse(text, out var result))
            {
                entry.Text = result.ToString("C");
            }
            //if (!entry.Text.StartsWith("$"))
            //{
            //    entry.Text = $@"${entry.Text}";
            //}
            //entry.Text = $@"${entry.Text}";
        }

        private void pickerStartDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            //if (pickerEndDate.Date < pickerStartDate.Date)
            //{
            //    pickerStartDate.Date = prevStart;
            //}

            //prevStart = pickerStartDate.Date;
        }

        private void pickerEndDate_DateSelected(object sender, DateChangedEventArgs e)
        {

            if(pickerEndDate.Date < pickerStartDate.Date)
            {
                pickerEndDate.Date = prevEnd;
            }

            prevEnd = pickerEndDate.Date;
        }

    }
}