using Acr.UserDialogs;
using BaseClasses;
using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using BudgetAppCross.StateManagers;
using MvvmCross;
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

        public string BudgetName => StateManager.Instance.DatabaseFilename;


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

        private IMvxAsyncCommand showAccountPageCommand;

        public IMvxAsyncCommand ShowAccountPageCommand
        {
            get 
            {
                showAccountPageCommand = showAccountPageCommand ?? new MvxAsyncCommand(ShowAccountPageAsync); 
                return showAccountPageCommand;
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
            Messenger.Instance.Register<UpdateMenuMessage>(this, async x => await OnUpdate());
            

            PageList = new MvxObservableCollection<NavigablePage>()
            {
                
                NavigablePage.DateRange,
                NavigablePage.BillList,
                NavigablePage.Agenda,
                NavigablePage.BankOverview,
                NavigablePage.About,
                //NavigablePage.Purchasing
            };
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);

            Messenger.Instance.Unregister(this);
        }


        #endregion

        #region Methods
        private async Task ShowDetailPageAsync()
        {
            CloseMenu();
            switch (SelectedPage)
            {
                //case NavigablePage.Account:
                //    await navigationService.Navigate<AccountViewModel>();
                //    break;
                case NavigablePage.LoadBudget:
                    await navigationService.Navigate<SelectBudgetViewModel>();
                    break;
                case NavigablePage.DateRange:
                    await navigationService.Navigate<DateRangeViewModel>();
                    break;
                case NavigablePage.BillList:
                    await navigationService.Navigate<BudgetListViewModel>();
                    break;
                case NavigablePage.Agenda:
                    await navigationService.Navigate<AgendaViewModel>();
                    break;
                case NavigablePage.BankOverview:
                    await navigationService.Navigate<BankOverviewViewModel>();
                    break;
                case NavigablePage.About:
                    await navigationService.Navigate<AboutViewModel>();
                    break;
                case NavigablePage.Purchasing:
                    await navigationService.Navigate<PurchasingViewModel>();
                    break;
                default:
                    break;
            }
            
        }

        private async Task OnUpdate()
        {
            RaisePropertyChanged(nameof(BudgetName));
        }

        private async Task ShowAccountPageAsync()
        {
            CloseMenu();
            await navigationService.Navigate<AccountViewModel>();
            
        }

        private void CloseMenu()
        {
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
