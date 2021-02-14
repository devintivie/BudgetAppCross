<<<<<<< HEAD
﻿using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.DataAccess
{
    public interface IBalanceRepo : IDataAccessRepo
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods
        Task<int> InsertBalanceAsync(Balance balance);
        Task<List<Balance>> GetAllBalancesAsync();
        Task<BankAccount> GetBalanceAsync(string nickname);
        Task<int> DeleteBalanceAsync(int accountId);
        Task<int> UpdateBalanceAsync(Balance balance);
        #endregion

=======
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.DataAccess
{
    public interface IBalanceRepo
    {
>>>>>>> feature/ModifySQLite
    }
}
