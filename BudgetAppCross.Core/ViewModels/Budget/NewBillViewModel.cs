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
    public class NewBillViewModel : MvxViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        private BillManager BillManager => BillManager.Instance;

        private string companyName;
        public string CompanyName
        {
            get { return companyName; }
            set
            {
                SetProperty(ref companyName, value);
            }
        }
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
        //public ICommand CancelCommand { get; }
        #endregion

        #region Constructors
        public NewBillViewModel(MvxNavigationService nav)
        {
            navigationService = nav;
            Bill = new Bill();

            SaveCommand = new Command(async () => await OnSave());
        }



        //public override void Prepare(string parameter)
        //{
        //    CompanyName = parameter;
        //}
        #endregion

        #region Methods
        private async Task OnSave()
        {
            navigationService.Close(this);
        }
        #endregion

    }
}
