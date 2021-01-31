using BudgetAppCross.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Core.Services
{
	[Flags]
	public enum SQLiteOpenFlags
	{
		ReadOnly = 1, 
		ReadWrite = 2, 
		Create = 4,
		NoMutex = 0x8000, 
		FullMutex = 0x10000,
		SharedCache = 0x20000, 
		PrivateCache = 0x40000,
		ProtectionComplete = 0x00100000,
		ProtectionCompleteUnlessOpen = 0x00200000,
		ProtectionCompleteUntilFirstUserAuthentication = 0x00300000,
		ProtectionNone = 0x00400000
	}

	[Flags]
	public enum CreateFlags
	{
		/// <summary>
		/// Use the default creation options
		/// </summary>
		None = 0x000,
		/// <summary>
		/// Create a primary key index for a property called 'Id' (case-insensitive).
		/// This avoids the need for the [PrimaryKey] attribute.
		/// </summary>
		ImplicitPK = 0x001,
		/// <summary>
		/// Create indices for properties ending in 'Id' (case-insensitive).
		/// </summary>
		ImplicitIndex = 0x002,
		/// <summary>
		/// Create a primary key for a property called 'Id' and
		/// create an indices for properties ending in 'Id' (case-insensitive).
		/// </summary>
		AllImplicit = 0x003,
		/// <summary>
		/// Force the primary key property to be auto incrementing.
		/// This avoids the need for the [AutoIncrement] attribute.
		/// The primary key property on the class should have type int or long.
		/// </summary>
		AutoIncPK = 0x004,
		/// <summary>
		/// Create virtual table using FTS3
		/// </summary>
		FullTextSearch3 = 0x100,
		/// <summary>
		/// Create virtual table using FTS4
		/// </summary>
		FullTextSearch4 = 0x200
	}
    public class SQLiteBudgetDatabase : IDataManager
    {
        public List<string> BankAccountNicknames { get; set; }
        public List<string> PayeeNames { get; set; }

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
            using (IDbConnection connection = new XamarinSQLiteConnection(connectionString))
            {
                var output = connection.Query<BankAccount>("select * from BankAccount", new DynamicParameters());
                return output.ToList();
            }
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
            //File.Delete(Constants.DatabasePath);
            var accountTable = new SQLiteTable("BankAccount");
            accountTable.AddColumn(new SQLiteColumn("AccountId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            accountTable.AddColumn(new SQLiteColumn("NickName").WithDatatype("TEXT").IsUnique());
            accountTable.AddColumn(new SQLiteColumn("AccountNumber").WithDatatype("TEXT").AsNullable(true));
            accountTable.AddColumn(new SQLiteColumn("BankName").WithDatatype("TEXT").AsNullable(true));

            CreateTable(accountTable);
                //connection.CreateTable<BankAccount>(CreateFlags.None);
                //connection.CreateTable<Bill>(CreateFlags.None);
                //connection.CreateTable<Balance>(CreateFlags.None);
            
            //Database.CreateTable<BankAccount>(CreateFlags.None);
            //Database.CreateTable<Bill>(CreateFlags.None);
            //Database.CreateTable<Balance>(CreateFlags.None);

            //initialized = true;

            await UpdateBankAccountNames();
        }

        public void CreateTable(SQLiteTable table)
        {
            using (var connection = new XamarinSQLiteConnection(connectionString))
            {
                connection.Open();
                var cmd = new SQLiteCommand(table.BuildTableScript(), connection);
                cmd.ExecuteNonQuery();
            }
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

        public async Task UpdateBankAccountNames()
        {
            var accts = await GetBankAccounts();

            BankAccountNicknames.Clear();
            foreach (var item in accts)
            {
                BankAccountNicknames.Add(item.Nickname);
            }
        }

        public Task UpdatePayeeNames()
        {
            throw new NotImplementedException();
        }
    }
}
