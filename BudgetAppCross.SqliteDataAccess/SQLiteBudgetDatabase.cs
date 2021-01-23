using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetAppCross.SqliteDataAccess
{
    public class SQLiteBudgetDatabase : IDataManager
    {
        #region Singleton
        private static readonly Lazy<SQLiteBudgetDatabase> instance = new Lazy<SQLiteBudgetDatabase>();
        public static SQLiteBudgetDatabase Instance => instance.Value;

        //static SQLiteConnection database;
        public SQLiteBudgetDatabase()
        {

        }
        #endregion

        #region Fields
        static bool initialized = false;
        #endregion

        #region Properties
        public List<string> BankAccountNicknames { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<string> PayeeNames { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        #region Init
        public Task Initialize()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region BankAccount
        public Task<List<BankAccount>> GetBankAccounts()
        {
            throw new NotImplementedException();
        }

        public Task SaveBankAccount(BankAccount acct)
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

        public Task DeleteBankAccount(BankAccount acct)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBankAccountNames()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Balance
        public Task<List<Balance>> GetBalances()
        {
            throw new NotImplementedException();
        }

        public Task SaveBalance(Balance balance)
        {
            throw new NotImplementedException();
        }

        public Task<Balance> GetBalance(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBalance(Balance balance)
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
        #endregion

        #region Bill
        public Task<List<Bill>> GetBills()
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

        public Task<Bill> GetBill(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteBill(Bill bill)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetBillPayees()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteBillsForPayee(string payee)
        {
            throw new NotImplementedException();
        }

        public Task<int> ChangePayeeName(string oldName, string newName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Bill>> GetBillsForPayee(string payee)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePayeeNames()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
