using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Ios.Core;
using Serilog;
using Serilog.Extensions.Logging;
using UIKit;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.IoC;

namespace BudgetAppCross.iOS
{
    public class Setup : MvxFormsIosSetup<Core.App, App>
    {
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