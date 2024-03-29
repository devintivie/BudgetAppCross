﻿//using BudgetAppCross.Core.Services;
//using BudgetAppCross.Models;
//using MvvmCross;
//using MvvmCross.Navigation;
//using MvvmCross.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using Xamarin.Forms;

//namespace BudgetAppCross.Core.ViewModels
//{
//    public class NewBillsViewModel : MvxViewModel<Bill, bool>//<string, Bill>
//    {
//        #region Fields
//        private IMvxNavigationService navigationService;
//        private IDataManager DataManager = Mvx.IoCProvider.Resolve<IDataManager>();
//        //private BillTracker billTracker;
//        #endregion

//        #region Properties

//        public Bill Bill { get; private set; }

//        private string newPayee;
//        public string NewPayee
//        {
//            get { return newPayee; }
//            set
//            {
//                SetProperty(ref newPayee, value);
//            }
//        }


//        //public string Payee
//        //{
//        //    get { return Bill.Payee; }
//        //    set
//        //    {
//        //        var company = Bill.Payee;
//        //        Bill.Payee = value;
//        //        SetProperty(ref company, value);
//        //        //SaveBill();

//        //    }
//        //}

//        public DateTime Date
//        {
//            get { return Bill.Date; }
//            set
//            {
//                var dueDate = Bill.Date;
//                Bill.Date = value;
//                SetProperty(ref dueDate, value);
//                //SaveBill();
//                //MessagingCenter.Send(this, "UpdateTotal");

//            }
//        }

//        public double Amount
//        {
//            get { return Bill.Amount; }
//            set
//            {
//                var amountDue = Bill.Amount;
//                Bill.Amount = value;
//                SetProperty(ref amountDue, value);
//                //SaveBill();
//                //MessagingCenter.Send(this, "UpdateTotal");
//            }
//        }

//        public string Confirmation
//        {
//            get { return Bill.Confirmation; }
//            set
//            {
//                var confirmation = Bill.Confirmation;
//                Bill.Confirmation = value;
//                SetProperty(ref confirmation, value);
//                //SaveBill();
//            }
//        }

//        public bool IsPaid
//        {
//            get { return Bill.IsPaid; }
//            set
//            {
//                var isPaid = Bill.IsPaid;
//                Bill.IsPaid = value;
//                SetProperty(ref isPaid, value);
//                //SaveBill();
//            }
//        }

//        public bool IsAuto
//        {
//            get { return Bill.IsAuto; }
//            set
//            {
//                var isAuto = Bill.IsAuto;
//                Bill.IsAuto = value;
//                SetProperty(ref isAuto, value);
//                //SaveBill();
//            }
//        }

//        private ObservableCollection<string> accountOptions = new ObservableCollection<string>();
//        public ObservableCollection<string> AccountOptions
//        {
//            get { return accountOptions; }
//            set
//            {
//                SetProperty(ref accountOptions, value);
//            }
//        }

//        private ObservableCollection<string> payeeOptions;
//        public ObservableCollection<string> PayeeOptions
//        {
//            get { return payeeOptions; }
//            set
//            {
//                SetProperty(ref payeeOptions, value);
//            }
//        }

//        private ObservableCollection<MultipleBillOptions> multiBillOptions;
//        public ObservableCollection<MultipleBillOptions> MultiBillOptions
//        {
//            get { return multiBillOptions; }
//            set
//            {
//                SetProperty(ref multiBillOptions, value);
//            }
//        }

//        private MultipleBillOptions selectedBillOption;
//        public MultipleBillOptions SelectedBillOption
//        {
//            get { return selectedBillOption; }
//            set
//            {
//                SetProperty(ref selectedBillOption, value);
//            }
//        }



//        private bool isNewPayee;
//        public bool IsNewPayee
//        {
//            get { return isNewPayee; }
//            set
//            {
//                SetProperty(ref isNewPayee, value);
//            }
//        }

//        private bool addMultiple;
//        public bool AddMultiple
//        {
//            get { return addMultiple; }
//            set
//            {
//                SetProperty(ref addMultiple, value);
//            }
//        }




//        //public int SelectedAccount
//        //{
//        //    get { return Bill.AccountID; }
//        //    set
//        //    {
//        //        var selectedAccount = Bill.AccountID;
//        //        Bill.AccountID = value;
//        //        SetProperty(ref selectedAccount, value);
//        //    }
//        //}

//        private string selectedAccount;
//        public string SelectedAccount
//        {
//            get { return selectedAccount; }
//            set
//            {
//                SetProperty(ref selectedAccount, value);
//            }
//        }

//        private string selectedPayee;
//        public string SelectedPayee
//        {
//            get { return selectedPayee; }
//            set
//            {
//                SetProperty(ref selectedPayee, value);
//            }
//        }



//        #endregion

//        #region Commands
//        public ICommand SaveCommand { get; }
//        public ICommand CancelCommand { get; }
//        #endregion

//        #region Constructors
//        public NewBillsViewModel(IMvxNavigationService nav)
//        {
//            navigationService = nav;
//            //Bill = new Bill();
//            LoadAccountOptions();
//            LoadPayeeOptions();

//            SaveCommand = new Command(async () => await OnSave());
//            CancelCommand = new Command(async () => await OnCancel());

//            MultiBillOptions = new MvxObservableCollection<MultipleBillOptions>()
//            {
//                MultipleBillOptions.ByDateRange,
//                MultipleBillOptions.ByNumber
//            };
//            SelectedBillOption = MultipleBillOptions.ByDateRange;
//        }

//        public override void Prepare(Bill parameter)
//        {
//            Bill = parameter;
//        }

//        //public override void Prepare(string parameter)
//        //{
//        //    companyName = parameter;
//        //}
//        #endregion

//        #region Methods
//        private async Task OnSave()
//        {
//            if (IsNewPayee)
//            {
//                Bill.Payee = NewPayee;
//            }
//            else
//            {
//                Bill.Payee = SelectedPayee;
//            }

//            //await navigationService.Close(this, Bill);
//            //var accts = await BudgetDatabase.Instance.GetBankAccounts();
//            var accts = await DataManager.GetBankAccounts();
//            var acct = accts.Where(x => x.Nickname.Equals(SelectedAccount)).First();
//            Bill.BankAccount = acct;
//            //Bill.AccountID = acct.AccountID;
//            //await BudgetDatabase.Instance.SaveBill(Bill);
//            await DataManager.SaveBill(Bill);
//            Messenger.Instance.Send(new ChangeBillMessage(Bill.AccountID));
//            await DataManager.UpdatePayeeNames();
//            await navigationService.Close(this, true);
//        }

//        private async Task OnCancel()
//        {
//            await navigationService.Close(this);
//        }

//        private void LoadAccountOptions()
//        {
//            //var options = await BudgetDatabase.Instance.GetBankAccounts();
//            AccountOptions = new ObservableCollection<string>(DataManager.BankAccountNicknames);// await DataManager.GetBankAccounts();
//            //AccountOptions.Clear();
//            //foreach (var item in options)
//            //{
//            //    AccountOptions.Add(item.Nickname);
//            //}
//            if(AccountOptions.Count > 1)
//            {
//                SelectedAccount = AccountOptions.ElementAt(1);
//            }
//            else
//            {
//                SelectedAccount = AccountOptions.FirstOrDefault();
//            }
            

//        }

//        private async Task LoadPayeeOptions()
//        {
//            PayeeOptions = new ObservableCollection<string>(DataManager.PayeeNames);

//            if(PayeeOptions.Count == 0)
//            {
//                IsNewPayee = true;
//            }
//            else
//            {
//                SelectedPayee = PayeeOptions.First();
//            }

            

//        }

        

//        #endregion

//    }
//}
