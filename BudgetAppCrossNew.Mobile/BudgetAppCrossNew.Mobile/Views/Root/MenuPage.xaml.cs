using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetAppCrossNew.Mobile.Views.Root
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Master, WrapInNavigationPage = false, Title = "Main")]
    public partial class MenuPage : MvxContentPage
    {
        public MenuPage()
        {

            var tmp = Application.Current.Resources;
            InitializeComponent();
        }

        private void MenuList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tmp = Application.Current.MainPage;
            if (Application.Current.MainPage is FlyoutPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
            }
            else if (Application.Current.MainPage is NavigationPage navigationPage
                && navigationPage.CurrentPage is FlyoutPage nestedMasterDetail)
            {
                nestedMasterDetail.IsPresented = false;
            }

        }

        //private void ListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        //{
        //    MenuList.SelectedItem = null;
        //}
    }
}