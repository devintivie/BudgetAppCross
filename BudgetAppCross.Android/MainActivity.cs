﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.Forms.Platforms.Android.Core;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Platforms.Android;

namespace BudgetAppCross.Droid
{
    [Activity(Label = "BudgetAppCross", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class RootActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<Core.App, App>, Core.App, App>
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);

            UserDialogs.Init(this);
        }
    }
    //public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    //{
    //    protected override void OnCreate(Bundle savedInstanceState)
    //    {
    //        TabLayoutResource = Resource.Layout.Tabbar;
    //        ToolbarResource = Resource.Layout.Toolbar;

    //        base.OnCreate(savedInstanceState);

    //        Xamarin.Essentials.Platform.Init(this, savedInstanceState);
    //        global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
    //        LoadApplication(new App());
    //    }
    //    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
    //    {
    //        Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

    //        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    //    }
    //}
}