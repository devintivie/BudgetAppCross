using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Extensions.Logging;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BudgetAppCrossNew.Mobile.Droid
{
    public class Setup : MvxFormsAndroidSetup<BudgetAppCross.Core.App, App>
    {
        public Setup()
        {
        }

        protected override ILoggerProvider CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }

        protected override ILoggerFactory CreateLogFactory()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }

        protected override IDictionary<Type, Type> InitializeLookupDictionary(IMvxIoCProvider iocProvider)
        {
            return DataTemplates.LookupDictionary;
            //var tmp = new Dictionary<Type, Type>()
            //{
            //    {typeof(MainViewModel), typeof(MainView) },
            //    {typeof(MenuViewModel), typeof(MenuPage) },
            //    {typeof(BTTestViewModel), typeof(BTTestPage) }
            //};
            //return tmp;
        }
    }
}