using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetAppCross.DataAccess;
using BudgetAppCross.StateManagers;
using BudgetAppCross.Models;
using SQLiteHelpers;

namespace BudgetAppCross.SqliteDataAccess
{
    public class SQLiteAccountAccess : IAccountRepo
    {
        #region Fields
        public const SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache |
            SQLiteOpenFlags.FullMutex;
        #endregion

        #region Properties
        public string TableName { get; private set; }
        public string ConnectionString => StateManager.Instance.DatabasePath;
        #endregion

        #region Constructors
        public SQLiteAccountAccess(string name)
        {
            TableName = name;
        }
        #endregion

        #region Methods
        public async Task<int> InsertAccountAsync(BankAccount acct)
        {
            var rowsAffected = 0;
            var lastId = 0;
            //using (var conn = new SQLiteConnection(ConnectionString, Flags))
            //{
            //    try
            //    {
            //        //rowsAffected = conn.Execute()
            //        ////IDbConnection
            //        //rowsAffected = Execute    conn.ExecuteAsync($"insert into {TableName} (NickName, AccountNumber, BankName) values (@NickName, @AccountNumber, @BankName)", acct);
            //        lastId = conn.ExecuteScalar<int>($"select max(AccountId) from {TableName}");
            //    }
            //    catch (SQLiteException sqle)
            //    {
            //        Debug.WriteLine(sqle.Message);
            //    }
            //}
            //acct.AccountID = lastId;
            //return acct;
            return lastId;
        }

        public async Task<int> DeleteAccountAsync(int transactionId)
        {
            var rowsAffected = 0;
            //using (var conn = new SQLiteConnection(ConnectionString, Flags))
            //{
            //    rowsAffected = await conn.ExecuteAsync("delete from BankAccount where AccountId=@ID", new { ID = transactionId });
            //}
            return rowsAffected;
        }

        public async Task<List<BankAccount>> GetAllAccountsAsync()
        {
            //using (var conn = new SQLiteConnection(ConnectionString, Flags))
            //{
            //    var output = await conn.QueryAsync<BankAccount>($"SELECT * from { TableName }");
            //    return output.ToList();
            //}
            return new List<BankAccount>();
        }

        public async Task<BankAccount> GetAccountAsync(string nickname)
        {
            //using(var conn = new SQLiteConnection(ConnectionString, Flags))
            //{
            //    var output = await conn.QueryAsync<BankAccount>("select * from BankAccount where NickName = @Nickname", new { Nickname = nickname });
            //    return output.Single();
            //}
            return new BankAccount();
        }

        public async Task<int> UpdateAccountAsync(BankAccount acct)
        {
            var rowsAffected = 0;
            //using (var conn = new SQLiteConnection(ConnectionString, Flags))
            //{
            //    rowsAffected = await conn.ExecuteAsync(@"UPDATE BankAccount SET 
            //        Nickname = @Nickname,
            //        AccountNumber = @AccountNumber,
            //        BankName = @BankName
            //        WHERE AccountId = @AccountId", acct);
            //}
            return rowsAffected;
        }


        #endregion
    }
}
