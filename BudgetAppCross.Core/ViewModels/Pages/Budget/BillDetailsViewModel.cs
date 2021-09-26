using BaseClasses;
using BaseViewModels;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Core.ViewModels.Pages
{
    public class BillDetailsViewModel : XamarinBaseViewModel<Bill>, IBillInfoViewModel
    {
        #region Fields
        private IDataManager _dataManager;
        #endregion

        #region Properties
        public Bill Bill { get; private set; }

        public string Payee => Bill.Payee;
        public bool IsDateInFuture => Bill.Date >= DateTime.Today.AddDays(1);

        public DateTime Date
        {
            get { return Bill.Date; }
            set
            {
                if (!Bill.Date.Equals(value))
                {
                    Bill.Date = value;
                    _ = RaiseAllPropertiesChanged();

                    _ = ChangeAndSave();
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

        public decimal ShareRatio
        {
            get{ return Bill.ShareRatio; }
            set
            {
                if (Bill.ShareRatio != value)
                {
                    Bill.ShareRatio = value;
                    RaisePropertyChanged();
                    _ = UpdateAndSave();
                }
            }
        }

        public decimal PaymentAmount => Bill.PaymentAmount;// Math.Round(ShareRatio * Amount / 100m, 2);

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
                    _ = RaiseAllPropertiesChanged();

                    _ = UpdateAndSave();
                }
            }
        }

        //public decimal PaymentAmount => ShareRatio * Amount;
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


        


        //public string ShareRatio
        //{
        //    get { return Bill.DisplayShareRatio(); }
        //    set
        //    {
        //        Bill.SetShareRatio(value);
        //        RaisePropertyChanged();
        //    }
        //}


        public bool IsAuto
        {
            get { return Bill.IsAuto; }
            set
            {
                if (Bill.IsAuto != value)
                {
                    Bill.IsAuto = value;
                    _ = RaiseAllPropertiesChanged();

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

        #region Constructors
        public BillDetailsViewModel(IMvxNavigationService navService, IBackgroundHandler backgroundHandler, IDataManager dataManager) 
            : base(navService, backgroundHandler)
        {
            _dataManager = dataManager;
        }

        public override void Prepare(Bill parameter)
        {
            Bill = parameter;

            LoadAccountOptions();
        }
        #endregion

        #region Methods
        private async Task UpdateAndSave()
        {
            await _dataManager.SaveBill(Bill);
            await RaiseAllPropertiesChanged();
            _backgroundHandler.SendMessage(new UpdateBillMessage(Bill.AccountID));
        }

        private async Task ChangeAndSave()
        {
            await _dataManager.SaveBill(Bill);
            _backgroundHandler.SendMessage(new ChangeBillMessage(Bill.AccountID));
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


        private void LoadAccountOptions()
        {
            AccountOptions = new ObservableCollection<string>(_dataManager.BankAccountNicknames);
            
            SelectedAccount = AccountOptions.Where(x => x.Equals(Bill.BankAccount.Nickname)).FirstOrDefault();
        }
        #endregion

    }
}
