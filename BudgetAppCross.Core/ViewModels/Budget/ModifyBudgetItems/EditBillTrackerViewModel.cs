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
using Acr.UserDialogs;
using System.Linq;
using MvvmCross;

namespace BudgetAppCross.Core.ViewModels
{
    public class EditBillTrackerViewModel : BaseViewModel<string>// MvxViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        private string oldPayee;
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
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Constructors
        public EditBillTrackerViewModel(IMvxNavigationService nav)
        {
            navigationService = nav;
            SaveCommand = new Command(async () => await OnSave());
            CancelCommand = new Command(async () => await OnCancel());
            
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
                var config = new AlertConfig().SetMessage("Invalid Payee Name");//.SetOkText(ConfirmConfig.DefaultOkText);
                Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(config);
                return;
            }
            await BudgetDatabase.ChangePayeeName(oldPayee, Payee);

            Messenger.Send(new ChangeBillMessage());
            await navigationService.Close(this);
        }

        private async Task OnCancel()
        {
            await navigationService.Close(this);
        }

        #endregion

    }
}
