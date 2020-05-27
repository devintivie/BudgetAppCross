using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BudgetAppCross.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
//using SQLiteNetExtensionsAsync.Extensions;

namespace BudgetAppCross.Core.Services
{
    public class BudgetDatabase : IDataManager
    {
        #region Singleton
        static readonly Lazy<BudgetDatabase> instance = new Lazy<BudgetDatabase>();
        public static BudgetDatabase Instance => instance.Value;

        static SQLiteConnection database;

        public static SQLiteConnection Database
        {
            get { return database ?? (database = new SQLiteConnection(Constants.DatabasePath, Constants.Flags)); }
        }

        public BudgetDatabase()
        {
            Initialize();
        }
        #endregion

        #region Fields
        static bool initialized = false;
        #endregion

        #region Properties
        public List<string> BankAccountNicknames { get; set; } = new List<string>();
        #endregion

        #region Methods

        void Initialize()
        {
            //File.Delete(Constants.DatabasePath);
            if (!initialized)
            {
                Database.CreateTable<BankAccount>(CreateFlags.None);
                Database.CreateTable<Bill>(CreateFlags.None);
                Database.CreateTable<Balance>(CreateFlags.None);

                initialized = true;

                var names = UpdateBankAccountNames();

                //, type).ConfigureAwait(false);
                //MapTable(typeof(BankAccount));
                //MapTable(typeof(Bill));
                //MapTable(typeof(Balance));


                //initialized = true;
                //Console.WriteLine();
                ////await MapTable(typeof(Bill));
                ////if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Bill).Name))
                ////{
                ////    await Database.CreateTablesAsync(CreateFlags.None, typeof(Bill)).ConfigureAwait(false);
                ////    initialized = true;
                ////}
            }
        }

        #region BankAccount
        public async Task SaveBankAccount(BankAccount acct)
        {
            await Task.Run(() =>
            {
                if (acct.AccountID != 0)
                {
                    Database.UpdateWithChildren(acct);
                }
                else
                {
                    Database.InsertWithChildren(acct);
                }
            });
        }

        public async Task<List<BankAccount>> GetBankAccounts()
        {
            //var list = new List<BankAccount>();
            var list = await Task.Run(() =>
            {
                return Database.GetAllWithChildren<BankAccount>();
            });

            await UpdateBankAccountNames();

            return list;
        }

        public async Task UpdateBankAccountNames()
        {
            var list = await Task.Run(() =>
            {
                return Database.GetAllWithChildren<BankAccount>();
            });

            BankAccountNicknames.Clear();
            foreach (var item in list)
            {
                BankAccountNicknames.Add(item.Nickname);
            }
        }

        //public async Task<IEnumerable<string>> GetBankAccountNames()
        //{
        //    return await Task.Run(() =>
        //    {
        //        var list = Database.Table<BankAccount>().ToList();
        //        var names = list.Select(x => x.Nickname);//.ToList();
        //        return names;
        //    });
        //}



        public async Task<BankAccount> GetBankAccount(int id)
        {
            return await Task.Run(() =>
            {
                return Database.GetWithChildren<BankAccount>(id);
            });
        }

        public async Task DeleteBankAccount(BankAccount acct)
        {
            await Task.Run(() =>
            {
                Database.Delete(acct);
            });
        }

        //public Task<int> DeleteBillAsync(Bill bill)
        //{
        //    return Database.DeleteAsync(bill);
        //}
        #endregion

        #region Balance
        public async Task SaveBalance(Balance balance)
        {
            await Task.Run(() =>
            {
                if (balance.ID != 0)
                {
                    Database.Update(balance);//     UpdateWithChildren(balance);
                }
                else
                {
                    Database.Insert(balance);//  WithChildren(balance);
                }
            });
        }

        public async Task<List<Balance>> GetBalances()
        {
            //var list = new List<BankAccount>();
            return await Task.Run(() =>
            {
                return Database.Table<Balance>().ToList();
                //return Database.Get WithChildren<Balance>();
            });

            //return list;
        }

        public async Task<Balance> GetBalance(int id)
        {
            return await Task.Run(() =>
            {
                return Database.Get<Balance>(id);//    WithChildren<Balance>(id);
            });
        }

        public async Task<Balance> GetLatestBalance(int id, DateTime date)
        {
            return await Task.Run(() =>
            {
                var balances = Database.Table<Balance>()
                .Where(bal => bal.AccountID == id).ToList();

                return balances.First();

                //if(balances.Count == 0)
                //{
                //    return new Balance();
                //}
                //else
                //{
                //    return new Balance();
                //}
                //return balance;
                //var list =  await (Database.Table<Balance>().Where(bal => bal.Timestamp <= date && )
                //.OrderByDescending(x => x.Timestamp)).FirstAsync();
                //var list = Database.Table<Balance>().ToList();
                //return list.First();
            });
            //var list = Database.Table<Balance>().ToList();
            //return list.FirstOrDefault();
            //return await (Database.Table<Balance>().Where(bal => bal.Timestamp <= date)
            //    .OrderByDescending(x => x.Timestamp)).FirstAsync();
        }

        public async Task DeleteBalance(Balance balance)
        {
            await Task.Run(() =>
            {
                Database.Delete(balance);
            });
        }
        #endregion

        #region Bill
        public async Task SaveBill(Bill bill)
        {
            await Task.Run(() =>
            {
                if (bill.ID != 0)
                {
                    Database.UpdateWithChildren(bill);
                }
                else
                {
                    Database.InsertWithChildren(bill);
                }
            });
        }

        public async Task<List<Bill>> GetBills()
        {
            //var list = new List<BankAccount>();
            var list = await Task.Run(() =>
            {
                //return Database.Table<Bill>().ToList();
                return Database.GetAllWithChildren<Bill>(recursive: true);
            });

            return list;
        }

        public async Task<List<Bill>> GetBillsForPayee(string payee)
        {
            var list = await Task.Run(() =>
            {
                return Database.GetAllWithChildren<Bill>()
                        .Where(x => x.Payee.Equals(payee))
                        .OrderBy(x => x.Date).ToList();
                //return Database.Table<Bill>().Where(x => x.Payee.Equals(payee)).ToList();
            });

            return list;
            // SQL queries are also possible
            //return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public async Task<int> DeleteBillsForPayee(string payee)
        {
            var count = 0;
            var bills = await GetBillsForPayee(payee);

            foreach (var bill in bills)
            {
                count += await DeleteBill(bill);
            }

            return count;
        }

        public async Task<List<string>> GetBillPayees()
        {
            var list = await Task.Run(() =>
            {
                return Database.GetAllWithChildren<Bill>()
                        .GroupBy(x => x.Payee)
                        .Select(x => x.Key).ToList();
            });
            return list;
        }



        public async Task<Bill> GetBill(int id)
        {
            return await Task.Run(() =>
            {
                return Database.Get<Bill>(id);
            });
        }

        public async Task<int> DeleteBill(Bill bill)
        {
            var count = await Task.Run(() =>
            {
                return Database.Delete(bill);
            });

            return count;
        }
        #endregion








        //async void MapTable(Type type)
        //{
        //    if(!Database.TableMappings.Any(m => m.MappedType.Name == type.Name))
        //    {
        //        Database.CreateTable(CreateFlags.None, type).ConfigureAwait(false);
        //    }
        //}
        //#region Bill
        //public async Task<List<Bill>> GetBillsAsync()
        //{
        //    //return await Database.Table<Bill>().ToListAsync();

        //    var list = await Database.Table<Bill>().ToListAsync();
        //    return list;

        //}

        ////public Task<List<Bill>> GetItemsNotDoneAsync()
        ////{
        ////    // SQL queries are also possible
        ////    return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        ////}

        //public async Task<Bill> GetBillAsync(int id)
        //{
        //    return await Database.Table<Bill>().Where(i => i.ID == id).FirstOrDefaultAsync();
        //}

        //public Task SaveBillAsync(Bill bill)
        //{
        //    if (bill.ID != 0)
        //    {
        //        //return Database.UpdateAsync(bill);
        //        return Database.UpdateWithChildrenAsync(bill);
        //    }
        //    else
        //    {
        //        return Database.InsertAsync(bill);
        //    }
        //}

        //public Task<int> DeleteBillAsync(Bill bill)
        //{
        //    return Database.DeleteAsync(bill);
        //}
        //#endregion

        //#region BankAccount
        //public async Task<List<BankAccount>> GetBankAccountsAsync()
        //{
        //    var list = await Database.Table<BankAccount>().ToListAsync();
        //    var balances = await Database.Table<Balance>().ToListAsync();
        //    return list;
        //}

        ////public Task<List<Bill>> GetItemsNotDoneAsync()
        ////{
        ////    // SQL queries are also possible
        ////    return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        ////}

        //public async Task<BankAccount> GetBankAccountAsync(int id)
        //{
        //    return await Database.Table<BankAccount>().Where(i => i.AccountID == id).FirstOrDefaultAsync();
        //}

        //public async Task<BankAccount> GetBankAccountIDAsync(string name)
        //{
        //    return await Database.Table<BankAccount>().Where(i => i.Nickname.Equals(name)).FirstOrDefaultAsync();
        //}

        //public async Task<int> SaveBankAccountAsync(BankAccount account)
        //{
        //    //AccountID = 0 if new account is saved
        //    if (account.AccountID != 0)
        //    {
        //        var updated = await Database.UpdateAsync(account);
        //        foreach (var item in account.History)
        //        {
        //            await SaveBalanceAsync(item);
        //        }
        //        return updated;
        //    }
        //    else
        //    {

        //        var pk = await Database.InsertAsync(account);
        //        foreach (var item in account.History)
        //        {
        //            await SaveBalanceAsync(item);
        //        }
        //        return pk;
        //    }
        //}

        //public Task<int> DeleteBankAccountAsync(BankAccount account)
        //{
        //    return Database.DeleteAsync(account);
        //}
        //#endregion

        //#region Balance
        //public async Task<List<Balance>> GetBalancesAsync()
        //{
        //    var list = await Database.Table<Balance>().ToListAsync();
        //    return list;
        //}

        ////public Task<List<Bill>> GetItemsNotDoneAsync()
        ////{
        ////    // SQL queries are also possible
        ////    return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        ////}

        //public async Task<Balance> GetBalanceAsync(int id)
        //{
        //    return await Database.Table<Balance>().Where(i => i.ID == id).FirstOrDefaultAsync();
        //}

        //public async Task<Balance> GetLatestBalanceAsync(DateTime date)
        //{
        //    var list = await Database.Table<Balance>().ToListAsync();
        //    return list.FirstOrDefault();
        //    return await (Database.Table<Balance>().Where(bal => bal.Timestamp <= date)
        //        .OrderByDescending(x => x.Timestamp)).FirstAsync();
        //}

        //public async Task<int> SaveBalanceAsync(Balance balance)
        //{
        //    if (balance.ID != 0)
        //    {
        //        return await Database.UpdateAsync(balance);
        //    }
        //    else
        //    {
        //        return await Database.InsertAsync(balance);
        //    }
        //}

        //public async Task<int> DeleteBalanceAsync(Balance balance)
        //{
        //    return await Database.DeleteAsync(balance);
        //}
        //#endregion

        //#region Income

        //#endregion
        #endregion










    }
}
