using BudgetAppCross.Models;
using BudgetAppCross.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BudgetAppCross.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        readonly IMvxNavigationService navigationService;
        public MainViewModel(IMvxNavigationService navService)
        {
            navigationService = navService;

            StateManager.Instance.LoadFromFile();
        }

        public override async void ViewAppearing()
        {
            base.ViewAppearing();
            
            await navigationService.Navigate<MenuViewModel>();
            await navigationService.Navigate<BankOverviewViewModel>();
        }

        public override async void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);

            await StateManager.Instance.SaveToFile();
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