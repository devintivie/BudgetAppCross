using BudgetAppCrossNew.Mobile.Views;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
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

    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true, Title = "Agenda Control")]

    public partial class AgendaPage
    {
        public AgendaPage()
        {
            InitializeComponent();
        }

        //private void AgendaBillView_Swipe(object sender, SwipedEventArgs e)
        //{
        //    var view = (AgendaBillView)sender;
        //    switch (e.Direction)
        //    {
        //        case SwipeDirection.Right:
        //            view.TestVisible = false;
        //            break;
        //        case SwipeDirection.Left:
        //            view.TestVisible = true;
        //            break;
        //        case SwipeDirection.Up:
        //            break;
        //        case SwipeDirection.Down:
        //            break;
        //        default:
        //            break;
        //    }
            
        //}
    }
}