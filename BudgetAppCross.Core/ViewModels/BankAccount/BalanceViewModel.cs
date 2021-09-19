using BaseClasses;
using BaseViewModels;
using BudgetAppCross.Core.Services;
using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BudgetAppCross.Core.ViewModels
{
    public class BalanceViewModel : BaseViewModel
    {
        #region Fields
        private IDataManager _dataManager;
        private Balance _balance;
        #endregion

        #region Properties
        //public Balance Balance { get; private set; }

        public decimal Amount
        {
            get { return _balance.Amount; }
            set
            {
                if (_balance.Amount != value)
                {
                    _balance.Amount = value;
                    RaisePropertyChanged();
                    _ = UpdateAndSave();
                }
            }
        }


        public DateTime Timestamp
        {
            get { return _balance.Timestamp; }
            set
            {
                if (!_balance.Timestamp.Equals(value))
                {
                    //var timestamp = _balance.Timestamp;
                    _balance.Timestamp = value;
                    RaisePropertyChanged();
                    //SetProperty(ref timestamp, value);
                    _ = ChangeAndSave();
                }
            }
        }
        #endregion

        #region Commands
        public IMvxCommand DeleteThisCommand { get; private set; }
        public IMvxCommand OnDateSelectedCommand { get; private set; }
        #endregion

        #region Constructors
        public BalanceViewModel(IBackgroundHandler backgroundHandler, IDataManager dataManager, Balance balance) : base(backgroundHandler)
        {
            _dataManager = dataManager;
            _balance = balance;

            DeleteThisCommand = new MvxAsyncCommand(OnDeleteThis);
            OnDateSelectedCommand = new MvxAsyncCommand(ChangeAndSave);
        }

        #endregion

        #region Methods

        private async Task ChangeAndSave()
        {
            await _dataManager.SaveBalance(_balance);
            _backgroundHandler.SendMessage(new ChangeBalanceMessage(_balance.AccountID));
        }

        private async Task UpdateAndSave()
        {
            await _dataManager.SaveBalance(_balance);
        }

        //private async Task UpdateAccount()
        //{
        //    if(SelectedAccount != null)
        //    {
        //        var accts = await BudgetDatabase.GetBankAccounts();
        //        var acct = accts.Where(x => x.Nickname.Equals(SelectedAccount)).First();

        //        if(Bill.BankAccount.Nickname != acct.Nickname)
        //        {
        //            Bill.BankAccount = acct;

        //            //Switch to change and save because daterangeentry needs to requery
        //            ChangeAndSave();
        //        }

        //    }
        //}

        //private async Task UpdateAndSave()
        //{
        //    await BudgetDatabase.SaveBill(Bill);
        //    Messenger.Send(new UpdateBillMessage(Bill.AccountID));
        //}

        //private async Task ChangeAndSave()
        //{
        //    await BudgetDatabase.SaveBill(Bill);
        //    Messenger.Send(new ChangeBillMessage(Bill.AccountID));
        //}

        private async Task OnDeleteThis()
        {
            await _dataManager.DeleteBalance(_balance);
            _backgroundHandler.SendMessage(new ChangeBalanceMessage(_balance.AccountID));
        }


        //private async void LoadAccountOptions()
        //{
        //    var options = await BudgetDatabase.GetBankAccounts();
        //    AccountOptions.Clear();
        //    foreach (var item in options)
        //    {
        //        AccountOptions.Add(item.AccountID);
        //    }

        //}

        #endregion

    }
}
