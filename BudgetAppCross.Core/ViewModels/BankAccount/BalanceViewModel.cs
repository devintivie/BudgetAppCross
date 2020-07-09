using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class BalanceViewModel : BaseViewModel
    {
        #region Fields

        #endregion

        #region Properties
        public Balance Balance { get; private set; }

        public decimal Amount
        {
            get { return Balance.Amount; }
            set
            {
                if(Balance.Amount != value)
                {
                    var amount = Balance.Amount;
                    Balance.Amount = value;
                    SetProperty(ref amount, value);
                    var _ = ChangeAndSave();
                }
            }
        }

        public DateTime Timestamp
        {
            get { return Balance.Timestamp; }
            set
            {
                if (!Balance.Timestamp.Equals(value))
                {
                    var timestamp = Balance.Timestamp;
                    Balance.Timestamp = value;
                    SetProperty(ref timestamp, value);
                    //var _ = ChangeAndSave();
                }
                
            }
        }



        #endregion

        #region Commands
        public ICommand DeleteThisCommand { get; private set; }
        public ICommand OnDateSelectedCommand { get; private set; }
        #endregion

        #region Constructors
        public BalanceViewModel(Balance balance)
        {
            Balance = balance;

            DeleteThisCommand = new Command(async () => await OnDeleteThis());
            OnDateSelectedCommand = new Command(async () => await ChangeAndSave());
        }

        #endregion

        #region Methods

        private async Task ChangeAndSave()
        {
            await BudgetDatabase.SaveBalance(Balance);
            Messenger.Send(new ChangeBalanceMessage(Balance.AccountID));
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
            await BudgetDatabase.DeleteBalance(Balance);
            Messenger.Send(new ChangeBalanceMessage(Balance.AccountID));
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
