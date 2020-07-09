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
    public partial class NewBankAccountPage
    {
        public NewBankAccountPage()
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
            //entry.Text = "0";
            //entry.CursorPosition = 0;
            //entry.SelectionLength = entry.Text.Length;
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            if (string.IsNullOrWhiteSpace(entry.Text))
            {
                entry.Text = "0";
            }
        }


    }
}