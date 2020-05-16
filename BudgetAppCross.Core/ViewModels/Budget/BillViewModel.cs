using BudgetAppCross.Models;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class BillViewModel : BaseViewModel
    {
        #region Fields

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
                BudgetDatabase.SaveBillAsync(Bill);

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
                BudgetDatabase.SaveBillAsync(Bill);
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
                BudgetDatabase.SaveBillAsync(Bill);
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
                BudgetDatabase.SaveBillAsync(Bill);
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
                BudgetDatabase.SaveBillAsync(Bill);
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
                BudgetDatabase.SaveBillAsync(Bill);
            }
        }

        #endregion

        #region Constructors
        public BillViewModel(Bill bill)
        {
            Bill = bill;
        }
        #endregion

        #region Methods
        #endregion

    }
}
