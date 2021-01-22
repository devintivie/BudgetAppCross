//using BudgetAppCross.Managers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace BudgetAppCross.SQLiteAccess
{
    public class SQLiteTableAccess
    {
        #region Fields

        #endregion

        #region Properties
        //public string ConnectionString => StateManager.Instance.DatabasePath;
        public string ConnectionString => ConfigurationManager.ConnectionStrings["main"].ConnectionString;

        public string TableName { get; private set; }
        #endregion

        #region Constructors
        public SQLiteTableAccess(string name)
        {
            TableName = name;
            var tmp = ConfigurationManager.AppSettings.AllKeys;
            
        }
        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion

    }
}
