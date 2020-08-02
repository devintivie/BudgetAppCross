using BudgetAppCross.Models;
using BudgetAppCross.Models.Bills;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class BudgetQuickViewModel : BaseViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        public string BudgetName { get; private set; }

        #endregion
       
        #region Commands
        public ICommand EditThisCommand { get; private set; }
        public ICommand DeleteThisCommand { get; private set; }
        #endregion
        
        #region Constructors
        public BudgetQuickViewModel(IMvxNavigationService navService, string name)
        {
            navigationService = navService;
            BudgetName = name;
            //EditThisCommand = new Command(async () => await navigationService.Navigate<EditBankAccountViewModel, BankAccount>(BankAccount));
            DeleteThisCommand = new Command(async () => await OnDeleteThis());
        }
        #endregion

        #region Methods
        //private async void GetLatestBalance()
        //{
        //    var temp = await BudgetDatabase.GetLatestBalance(BankAccount.AccountID, DateTime.Today);
        //    //var temp = 0.0;
        //    if (temp == null)
        //    {
        //        Balance = 0.0m;
        //    }
        //    else
        //    {
        //        Balance = temp.Amount;
        //    }
        //}

        private async Task OnDeleteThis()
        {
            await StateManager.DeleteBudgetFile(BudgetName);

            Messenger.Send(new ChangeBudgetsMessage());
            
        }
        #endregion

    }
}
