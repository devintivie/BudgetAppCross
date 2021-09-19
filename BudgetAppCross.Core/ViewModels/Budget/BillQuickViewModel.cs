using BaseClasses;
using BaseViewModels;
using BudgetAppCross.Core.Services;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using MvvmCross.Commands;
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
    public class BillQuickViewModel : BaseViewModel, IBillInfoViewModel
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
        public IMvxCommand DeleteThisCommand { get; private set; }
        #endregion

        #region Constructors
        public BillQuickViewModel(IBackgroundHandler backgroundHandler, IDataManager dataManager, Bill bill) : base(backgroundHandler)
        {
            _dataManager = dataManager;
            Bill = bill;

            DeleteThisCommand = new MvxAsyncCommand(OnDeleteThis);
        }
        #endregion

        #region Methods
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

        private async Task OnDeleteThis()
        {
            await _dataManager.DeleteBill(Bill);
            _backgroundHandler.SendMessage(new ChangeBillMessage(Bill.AccountID));
        }

        #endregion

    }
}
