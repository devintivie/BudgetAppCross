using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class NewBillViewModel : MvxViewModel//<string, Bill>
    {
        #region Fields
        private IMvxNavigationService navigationService;
        //private BillTracker billTracker;
        #endregion

        #region Properties

        private Bill bill;
        public Bill Bill
        {
            get { return bill; }
            set
            {
                SetProperty(ref bill, value);
            }
        }

        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Constructors
        public NewBillViewModel(IMvxNavigationService nav)
        {
            navigationService = nav;
            Bill = new Bill();

            SaveCommand = new Command(async () => await OnSave());
            CancelCommand = new Command(async () => await OnCancel());
        }

        //public override void Prepare(string parameter)
        //{
        //    companyName = parameter;
        //}
        #endregion

        #region Methods
        private async Task OnSave()
        {
            //await navigationService.Close(this, Bill);
            await BudgetDatabase.Instance.SaveBill(Bill);
            await navigationService.Close(this);
        }

        private async Task OnCancel()
        {
            await navigationService.Close(this);
        }
        #endregion

    }
}
