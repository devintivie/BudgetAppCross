using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Core.Services
{
    public interface IDataManager
    {
        #region BankAccounts
        Task<List<BankAccount>> GetBankAccounts();
        Task SaveBankAccount(BankAccount acct);
        Task<BankAccount> GetBankAccount(int id);
        Task DeleteBankAccount(BankAccount acct);
        #endregion

        #region Balances
        Task<List<Balance>> GetBalances();
        Task SaveBalance(Balance balance);
        Task<Balance> GetBalance(int id);
        Task DeleteBalance(Balance balance);
        Task<Balance> GetLatestBalance(int id, DateTime date);
        #endregion

        #region Bills
        Task<List<Bill>> GetBills();
        Task SaveBill(Bill bill);
        Task<Bill> GetBill(int id);
        Task DeleteBill(Bill bill);

        Task<List<Bill>> GetBillsForPayee(string payee);
        #endregion

    }
}
