using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.SQLiteAccess
{
    public interface ITableAttribute
    {
        string ToDbString();
    }
}
