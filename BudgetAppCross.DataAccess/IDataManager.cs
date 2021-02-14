using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.DataAccess
{
    public interface IDataManager
    {
<<<<<<< HEAD:BudgetAppCross.DataAccess/IDataManager.cs
        #region Properties
        List<string> BankAccountNicknames { get; set; }
        List<string> PayeeNames { get; set; }
        #endregion

        #region Init
        Task Initialize();
        #endregion

        #region BankAccounts
        Task<List<BankAccount>> GetBankAccounts();
        Task<int> SaveBankAccount(BankAccount acct);
        Task<BankAccount> GetBankAccount(int id);
        Task<int> GetBankAccountID(string name);
        Task DeleteBankAccount(BankAccount acct);
=======

        
        #region Properties
        List<string> BankAccountNicknames { get; set; }
        //Dictionary<int, string> BankAccountDict { get; set; }
        List<string> PayeeNames { get; set; }
        #endregion

        Task Initialize();
        Task CreateDefaultAccount();
        #region BankAccounts
        Task<List<BankAccount>> GetBankAccounts();
        Task SaveBankAccount(BankAccount acct);
        Task<BankAccount> GetBankAccount(int id);
        Task<int> GetBankAccountID(string name);
        Task<int> DeleteBankAccount(BankAccount acct);
>>>>>>> feature/ModifySQLite:BudgetAppCross.Core/Services/Data/IDataManager.cs
        Task UpdateBankAccountNames();
        #endregion

        #region Balances
        Task<List<Balance>> GetBalances();
        Task SaveBalance(Balance balance);
<<<<<<< HEAD:BudgetAppCross.DataAccess/IDataManager.cs
        Task<Balance> GetBalance(int id);
        Task DeleteBalance(Balance balance);
        Task<Balance> GetLatestBalance(int id, DateTime date);
        Task<Balance> GetLatestBalance(string name, DateTime date);
=======
        //Task<Balance> GetBalance(int id);
        Task<int> DeleteBalance(Balance balance);
        Task<Balance> GetLatestBalance(int id, DateTime date);
        Task<Balance> GetLatestBalance(string name, DateTime date);

        Task<List<Balance>> GetBalancesForAccount(int acctId);

>>>>>>> feature/ModifySQLite:BudgetAppCross.Core/Services/Data/IDataManager.cs
        #endregion

        #region Bills
        Task<List<Bill>> GetBills();
<<<<<<< HEAD:BudgetAppCross.DataAccess/IDataManager.cs
        Task SaveBill(Bill bill);
        Task SaveBills(IEnumerable<Bill> bills);
=======
        Task<List<Bill>> GetBillsDateRangeForAccount(DateTime start, DateTime end, string selectedAccount);
        Task SaveBill(Bill bill);
        Task<int> InsertBills(IEnumerable<Bill> bills);
>>>>>>> feature/ModifySQLite:BudgetAppCross.Core/Services/Data/IDataManager.cs
        Task<Bill> GetBill(int id);
        Task<int> DeleteBill(Bill bill);
        Task<List<string>> GetBillPayees();
        Task<int> DeleteBillsForPayee(string payee);
<<<<<<< HEAD:BudgetAppCross.DataAccess/IDataManager.cs
        Task<int> ChangePayeeName(string oldName, string newName);
=======
        Task ChangePayeeName(string oldName, string newName);
>>>>>>> feature/ModifySQLite:BudgetAppCross.Core/Services/Data/IDataManager.cs
        Task<List<Bill>> GetBillsForPayee(string payee);
        Task UpdatePayeeNames();
        #endregion

<<<<<<< HEAD:BudgetAppCross.DataAccess/IDataManager.cs
=======
        #region Agenda
        Task<List<Bill>> GetUnpaidAndFutureBills(DateTime start, DateTime end);
        #endregion
>>>>>>> feature/ModifySQLite:BudgetAppCross.Core/Services/Data/IDataManager.cs
    }
}
