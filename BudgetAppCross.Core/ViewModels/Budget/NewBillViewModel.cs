using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
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
    public class NewBillViewModel : BaseViewModel//<string, Bill>
    {
        #region Fields
        private IMvxNavigationService navigationService;
        //private BillTracker billTracker;
        #endregion

        #region Properties

        public Bill Bill { get; private set; }

        public string Company
        {
            get { return Bill.Payee; }
            set
            {
                var company = Bill.Payee;
                Bill.Payee = value;
                SetProperty(ref company, value);
                //SaveBill();

            }
        }

        public DateTime Date
        {
            get { return Bill.Date; }
            set
            {
                var dueDate = Bill.Date;
                Bill.Date = value;
                SetProperty(ref dueDate, value);
                //SaveBill();
                MessagingCenter.Send(this, "UpdateTotal");

            }
        }

        public double Amount
        {
            get { return Bill.Amount; }
            set
            {
                var amountDue = Bill.Amount;
                Bill.Amount = value;
                SetProperty(ref amountDue, value);
                //SaveBill();
                MessagingCenter.Send(this, "UpdateTotal");
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
                //SaveBill();
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
                //SaveBill();
            }
        }

        public bool IsAuto
        {
            get { return Bill.IsAuto; }
            set
            {
                var isAuto = Bill.IsAuto;
                Bill.IsAuto = value;
                SetProperty(ref isAuto, value);
                //SaveBill();
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

        //public int SelectedAccount
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
                SetProperty(ref selectedAccount, value);
            }
        }


        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Constructors
        public NewBillViewModel(IMvxNavigationService nav)
        {
            navigationService = nav;
            Bill = new Bill();
            LoadAccountOptions();

            SaveCommand = new Command(async () => await OnSave());
            CancelCommand = new Command(async () => await OnCancel());
        }

        //public override void Prepare(string parameter)
        //{
        //    companyName = parameter;
        //}
        #endregion

        #region Methods
        private async Task OnSave()
        {
            //await navigationService.Close(this, Bill);
            var accts = await BudgetDatabase.GetBankAccounts();
            var acct = accts.Where(x => x.Nickname.Equals(SelectedAccount)).First();
            Bill.BankAccount = acct;
            //Bill.AccountID = acct.AccountID;
            await BudgetDatabase.Instance.SaveBill(Bill);
            await navigationService.Close(this);
        }

        private async Task OnCancel()
        {
            await navigationService.Close(this);
        }

        private async void LoadAccountOptions()
        {
            var options = await BudgetDatabase.GetBankAccounts();
            AccountOptions.Clear();
            foreach (var item in options)
            {
                AccountOptions.Add(item.Nickname);
            }
            SelectedAccount = AccountOptions.FirstOrDefault();

        }

        #endregion

    }
}
