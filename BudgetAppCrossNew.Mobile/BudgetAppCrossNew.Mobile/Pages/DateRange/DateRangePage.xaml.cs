using MvvmCross.Forms.Presenters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetAppCrossNew.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true)]

    public partial class DateRangePage
    {
        public DateRangePage()
        {
            InitializeComponent();
        }
    }
}