using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BudgetAppCross.Core.ViewModels
{
    public class MenuViewModel : MvxViewModel
    {
        readonly IMvxNavigationService navigationService;

        private ObservableCollection<string> menuItemList;

        public ObservableCollection<string> MenuItemList
        {
            get { return menuItemList; }
            set { SetProperty(ref menuItemList, value); }
        }

        public MenuViewModel(IMvxNavigationService navService)
        {
            navigationService = navService;

            MenuItemList = new MvxObservableCollection<string>()
            {
                "Contacts",
                "Todo"
            };
        }
    }
}
