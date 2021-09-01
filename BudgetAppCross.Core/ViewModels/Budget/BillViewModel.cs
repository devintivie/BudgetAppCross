﻿using BaseClasses;
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
    public class BillViewModel : BaseViewModel
    {
        #region Fields
        private bool initialAccountSet = false;
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
                SetProperty(ref accountOptions, value);
            }
        }

        //private string selectedAccount;
        //public string SelectedAccount
        //{
        //    get { return selectedAccount; }
        //    set
        //    {
        //        if(selectedAccount != value)
        //        {
        //            SetProperty(ref selectedAccount, value);
        //            var _ = UpdateAccount();
        //        }
                
        //    }
        //}

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
        public IMvxCommand DeleteThisCommand { get; private set; }
        public IMvxCommand OnDateSelectedCommand { get; private set; }
        #endregion

        #region Constructors
        public BillViewModel(IBackgroundHandler backgroundHandler, IDataManager dataManager, Bill bill) : base(backgroundHandler)
        {
            _dataManager = dataManager;
            Bill = bill;

            DeleteThisCommand = new MvxAsyncCommand(OnDeleteThis);
            OnDateSelectedCommand = new MvxAsyncCommand(ChangeAndSave);

            _ = LoadAccountOptions();
        }

        //public override void Prepare(int id)
        //{
        //    //CompanyName = parameter;
        //}

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
            AccountOptions = new ObservableCollection<string>(_dataManager.BankAccountNicknames);
            SelectedAccount = AccountOptions.Where(x => x.Equals(Bill.BankAccount.Nickname)).FirstOrDefault();

        }

        private async Task UpdateAccount()
        {
            if(SelectedAccount != null && initialAccountSet)
            {
                var acctId = await _dataManager.GetBankAccountID(SelectedAccount);

                Bill.AccountID = acctId;
                await ChangeAndSave();
                //var accts = await BudgetDatabase.GetBankAccounts();
                //var acct = accts.Where(x => x.Nickname.Equals(SelectedAccount)).First();
                //if(Bill.BankAccount == null)
                //{
                //    Bill.BankAccount = acct;

                //    //Switch to change and save because daterangeentry needs to requery
                //    ChangeAndSave();
                //}
                //else
                //{
                //    if (Bill.BankAccount.Nickname != acct.Nickname)
                //    {
                //        Bill.BankAccount = acct;

                //        //Switch to change and save because daterangeentry needs to requery
                //        ChangeAndSave();
                //    }
                //}

            }
            else
            {
                initialAccountSet = true;
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
