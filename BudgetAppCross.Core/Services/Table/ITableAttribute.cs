using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core.Services
{
    public interface ITableAttribute
    {
        string ToDbString();
    }
}
