using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core.Services
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
