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
    public class NewBillTrackerViewModel : MvxViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        private BillTracker billTracker;
        public BillTracker BillTracker
        {
            get { return billTracker; }
            set
            {
                SetProperty(ref billTracker, value);
            }
        }

        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        #endregion

        #region Constructors
        public NewBillTrackerViewModel(IMvxNavigationService nav)
        {
            navigationService = nav;
            BillTracker = new BillTracker("");

            SaveCommand = new Command(async () => await OnSave());
        }


        #endregion

        #region Methods
        private async Task OnSave()
        {
            BillManager.Instance.AddTracker(BillTracker);
            await navigationService.Close(this);
        }
        #endregion

    }
}
