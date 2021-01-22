using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.DataAccess
{
    public struct InsertResult
    {
        public int RowsAffected { get; set; }
        public int LastId { get; set; }

        public InsertResult(int rows, int id)
        {
            RowsAffected = rows;
            LastId = id;
        }

    }
}
