using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetAppCross.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillDetailsPage
    {
        DateTime prevEnd;
        public BillDetailsPage()
        {
            InitializeComponent();
        }
    }
}