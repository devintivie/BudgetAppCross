using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.SQLiteAccess
{
    public enum ForeignKeyAction
    {
        NO_ACTION,
        RESTRICT,
        SET_NULL,
        SET_DEFAULT,
        CASCADE
    }
}
