using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//using Acr.UserDialogs;
using System.Linq;
using MvvmCross;
using MvvmCross.Commands;
using BaseViewModels;
using BaseClasses;
using BudgetAppCross.DataAccess;

namespace BudgetAppCross.Core.ViewModels
{
    public class EditBillTrackerViewModel : XamarinBaseViewModel<string>// MvxViewModel
    {
        #region Fields
        private string oldPayee;
        private IDataManager _dataManager;
        #endregion

        #region Properties
        private string payee;
        public string Payee
        {
            get { return payee; }
            set
            {
                if (payee != value)
                {
                    payee = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Commands
        public IMvxCommand SaveCommand { get; }
        public IMvxCommand CancelCommand { get; }
        #endregion

        #region Constructors
        public EditBillTrackerViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            SaveCommand = new MvxAsyncCommand(OnSave);
            CancelCommand = new MvxAsyncCommand(OnCancel);
            
        }

        public override void Prepare(string parameter)
        {
            oldPayee = parameter;
            Payee = parameter;
        }
        #endregion

        #region Methods
        private async Task OnSave()
        {
            if (string.IsNullOrWhiteSpace(Payee))
            {
                _backgroundHandler.Notify("Invalid Payee Name");//.SetOkText(ConfirmConfig.DefaultOkText);
                return;
            }
            await _dataManager.ChangePayeeName(oldPayee, Payee);

            _backgroundHandler.SendMessage(new ChangeBillMessage());
            await _navService.Close(this);
        }

        private async Task OnCancel()
        {
            await _navService.Close(this);
        }

        #endregion

    }
}
