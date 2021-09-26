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

namespace BudgetAppCross.Core.ViewModels.Pages
{
    public class BillTrackerViewModel : XamarinBaseViewModel<string>//MvxViewModel<BillTracker>
    {
        #region Fields
        private IDataManager _dataManager;
        #endregion

        #region Properties
        public string CompanyName { get; private set; }

        private ObservableCollection<IBillInfoViewModel> bills = new ObservableCollection<IBillInfoViewModel>();
        public ObservableCollection<IBillInfoViewModel> Bills
        {
            get { return bills; }
            set
            {
                if (bills != value)
                {
                    bills = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IBillInfoViewModel selectedBill;
        public IBillInfoViewModel SelectedBill
        {
            get { return selectedBill; }
            set
            {
                if (selectedBill != value)
                {
                    selectedBill = value;
                    _ = UpdateUIControlAccess();
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Commands
        public IMvxCommand AddBillCommand { get; }
        public IMvxCommand EditCommand { get;}
        public IMvxCommand DeleteThisCommand { get; }
        public IMvxCommand ShowBillCommand { get; }
        #endregion

        #region Constructors
        public BillTrackerViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            AddBillCommand = new MvxAsyncCommand(OnAddBill);
            ShowBillCommand = new MvxAsyncCommand(OnShowBill, CanShowBill);
            EditCommand = new MvxAsyncCommand(OnEdit);
            DeleteThisCommand = new MvxAsyncCommand(OnDeleteThis);

            _backgroundHandler.RegisterMessage<ChangeBillMessage>(this, async x => await OnChangeBillMessage());
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            _ = UpdateBills();
        }
        #endregion

        #region Methods
        public override void Prepare(string parameter)
        {
            CompanyName = parameter;
        }

        private async Task UpdateBills()
        {
            //await _dataManager.UpdateBankAccountNames();
            var tmpBills = await _dataManager.GetBillsForPayee(CompanyName);
            tmpBills = tmpBills.OrderBy(x => x.Date).ToList();
            var bvms = new List<IBillInfoViewModel>();
            foreach (var item in tmpBills)
            {
                bvms.Add(new BillQuickViewModel(_navService, _backgroundHandler, _dataManager, item));
            }

            Bills = new ObservableCollection<IBillInfoViewModel>(bvms);
        }

        private async Task OnEdit()
        {
            await _navService.Navigate<EditBillTrackerViewModel, string>(CompanyName);
        }

        private async Task OnDeleteThis()
        {
            await _dataManager.DeleteBillsForPayee(CompanyName);
            _backgroundHandler.SendMessage(new ChangeBillMessage());
        }

        //private async Task<List<string>> LoadAccountOptions()
        //{
        //    //var options = await _dataManager.GetBankAccounts();
        //    var options = _dataManager.BankAccountNicknames;

        //    var names = new List<string>();
        //    foreach (var item in options)
        //    {
        //        names.Add(item);
        //    }

        //    return names;

        //}

        /// <summary>
        /// Add Bill for specified company/payee
        /// </summary>
        /// <returns></returns>
        private async Task OnAddBill()
        {

            await _navService.Navigate<NewBillsViewModel>();
            //await _navService.Navigate<NewBillsViewModel, string>(CompanyName);
        }

        private async Task OnShowBill()
        {
            await _navService.Navigate<BillDetailsViewModel, Bill>(SelectedBill.Bill);
        }

        private bool CanShowBill()
        {
            return true;
        }

        private async Task OnChangeBillMessage()
        {
            await UpdateBills();
            if(Bills.Count == 0)
            {
                await _navService.Close(this);
            }
            
        }


        #endregion







    }
}
