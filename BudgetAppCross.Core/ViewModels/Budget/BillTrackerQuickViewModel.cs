using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class BillTrackerQuickViewModel : BaseViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
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
        public ICommand EditThisCommand { get; private set; }
        public ICommand DeleteThisCommand { get; private set; }
        #endregion

        #region Constructors
        public BillTrackerQuickViewModel(IMvxNavigationService navService, string name)
        {
            navigationService = navService;
            Payee = name;
            EditThisCommand = new Command(async () => await navigationService.Navigate<EditBillTrackerViewModel, string>(Payee));
            DeleteThisCommand = new Command(async () => await OnDeleteThis());
        }
        #endregion

        #region Methods
        private async Task OnDeleteThis()
        {
            await BudgetDatabase.DeleteBillsForPayee(Payee);
            Messenger.Send(new ChangeBillMessage());
        }

        //private async Task OnDeleteThis()
        //{
        //    await BudgetDatabase.DeleteBill(Bill);
        //    Messenger.Send(new ChangeBillMessage(Bill.AccountID));
        //}


        #endregion

    }
}
