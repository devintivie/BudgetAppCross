using System;
using System.Collections.Generic;
using System.Text;

using Sqlite3DatabaseHandle = SQLitePCL.sqlite3;
using Sqlite3BackupHandle = SQLitePCL.sqlite3_backup;
using Sqlite3Statement = SQLitePCL.sqlite3_stmt;
using Sqlite3 = SQLitePCL.raw;
using System.Data.Common;

namespace BudgetAppCross.Core.Services
{
	public class SQLiteException : DbException
	{
		public SQLiteIvie.Result Result { get; private set; }

		protected SQLiteException(SQLiteIvie.Result r, string message) : base(message)
		{
			Result = r;
		}

		public static SQLiteException New(SQLiteIvie.Result r, string message)
		{
			return new SQLiteException(r, message);
		}
	}

}
