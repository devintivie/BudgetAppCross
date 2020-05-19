using BudgetAppCore.Sqlite;
using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Core.Services.Data
{
    public class DataManagerSqlite : IDataManager
    {
        #region Singleton
        static readonly Lazy<DataManagerSqlite> instance = new Lazy<DataManagerSqlite>();
        public static DataManagerSqlite Instance => instance.Value;
        #endregion
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods
        #region BankAccount
        public async Task SaveBankAccount(BankAccount acct)
        {
            await Task.Run(() =>
            {
                using (var ctx = new SQLiteDBContext())
                {
                    if (acct.AccountID != 0)
                    {
                        ctx.Add(      Database.UpdateWithChildren(acct);
                    }
                    else
                    {
                        ctx.BankAccounts.Update(acct);
                    }
                }
            });
        }

        public async Task<List<BankAccount>> GetBankAccounts()
        {
            //var list = new List<BankAccount>();
            var list = await Task.Run(() =>
            {
                using (var ctx = new SQLiteDBContext())
                {
                    return ctx.BankAccounts.ToList();
                }
            });

            return list;
        }

        public async Task<IEnumerable<string>> GetBankAccountNames()
        {
            return await Task.Run(() =>
            {
                var list = Database.Table<BankAccount>().ToList();
                var names = list.Select(x => x.Nickname);//.ToList();
                return names;
            });
        }

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
                return Database.GetAllWithChildren<Bill>();
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

        public async Task<Bill> GetBill(int id)
        {
            return await Task.Run(() =>
            {
                return Database.Get<Bill>(id);
            }); 
        }

        public async Task DeleteBill(Bill bill)
        {
            await Task.Run(() =>
            {
                Database.Delete(bill);
            });
        }
        #endregion
        #endregion

    }
}
