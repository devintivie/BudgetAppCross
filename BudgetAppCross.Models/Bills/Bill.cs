﻿using Newtonsoft.Json;
//using SQLite;
//using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BudgetAppCross.Models
{
    [Serializable]
    public class Bill : IComparable
    {
        #region Fields
        const string DEFAULT_CONFIRMATION = "None";
        #endregion

        #region Properties
        public int BillId { get; set; }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    GetBillStatus();
                }
            }
        }

        public decimal Amount { get; set; }
        public string Payee { get; set; }

        private bool isPaid;
        public bool IsPaid
        {
            get { return isPaid; }
            set
            {
                if (isPaid != value)
                {
                    isPaid = value;
                    GetBillStatus();
                }
            }
        }

        private bool isAuto;
        public bool IsAuto
        {
            get { return isAuto; }
            set
            {
                if (isAuto != value)
                {
                    isAuto = value;
                    GetBillStatus();
                }
            }
        }

        public int AccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        public BillStatus BillStatus { get; set; }
        public string Confirmation { get; set; }


        #endregion

        #region Constructors
        public Bill() : this("", 0.0m, DateTime.Today) { }

        public Bill(string payee) : this(payee, 0.0m, DateTime.Today) { }

        public Bill(string name, int month, int day) : this(name, 0, month, day) { }

        public Bill(string name, decimal iAmount, int month, int day) : this(name, iAmount, new DateTime(DateTime.Now.Year, month, day)) { }

        //public Bill(double iAmount, int month, int day) : this(iAmount, new DateTime(DateTime.Now.Year, month, day), acctID) { }

        public Bill(string payee, decimal iAmount, DateTime iDueDate)
        {
            Payee = payee;
            Date = iDueDate;
            Amount = iAmount;
            Confirmation = DEFAULT_CONFIRMATION;
            IsPaid = false;
            GetBillStatus();
        }

        #endregion

        #region Methods
        private void GetBillStatus()
        {
            BillStatus = BillStatusHelper.GetBillStatus(DateTime.Today, Date, IsPaid, IsAuto);
        }

        public int CompareTo(object obj)
        {
            /*
             * If return < 0, the instance is earlier than obj
             * If return == 0, the instance is the same as obj
             * If return >0, the instance is later than obj
             */


            if (obj == null) return 1;

            if (obj is Bill otherBill)
                return Date.CompareTo(otherBill.Date);
            else
                throw new ArgumentException("Object is not a Bill Object");

        }

        /// <summary>
        /// Positive if bill2 is after bill1, Negative if bill2 is before bill1
        /// 0 if bill1 and bill2 are on the same day
        /// </summary>
        public static int CompareBillDate(Bill bill1, Bill bill2)
        {
            var dateCompare = (bill1.Date - bill2.Date).TotalDays;
            return (int)dateCompare;

        }

        /// <summary>
        /// Positive if date occurs after bill.Date, Negative if bill is before bill.Date
        /// 0 if bill.Date is on same day as date
        /// </summary>
        public static int CompareDate(Bill bill, DateTime date)
        {
            var dateCompare = (bill.Date - date).TotalDays;
            return (int)dateCompare;
        }

        override public string ToString()
        {
            string amountString = Amount.ToString("C", CultureInfo.CurrentCulture);
            string tempString = String.Format("{0} is due on {1:D}", Amount.ToString("C", CultureInfo.CurrentCulture), Date);

            if (isPaid)
            {
                tempString += ": Paid";
            }
            else
            {
                tempString += ": Need to paid";
            }
            return tempString;
        }
        #endregion
    }
}
