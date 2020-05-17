﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BudgetAppCross.Models;
using SQLite;

namespace BudgetAppCross.Core.Services
{
    public class BudgetDatabase
    {
        #region Singleton
        static readonly Lazy<BudgetDatabase> instance = new Lazy<BudgetDatabase>();
        public static BudgetDatabase Instance => instance.Value;

        static readonly Lazy<SQLiteAsyncConnection> database = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => database.Value;

        public BudgetDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }
        #endregion

        #region Fields
        static bool initialized = false;
        #endregion

        #region Properties

        #endregion

        #region Methods

        async Task InitializeAsync()
        {
            //File.Delete(Constants.DatabasePath);
            if (!initialized)
            {
                MapTable(typeof(BankAccount));
                MapTable(typeof(Bill));
                MapTable(typeof(Balance));


                initialized = true;
                Console.WriteLine();
                //await MapTable(typeof(Bill));
                //if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Bill).Name))
                //{
                //    await Database.CreateTablesAsync(CreateFlags.None, typeof(Bill)).ConfigureAwait(false);
                //    initialized = true;
                //}
            }
        }

        async void MapTable(Type type)
        {
            if(!Database.TableMappings.Any(m => m.MappedType.Name == type.Name))
            {
                await Database.CreateTablesAsync(CreateFlags.None, type).ConfigureAwait(false);
            }
        }
        #region Bill
        public async Task<List<Bill>> GetBillsAsync()
        {
            //return await Database.Table<Bill>().ToListAsync();

            var list = await Database.Table<Bill>().ToListAsync();
            return list;

        }

        //public Task<List<Bill>> GetItemsNotDoneAsync()
        //{
        //    // SQL queries are also possible
        //    return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}

        public async Task<Bill> GetBillAsync(int id)
        {
            return await Database.Table<Bill>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveBillAsync(Bill bill)
        {
            if (bill.ID != 0)
            {
                return Database.UpdateAsync(bill);
            }
            else
            {
                return Database.InsertAsync(bill);
            }
        }

        public Task<int> DeleteBillAsync(Bill bill)
        {
            return Database.DeleteAsync(bill);
        }
        #endregion

        #region BankAccount
        public async Task<List<BankAccount>> GetBankAccountsAsync()
        {
            var list = await Database.Table<BankAccount>().ToListAsync();
            return list;
        }

        //public Task<List<Bill>> GetItemsNotDoneAsync()
        //{
        //    // SQL queries are also possible
        //    return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}

        public async Task<BankAccount> GetBankAccountAsync(int id)
        {
            return await Database.Table<BankAccount>().Where(i => i.AccountID == id).FirstOrDefaultAsync();
        }

        public async Task<BankAccount> GetBankAccountIDAsync(string name)
        {
            return await Database.Table<BankAccount>().Where(i => i.Nickname.Equals(name)).FirstOrDefaultAsync();
        }

        public Task<int> SaveBankAccountAsync(BankAccount account)
        {
            if (account.AccountID != 0)
            {
                return Database.UpdateAsync(account);
            }
            else
            {
                return Database.InsertAsync(account);
            }
        }

        public Task<int> DeleteBankAccountAsync(BankAccount account)
        {
            return Database.DeleteAsync(account);
        }
        #endregion

        #region Balance
        public async Task<List<Balance>> GetBalancesAsync()
        {
            var list = await Database.Table<Balance>().ToListAsync();
            return list;
        }

        //public Task<List<Bill>> GetItemsNotDoneAsync()
        //{
        //    // SQL queries are also possible
        //    return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}

        public async Task<Balance> GetBalanceAsync(int id)
        {
            return await Database.Table<Balance>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<Balance> GetLatestBalanceAsync(DateTime date)
        {
            var list = await Database.Table<Balance>().ToListAsync();
            return list.FirstOrDefault();
            return await (Database.Table<Balance>().Where(bal => bal.Timestamp <= date)
                .OrderByDescending(x => x.Timestamp)).FirstAsync();
        }

        public async Task<int> SaveBalanceAsync(Balance balance)
        {
            if (balance.ID != 0)
            {
                return await Database.UpdateAsync(balance);
            }
            else
            {
                return await Database.InsertAsync(balance);
            }
        }

        public async Task<int> DeleteBalanceAsync(Balance balance)
        {
            return await Database.DeleteAsync(balance);
        }
        #endregion

        #region Income

        #endregion
        #endregion










    }
}
