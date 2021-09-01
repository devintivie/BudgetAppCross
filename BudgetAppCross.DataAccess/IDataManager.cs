using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.DataAccess
{
    public interface IDataManager
    {

        
        #region Properties
        List<string> BankAccountNicknames { get; set; }
        //Dictionary<int, string> BankAccountDict { get; set; }
        List<string> PayeeNames { get; set; }
        #endregion

        Task Initialize(string newBudgetName = null);
        Task CreateDefaultAccount();
        #region BankAccounts
        Task<List<BankAccount>> GetBankAccounts();
        Task SaveBankAccount(BankAccount acct);
        Task<BankAccount> GetBankAccount(int id);
        Task<int> GetBankAccountID(string name);
        Task<int> DeleteBankAccount(BankAccount acct);
        Task UpdateBankAccountNames();
        #endregion

        #region Balances
        Task<List<Balance>> GetBalances();
        Task SaveBalance(Balance balance);
        //Task<Balance> GetBalance(int id);
        Task<int> DeleteBalance(Balance balance);
        Task<Balance> GetLatestBalance(int id, DateTime date);
        Task<Balance> GetLatestBalance(string name, DateTime date);

        Task<List<Balance>> GetBalancesForAccount(int acctId);

        #endregion

        #region Bills
        Task<List<Bill>> GetBills();
        Task<List<Bill>> GetBillsDateRangeForAccount(DateTime start, DateTime end, string selectedAccount);
        Task SaveBill(Bill bill);
        Task<int> InsertBills(IEnumerable<Bill> bills);
        Task<Bill> GetBill(int id);
        Task<int> DeleteBill(Bill bill);
        Task<List<string>> GetBillPayees();
        Task<int> DeleteBillsForPayee(string payee);
        Task ChangePayeeName(string oldName, string newName);
        Task<List<Bill>> GetBillsForPayee(string payee);
        Task UpdatePayeeNames();
        #endregion

        #region Agenda
        Task<List<Bill>> GetUnpaidAndFutureBills(DateTime start, DateTime end);
        #endregion
    }
}
