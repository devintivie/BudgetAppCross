using BudgetAppCross.DataAccess;
using BudgetAppCross.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.SQLiteAccess
{
    public class SQLiteBillAccess : SQLiteTableAccess, IBillRepo
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public SQLiteBillAccess(string name) : base(name)
        {

        }


        #endregion

        #region Methods
        public async Task<List<Bill>> GetAgendaBills()
        {
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                var p = new
                {
                    IsPaid = false,
                    Today = DateTime.Now,
                };

                //WHERE IsPaid = @IsPaid OR
                var query = $@"SELECT * FROM {TableName} 
                        WHERE IsPaid = @IsPaid 
                        OR Timestamp >= @Today
                        )";
                var output = await conn.QueryAsync<Bill>(query, p);
                return output.ToList();
            }
        }
        #endregion

    }
}
