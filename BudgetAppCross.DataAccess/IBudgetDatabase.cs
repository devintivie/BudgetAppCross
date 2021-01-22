using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.DataAccess
{
    public interface IBudgetDatabase
    {
        IAccountRepo AccountAccess { get; }
        IBillRepo BillAccess { get; }
        IBalanceRepo BalanceAccess { get; }
        string ConnectionString { get; }

        Task Initialize();
    }
}
