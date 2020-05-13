using BudgetAppCross.Models;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BudgetAppCross.Core.ViewModels
{
    public class AgendaBillViewModel : BaseViewModel
    {
        #region Fields

        #endregion

        #region Properties
        public AgendaBill AgendaBill { get; private set; }

        public string Company
        {
            get { return AgendaBill.Company; }
            set
            {
                var company = AgendaBill.Company;
                AgendaBill.Company = value;
                SetProperty(ref company, value);
            }
        }

        public DateTime DueDate
        {
            get { return AgendaBill.DueDate; }
            set
            {
                var dueDate = AgendaBill.DueDate;
                AgendaBill.DueDate = value;
                SetProperty(ref dueDate, value);
                MessagingCenter.Send(this, "UpdateTotal");
            }
        }

        public double AmountDue
        {
            get { return AgendaBill.AmountDue; }
            set
            {
                var amountDue = AgendaBill.AmountDue;
                AgendaBill.AmountDue = value;
                SetProperty(ref amountDue, value);
                MessagingCenter.Send(this, "UpdateTotal");
            }
        }

        public string Confirmation
        {
            get { return AgendaBill.Confirmation; }
            set
            {
                var confirmation = AgendaBill.Confirmation;
                AgendaBill.Confirmation = value;
                SetProperty(ref confirmation, value);
            }
        }

        public bool IsPaid
        {
            get { return AgendaBill.IsPaid; }
            set
            {
                var isPaid = AgendaBill.IsPaid;
                AgendaBill.IsPaid = value;
                SetProperty(ref isPaid, value);
            }
        }

        public bool IsAuto
        {
            get { return AgendaBill.IsAuto; }
            set
            {
                var isAuto = AgendaBill.IsPaid;
                AgendaBill.IsPaid = value;
                SetProperty(ref isAuto, value);
            }
        }

        #endregion

        #region Constructors
        public AgendaBillViewModel(AgendaBill bill)
        {
            AgendaBill = bill;
        }
        #endregion

        #region Methods

        
        #endregion

    }
}
