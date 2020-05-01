using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System;

namespace BudgetAppCross.ViewModels
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
