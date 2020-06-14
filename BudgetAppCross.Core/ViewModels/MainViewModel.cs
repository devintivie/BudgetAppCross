using BudgetAppCross.Models;
using BudgetAppCross.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SQLite;
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
            var name = await StateManager.LoadState();

            if (name == null)
            {
                //if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
                //{
                //    masterDetailPage.IsPresented = false;
                //}
                //else if (Application.Current.MainPage is NavigationPage navigationPage
                //         && navigationPage.CurrentPage is MasterDetailPage nestedMasterDetail)
                //{
                //    nestedMasterDetail.IsPresented = false;
                //}

                if(Application.Current.MainPage is MasterDetailPage masterDetailPage)
                {
                    masterDetailPage.IsGestureEnabled = false;
                }


                await navigationService.Navigate<MenuViewModel>();
                await navigationService.Navigate<SelectBudgetViewModel>();
            }
            else
            {
                await BudgetDatabase.Initialize();
                await BudgetDatabase.GetBankAccounts();
                await navigationService.Navigate<MenuViewModel>();
                await navigationService.Navigate<DateRangeViewModel>();
            }

            
        }

        //public override async void ViewDestroy(bool viewFinishing = true)
        //{
        //    base.ViewDestroy(viewFinishing);

        //    //await StateManager.Instance.SaveToFile();
        //}

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