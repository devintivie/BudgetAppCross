using BaseClasses;
using BaseViewModels;
using BudgetAppCross.Core.Services;
using BudgetAppCross.Core.ViewModels.Pages;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
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
    public class BillViewModel : MvxNavigationBaseViewModel, IBillInfoViewModel
    {
        #region Fields
        private IDataManager _dataManager;
        #endregion

        #region Properties
        public Bill Bill { get; private set; }

        public string Payee => Bill.Payee;
        public BillStatus BillStatus => Bill.BillStatus;

        public DateTime Date
        {
            get { return Bill.Date; }
            set
            {
                if (!Bill.Date.Equals(value))
                {
                    Bill.Date = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(BillStatus));

                    _ = UpdateAndSave();
                }
            }
        }

        public decimal Amount
        {
            get { return Bill.Amount; }
            set
            {
                if (Bill.Amount != value)
                {
                    Bill.Amount = value;
                    RaisePropertyChanged();

                    _ = UpdateAndSave();
                }
            }
        }

        public decimal PaymentAmount => Bill.PaymentAmount;// * Amount;
        public decimal ShareRatio => Bill.ShareRatio;
        //public decimal ShareRatio
        //{
        //    get { return Bill.ShareRatio; }
        //    set
        //    {
        //        if (Bill.ShareRatio != value)
        //        {
        //            Bill.ShareRatio = value;
        //            RaisePropertyChanged();
        //            RaisePropertyChanged(nameof(PaymentAmount));

        //            _ = UpdateAndSave();
        //        }
        //    }
        //}


        public string Confirmation
        {
            get { return Bill.Confirmation; }
            set
            {
                if (Bill.Confirmation != value)
                {
                    Bill.Confirmation = value;
                    RaisePropertyChanged();

                    _ = UpdateAndSave();
                }
            }
        }

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

        public bool IsAuto
        {
            get { return Bill.IsAuto; }
            set
            {
                if (Bill.IsAuto != value)
                {
                    Bill.IsAuto = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(BillStatus));

                    _ = UpdateAndSave();
                }
            }
        }

        private ObservableCollection<string> accountOptions = new ObservableCollection<string>();
        public ObservableCollection<string> AccountOptions
        {
            get { return accountOptions; }
            set
            {
                if (accountOptions != value)
                {
                    accountOptions = value;
                    RaisePropertyChanged();
                }
            }
        }


        //selectedAccount is null at load and therefore cannot be checked for equality....not sure what to do to keep it from updating account on view model load
        private string selectedAccount;
        public string SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                if (selectedAccount != value)
                {
                    selectedAccount = value;
                    _ = UpdateAccount();

                    RaisePropertyChanged();
                }
                
            }
        }


        #endregion

        #region Commands
        //public IMvxCommand DeleteThisCommand { get; private set; }
        public IMvxCommand OnDateSelectedCommand { get; private set; }
        public IMvxCommand DetailsCommand { get; private set; }
        #endregion

        #region Constructors
        public BillViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager, Bill bill) : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
            Bill = bill;

            //DeleteThisCommand = new MvxAsyncCommand(OnDeleteThis);
            OnDateSelectedCommand = new MvxAsyncCommand(ChangeAndSave);
            DetailsCommand = new MvxAsyncCommand(OnDetails, CanDetails);

            LoadAccountOptions();
        }

        private bool CanDetails()
        {
            return true;
        }

        private async Task OnDetails()
        {
            await _navService.Navigate<BillDetailsViewModel, Bill>(Bill);
        }
        #endregion

        #region Methods

        private void LoadAccountOptions()
        {
            try
            {
                AccountOptions = new ObservableCollection<string>(_dataManager.BankAccountNicknames);
                SelectedAccount = Bill.BankAccount.Nickname;
                //SelectedAccount = AccountOptions.Where(x => x.Equals(Bill.BankAccount.Nickname)).FirstOrDefault();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            

        }

        private async Task UpdateAccount()
        {
            if (SelectedAccount != null)
            {
                if (!Bill.BankAccount.Nickname.Equals(SelectedAccount))
                {
                    var acctId = await _dataManager.GetBankAccountID(SelectedAccount);

                    Bill.AccountID = acctId;
                    await ChangeAndSave();
                }
            }
        }

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
