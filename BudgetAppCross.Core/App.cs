using Acr.UserDialogs;
using BaseClasses;
using BaseClasses.Configurations.Local;
using BudgetAppCross.Configurations;
using BudgetAppCross.Core.ViewModels.Root;
using BudgetAppCross.DataAccess;
using BudgetAppCross.SQLiteDataAccess;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace BudgetAppCross.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMessenger, Messenger>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ILogManager, LogManager>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<INotificationHandler, SimpleNotificationService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IBackgroundHandler, SimpleBackgroundHandler>();

            var configFileExt = ".db3";
            Mvx.IoCProvider.RegisterSingleton<ISettingsManager>(() => 
                new SettingsManager(ApplicationPlatform.Xamarin, Mvx.IoCProvider.GetSingleton<IBackgroundHandler>(), configFileExt));
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IConfigManager<SQLiteConfiguration>, SQLiteConfigurationManager>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IDataManager, SQLiteBudgetDatabase>();
            
            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            
            RegisterAppStart<RootViewModel>();
        }

        
    }
}
