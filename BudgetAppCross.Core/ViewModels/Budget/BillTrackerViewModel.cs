using BaseClasses;
using BaseViewModels;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BudgetAppCross.Core.ViewModels
{
    public class BillTrackerViewModel : XamarinBaseViewModel<string>//MvxViewModel<BillTracker>
    {
        #region Fields
        private IDataManager _dataManager;
        #endregion

        #region Properties
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
        public IMvxCommand AddBillCommand { get; }
        public IMvxCommand ShowOptionsCommand { get; }
        public IMvxCommand DeleteBillCommand { get; }
        #endregion

        #region Constructors
        public BillTrackerViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            //AddBillCommand = new Command(async result => await navigationService.Navigate<NewBillViewModel, string, BillTracker>(BillTracker.CompanyName));
            AddBillCommand = new MvxAsyncCommand(async () => await OnAddBill());
            //ShowOptionsCommand = new Command(() => Console.WriteLine("Swipe Left"));
            DeleteBillCommand = new MvxAsyncCommand(async () => await OnDeleteBill(), () => CanDeleteBill());

            _backgroundHandler.RegisterMessage<ChangeBillMessage>(this, async x => await OnChangeBillMessage());
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            var _ = UpdateBills();
        }

        

        #endregion

        #region Methods
        public override void Prepare(string parameter)
        {
            CompanyName = parameter;
        }

        private async Task UpdateBills()
        {
            //var options = await LoadAccountOptions();
            await _dataManager.UpdateBankAccountNames();
            var temp = await _dataManager.GetBillsForPayee(CompanyName);
            temp = temp.OrderBy(x => x.Date).ToList();
            var bvms = new List<BillViewModel>();
            foreach (var item in temp)
            {
                bvms.Add(new BillViewModel(_backgroundHandler, _dataManager, item));

            }

            Bills = new ObservableCollection<BillViewModel>(bvms);
        }

        private async Task<List<string>> LoadAccountOptions()
        {
            var options = await _dataManager.GetBankAccounts();

            var names = new List<string>();
            foreach (var item in options)
            {
                names.Add(item.Nickname);
            }

            return names;

        }

        //private async Task SaveBills()
        //{
        //    //foreach (var bill in Bills)
        //    //{
        //    //    await BudgetDatabase.SaveBill(bill.Bill);
        //    //}
        //    await BudgetDatabase.InsertBills(Bills);
        //}

        private void RefreshCanExecutes()
        {
            //(DeleteBillCommand as Command).ChangeCanExecute();
            (DeleteBillCommand as MvxAsyncCommand).RaiseCanExecuteChanged();
        }

        private async Task OnAddBill()
        {
            await _navService.Navigate<NewBillsViewModel, string>(CompanyName);
            //await navigationService.Navigate<NewBillsViewModel, string, bool>(new Bill(CompanyName));
            //UpdateBills();
        }



        private async Task OnDeleteBill()
        {
            await _dataManager.DeleteBill(SelectedBill.Bill);
            //BillManager.DeleteBill(BillTracker.CompanyName, SelectedBill);
            _ = UpdateBills();
        }

        private bool CanDeleteBill()
        {
            return SelectedBill != null;
        }

        private async Task OnChangeBillMessage()
        {
            await UpdateBills();
            if(Bills.Count == 0)
            {
                await _navService.Close(this);
            }
            
        }

        public Task ShowBill()
        {
            return _navService.Navigate<BillDetailsViewModel, Bill>(SelectedBill.Bill);
        }

        //public Task ShowBill(int id)
        //{
        //    return navigationService.Navigate<BillViewModel, int>(id);
        //}


        #endregion







    }
}
