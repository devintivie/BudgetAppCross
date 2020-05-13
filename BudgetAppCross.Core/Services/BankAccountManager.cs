using BudgetAppCross.Core.ViewModels;
using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BudgetAppCross.Core.Services
{
    public class BankAccountManager
    {

        #region Singleton
        private static readonly Lazy<BankAccountManager> instance = new Lazy<BankAccountManager>();
        public static BankAccountManager Instance => instance.Value;
        #endregion
        #region Fields

        #endregion

        #region Properties
        public Dictionary<string, BankAccount> AccountsByName { get; set; } = new Dictionary<string, BankAccount>(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, BankAccount> AccountsByID { get; set; } = new Dictionary<string, BankAccount>(StringComparer.OrdinalIgnoreCase);
        public List<BankAccount> AllAccounts { get; set; } = new List<BankAccount>();
        #endregion

        #region Methods
        public void Clear()
        {
            AccountsByName.Clear();
            AccountsByID.Clear();
            AllAccounts.Clear();
        }

        private bool CanAddAccount(BankAccount ba)
        {
            return CanAddAccount(ba.Nickname, ba.UniqueID);
        }
        private bool CanAddAccount(string name, string id)
        {
            //ok if id is not already in dict
            var idok = !AccountsByID.ContainsKey(id);

            //ok if name is not already in dict
            var nameok = !AccountsByName.ContainsKey(name);

            return idok && nameok;
        }

        public void AddAccount(BankAccount ba)
        {
            var random = new Random();
            if (ba.UniqueID.Equals("0000"))
            {
                do
                {
                    ba.UniqueID = new string(Enumerable.Repeat("0123456789", 4).Select(s => s[random.Next(s.Length)]).ToArray());

                } while (AccountsByID.ContainsKey(ba.UniqueID));
            }
            if (CanAddAccount(ba))
            {
                AccountsByName.Add(ba.Nickname, ba);
                AccountsByID.Add(ba.UniqueID, ba);
                AllAccounts.Add(ba);
            }
            if(!AccountsByID.ContainsKey(ba.UniqueID))
            {
                AccountsByID.Add(ba.UniqueID, ba);
            }
        }

        public void AddAccount(string nickname, string uid, string acctNumber = "-", string bankName = "-")
        {
            if (CanAddAccount(nickname, uid))
            {
                var acct = new BankAccount(0, acctNumber, bankName, nickname, uid);
                AddAccount(acct);
            }
        }

        //public void DeleteAccount(string name)
        //{
            
        //}

        public void DeleteAccount(BankAccount account)
        {
            AccountsByID.Remove(account.UniqueID);
            AccountsByName.Remove(account.Nickname);
            AllAccounts.Remove(account);
        }

        public void SwapAccountOrder(BankAccount ba1, BankAccount ba2)
        {
            if (AllAccounts.Contains(ba1) && AllAccounts.Contains(ba2))
            {
                var index2 = AllAccounts.IndexOf(ba2);
                AllAccounts.Remove(ba2);
                var index1 = AllAccounts.IndexOf(ba1);
                AllAccounts.Insert(index1, ba2);

                AllAccounts.Insert(index2, ba1);
            }
        }
        #endregion

    }
}
