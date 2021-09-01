using BaseClasses;
using BaseViewModels;
using BudgetAppCross.Models;
using BudgetAppCross.Models.Bills;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BudgetAppCross.Core.ViewModels
{
    public class BudgetQuickViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        ISettingsManager _settings;
        #endregion

        #region Properties
        public string BudgetName { get; private set; }

        #endregion
       
        #region Commands
        public IMvxCommand EditThisCommand { get; private set; }
        public IMvxCommand DeleteThisCommand { get; private set; }
        #endregion
        
        #region Constructors
        public BudgetQuickViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, 
            ISettingsManager settings, string name) : base(navService, backgroundHandler)
        {
            BudgetName = name;
            _settings = settings;
            //EditThisCommand = new Command(async () => await navigationService.Navigate<EditBankAccountViewModel, BankAccount>(BankAccount));
            DeleteThisCommand = new MvxAsyncCommand(OnDeleteThis);
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
            await _settings.DeleteConfigFile(BudgetName);
            //await StateManager.DeleteBudgetFile(BudgetName);

            _backgroundHandler.SendMessage(new ChangeBudgetsMessage());
            //Messenger.Send(new ChangeBudgetsMessage());
            
        }
        #endregion

    }
}
