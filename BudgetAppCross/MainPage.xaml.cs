using BudgetAppCross.Models;
using MvvmCross.Forms.Views;
using MvvmCross.Forms.Presenters.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace BudgetAppCross
{
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Root, WrapInNavigationPage = false, Title = "MainPAge")]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
    //// Learn more about making custom code visible in the Xamarin.Forms previewer
    //// by visiting https://aka.ms/xamarinforms-previewer
    //[DesignTimeVisible(false)]
    //public partial class MainPage
    //{
    //    public MainPage()
    //    {
    //        InitializeComponent();
    //    }

    //    private void ContactSelected(object sender, SelectedItemChangedEventArgs e)
    //    {
    //        ViewModel.ShowContactDetails(e.SelectedItem as Contact);
    //        //ViewModel.ShowContactDetails(e.SelectedItem as Contact);
    //    }
    //}
}




//// Learn more about making custom code visible in the Xamarin.Forms previewer
//// by visiting https://aka.ms/xamarinforms-previewer
//[DesignTimeVisible(false)]
//public partial class MainPage// : MasterDetailPage
//{
//    //Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
//    public MainPage()
//    {
//        InitializeComponent();

//        //MasterBehavior = MasterBehavior.Popover;

//        //MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
//    }

//    private void ContactSelected(object sender, SelectedItemChangedEventArgs e)
//    {
//        //ViewModel.ShowContactDetails(e.SelectedItem as Contact);
//    }

//    /*
//    public async Task NavigateFromMenu(int id)
//    {
//        if (!MenuPages.ContainsKey(id))
//        {
//            switch (id)
//            {
//                case (int)MenuItemType.Browse:
//                    MenuPages.Add(id, new NavigationPage(new ItemsPage()));
//                    break;
//                case (int)MenuItemType.About:
//                    MenuPages.Add(id, new NavigationPage(new AboutPage()));
//                    break;
//            }
//        }

//        var newPage = MenuPages[id];

//        if (newPage != null && Detail != newPage)
//        {
//            Detail = newPage;

//            if (Device.RuntimePlatform == Device.Android)
//                await Task.Delay(100);

//            IsPresented = false;
//        }
//    }
//    */
//}