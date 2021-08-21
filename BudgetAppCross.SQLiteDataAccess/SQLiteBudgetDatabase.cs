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
            await UpdateAutopayBills();
        }

        public async Task CreateDefaultAccount()
        {
            var defaultAccount = new BankAccount(0, "Undecided");
            await SaveBankAccount(defaultAccount);
        }

        public async Task UpdateAutopayBills()
        {
            var unpaidBills = await GetUnpaidBills();

            var autoPayUpdates = new List<Bill>();
            foreach (var item in unpaidBills)
            {
                if (item.IsAuto)
                {
                    if (item.BillStatus == BillStatus.AutoPayPast)
                    {
                        item.IsPaid = true;
                        item.IsAuto = false;
                        autoPayUpdates.Add(item);
                    }
                }
            }

            await UpdateBills(autoPayUpdates);
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

        public async Task<int> DeleteBankAccount(BankAccount acct)
        {
            var deleted = 0;
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        deleted = conn.Delete(acct);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            return deleted;
        }

        public async Task<BankAccount> GetBankAccount(int id)
        {

            BankAccount ba = await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        var query = $@"SELECT * FROM BankAccount 
                                    WHERE AccountID = @AccountId";
                        var cmd = conn.CreateCommand(query);
                        cmd.Bind("@AccountId", id);
                        return cmd.ExecuteQuery<BankAccount>().Single();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
            });

            return ba;

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

        public async Task<Balance> GetLatestBalance(string name, DateTime date)
        {
            var acctId = await GetBankAccountID(name);
            return await GetLatestBalance(acctId, date);
            //GetLatestBalance
            //throw new NotImplementedException();
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
        #endregion

        #region Bill
        public async Task UpdatePayeeNames()
        {
            try
            {
                PayeeNames = await GetBillPayees();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task ChangePayeeName(string oldName, string newName)
        {
            var bills = await GetBillsForPayee(oldName);

            foreach (var item in bills)
            {
                item.Payee = newName;
            }

            await UpdateBills(bills);
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

        public async Task<int> DeleteBillsForPayee(string payee)
        {
            var bills = await GetBillsForPayee(payee);
            await Task.Run(() =>
            {
                using (var conn = new ShortConnection(connectionString))
                {
                    foreach (var item in bills)
                    {
                        conn.Delete(item);
                    }

                }
            });
            return 0;
        }

        public async Task<Bill> GetBill(int id)
        {
            Bill b = await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        var query = $@"SELECT * FROM Bill 
                                    WHERE ID = @ID";
                        var cmd = conn.CreateCommand(query);
                        cmd.Bind("@ID", id);
                        return cmd.ExecuteQuery<Bill>().Single();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
            });

            return b;
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

        public async Task<List<Bill>> GetBills()
        {
            var bills = new List<Bill>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        bills = conn.Query<Bill>(@"SELECT * FROM Bill");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            return bills;
        }

        public async Task<List<Bill>> GetBillsDateRangeForAccount(DateTime start, DateTime end, string selectedAccount)
        {
            var agendaBills = new List<Bill>();
            var acctId = await GetBankAccountID(selectedAccount);
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        var query = $@"SELECT * FROM Bill 
                                WHERE AccountID = @AccountId
                                AND Date BETWEEN @Start AND @End";
                        var cmd = conn.CreateCommand(query);
                        cmd.Bind("@AccountId", acctId);
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
                                    WHERE Payee = @Payee";
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

        public async Task<int> InsertBills(IEnumerable<Bill> bills)
        {
            var added = 0;
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        added = conn.InsertAll(bills);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            return added;
        }

        public async Task<int> UpdateBills(IEnumerable<Bill> bills)
        {
            var updated = 0;
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        updated = conn.UpdateAll(bills);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            return updated;
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
                                    AND Date < @Start
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

        public async Task<List<Bill>> GetUnpaidBills()
        {
            var unpaidBills = new List<Bill>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conn = new ShortConnection(connectionString))
                    {
                        var query = $@"SELECT * FROM Bill 
                                    WHERE IsPaid = @IsPaid";
                        var cmd = conn.CreateCommand(query);
                        cmd.Bind("@IsPaid", false);
                        unpaidBills = cmd.ExecuteQuery<Bill>();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            unpaidBills = await AttachAccounts(unpaidBills);
            return unpaidBills;
        }
        #endregion



        //public Task<Balance> GetBalance(int id)
        //{
        //    throw new NotImplementedException();
        //}














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
