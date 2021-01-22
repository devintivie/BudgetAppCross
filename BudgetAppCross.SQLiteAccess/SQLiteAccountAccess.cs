using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using Dapper;
using BudgetAppCross.StateManagers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.SQLiteAccess
{
    public class SQLiteAccountAccess : IAccountRepo
    {
        #region Fields
        public string TableName { get; private set; }
        public string ConnectionString => StateManager.Instance.DatabasePath;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public SQLiteAccountAccess(string name)
        {
            TableName = name;
        }
        #endregion

        #region Methods
        public async Task<InsertResult> InsertAccountAsync(BankAccount acct)
        {
            var rowsAffected = 0;
            var lastId = 0;
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    rowsAffected = await conn.ExecuteAsync($"insert into {TableName} (NickName, AccountNumber, BankName) values (@NickName, @AccountNumber, @BankName)", acct);
                    lastId = conn.ExecuteScalar<int>($"select max(AccountId) from {TableName}");
                }
                catch (SQLiteException sqle)
                {
                    Debug.WriteLine(sqle.Message);
                }
            }
            //acct.AccountID = lastId;
            //return acct;
            return new InsertResult(rowsAffected, lastId);
        }

        public async Task<int> DeleteAccountAsync(int accountId)
        {
            var rowsAffected = 0;
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                rowsAffected = await conn.ExecuteAsync("delete from BankAccount where AccountId=@ID", new { ID = accountId });
            }
            return rowsAffected;
        }

        public async Task<List<BankAccount>> GetAllAccountsAsync()
        {
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                var output = await conn.QueryAsync<BankAccount>($"SELECT * from { TableName }");
                return output.ToList();
            }
        }

        public async Task<BankAccount> GetAccountAsync(string nickname)
        {
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                var output = await conn.QueryAsync<BankAccount>("select * from BankAccount where NickName = @Nickname", new { Nickname = nickname });
                return output.Single();
            }
        }

        public async Task<int> UpdateAccountAsync(BankAccount acct)
        {
            var rowsAffected = 0;
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                rowsAffected = await conn.ExecuteAsync(@"UPDATE BankAccount SET 
                    Nickname = @Nickname,
                    AccountNumber = @AccountNumber,
                    BankName = @BankName
                    WHERE AccountId = @AccountId", acct);
            }
            return rowsAffected;
        }


        #endregion
    }
}
