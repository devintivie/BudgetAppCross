using BudgetAppCross.Models;
using BudgetAppCross.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
//using SQLite;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        readonly IMvxNavigationService navigationService;
        public MainViewModel(IMvxNavigationService navService)
        {
            navigationService = navService;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            var _ = NavigateInitial();
            //var mainAccount = new BankAccount(450, "-", "Chase", "Main Account");
            //await BudgetDatabase.SaveBankAccount(mainAccount);
        }

        private async Task NavigateInitial()
        {
            var name = StateManager.LoadState();

            if (name != null)
            {
                try
                {
                    await BudgetDatabase.Initialize();
                    await BudgetDatabase.GetBankAccounts();
                    await navigationService.Navigate<MenuViewModel>();
                    await navigationService.Navigate<AgendaViewModel>();
                }
                catch (Exception ex)
                {
                    await LoadWelcome();
                }
            }
            else
            {
                if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
                {
                    masterDetailPage.IsGestureEnabled = false;
                }

                var files = await StateManager.FindBudgetFiles();
                if (files.Count == 0)
                {
                    await LoadWelcome();
                }
                else
                {
                    await navigationService.Navigate<MenuViewModel>();
                    await navigationService.Navigate<SelectBudgetViewModel>();
                }
            }
        }

        private async Task LoadWelcome()
        {
            await navigationService.Navigate<MenuViewModel>();
            await navigationService.Navigate<WelcomeViewModel>();
        }
    }
}




//public class MainViewModel : MvxViewModel
//{
//    public string WelcomeText => " Hello mutha fucka";

//    private IContactService ContactService { get; }
//    private IMvxNavigationService NavigationService { get; }

//    public ObservableCollection<Contact> Contacts { get; } = new ObservableCollection<Contact>();
//    public MainViewModel(IMvxNavigationService navigation, IContactService contactService)
//    {
//        NavigationService = navigation;
//        ContactService = contactService;
//    }


//    public override void ViewAppeared()
//    {
//        base.ViewAppeared();

//        var contacts = ContactService.FetchContacts();
//        foreach (var contact in contacts)
//        {
//            Contacts.Add(contact);
//        }
//    }


//    public Task ShowContactDetails(Contact contact)
//    {
//        return NavigationService.Navigate<ContactDetailsViewModel, Contact>(contact);
//    }
//}