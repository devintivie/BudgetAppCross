using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using BudgetAppCross.Services;
//using BudgetAppCross.Views;

namespace BudgetAppCross
{
    public partial class App : Application
    {

        public App()
        {

            Device.SetFlags(new string[] { "AppTheme_Experimental" });

            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
