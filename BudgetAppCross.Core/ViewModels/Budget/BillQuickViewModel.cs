using BaseClasses;
using BaseViewModels;
using BudgetAppCross.Core.Services;
using BudgetAppCross.Core.ViewModels.Pages;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BudgetAppCross.Core.ViewModels
{
    public class BillQuickViewModel : MvxNavigationBaseViewModel, IBillInfoViewModel
    {
        #region Fields
        private IDataManager _dataManager;
        #endregion

        #region Properties
        public Bill Bill { get; private set; }

        public string Payee => Bill.Payee;
        public BillStatus BillStatus => Bill.BillStatus;
        public DateTime Date => Bill.Date;
        public decimal Amount => Bill.Amount;
        public decimal PaymentAmount => Bill.PaymentAmount;


        public bool IsPaid
        {
            get { return Bill.IsPaid; }
            set
            {
                if (Bill.IsPaid != value)
                {
                    Bill.IsPaid = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(BillStatus));

                    _ = UpdateAndSave();
                }
            }
        }

        //public bool IsAuto
        //{
        //    get { return Bill.IsAuto; }
        //    set
        //    {
        //        if (Bill.IsAuto != value)
        //        {
        //            Bill.IsAuto = value;
        //            RaisePropertyChanged();
        //            RaisePropertyChanged(nameof(BillStatus));

        //            _ = UpdateAndSave();
        //        }
        //    }
        //}


        #endregion

        #region Commands
        public IMvxCommand DetailsCommand { get; private set; }
        #endregion

        #region Constructors
        public BillQuickViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager, Bill bill) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            Bill = bill;
            DetailsCommand = new MvxAsyncCommand(OnDetails, CanDetails);
            _backgroundHandler.RegisterMessage<UpdateBillMessage>(this, async x => await OnUpdateBill());

        }

        private async Task OnUpdateBill()
        {
            await RaiseAllPropertiesChanged();
        }

        #endregion

        #region Methods

        private bool CanDetails()
        {
            return true;
        }

        private async Task OnDetails()
        {
            await _navService.Navigate<BillDetailsViewModel, Bill>(Bill);
        }
        private async Task UpdateAndSave()
        {
            await _dataManager.SaveBill(Bill);
            _backgroundHandler.SendMessage(new UpdateBillMessage(Bill.AccountID));
        }

        private async Task ChangeAndSave()
        {
            await _dataManager.SaveBill(Bill);
            _backgroundHandler.SendMessage(new ChangeBillMessage(Bill.AccountID));
        }

        

        #endregion

    }
}
