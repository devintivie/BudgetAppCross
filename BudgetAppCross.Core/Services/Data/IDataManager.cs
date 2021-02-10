using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Core.Services
{
    public interface IDataManager
    {

        
        #region Properties
        List<string> BankAccountNicknames { get; set; }
        List<string> PayeeNames { get; set; }
        #endregion

        Task Initialize();
        #region BankAccounts
        Task<List<BankAccount>> GetBankAccounts();
        Task SaveBankAccount(BankAccount acct);
        Task<BankAccount> GetBankAccount(int id);
        Task<int> GetBankAccountID(string name);
        Task DeleteBankAccount(BankAccount acct);
        Task UpdateBankAccountNames();
        #endregion

        #region Balances
        Task<List<Balance>> GetBalances();
        Task SaveBalance(Balance balance);
        Task<Balance> GetBalance(int id);
        Task DeleteBalance(Balance balance);
        Task<Balance> GetLatestBalance(int id, DateTime date);
        Task<Balance> GetLatestBalance(string name, DateTime date);
        #endregion

        #region Bills
        Task<List<Bill>> GetBills();
        Task<List<Bill>> GetBillsDateRangeForAccount(DateTime start, DateTime end, string selectedAccount);
        Task SaveBill(Bill bill);
        Task SaveBills(IEnumerable<Bill> bills);
        Task<Bill> GetBill(int id);
        Task<int> DeleteBill(Bill bill);
        Task<List<string>> GetBillPayees();
        Task<int> DeleteBillsForPayee(string payee);
        Task<int> ChangePayeeName(string oldName, string newName);
        Task<List<Bill>> GetBillsForPayee(string payee);
        Task UpdatePayeeNames();
        #endregion

    }
}
