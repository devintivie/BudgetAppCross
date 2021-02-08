using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core.Services
{
    public class SQLiteColumn
    {
        public string Name { get; private set; } = "Id";
        public SQLiteColumnType SQLiteDataType { get; private set; } = SQLiteColumnType.Integer;
        public Type ColumnType { get; set; } = typeof(int);
        public bool Nullable { get; private set; } = false;
        public bool PrimaryKey { get; private set; } = false;
        public bool AutoIncrement { get; private set; } = false;
        public bool Unique { get; private set; } = false;

        private List<ForeignKey> ForeignKeys = new List<ForeignKey>();
        

        public SQLiteColumn(string name)
        {
            Name = name;
        }

        public SQLiteColumn() : this("Id") { }

        #region FluentMethods

        public SQLiteColumn WithDatatype(SQLiteColumnType datatype)
        {
            SQLiteDataType = datatype;
            return this;
        }
        public SQLiteColumn AsNullable(bool nullable = true)
        {
            Nullable = nullable;
            return this;
        }

        public SQLiteColumn AsPrimaryKey(bool primary = true)
        {
            PrimaryKey = primary;
            return this;
        }

        public SQLiteColumn WithAutoIncrement(bool ai = true)
        {
            AutoIncrement = ai;
            return this;
        }

        public SQLiteColumn IsUnique(bool unique = true)
        {
            Unique = unique;
            return this;
        }

        public SQLiteColumn HasForeignKey(string thisKey, string otherTable, string otherId)
        {

            return this;
        }



        public string ToDbString()
        {
            var nullable = Nullable ? "" : " NOT NULL";
            var pk = PrimaryKey ? " PRIMARY KEY": "";
            var ai = AutoIncrement ? " AUTOINCREMENT" : "";
            var unique = Unique ? " UNIQUE" : "";
            return $"'{Name}' {SQLiteDataType}{nullable}{pk}{ai}{unique}";
        }
        #endregion

    }

   

}
