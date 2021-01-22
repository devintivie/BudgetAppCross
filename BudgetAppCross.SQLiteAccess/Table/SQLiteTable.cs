using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BudgetAppCross.SQLiteAccess
{
    public class SQLiteTable
    {
        #region Properties
        public string TableName { get; set; }
        public Dictionary<string, SQLiteColumn> Columns { get; private set; } = new Dictionary<string, SQLiteColumn>();
        public Dictionary<string, ITableAttribute> Attributes { get; set; } = new Dictionary<string, ITableAttribute>();
        #endregion

        #region Constructors
        public SQLiteTable(string tableName)
        {
            TableName = tableName;
        }
        #endregion

        #region Prep Methods
        public void AddForeignKey(ForeignKey fk)
        {
            Attributes.Add(fk.KeyName, fk);
        }

        public void AddColumn(SQLiteColumn column)
        {
            Columns.Add(column.Name, column);
        }


        #endregion

        #region File Methods
        public string BuildTableScript()
        {
            var sb = new StringBuilder();
            sb.Append($"CREATE TABLE IF NOT EXISTS '{TableName}' (\n");
            var lastCol = Columns.Keys.Last();
            foreach (var col in Columns)
            {
                sb.Append(col.Value.ToDbString());
                if (!col.Key.Equals(lastCol)) { sb.Append(",\n"); }
            }

            foreach (var attr in Attributes)
            {
                sb.Append(",\n");
                sb.Append(attr.Value.ToDbString());
            }

            sb.Append(");");


            return sb.ToString();
        }
        #endregion
    }
}
