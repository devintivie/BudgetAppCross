using BudgetAppCross.Models;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Core.ViewModels
{
    public class BillDetailsViewModel : BaseViewModel<Bill>
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        public Bill Bill { get; private set; }

        public string Payee => Bill.Payee;

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

                    var _ = UpdateAndSave();
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

                    var _ = UpdateAndSave();
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

                    var _ = UpdateAndSave();
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

                    var _ = UpdateAndSave();
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

                    var _ = UpdateAndSave();
                }
            }
        }

        private ObservableCollection<string> accountOptions = new ObservableCollection<string>();
        public ObservableCollection<string> AccountOptions
        {
            get { return accountOptions; }
            set
            {
                if (accountOptions != value)
                {
                    accountOptions = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string selectedAccount;
        public string SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                if (selectedAccount != value)
                {
                    selectedAccount = value;
                    var _ = UpdateAccount();
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors
        public BillDetailsViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;

        }

        public override void Prepare(Bill parameter)
        {
            Bill = parameter;

            var _ = LoadAccountOptions();
        }
        #endregion

        #region Methods
        private async Task UpdateAndSave()
        {
            await BudgetDatabase.SaveBill(Bill);
            Messenger.Send(new UpdateBillMessage(Bill.AccountID));
        }

        private async Task ChangeAndSave()
        {
            await BudgetDatabase.SaveBill(Bill);
            Messenger.Send(new ChangeBillMessage(Bill.AccountID));
        }

        private async Task UpdateAccount()
        {
            if (SelectedAccount != null)
            {
                var accts = await BudgetDatabase.GetBankAccounts();
                var acct = accts.Where(x => x.Nickname.Equals(SelectedAccount)).First();
                if (Bill.BankAccount == null)
                {
                    Bill.BankAccount = acct;

                    //Switch to change and save because daterangeentry needs to requery
                    await ChangeAndSave();
                }
                else
                {
                    if (Bill.BankAccount.Nickname != acct.Nickname)
                    {
                        Bill.BankAccount = acct;

                        //Switch to change and save because daterangeentry needs to requery
                        await ChangeAndSave();
                    }
                }

            }
        }

        private Task LoadAccountOptions()
        {
            AccountOptions = new ObservableCollection<string>(BudgetDatabase.BankAccountNicknames);
            
            SelectedAccount = AccountOptions.Where(x => x.Equals(Bill.BankAccount.Nickname)).Single();
            return Task.CompletedTask;
        }
        #endregion



    }
}
