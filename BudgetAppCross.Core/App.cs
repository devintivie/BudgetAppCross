using Acr.UserDialogs;
using BudgetAppCross.Core.Services;
using BudgetAppCross.Core.ViewModels;
using BudgetAppCross.DataAccess;
<<<<<<< HEAD
using BudgetAppCross.SqliteDataAccess;
=======
using BudgetAppCross.SQLiteDataAccess;
>>>>>>> feature/ModifySQLite
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System;

namespace BudgetAppCross.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            CreatableTypes().EndingWith("Service").AsInterfaces().RegisterAsLazySingleton();
            Mvx.IoCProvider.RegisterSingleton(() => UserDialogs.Instance);
            Mvx.IoCProvider.RegisterSingleton<IDataManager>(() => new SQLiteBudgetDatabase());
<<<<<<< HEAD

=======
            
>>>>>>> feature/ModifySQLite
            RegisterAppStart<MainViewModel>();
        }

        
    }
}
