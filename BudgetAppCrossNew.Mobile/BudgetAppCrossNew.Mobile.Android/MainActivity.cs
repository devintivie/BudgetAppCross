using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using MvvmCross;
//using Android.OS;
//using Android.Runtime;
//using AndroidX.Core.Content;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BudgetAppCrossNew.Mobile.Droid
{
    [Activity(Label = "WineFridgeApp.Droid", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]

    public class SplashScreen : MvxFormsAppCompatActivity<Setup, BudgetAppCross.Core.App, App>
    {
        readonly string[] Permissions =
        {
          //Manifest.Permission.AccessFineLocation
        };

        const int RequestLocationId = 0;
        public SplashScreen()
        {
        }
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            UserDialogs.Init(this);
            base.OnCreate(bundle);


            //RequestPermissionsManually();
            //Mvx.IoCProvider.RegisterType<IWifiHelper, GetSSIDAndroid>();

        }

        private void RequestPermissionsManually()
        {
            List<string> requests = new List<string>();
            foreach (var item in Permissions)
            {
                try
                {
                    //if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                    //{
                    //    requests.Add(item);
                    //}
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            }

            if (requests.Count > 0)
            {
                string[] array = requests.ToArray();
                RequestPermissions(array, array.Length);
            }

        }
    }
}