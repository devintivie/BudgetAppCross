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
    public class BillTrackerViewModel : BaseViewModel<BillTracker>//MvxViewModel<BillTracker>
    {
        #region Fields
        private IMvxNavigationService navigationService;
        //private BillTracker billTracker;
        #endregion

        #region Properties
        public BillTracker BillTracker { get; private set; }

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

        }

        private void RefreshCanExecutes()
        {
            (DeleteBillCommand as Command).ChangeCanExecute();
        }

        private async Task OnAddBill()
        {
            //var result = await navigationService.Navigate<NewBillViewModel, string, Bill>(BillTracker.CompanyName);

            //if(result != null)
            //{
            //    BillManager.AddBill(BillTracker.CompanyName, result);
            //}

            //UpdateBills();

        }

        

        private void OnDeleteBill()
        {
            //BillManager.DeleteBill(BillTracker.CompanyName, SelectedBill);
            UpdateBills();
        }

        private bool CanDeleteBill()
        {
            return SelectedBill != null;
        }

        #endregion

        #region Methods
        public override void Prepare(BillTracker parameter)
        {
            BillTracker = parameter;
            //billTracker = parameter;
            UpdateBills();
        }

        private void UpdateBills()
        {
            //Bills = new ObservableCollection<Bill>(BillTracker.Bills);
            Bills.Clear();
            foreach (var item in BillTracker.Bills)
            {
                Bills.Add(new BillViewModel(item));
            }
        }
        #endregion

        

        


        
    }
}
