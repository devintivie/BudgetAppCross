using BudgetAppCross.Models;
using SQLiteHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.DataAccess
{
    public interface IAccountRepo : IDataAccessRepo
    {
        #region Fields

        #endregion

        #region Properties
        #endregion

        #region Constructors

        #endregion

        #region Methods
        Task<InsertResult> InsertAccountAsync(BankAccount acct);
        Task<List<BankAccount>> GetAllAccountsAsync();
        Task<BankAccount> GetAccountAsync(string nickname);
        Task<int> DeleteAccountAsync(int accountId);
        Task<int> UpdateAccountAsync(BankAccount acct);
        #endregion
    }
}
