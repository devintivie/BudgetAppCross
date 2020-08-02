//using BudgetAppCross.Models;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

using BudgetAppCross.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace BudgetAppCross.Views
{
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Master, WrapInNavigationPage = false) ]
    public partial class MenuPage// : MvxContentPage<MenuViewModel>
    {
        
        public MenuPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            MenuList.SelectedItem = null;
        }
    }
}
//    // Learn more about making custom code visible in the Xamarin.Forms previewer
//    // by visiting https://aka.ms/xamarinforms-previewer
//    [DesignTimeVisible(false)]
//    public partial class MenuPage : ContentPage
//    {
//        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
//        List<HomeMenuItem> menuItems;
//        public MenuPage()
//        {
//            InitializeComponent();

//            menuItems = new List<HomeMenuItem>
//            {
//                new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
//                new HomeMenuItem {Id = MenuItemType.About, Title="About" }
//            };

//            ListViewMenu.ItemsSource = menuItems;

//            ListViewMenu.SelectedItem = menuItems[0];
//            ListViewMenu.ItemSelected += async (sender, e) =>
//            {
//                if (e.SelectedItem == null)
//                    return;

//                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
//                await RootPage.NavigateFromMenu(id);
//            };
//        }
//    }
//}