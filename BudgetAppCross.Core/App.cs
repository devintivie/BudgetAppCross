﻿using Acr.UserDialogs;
using BudgetAppCross.Core.Services;
using BudgetAppCross.Core.ViewModels;
using BudgetAppCross.DataAccess;
using BudgetAppCross.SQLiteDataAccess;
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
            
            RegisterAppStart<MainViewModel>();
        }

        
    }
}
