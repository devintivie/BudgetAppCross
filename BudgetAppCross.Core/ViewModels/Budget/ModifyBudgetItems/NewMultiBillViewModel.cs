using BaseViewModels;
using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross;
using MvvmCross.Navigation;
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
    public class NewMultiBillViewModel : MvxNavigationBaseViewModel, INewBillViewModel
    {
        #region Fields
        private int billNumber;
        #endregion

        #region Properties

        public string BillNumberString => $"Bill {billNumber}";

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                SetProperty(ref date, value);
            }
        }

        private decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set
            {
                SetProperty(ref amount, value);
            }
        }

        private string confirmation;
        public string Confirmation
        {
            get { return confirmation; }
            set
            {
                SetProperty(ref confirmation, value);
            }
        }

        private bool isPaid;
        public bool IsPaid
        {
            get { return isPaid; }
            set
            {
                SetProperty(ref isPaid, value);
            }
        }

        private bool isAuto;
        public bool IsAuto
        {
            get { return isAuto; }
            set
            {
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

        #region Constructors
        public NewMultiBillViewModel(int number, DateTime? dueDate = null)
        {
            billNumber = number;
            LoadAccountOptions();
            if(dueDate == null)
            {
                Date = DateTime.Today;
            }
            else
            {
                Date = (DateTime)dueDate;
            }
        }
        #endregion

        #region Methods
        private void LoadAccountOptions()
        {
            AccountOptions = new ObservableCollection<string>(BudgetDatabase_old.BankAccountNicknames);
            if (AccountOptions.Count > 1)
            {
                SelectedAccount = AccountOptions.ElementAt(1);
            }
            else
            {
                SelectedAccount = AccountOptions.FirstOrDefault();
            }
        }
        #endregion

    }
}
