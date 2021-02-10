using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using BudgetAppCross.StateManagers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAppCross.SQLiteDataAccess
{
    public class SQLiteBudgetDatabase : IDataManager
    {
        #region Fields
        private string connectionString => StateManager.Instance.DatabasePath;
        //private SQLiteOpenFlags flags => StateManager.Flags;

        #endregion

        #region Properties
        public List<string> BankAccountNicknames { get; set; } = new List<string>();
        public List<string> PayeeNames { get; set; } = new List<string>();

        public IAccountRepo AccountAccess { get; private set; }
        public IBillRepo BillRepo { get; private set; }
        public IBalanceRepo BalanceAccess { get; private set; }
        #endregion

        #region Constructors

        #endregion

        #region Init
        public async Task Initialize()
        {
            //File.Delete(Constants.DatabasePath);
            using (var connection = new ShortConnection(connectionString))
            {
                connection.CreateTable<BankAccount>(CreateFlags.None);
                connection.CreateTable<Bill>(CreateFlags.None);
                connection.CreateTable<Balance>(CreateFlags.None);
            }

            await UpdateBankAccountNames();
            await UpdatePayeeNames();
        }
        #endregion

        #region BankAccount
        public async Task<List<BankAccount>> GetBankAccounts()
        {
            var accts = new List<BankAccount>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        accts = conn.Query<BankAccount>(@"SELECT * FROM BankAccount");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            return accts;
        }

        public async Task UpdateBankAccountNames()
        {
            try
            {
                var allAccounts = await GetBankAccounts();
                BankAccountNicknames.Clear();
                foreach (var item in allAccounts)
                {
                    BankAccountNicknames.Add(item.Nickname);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        #endregion

        #region Balance

        #endregion

        #region Bill
        public async Task UpdatePayeeNames()
        {

            var distinctPayees = new List<string>();
            var distinctBills = new List<Bill>();
            await Task.Run(() =>
            {
                try
                {
                    
                    using (var conn = new ShortConnection(connectionString))
                    {
                         distinctBills = conn.Query<Bill>(@"SELECT DISTINCT Payee FROM Bill");
                    }

                    var tmp = distinctBills.Select(x => x.Payee);
                    PayeeNames = tmp.ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

        }
        #endregion

        public Task<int> ChangePayeeName(string oldName, string newName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBalance(Balance balance)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBankAccount(BankAccount acct)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteBill(Bill bill)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteBillsForPayee(string payee)
        {
            throw new NotImplementedException();
        }

        public Task<Balance> GetBalance(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Balance>> GetBalances()
        {
            throw new NotImplementedException();
        }

        public Task<BankAccount> GetBankAccount(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetBankAccountID(string name)
        {
            throw new NotImplementedException();
        }

        

        public Task<Bill> GetBill(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetBillPayees()
        {
            throw new NotImplementedException();
        }

        public Task<List<Bill>> GetBills()
        {
            throw new NotImplementedException();
        }

        public Task<List<Bill>> GetBillsDateRangeForAccount(DateTime start, DateTime end, string selectedAccount)
        {
            throw new NotImplementedException();
        }

        public Task<List<Bill>> GetBillsForPayee(string payee)
        {
            throw new NotImplementedException();
        }

        public Task<Balance> GetLatestBalance(int id, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<Balance> GetLatestBalance(string name, DateTime date)
        {
            throw new NotImplementedException();
        }


        public Task SaveBalance(Balance balance)
        {
            throw new NotImplementedException();
        }

        public Task SaveBankAccount(BankAccount acct)
        {
            throw new NotImplementedException();
        }

        public Task SaveBill(Bill bill)
        {
            throw new NotImplementedException();
        }

        public Task SaveBills(IEnumerable<Bill> bills)
        {
            throw new NotImplementedException();
        }

        

        
    }
}
