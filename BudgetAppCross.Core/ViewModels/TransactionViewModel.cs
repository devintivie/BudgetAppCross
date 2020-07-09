using BudgetAppCross.Models;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    //public class TransactionViewModel : BaseViewModel
    //{
    //    #region Fields

    //    #endregion

    //    #region Properties
    //    public Transaction Transaction { get; private set; }

    //    public string Company
    //    {
    //        get { return Transaction.SourceDest; }
    //        set
    //        {
    //            var company = Transaction.SourceDest;
    //            Transaction.SourceDest = value;
    //            SetProperty(ref company, value);
    //        }
    //    }

    //    public DateTime Date
    //    {
    //        get { return Transaction.Date; }
    //        set
    //        {
    //            var dueDate = Transaction.Date;
    //            Transaction.Date = value;
    //            SetProperty(ref dueDate, value);
    //            MessagingCenter.Send(this, "UpdateTotal");
    //        }
    //    }

    //    public double Amount
    //    {
    //        get { return Transaction.Amount; }
    //        set
    //        {
    //            var amountDue = Transaction.Amount;
    //            Transaction.Amount = value;
    //            SetProperty(ref amountDue, value);
    //            MessagingCenter.Send(this, "UpdateTotal");
    //        }
    //    }

    //    public string Confirmation
    //    {
    //        get { return Transaction.Confirmation; }
    //        set
    //        {
    //            var confirmation = Transaction.Confirmation;
    //            Transaction.Confirmation = value;
    //            SetProperty(ref confirmation, value);
    //        }
    //    }
    //    #endregion

    //    #region Constructors
    //    public TransactionViewModel(Transaction trans)
    //    {
    //        Transaction = trans;
    //    }
    //    #endregion

    //    #region Methods

    //    #endregion

    //}
}
