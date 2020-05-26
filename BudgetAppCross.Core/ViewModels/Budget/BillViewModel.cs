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
    public class BillViewModel : BaseViewModel
    {
        #region Fields

        #endregion

        #region Properties
        public Bill Bill { get; private set; }

        public string Payee
        {
            get { return Bill.Payee; }
            set
            {
                if(Bill.Payee != value)
                {
                    var payee = Bill.Payee;
                    Bill.Payee = value;
                    SetProperty(ref payee, value);
                }
                
            }
        }

        public DateTime Date
        {
            get { return Bill.Date; }
            set
            {
                if(!Bill.Date.Equals(value))
                {
                    var dueDate = Bill.Date;
                    Bill.Date = value;
                    SetProperty(ref dueDate, value);

                    //Switch to change and save because daterangeentry needs to requery
                    ChangeAndSave();

                }
            }
        }

        public double Amount
        {
            get { return Bill.Amount; }
            set
            {
                if(Bill.Amount != value)
                {
                    var amountDue = Bill.Amount;
                    Bill.Amount = value;
                    SetProperty(ref amountDue, value);

                    UpdateAndSave();
                    
                    //MessagingCenter.Send(this, "Update");
                    //Messenger.Send(new UpdateBillMessage());
                }
                
            }
        }

        public string Confirmation
        {
            get { return Bill.Confirmation; }
            set
            {
                var confirmation = Bill.Confirmation;
                Bill.Confirmation = value;
                SetProperty(ref confirmation, value);
            }
        }

        public bool IsPaid
        {
            get { return Bill.IsPaid; }
            set
            {
                var isPaid = Bill.IsPaid;
                Bill.IsPaid = value;
                SetProperty(ref isPaid, value);
            }
        }

        public bool IsAuto
        {
            get { return Bill.IsAuto; }
            set
            {
                var isAuto = Bill.IsPaid;
                Bill.IsPaid = value;
                SetProperty(ref isAuto, value);
            }
        }

        private ObservableCollection<string> accountOptions = new ObservableCollection<string>();
        public ObservableCollection<string> AccountOptions
        {
            get { return accountOptions; }
            set
            {
                SetProperty(ref accountOptions, value);
            }
        }

        //private string selectedAccount;
        //public string SelectedAccount
        //{
        //    get { return Bill.AccountID; }
        //    set
        //    {
        //        var selectedAccount = Bill.AccountID;
        //        Bill.AccountID = value;
        //        SetProperty(ref selectedAccount, value);
        //    }
        //}

        private string selectedAccount;
        public string SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                if(selectedAccount != value)
                {
                    SetProperty(ref selectedAccount, value);
                    UpdateAccount();
                }
                
            }
        }

        #endregion

        #region Commands
        public ICommand DeleteThisCommand { get; private set; }
        #endregion

        #region Constructors
        public BillViewModel(Bill bill)
        {
            Bill = bill;

            DeleteThisCommand = new Command(async () => await OnDeleteThis());

            LoadAccountOptions();
        }

        //public BillViewModel(Bill bill, List<string> options)
        //{
        //    Bill = bill;
        //    AccountOptions = new ObservableCollection<string>(options);
        //    SelectedAccount = Bill.BankAccount.Nickname;
        //}
        #endregion

        #region Methods
        //private async void SaveBill()
        //{
        //    await BudgetDatabase.SaveBill(Bill);
        //}

        private async Task LoadAccountOptions()
        {
            
            AccountOptions = new ObservableCollection<string>(BudgetDatabase.BankAccountNicknames);
            SelectedAccount = AccountOptions.Where(x => x.Equals(Bill.BankAccount.Nickname)).FirstOrDefault();

        }

        private async Task UpdateAccount()
        {
            if(SelectedAccount != null)
            {
                var accts = await BudgetDatabase.GetBankAccounts();
                var acct = accts.Where(x => x.Nickname.Equals(SelectedAccount)).First();

                if(Bill.BankAccount.Nickname != acct.Nickname)
                {
                    Bill.BankAccount = acct;

                    //Switch to change and save because daterangeentry needs to requery
                    ChangeAndSave();
                }

            }
        }

        private async Task UpdateAndSave()
        {
            await BudgetDatabase.SaveBill(Bill);
            Messenger.Send(new UpdateBillMessage(Bill.AccountID));
        }

        private async Task ChangeAndSave()
        {
            await BudgetDatabase.SaveBill(Bill);
            Messenger.Send(new ChangeBillMessage(Bill.AccountID));
        }

        private async Task OnDeleteThis()
        {
            await BudgetDatabase.DeleteBill(Bill);
            Messenger.Send(new ChangeBillMessage(Bill.AccountID));
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
