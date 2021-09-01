using Acr.UserDialogs;
using BaseClasses;
using BaseViewModels;
using BudgetAppCross.Configurations;
using BudgetAppCross.Core.Services;
using BudgetAppCross.Core.ViewModels.Pages;
using BudgetAppCross.Models;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Core.ViewModels.Root
{
    public class MenuViewModel : MvxNavigationBaseViewModel
    {

        #region Fields
        private readonly Dictionary<string, NavigablePage> PageDictionary = new Dictionary<string, NavigablePage>
        {
            {"Custom Date View", NavigablePage.DateRange },
            {"Bill List", NavigablePage.BillList },
            {"Agenda", NavigablePage.Agenda },
            {"Bank Accounts", NavigablePage.BankOverview },
            {"About", NavigablePage.About },
        };

        private IConfigManager<SQLiteConfiguration> _configManager;
        #endregion

        #region Properties

        public ObservableCollection<string> PageList { get; set; } = new ObservableCollection<string>();
        public string BudgetName => _configManager.Configuration.DatabaseFilename;//   StateManager.Instance.DatabaseFilename;

        private string selectedPage;
        public string SelectedPage
        {
            get { return selectedPage; }
            set
            {
                if (selectedPage != value)
                {
                    selectedPage = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string selectedMenuItem;
        public string SelectedMenuItem
        {
            get { return selectedMenuItem; }
            set
            {
                if (selectedMenuItem != value)
                {
                    selectedMenuItem = value;
                    RaisePropertyChanged();
                }
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

        public IMvxAsyncCommand ShowAccountPageCommand { get; }
        //{
        //    get
        //    {
        //        showAccountPageCommand = showAccountPageCommand ?? new MvxAsyncCommand(ShowAccountPageAsync);
        //        return showAccountPageCommand;
        //    }
        //}

        #endregion

        #region Constructors
        public MenuViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IConfigManager<SQLiteConfiguration> configManager) : base(navService, backgroundHandler)
        {
            _configManager = configManager;
            ShowAccountPageCommand = new MvxAsyncCommand(ShowAccountPageAsync);
            //Messenger.Instance.Register<UpdateMenuMessage>(this, async x => await OnUpdate());
        }

        private async Task ShowAccountPageAsync()
        {
            //await navigationService.Navigate<AccountViewModel>();
        }

        public override void ViewAppearing()
        {
            PageList.Clear();
            foreach (var key in PageDictionary.Keys)
            {
                PageList.Add(key);
            }
        }

        //public override void ViewDestroy(bool viewFinishing = true)
        //{
        //    base.ViewDestroy(viewFinishing);

        //    Messenger.Instance.Unregister(this);
        //}


        #endregion

        #region Methods
        private async Task ShowDetailPageAsync()
        {
            if (PageDictionary.Keys.Any(SelectedPage.Equals))
            {
                var nav = PageDictionary[SelectedPage];
                switch (nav)
                {
                    case NavigablePage.Account:
                        break;
                    case NavigablePage.LoadBudget:
                        await _navService.Navigate<BudgetSelectViewModel>();
                        break;
                    case NavigablePage.DateRange:
                        await _navService.Navigate<DateRangeViewModel>();
                        break;
                    case NavigablePage.BillList:
                        await _navService.Navigate<BudgetListViewModel>();
                        break;
                    case NavigablePage.Agenda:
                        await _navService.Navigate<AgendaViewModel>();
                        break;
                    case NavigablePage.BankOverview:
                        await _navService.Navigate<BankOverviewViewModel>();
                        break;
                    case NavigablePage.About:
                        await _navService.Navigate<AboutViewModel>();
                        break;
                    //case NavigablePage.Purchasing:
                    //    await _navService.Navigate<PurchasingViewModel>();
                    //    break;
                    default:
                        break;
                }
            }
            
        }

        private async Task OnUpdate()
        {
            await RaisePropertyChanged(nameof(BudgetName));
        }
        #endregion






    }

    
}
