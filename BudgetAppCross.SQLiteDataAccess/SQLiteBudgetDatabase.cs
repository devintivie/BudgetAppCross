using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using BudgetAppCross.StateManagers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAppCross.SQLiteDataAccess
{
    public class SQLiteBudgetDatabase : IDataManager
    {
        #region Fields
        private string connectionString => StateManager.Instance.DatabasePath;
        //private SQLiteOpenFlags flags => StateManager.Flags;

        #endregion

        #region Properties
        public List<string> BankAccountNicknames { get; set; } = new List<string>();
        public List<string> PayeeNames { get; set; } = new List<string>();

        public IAccountRepo AccountAccess { get; private set; }
        public IBillRepo BillRepo { get; private set; }
        public IBalanceRepo BalanceAccess { get; private set; }
        //public Dictionary<int, string> BankAccountDict { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        #region Constructors

        #endregion

        #region Init
        public async Task Initialize()
        {
            //File.Delete(Constants.DatabasePath);
            using (var connection = new ShortConnection(connectionString))
            {
                connection.CreateTable<BankAccount>(CreateFlags.None);
                connection.CreateTable<Bill>(CreateFlags.None);
                connection.CreateTable<Balance>(CreateFlags.None);
            }

            await UpdateBankAccountNames();
            await UpdatePayeeNames();
        }

        public async Task CreateDefaultAccount()
        {
            var defaultAccount = new BankAccount(0, "Undecided");
            await SaveBankAccount(defaultAccount);
        }
        #endregion

        #region BankAccount
        public async Task<List<BankAccount>> GetBankAccounts()
        {
            var accts = new List<BankAccount>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        accts = conn.Query<BankAccount>(@"SELECT * FROM BankAccount");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            return accts;
        }

        public async Task UpdateBankAccountNames()
        {
            try
            {
                var allAccounts = await GetBankAccounts();
                BankAccountNicknames.Clear();
                foreach (var item in allAccounts)
                {
                    BankAccountNicknames.Add(item.Nickname);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        #endregion

        #region Balance
        public async Task<List<Balance>> GetBalancesForAccount(int acctId)
        {
            var balances = new List<Balance>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        var query = $@"SELECT * FROM Balance 
                                    WHERE AccountId = @AccountId";
                        var cmd = conn.CreateCommand(query);
                        cmd.Bind("@AccountId", acctId);
                        balances = cmd.ExecuteQuery<Balance>();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            foreach (var item in balances)
            {
                item.AccountID = acctId;
            }
            //balances = await AttachAccounts(balances);
            return balances;
        }
        #endregion

        #region Bill
        public async Task UpdatePayeeNames()
        {
            //var distinctPayees = new List<string>();
            //var distinctBills = new List<Bill>();
            try
            {
                PayeeNames = await GetBillPayees();
                //var tmp = distinctBills.Select(x => x.Payee);
                //PayeeNames = tmp.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        #endregion

        public Task<int> ChangePayeeName(string oldName, string newName)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteBalance(Balance balance)
        {
            var deleted = 0;
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        deleted = conn.Delete(balance);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            return deleted;
        }

        public Task DeleteBankAccount(BankAccount acct)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteBill(Bill bill)
        {
            var deleted = 0;
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        deleted = conn.Delete(bill);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            return deleted;
        }

        public Task<int> DeleteBillsForPayee(string payee)
        {
            throw new NotImplementedException();
        }

        public Task<Balance> GetBalance(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Balance>> GetBalances()
        {
            var balances = new List<Balance>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        balances = conn.Query<Balance>(@"SELECT * FROM Balance");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            return balances;
        }

        public Task<BankAccount> GetBankAccount(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetBankAccountID(string name)
        {
            //var acctId = 0;
            var accts = new List<BankAccount>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        var query = $@"SELECT * FROM BankAccount 
                                    WHERE Nickname = @Nickname";
                        var cmd = conn.CreateCommand(query);
                        cmd.Bind("@Nickname", name);
                        accts = cmd.ExecuteQuery<BankAccount>();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            return accts.FirstOrDefault().AccountID;
            //return acctId;
        }

        

        public Task<Bill> GetBill(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetBillPayees()
        {
            var distinctPayees = new List<string>();
            var distinctBills = new List<Bill>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        distinctBills = conn.Query<Bill>(@"SELECT DISTINCT Payee FROM Bill ORDER BY Payee");
                    }

                    var tmp = distinctBills.Select(x => x.Payee);
                    distinctPayees = tmp.ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            return distinctPayees;
        }

        public Task<List<Bill>> GetBills()
        {
            throw new NotImplementedException();
        }

        public Task<List<Bill>> GetBillsDateRangeForAccount(DateTime start, DateTime end, string selectedAccount)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Bill>> GetBillsForPayee(string payee)
        {
            var bills = new List<Bill>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        var query = $@"SELECT * FROM Bill 
                                    WHERE Payee = @Payee" ;
                        var cmd = conn.CreateCommand(query);
                        cmd.Bind("@Payee", payee);
                        bills = cmd.ExecuteQuery<Bill>();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            bills = await AttachAccounts(bills);
            return bills;
        }

        public Task<Balance> GetLatestBalance(int id, DateTime date)
        {
            //var acctId = 0;
            var bals = new List<Balance>();
            //await Task.Run(() =>
            //{
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        var query = $@"SELECT * FROM Balance 
                                      WHERE AccountID = @AccountID
                                        ORDER BY Timestamp DESC LIMIT 1";// Nickname = @Nickname";
                        var cmd = conn.CreateCommand(query);
                    cmd.Bind("@AccountID", id);
                    bals = cmd.ExecuteQuery<Balance>();
                        //bals = conn.Query<Balance>(query); // = cmd.ExecuteQuery<BankAccount>();
                        //distinctBills = conn.Query<Bill>(@"SELECT DISTINCT Payee FROM Bill ORDER BY Payee");
                    }
                    //(substr(Timestamp, 7, 4) || substr(Timestamp, 4, 2) || substr(Timestamp, 1, 2))
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            //});

            return Task.FromResult(bals.Single());
            //return acctId;
        }

        public Task<Balance> GetLatestBalance(string name, DateTime date)
        {
            throw new NotImplementedException();
        }


        public async Task SaveBalance(Balance balance)
        {
            if (balance.ID != 0)
            {
                await UpdateBalance(balance);
            }
            else
            {
                await InsertBalance(balance);
            }
        }

        public async Task<int> InsertBalance(Balance balance)
        {
            var added = 0;
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        added = conn.Insert(balance);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            return added;
        }

        public async Task<int> UpdateBalance(Balance balance)
        {
            var updated = 0;
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        updated = conn.Update(balance);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            return updated;
        }

        public async Task SaveBankAccount(BankAccount acct)
        {
            if (acct.AccountID != 0)
            {
                await UpdateBankAccount(acct);
            }
            else
            {
                await InsertBankAccount(acct);

                var acctId = await GetBankAccountID(acct.Nickname);

                foreach (var bal in acct.History)
                {
                    bal.AccountID = acctId;
                    await SaveBalance(bal);
                }

            }
        }

        public async Task<int> InsertBankAccount(BankAccount acct)
        {
            var added = 0;
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        added = conn.Insert(acct);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            return added;
        }

        public async Task<int> UpdateBankAccount(BankAccount acct)
        {
            var updated = 0;
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        updated = conn.Update(acct);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            return updated;
        }

        public async Task SaveBill(Bill bill)
        {
            if (bill.ID != 0)
            {
                await UpdateBill(bill);
            }
            else
            {
                await InsertBill(bill);
            }
        }

        public async Task<int> InsertBill(Bill bill)
        {
            var added = 0;
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        added = conn.Insert(bill);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            return added;
        }

        public async Task<int> UpdateBill(Bill bill)
        {
            var updated = 0;
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        updated = conn.Update(bill);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            return updated;
        }



        public Task SaveBills(IEnumerable<Bill> bills)
        {
            throw new NotImplementedException();
        }
        #region Agenda

        public async Task<List<Bill>> GetUnpaidAndFutureBills(DateTime start, DateTime end)
        {
            var agendaBills = new List<Bill>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        var query = $@"SELECT * FROM Bill 
                                    WHERE IsPaid = @IsPaid 
                                    OR Date BETWEEN @Start AND @End";
                        var cmd = conn.CreateCommand(query);
                        cmd.Bind("@IsPaid", false);
                        cmd.Bind("@Start", start);
                        cmd.Bind("@End", end);
                        agendaBills = cmd.ExecuteQuery<Bill>();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            agendaBills = await AttachAccounts(agendaBills);
            return agendaBills;
        }
        #endregion

        #region Private Extensions
        public async Task<List<Bill>> AttachAccounts(List<Bill> bills)
        {
            var accounts = await GetBankAccounts();
            var acctDict = accounts.ToDictionary(x => x.AccountID, y => y);
            
            foreach (var bill in bills)
            {
                bill.BankAccount = acctDict[bill.AccountID];
            }

            return bills;
        }

        
        #endregion

    }
}
