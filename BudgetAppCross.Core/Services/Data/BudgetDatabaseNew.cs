//using BudgetAppCross.Models;
//using SQLite;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace BudgetAppCross.Core.Services
//{
//    public class BudgetDatabaseNew : IDataManager
//    {
//        #region Fields
//        private const SQLite.SQLiteOpenFlags openFlags =
//            // open the database in read/write mode
//            SQLite.SQLiteOpenFlags.ReadWrite |
//            // create the database if it doesn't exist
//            SQLite.SQLiteOpenFlags.Create |
//            // enable multi-threaded database access
//            SQLite.SQLiteOpenFlags.SharedCache |
//            SQLite.SQLiteOpenFlags.FullMutex;

//        private string connectionString => StateManager.Instance.DatabasePath;
//        #endregion

//        #region Properties
//        public List<string> BankAccountNicknames { get; set; } = new List<string>();
//        public List<string> PayeeNames { get; set; } = new List<string>();
//        #endregion

//        #region Constructors

//        #endregion

//        #region Init
//        public async Task Initialize()
//        {
//            using (var conn = new SQLiteConnection(connectionString, openFlags))
//            {
//                //File.Delete(Constants.DatabasePath);
//                var accountTable = new SQLiteTable("BankAccount");
//                accountTable.AddColumn(new SQLiteColumn("AccountId").AsPrimaryKey().WithAutoIncrement().IsUnique());
//                accountTable.AddColumn(new SQLiteColumn("NickName").WithDatatype("TEXT").IsUnique());
//                accountTable.AddColumn(new SQLiteColumn("AccountNumber").WithDatatype("TEXT").AsNullable(true));
//                accountTable.AddColumn(new SQLiteColumn("BankName").WithDatatype("TEXT").AsNullable(true));

//                await conn.CreateTable

//                CreateTable(accountTable);

//                var newAcct = new BankAccount
//                {
//                    Nickname = "MainAccount",
//                    AccountNumber = "12345678",
//                    BankName = "Main Street Bank"
//                };

//                await SaveBankAccount(newAcct);
//            }
//        }

//        public void CreateTable()
//        {

//        }
//        #endregion






//        public Task<int> ChangePayeeName(string oldName, string newName)
//        {
//            throw new NotImplementedException();
//        }

//        public Task DeleteBalance(Balance balance)
//        {
//            throw new NotImplementedException();
//        }

//        public Task DeleteBankAccount(BankAccount acct)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<int> DeleteBill(Bill bill)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<int> DeleteBillsForPayee(string payee)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Balance> GetBalance(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<Balance>> GetBalances()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<BankAccount> GetBankAccount(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<int> GetBankAccountID(string name)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<BankAccount>> GetBankAccounts()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Bill> GetBill(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<string>> GetBillPayees()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<Bill>> GetBills()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<Bill>> GetBillsForPayee(string payee)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Balance> GetLatestBalance(int id, DateTime date)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Balance> GetLatestBalance(string name, DateTime date)
//        {
//            throw new NotImplementedException();
//        }

        

//        public Task SaveBalance(Balance balance)
//        {
//            throw new NotImplementedException();
//        }

//        public Task SaveBankAccount(BankAccount acct)
//        {
//            throw new NotImplementedException();
//        }

//        public Task SaveBill(Bill bill)
//        {
//            throw new NotImplementedException();
//        }

//        public Task SaveBills(IEnumerable<Bill> bills)
//        {
//            throw new NotImplementedException();
//        }

//        public Task UpdateBankAccountNames()
//        {
//            throw new NotImplementedException();
//        }

//        public Task UpdatePayeeNames()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
