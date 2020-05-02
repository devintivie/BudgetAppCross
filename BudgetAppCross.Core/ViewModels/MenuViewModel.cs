using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class MenuViewModel : MvxViewModel
    {

        #region Fields
        readonly IMvxNavigationService navigationService;
        #endregion

        #region Properties
        //private ObservableCollection<string> menuItemList;

        //public ObservableCollection<string> MenuItemList
        //{
        //    get { return menuItemList; }
        //    set { SetProperty(ref menuItemList, value); }
        //}

        private ObservableCollection<NavigablePage> pageList;
        public ObservableCollection<NavigablePage> PageList
        {
            get { return pageList; }
            set
            {
                SetProperty(ref pageList, value);
            }
        }


        private string selectedMenuItem;
        public string SelectedMenuItem
        {
            get { return selectedMenuItem; }
            set
            {
                SetProperty(ref selectedMenuItem, value);
            }
        }

        private NavigablePage selectedPage;
        public NavigablePage SelectedPage
        {
            get { return selectedPage; }
            set
            {
                SetProperty(ref selectedPage, value);
            }
        }


        #endregion

        #region Commands
        private IMvxAsyncCommand showDetailPageAsyncCommand;

        public IMvxAsyncCommand ShowDetailPageAsyncCommand
        {
            get 
            {
                showDetailPageAsyncCommand = showDetailPageAsyncCommand ?? new MvxAsyncCommand(ShowDetailPageAsync);
                return showDetailPageAsyncCommand;
            }
        }

        #endregion

        #region Constructors
        public MenuViewModel(IMvxNavigationService navService)
        {
            navigationService = navService;

            //MenuItemList = new MvxObservableCollection<string>()
            //{
            //    "Bill List",
            //    "About"
            //};

            PageList = new MvxObservableCollection<NavigablePage>()
            {
                NavigablePage.BillList,
                NavigablePage.About
            };
        }
        #endregion

        #region Methods
        private async Task ShowDetailPageAsync()
        {
            // Implement your logic here.
            //switch (SelectedMenuItem)
            //{
            //    case "Bill List":
            //        await navigationService.Navigate<BudgetListViewModel>();
            //        break;
            //    case "About":
            //        await navigationService.Navigate<AboutViewModel>();
            //        break;
            //    default:
            //        break;
            //}
            switch (SelectedPage)
            {
                case NavigablePage.BillList:
                    await navigationService.Navigate<BudgetListViewModel>();
                    break;
                case NavigablePage.About:
                    await navigationService.Navigate<AboutViewModel>();
                    break;
                default:
                    break;
            }
            if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
            }
            else if (Application.Current.MainPage is NavigationPage navigationPage
                     && navigationPage.CurrentPage is MasterDetailPage nestedMasterDetail)
            {
                nestedMasterDetail.IsPresented = false;
            }
        }
        #endregion






    }

    
}
