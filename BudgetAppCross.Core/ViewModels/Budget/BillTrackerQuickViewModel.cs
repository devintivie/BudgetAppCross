using BaseClasses;
using BaseViewModels;
using BudgetAppCross.Core.Services;
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
    public class BillTrackerQuickViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        private IDataManager _dataManager;
        #endregion

        #region Properties
        //public BillTracker BillTracker { get; private set; }

        private string payee;
        public string Payee
        {
            get { return payee; }
            set
            {
                SetProperty(ref payee, value);
            }
        }

        #endregion

        #region Commands
        public IMvxCommand EditThisCommand { get; private set; }
        public IMvxCommand DeleteThisCommand { get; private set; }
        #endregion

        #region Constructors
        public BillTrackerQuickViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler,
            IDataManager dataManager, string name) : base(navService, backgroundHandler)
        {
            Payee = name;
            EditThisCommand = new MvxAsyncCommand(async () => await _navService.Navigate<EditBillTrackerViewModel, string>(Payee));
            DeleteThisCommand = new MvxAsyncCommand(OnDeleteThis);
        }
        #endregion

        #region Methods
        private async Task OnDeleteThis()
        {
            await _dataManager.DeleteBillsForPayee(Payee);
            _backgroundHandler.SendMessage(new ChangeBillMessage());
        }

        //private async Task OnDeleteThis()
        //{
        //    await BudgetDatabase.DeleteBill(Bill);
        //    Messenger.Send(new ChangeBillMessage(Bill.AccountID));
        //}


        #endregion

    }
}
