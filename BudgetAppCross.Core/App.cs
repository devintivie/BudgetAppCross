using BudgetAppCross.Core.ViewModels;
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

            RegisterAppStart<MainViewModel>();
        }
    }
}
