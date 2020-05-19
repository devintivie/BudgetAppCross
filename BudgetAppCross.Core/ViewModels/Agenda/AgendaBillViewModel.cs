using BudgetAppCross.Models;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class AgendaBillViewModel : BillViewModel
    {
        public AgendaBillViewModel(Bill bill) : base(bill)
        {
        }
    }
    //public class AgendaBillViewModel : BaseViewModel
    //{
    //    #region Fields

    //    #endregion

    //    #region Properties
    //    public Bill AgendaBill { get; private set; }

    //    public string Payee
    //    {
    //        get { return AgendaBill.Payee; }
    //        set
    //        {
    //            var company = AgendaBill.Payee;
    //            AgendaBill.Payee = value;
    //            SetProperty(ref company, value);
    //        }
    //    }

    //    public DateTime Date
    //    {
    //        get { return AgendaBill.Date; }
    //        set
    //        {
    //            var dueDate = AgendaBill.Date;
    //            AgendaBill.Date = value;
    //            SetProperty(ref dueDate, value);
    //            MessagingCenter.Send(this, "UpdateTotal");
    //        }
    //    }

    //    public double Amount
    //    {
    //        get { return AgendaBill.Amount; }
    //        set
    //        {
    //            var amountDue = AgendaBill.Amount;
    //            AgendaBill.Amount = value;
    //            SetProperty(ref amountDue, value);
    //            MessagingCenter.Send(this, "UpdateTotal");
    //        }
    //    }

    //    public string Confirmation
    //    {
    //        get { return AgendaBill.Confirmation; }
    //        set
    //        {
    //            var confirmation = AgendaBill.Confirmation;
    //            AgendaBill.Confirmation = value;
    //            SetProperty(ref confirmation, value);
    //        }
    //    }

    //    public bool IsPaid
    //    {
    //        get { return AgendaBill.IsPaid; }
    //        set
    //        {
    //            var isPaid = AgendaBill.IsPaid;
    //            AgendaBill.IsPaid = value;
    //            SetProperty(ref isPaid, value);
    //        }
    //    }

    //    public bool IsAuto
    //    {
    //        get { return AgendaBill.IsAuto; }
    //        set
    //        {
    //            var isAuto = AgendaBill.IsPaid;
    //            AgendaBill.IsPaid = value;
    //            SetProperty(ref isAuto, value);
    //        }
    //    }

    //    private ObservableCollection<string> accountOptions = new ObservableCollection<string>();
    //    public ObservableCollection<string> AccountOptions
    //    {
    //        get { return accountOptions; }
    //        set
    //        {
    //            SetProperty(ref accountOptions, value);
    //        }
    //    }

    //    private string selectedAccount;
    //    public string SelectedAccount
    //    {
    //        get { return selectedAccount; }
    //        set
    //        {
    //            SetProperty(ref selectedAccount, value);
    //        }
    //    }

    //    #endregion

    //    #region Constructors
    //    public AgendaBillViewModel(Bill bill)
    //    {
    //        AgendaBill = bill;
    //    }
    //    #endregion

    //    #region Methods
    //    private async void LoadAccountOptions()
    //    {
    //        var options = await BudgetDatabase.GetBankAccounts();
    //        AccountOptions.Clear();
    //        foreach (var item in options)
    //        {
    //            AccountOptions.Add(item.Nickname);
    //        }
    //        SelectedAccount = AccountOptions.FirstOrDefault();

    //    }

    //    #endregion

    //}
}
