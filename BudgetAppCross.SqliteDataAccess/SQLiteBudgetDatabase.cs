using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using BudgetAppCross.StateManagers;
using SQLiteHelpers;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace BudgetAppCross.SqliteDataAccess
{
    public class SQLiteBudgetDatabase : IDataManager
    {
        #region Singleton
        private static readonly Lazy<SQLiteBudgetDatabase> instance = new Lazy<SQLiteBudgetDatabase>();
        public static SQLiteBudgetDatabase Instance => instance.Value;

        private string connectionString => StateManager.Instance.DatabasePath;
        //private const SQLiteOpenFlags flags =
        //    // open the database in read/write mode
        //    SQLiteOpenFlags.ReadWrite |
        //    // create the database if it doesn't exist
        //    SQLiteOpenFlags.Create |
        //    // enable multi-threaded database access
        //    SQLiteOpenFlags.SharedCache |
        //    SQLiteOpenFlags.FullMutex;

        //static SQLiteConnection database;
        public SQLiteBudgetDatabase()
        {
            //connectionString = StateManager.Instance.DatabasePath;
        }
        #endregion

        #region Fields
        static bool initialized = false;
        private IAccountRepo AccountAccess;
        #endregion

        #region Properties
        public List<string> BankAccountNicknames { get; set; } = new List<string>();
        public List<string> PayeeNames { get; set; } = new List<string>();
        #endregion

        

        #region Init
        public async Task Initialize()
        {
            var accountTableName = "BankAccount";
            var balanceTableName = "Balance";
            var billTableName = "Bill";
            var transactionTableName = "BankTransaction";

            var accountTable = BuildBankAccountTable(accountTableName);
            var balanceTable = BuildBalanceTable(balanceTableName, accountTable);
            var billTable = BuildBillTable(billTableName, accountTable);
            //var transactionTable = BuildTransactionTable(transactionTableName, accountTable);

            SQLiteHelper.CreateTable(connectionString, accountTable);
            SQLiteHelper.CreateTable(connectionString, balanceTable);
            SQLiteHelper.CreateTable(connectionString, billTable);

            AccountAccess = new SQLiteAccountAccess(accountTableName);

            await UpdateBankAccountNames();
            //await UpdatePayeeNames();
            //return Task.CompletedTask;
            //CreateTable(transactionTable);
        }
        #endregion

        #region Tables
        private SQLiteTable BuildBankAccountTable(string tableName)
        {
            //Build BankAccount Table
            var accountTable = new SQLiteTable(tableName);
            accountTable.AddColumn(new SQLiteColumn("AccountId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            accountTable.AddColumn(new SQLiteColumn("NickName").WithDatatype("TEXT").IsUnique());
            accountTable.AddColumn(new SQLiteColumn("AccountNumber").WithDatatype("TEXT").AsNullable(true));
            accountTable.AddColumn(new SQLiteColumn("BankName").WithDatatype("TEXT").AsNullable(true));

            return accountTable;
        }

        private SQLiteTable BuildBalanceTable(string tableName, SQLiteTable accountTable)
        {
            //Build Balance Table
            var balanceTable = new SQLiteTable(tableName);
            balanceTable.AddColumn(new SQLiteColumn("BalanceId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            balanceTable.AddColumn(new SQLiteColumn("Amount").WithDatatype("FLOAT"));
            balanceTable.AddColumn(new SQLiteColumn("Timestamp").WithDatatype("TEXT"));
            balanceTable.AddColumn(new SQLiteColumn("AccountId"));
            balanceTable.AddForeignKey(new ForeignKey("FK_BalanceToBank", "AccountId", accountTable.TableName, "AccountId")
                .HasUpdateAction(ForeignKeyAction.CASCADE)
                .HasDeleteAction(ForeignKeyAction.CASCADE));
            return balanceTable;
        }

        private SQLiteTable BuildBillTable(string tableName, SQLiteTable accountTable)
        {
            //Build Bill Table
            var billTable = new SQLiteTable(tableName);
            billTable.AddColumn(new SQLiteColumn("BillId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            billTable.AddColumn(new SQLiteColumn("Date").WithDatatype("TEXT"));
            billTable.AddColumn(new SQLiteColumn("Amount").WithDatatype("FLOAT"));
            billTable.AddColumn(new SQLiteColumn("Payee").WithDatatype("TEXT"));
            billTable.AddColumn(new SQLiteColumn("IsPaid"));
            billTable.AddColumn(new SQLiteColumn("IsAuto"));
            billTable.AddColumn(new SQLiteColumn("AccountId"));
            billTable.AddForeignKey(new ForeignKey("FK_BillToBank", "AccountId", accountTable.TableName)
                .HasUpdateAction(ForeignKeyAction.CASCADE)
                .HasDeleteAction(ForeignKeyAction.SET_DEFAULT));

            return billTable;
        }
        #endregion

        #region BankAccount
        public Task<List<BankAccount>> GetBankAccounts()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveBankAccount(BankAccount acct)
        {
            if(acct.AccountId != 0)
            {
                var acctId = await AccountAccess.UpdateAccountAsync(acct);
                return acctId;
            }
            else
            {
                var result = await AccountAccess.InsertAccountAsync(acct);
                return result;
            }
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

        public async Task UpdateBankAccountNames()
        {
            var list = await AccountAccess.GetAllAccountsAsync();
            BankAccountNicknames.Clear();

            foreach (var item in list)
            {
                BankAccountNicknames.Add(item.Nickname);
            }
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

        public async Task UpdatePayeeNames()
        {
            
        }
        #endregion

    }
}
