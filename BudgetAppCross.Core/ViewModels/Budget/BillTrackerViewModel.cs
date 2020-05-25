using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class BillTrackerViewModel : BaseViewModel<string>//MvxViewModel<BillTracker>
    {
        #region Fields
        private IMvxNavigationService navigationService;
        //private BillTracker billTracker;
        #endregion

        #region Properties
        //public BillTracker BillTracker { get; private set; }
        public string CompanyName { get; private set; }

        private ObservableCollection<BillViewModel> bills = new ObservableCollection<BillViewModel>();
        public ObservableCollection<BillViewModel> Bills
        {
            get { return bills; }
            private set
            {
                SetProperty(ref bills, value);
            }
        }

        private BillViewModel selectedBill;
        public BillViewModel SelectedBill
        {
            get { return selectedBill; }
            set
            {
                SetProperty(ref selectedBill, value);
                RefreshCanExecutes();
            }
        }


        //private string companyName;
        //public string CompanyName
        //{
        //    get { return companyName; }
        //    set
        //    {
        //        SetProperty(ref companyName, value);
        //    }
        //}

        //private bool autopay;
        //public bool Autopay
        //{
        //    get { return autopay; }
        //    set
        //    {
        //        SetProperty(ref autopay, value);
        //    }
        //}




        #endregion

        #region Commands
        public ICommand AddBillCommand { get; }
        public ICommand ShowOptionsCommand { get; }
        public ICommand DeleteBillCommand { get; }
        #endregion

        #region Constructors
        public BillTrackerViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            //AddBillCommand = new Command(async result => await navigationService.Navigate<NewBillViewModel, string, BillTracker>(BillTracker.CompanyName));
            AddBillCommand = new Command(async () => await OnAddBill());
            ShowOptionsCommand = new Command(() => Console.WriteLine("Swipe Left"));
            DeleteBillCommand = new Command(() =>  OnDeleteBill(), () => CanDeleteBill());
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            UpdateBills();
        }

        #endregion

        #region Methods
        public override void Prepare(string parameter)
        {
            CompanyName = parameter;
            //billTracker = parameter;
            
        }

        

        public override void ViewDestroy(bool viewFinishing = true)
        {
            SaveBills();
            base.ViewDestroy(viewFinishing);
            
        }

        private async void UpdateBills()
        {
            //var options = await LoadAccountOptions();
            await BudgetDatabase.UpdateBankAccountNames();
            var temp = await BudgetDatabase.GetBillsForPayee(CompanyName);
            var bvms = new List<BillViewModel>();
            foreach (var item in temp)
            {
                bvms.Add(new BillViewModel(item));
            }

            Bills = new ObservableCollection<BillViewModel>(bvms);
        }

        private async Task<List<string>> LoadAccountOptions()
        {
            var options = await BudgetDatabase.GetBankAccounts();

            var names = new List<string>();
            foreach (var item in options)
            {
                names.Add(item.Nickname);
            }

            return names;

        }

        private async void SaveBills()
        {
            foreach (var bill in Bills)
            {
                await BudgetDatabase.SaveBill(bill.Bill);
            }
        }

        private void RefreshCanExecutes()
        {
            (DeleteBillCommand as Command).ChangeCanExecute();
        }

        private async Task OnAddBill()
        {
            await navigationService.Navigate<NewBillViewModel, Bill, bool>(new Bill(CompanyName));
            UpdateBills();
        }



        private async void OnDeleteBill()
        {
            await BudgetDatabase.DeleteBill(SelectedBill.Bill);
            //BillManager.DeleteBill(BillTracker.CompanyName, SelectedBill);
            UpdateBills();
        }

        private bool CanDeleteBill()
        {
            return SelectedBill != null;
        }
        #endregion







    }
}
