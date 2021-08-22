using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BudgetAppCross.Droid
{
    //[Activity(Label = "BudgetAppCross", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [Activity(Label = "WineFridgeApp.Droid", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]

    public class RootActivity : MvxFormsAppCompatActivity<Setup, Core.App, App>
    {
        readonly string[] Permissions =
        {
          //Manifest.Permission.AccessFineLocation
        };

        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            RequestPermissionsManually();
            UserDialogs.Init(this);
            //Mvx.IoCProvider.RegisterType<IWifiHelper, GetSSIDAndroid>();

        }
        //protected override void OnCreate(Bundle bundle)
        //{
        //    TabLayoutResource = Resource.Layout.Tabbar;
        //    ToolbarResource = Resource.Layout.Toolbar;
        //    base.OnCreate(bundle);

        //    UserDialogs.Init(this);
        //}

        private void RequestPermissionsManually()
        {
            List<string> requests = new List<string>();
            foreach (var item in Permissions)
            {
                try
                {
                    if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                    {
                        requests.Add(item);
                    }
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