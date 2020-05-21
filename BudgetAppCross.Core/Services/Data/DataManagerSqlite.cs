using BudgetAppCore.Sqlite;
using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Core.Services
{
    //public class DataManagerSqlite : IDataManager
    //{
    //    #region Singleton
    //    static readonly Lazy<DataManagerSqlite> instance = new Lazy<DataManagerSqlite>();
    //    public static DataManagerSqlite Instance => instance.Value;
    //    #endregion
    //    #region Fields

    //    #endregion

    //    #region Properties

    //    #endregion

    //    #region Methods
    //    #region BankAccount
    //    public async Task SaveBankAccount(BankAccount acct)
    //    {
    //        await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                if (acct.AccountID != 0)
    //                {
    //                    ctx.BankAccounts.Add(acct);
    //                }
    //                else
    //                {
    //                    ctx.BankAccounts.Update(acct);
    //                }
    //            }
    //        });
    //    }

    //    public async Task<List<BankAccount>> GetBankAccounts()
    //    {
    //        //var list = new List<BankAccount>();
    //        var list = await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                return ctx.BankAccounts.ToList();
    //            }
    //        });

    //        return list;
    //    }

    //    public async Task<IEnumerable<string>> GetBankAccountNames()
    //    {
    //        return await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                var list = ctx.BankAccounts.ToList();
    //                var names = list.Select(x => x.Nickname);//.ToList();
    //                return names;
    //            }
    //        });
    //    }

    //    public async Task<BankAccount> GetBankAccount(int id)
    //    {
    //        return await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                return ctx.BankAccounts.Single(ba => ba.AccountID == id);
    //            }
    //        });
    //    }

    //    public async Task DeleteBankAccount(BankAccount acct)
    //    {
    //        await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                ctx.BankAccounts.Remove(acct);
    //            }
    //        });
    //    }
    //    #endregion

    //    #region Balance
    //    public async Task SaveBalance(Balance balance)
    //    {
    //        await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                if (balance.ID != 0)
    //                {
    //                    ctx.BalanceHistory.Add(balance);
    //                }
    //                else
    //                {
    //                    ctx.BalanceHistory.Update(balance);
    //                }
    //            }
    //        });
    //    }

    //    public async Task<List<Balance>> GetBalances()
    //    {
    //        return await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                return ctx.BalanceHistory.ToList();
    //            }
    //        });
    //    }

    //    public async Task<Balance> GetBalance(int id)
    //    {
    //        return await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                return ctx.BalanceHistory.Single(b => b.ID == id);
    //            }
    //        });
    //    }

    //    public async Task<Balance> GetLatestBalance(int id, DateTime date)
    //    {
    //        return await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                var balances = ctx.BalanceHistory.Where(bal => bal.Timestamp <= date)
    //                .Where(bal => bal.AccountID == id)
    //                .OrderByDescending(x => x.Timestamp).ToList();

    //                var balance = balances.FirstOrDefault();
    //                return balance;
    //            }
    //        });
    //    }

    //    public async Task DeleteBalance(Balance balance)
    //    {
    //        await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                ctx.BalanceHistory.Remove(balance);
    //            }
    //        });
    //    }
    //    #endregion

    //    #region Bill
    //    public async Task SaveBill(Bill bill)
    //    {
    //        await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                if (bill.ID != 0)
    //                {
    //                    ctx.Bills.Add(bill);
    //                }
    //                else
    //                {
    //                    ctx.Bills.Update(bill);
    //                }
    //            }
    //        });
    //    }

    //    public async Task<List<Bill>> GetBills()
    //    {
    //        var list = await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                return ctx.Bills.ToList();
    //            }
    //        });

    //        return list;
    //    }

    //    public async Task<List<Bill>> GetBillsForPayee(string payee)
    //    {
    //        var list = await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                return ctx.Bills.Where(b => b.Payee.Equals(payee))
    //                .OrderBy(x => x.Date).ToList();
    //            }
    //        });

    //        return list;
    //    }

    //    public async Task<Bill> GetBill(int id)
    //    {
    //        return await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                return ctx.Bills.Single(b => b.ID == id);
    //            }
    //        });
    //    }

    //    public async Task DeleteBill(Bill bill)
    //    {
    //        await Task.Run(() =>
    //        {
    //            using (var ctx = new SQLiteDBContext())
    //            {
    //                ctx.Bills.Remove(bill);
    //            }
    //        });
    //    }
    //    #endregion
    //    #endregion

    //}
}
