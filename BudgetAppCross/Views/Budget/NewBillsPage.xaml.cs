using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetAppCross.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewBillsPage
    {
        DateTime prevEnd;
        public NewBillsPage()
        {
            InitializeComponent();
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            entry.CursorPosition = entry.Text.Length;
            //if (entry.Text.Equals("0.00") || entry.Text.Equals("$0.00"))
            //{
            //    entry.Text = "";
            //}
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
            else if (decimal.TryParse(text, out var result))
            {
                var temp = Math.Truncate(100 * result) / 100;
                entry.Text = temp.ToString("C");
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

            if (pickerEndDate.Date < pickerStartDate.Date)
            {
                pickerEndDate.Date = prevEnd;
            }

            prevEnd = pickerEndDate.Date;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
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
            //else if(text.Length > )
            //{

            //}
        }

        //private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var entry = sender as Entry;
        //    var text = entry.Text;

        //    var value = Regex.Replace(text, @"\..*?(\.)", "");
        //    Console.WriteLine(value);
        //    entry.Text = value;

        //}
    }
}