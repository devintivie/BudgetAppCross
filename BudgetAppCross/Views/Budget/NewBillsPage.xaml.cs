using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (entry.Text.Equals("0"))
            {
                entry.Text = "";
            }
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            if (string.IsNullOrWhiteSpace(entry.Text))
            {
                entry.Text = "0";
            }
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