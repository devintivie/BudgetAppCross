using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Core.Services
{
	
    public class SQLiteBudgetDatabase : IDataManager
    {
        public List<string> BankAccountNicknames { get; set; } = new List<string>();
        public List<string> PayeeNames { get; set; } = new List<string>();

        private string connectionString => StateManager.Instance.DatabasePath;

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

        public async Task<List<BankAccount>> GetBankAccounts()
        {
            try
            {
                using (var connection = new XamarinSQLiteConnection(connectionString))
                {
                    var output = connection.Query<BankAccount>("select * from BankAccount");
                    //connection.Open();
                    //object temp;
                    //using (var cmd = new SQLiteCommand(@"select * from BankAccount", connection))
                    //{
                    //    temp = cmd.ExecuteR();
                    //    Console.WriteLine();
                    //}
                    //var output = await connection.ExecuteScalarAsync        //QueryAsync<BankAccount>("select * from BankAccount");
                    //return new List<BankAccount>();//   output.ToList();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new List<BankAccount>();
            
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

        public async Task Initialize()
        {

            //SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

            //File.Delete(Constants.DatabasePath);
            var accountTable = new SQLiteTable("BankAccount");
            accountTable.AddColumn(new SQLiteColumn("AccountId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            accountTable.AddColumn(new SQLiteColumn("NickName").WithDatatype("TEXT").IsUnique());
            accountTable.AddColumn(new SQLiteColumn("AccountNumber").WithDatatype("TEXT").AsNullable(true));
            accountTable.AddColumn(new SQLiteColumn("BankName").WithDatatype("TEXT").AsNullable(true));

            CreateTable(accountTable);

            var newAcct = new BankAccount
            {
                Nickname = "MainAccount",
                AccountNumber = "12345678",
                BankName = "Main Street Bank"
            };

            await SaveBankAccount(newAcct);
            //connection.CreateTable<BankAccount>(CreateFlags.None);
            //connection.CreateTable<Bill>(CreateFlags.None);
            //connection.CreateTable<Balance>(CreateFlags.None);

            //Database.CreateTable<BankAccount>(CreateFlags.None);
            //Database.CreateTable<Bill>(CreateFlags.None);
            //Database.CreateTable<Balance>(CreateFlags.None);

            //initialized = true;
            Console.WriteLine("Updating Account Names");

            await UpdateBankAccountNames();
        }

        public void CreateTable(SQLiteTable table)
        {
            try
            {
                using (var connection = new XamarinSQLiteConnection(connectionString))
                {
                    Debug.WriteLine("WTF");
                    connection.Open();
                    Debug.WriteLine("Open");
                    var cmd = new SQLiteCommand(table.BuildTableScript(), connection);
                    cmd.ExecuteNonQuery();
                }
                Debug.WriteLine("WTF");
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }

        public Task SaveBalance(Balance balance)
        {

            throw new NotImplementedException();
        }

        public Task SaveBankAccount(BankAccount acct)
        {
            using (var connection = new XamarinSQLiteConnection(connectionString))
            {
                connection.Open();
                var cmd = new SQLiteCommand($"insert into BankAccount (NickName, AccountNumber, BankName) values ('{acct.Nickname}', '{acct.AccountNumber}', '{acct.BankName}')", connection);
                cmd.ExecuteNonQuery(); 
            }

            return Task.CompletedTask;
        }

        public Task SaveBill(Bill bill)
        {
            throw new NotImplementedException();
        }

        public Task SaveBills(IEnumerable<Bill> bills)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateBankAccountNames()
        {
            var accts = new List<BankAccount>();
            try
            {
                accts = await GetBankAccounts();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try 
            { 

                BankAccountNicknames.Clear();
                foreach (var item in accts)
                {
                    BankAccountNicknames.Add(item.Nickname);
                    Debug.WriteLine(item.Nickname);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public Task UpdatePayeeNames()
        {
            throw new NotImplementedException();
        }
    }
}
