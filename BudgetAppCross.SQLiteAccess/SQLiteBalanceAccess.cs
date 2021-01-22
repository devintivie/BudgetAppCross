using BudgetAppCross.DataAccess;
using BudgetAppCross.StateManagers;
using BudgetAppCross.Models;
using Dapper;
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
    public class SQLiteBalanceAccess : IBalanceRepo
    {
        #region Fields

        #endregion

        #region Properties
        public string ConnectionString => StateManager.Instance.DatabasePath;

        public string TableName { get; private set; }
        #endregion

        #region Constructors
        public SQLiteBalanceAccess(string name)
        {
            TableName = name;
        }
        #endregion

        #region Methods

        public async Task<InsertResult> InsertBalanceAsync(Balance balance)
        {
            var rowsAffected = 0;
            var lastId = 0;
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    rowsAffected = await conn.ExecuteAsync($"insert into {TableName} (Timestamp, Amount, AccountId) values (@Timestamp, @Amount, @AccountId)", balance);
                    lastId = await conn.ExecuteScalarAsync<int>($"select max(BalanceId) from {TableName}");
                }
                catch (SQLiteException sqle)
                {
                    Debug.WriteLine(sqle.Message);
                }
            }

            return new InsertResult(rowsAffected, lastId);
        }

        public async Task<int> DeleteBalanceAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Balance>> GetAllBalancesAsync()
        {
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                //var output = await conn.QueryAsync<Balance>($"SELECT * from { TableName }");
                var query = $@"SELECT bal.*, acc.* from {TableName} bal
                            left join BankAccount acc
                            on acc.AccountId = bal.AccountId";
                var output = await conn.QueryAsync<Balance, BankAccount, Balance>(query, (balance, account) => { balance.BankAccount = account; return balance; }, splitOn: "AccountId");
                return output.ToList();
            }
        }

        public async Task<BankAccount> GetBalanceAsync(string nickname)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateBalanceAsync(Balance balance)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
