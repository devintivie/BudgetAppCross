﻿//using BudgetAppCross.Models;
//using MvvmCross.Navigation;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using Xamarin.Forms;

//namespace BudgetAppCross.Core.ViewModels
//{
//    public class PaycheckBillViewModel : BaseViewModel
//    {
//        #region Fields

//        #endregion

//        #region Properties
//        public Bill AgendaBill { get; private set; }

//        public string Company
//        {
//            get { return AgendaBill.Company; }
//            set
//            {
//                var company = AgendaBill.Company;
//                AgendaBill.Company = value;
//                SetProperty(ref company, value);
//            }
//        }

//        public DateTime Date
//        {
//            get { return AgendaBill.Date; }
//            set
//            {
//                var dueDate = AgendaBill.Date;
//                AgendaBill.Date = value;
//                SetProperty(ref dueDate, value);
//                MessagingCenter.Send(this, "UpdateTotal");
//            }
//        }

//        public double Amount
//        {
//            get { return AgendaBill.Amount; }
//            set
//            {
//                var amountDue = AgendaBill.Amount;
//                AgendaBill.Amount = value;
//                SetProperty(ref amountDue, value);
//                MessagingCenter.Send(this, "UpdateTotal");
//            }
//        }

//        public string Confirmation
//        {
//            get { return AgendaBill.Confirmation; }
//            set
//            {
//                var confirmation = AgendaBill.Confirmation;
//                AgendaBill.Confirmation = value;
//                SetProperty(ref confirmation, value);
//            }
//        }

//        public bool IsPaid
//        {
//            get { return AgendaBill.IsPaid; }
//            set
//            {
//                var isPaid = AgendaBill.IsPaid;
//                AgendaBill.IsPaid = value;
//                SetProperty(ref isPaid, value);
//            }
//        }

//        public bool IsAuto
//        {
//            get { return AgendaBill.IsAuto; }
//            set
//            {
//                var isAuto = AgendaBill.IsPaid;
//                AgendaBill.IsPaid = value;
//                SetProperty(ref isAuto, value);
//            }
//        }

//        #endregion

//        #region Constructors
//        public PaycheckBillViewModel(AgendaBill bill)
//        {
//            AgendaBill = bill;
//        }
//        #endregion

//        #region Methods


//        #endregion

//    }
//}
