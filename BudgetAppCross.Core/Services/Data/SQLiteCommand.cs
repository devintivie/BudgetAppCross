using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

using Sqlite3DatabaseHandle = SQLitePCL.sqlite3;
using Sqlite3BackupHandle = SQLitePCL.sqlite3_backup;
using Sqlite3Statement = SQLitePCL.sqlite3_stmt;
using Sqlite3 = SQLitePCL.raw;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace BudgetAppCross.Core.Services
{
	public partial class SQLiteCommand
	{
		XamarinSQLiteConnection _conn;
		private List<Binding> _bindings;

		public string CommandText { get; set; }

		public SQLiteCommand(XamarinSQLiteConnection conn)
		{
			_conn = conn;
			_bindings = new List<Binding>();
			CommandText = "";
		}

		public int ExecuteNonQuery()
		{
			if (_conn.Trace)
			{
				_conn.Tracer?.Invoke("Executing: " + this);
			}

			var r = SQLiteIvie.Result.OK;
			var stmt = Prepare();
			r = SQLiteIvie.Step(stmt);
			Finalize(stmt);
			if (r == SQLiteIvie.Result.Done)
			{
				int rowsAffected = SQLiteIvie.Changes(_conn.Handle);
				return rowsAffected;
			}
			else if (r == SQLiteIvie.Result.Error)
			{
				string msg = SQLiteIvie.GetErrmsg(_conn.Handle);
				throw SQLiteException.New(r, msg);
			}
			else if (r == SQLiteIvie.Result.Constraint)
			{
				if (SQLiteIvie.ExtendedErrCode(_conn.Handle) == SQLiteIvie.ExtendedResult.ConstraintNotNull)
				{
					throw NotNullConstraintViolationException.New(r, SQLiteIvie.GetErrmsg(_conn.Handle));
				}
			}

			throw SQLiteException.New(r, SQLiteIvie.GetErrmsg(_conn.Handle));
		}

		public IEnumerable<T> ExecuteDeferredQuery<T>()
		{
			return ExecuteDeferredQuery<T>(_conn.GetMapping(typeof(T)));
		}

		public List<T> ExecuteQuery<T>()
		{
			return ExecuteDeferredQuery<T>(_conn.GetMapping(typeof(T))).ToList();
		}

		public List<T> ExecuteQuery<T>(TableMapping map)
		{
			return ExecuteDeferredQuery<T>(map).ToList();
		}

		/// <summary>
		/// Invoked every time an instance is loaded from the database.
		/// </summary>
		/// <param name='obj'>
		/// The newly created object.
		/// </param>
		/// <remarks>
		/// This can be overridden in combination with the <see cref="SQLiteConnection.NewCommand"/>
		/// method to hook into the life-cycle of objects.
		/// </remarks>
		protected virtual void OnInstanceCreated(object obj)
		{
			// Can be overridden.
		}

		public IEnumerable<T> ExecuteDeferredQuery<T>(TableMapping map)
		{
			if (_conn.Trace)
			{
				_conn.Tracer?.Invoke("Executing Query: " + this);
			}

			var stmt = Prepare();
			try
			{
				var cols = new TableMapping.Column[SQLiteIvie.ColumnCount(stmt)];
				var fastColumnSetters = new Action<T, Sqlite3Statement, int>[SQLiteIvie.ColumnCount(stmt)];

				for (int i = 0; i < cols.Length; i++)
				{
					var name = SQLiteIvie.ColumnName16(stmt, i);
					cols[i] = map.FindColumn(name);
					if (cols[i] != null)
						fastColumnSetters[i] = FastColumnSetter.GetFastSetter<T>(_conn, cols[i]);
				}

				while (SQLiteIvie.Step(stmt) == SQLiteIvie.Result.Row)
				{
					var obj = Activator.CreateInstance(map.MappedType);
					for (int i = 0; i < cols.Length; i++)
					{
						if (cols[i] == null)
							continue;

						if (fastColumnSetters[i] != null)
						{
							fastColumnSetters[i].Invoke((T)obj, stmt, i);
						}
						else
						{
							var colType = SQLiteIvie.ColumnType(stmt, i);
							var val = ReadCol(stmt, i, colType, cols[i].ColumnType);
							cols[i].SetValue(obj, val);
						}
					}
					OnInstanceCreated(obj);
					yield return (T)obj;
				}
			}
			finally
			{
				SQLiteIvie.Finalize(stmt);
			}
		}

		public T ExecuteScalar<T>()
		{
			if (_conn.Trace)
			{
				_conn.Tracer?.Invoke("Executing Query: " + this);
			}

			T val = default(T);

			var stmt = Prepare();

			try
			{
				var r = SQLiteIvie.Step(stmt);
				if (r == SQLiteIvie.Result.Row)
				{
					var colType = SQLiteIvie.ColumnType(stmt, 0);
					var colval = ReadCol(stmt, 0, colType, typeof(T));
					if (colval != null)
					{
						val = (T)colval;
					}
				}
				else if (r == SQLiteIvie.Result.Done)
				{
				}
				else
				{
					throw SQLiteException.New(r, SQLiteIvie.GetErrmsg(_conn.Handle));
				}
			}
			finally
			{
				Finalize(stmt);
			}

			return val;
		}

		public IEnumerable<T> ExecuteQueryScalars<T>()
		{
			if (_conn.Trace)
			{
				_conn.Tracer?.Invoke("Executing Query: " + this);
			}
			var stmt = Prepare();
			try
			{
				if (SQLiteIvie.ColumnCount(stmt) < 1)
				{
					throw new InvalidOperationException("QueryScalars should return at least one column");
				}
				while (SQLiteIvie.Step(stmt) == SQLiteIvie.Result.Row)
				{
					var colType = SQLiteIvie.ColumnType(stmt, 0);
					var val = ReadCol(stmt, 0, colType, typeof(T));
					if (val == null)
					{
						yield return default(T);
					}
					else
					{
						yield return (T)val;
					}
				}
			}
			finally
			{
				Finalize(stmt);
			}
		}

		public void Bind(string name, object val)
		{
			_bindings.Add(new Binding
			{
				Name = name,
				Value = val
			});
		}

		public void Bind(object val)
		{
			Bind(null, val);
		}

		public override string ToString()
		{
			var parts = new string[1 + _bindings.Count];
			parts[0] = CommandText;
			var i = 1;
			foreach (var b in _bindings)
			{
				parts[i] = string.Format("  {0}: {1}", i - 1, b.Value);
				i++;
			}
			return string.Join(Environment.NewLine, parts);
		}

		Sqlite3Statement Prepare()
		{
			var stmt = SQLiteIvie.Prepare2(_conn.Handle, CommandText);
			BindAll(stmt);
			return stmt;
		}

		void Finalize(Sqlite3Statement stmt)
		{
			SQLiteIvie.Finalize(stmt);
		}

		void BindAll(Sqlite3Statement stmt)
		{
			int nextIdx = 1;
			foreach (var b in _bindings)
			{
				if (b.Name != null)
				{
					b.Index = SQLiteIvie.BindParameterIndex(stmt, b.Name);
				}
				else
				{
					b.Index = nextIdx++;
				}

				BindParameter(stmt, b.Index, b.Value, _conn.StoreDateTimeAsTicks, _conn.DateTimeStringFormat, _conn.StoreTimeSpanAsTicks);
			}
		}

		static IntPtr NegativePointer = new IntPtr(-1);

		internal static void BindParameter(Sqlite3Statement stmt, int index, object value, bool storeDateTimeAsTicks, string dateTimeStringFormat, bool storeTimeSpanAsTicks)
		{
			if (value == null)
			{
				SQLiteIvie.BindNull(stmt, index);
			}
			else
			{
				if (value is Int32)
				{
					SQLiteIvie.BindInt(stmt, index, (int)value);
				}
				else if (value is String)
				{
					SQLiteIvie.BindText(stmt, index, (string)value, -1, NegativePointer);
				}
				else if (value is Byte || value is UInt16 || value is SByte || value is Int16)
				{
					SQLiteIvie.BindInt(stmt, index, Convert.ToInt32(value));
				}
				else if (value is Boolean)
				{
					SQLiteIvie.BindInt(stmt, index, (bool)value ? 1 : 0);
				}
				else if (value is UInt32 || value is Int64)
				{
					SQLiteIvie.BindInt64(stmt, index, Convert.ToInt64(value));
				}
				else if (value is Single || value is Double || value is Decimal)
				{
					SQLiteIvie.BindDouble(stmt, index, Convert.ToDouble(value));
				}
				else if (value is TimeSpan)
				{
					if (storeTimeSpanAsTicks)
					{
						SQLiteIvie.BindInt64(stmt, index, ((TimeSpan)value).Ticks);
					}
					else
					{
						SQLiteIvie.BindText(stmt, index, ((TimeSpan)value).ToString(), -1, NegativePointer);
					}
				}
				else if (value is DateTime)
				{
					if (storeDateTimeAsTicks)
					{
						SQLiteIvie.BindInt64(stmt, index, ((DateTime)value).Ticks);
					}
					else
					{
						SQLiteIvie.BindText(stmt, index, ((DateTime)value).ToString(dateTimeStringFormat, System.Globalization.CultureInfo.InvariantCulture), -1, NegativePointer);
					}
				}
				else if (value is DateTimeOffset)
				{
					SQLiteIvie.BindInt64(stmt, index, ((DateTimeOffset)value).UtcTicks);
				}
				else if (value is byte[])
				{
					SQLiteIvie.BindBlob(stmt, index, (byte[])value, ((byte[])value).Length, NegativePointer);
				}
				else if (value is Guid)
				{
					SQLiteIvie.BindText(stmt, index, ((Guid)value).ToString(), 72, NegativePointer);
				}
				else if (value is Uri)
				{
					SQLiteIvie.BindText(stmt, index, ((Uri)value).ToString(), -1, NegativePointer);
				}
				else if (value is StringBuilder)
				{
					SQLiteIvie.BindText(stmt, index, ((StringBuilder)value).ToString(), -1, NegativePointer);
				}
				else if (value is UriBuilder)
				{
					SQLiteIvie.BindText(stmt, index, ((UriBuilder)value).ToString(), -1, NegativePointer);
				}
				else
				{
					// Now we could possibly get an enum, retrieve cached info
					var valueType = value.GetType();
					var enumInfo = EnumCache.GetInfo(valueType);
					if (enumInfo.IsEnum)
					{
						var enumIntValue = Convert.ToInt32(value);
						if (enumInfo.StoreAsText)
							SQLiteIvie.BindText(stmt, index, enumInfo.EnumValues[enumIntValue], -1, NegativePointer);
						else
							SQLiteIvie.BindInt(stmt, index, enumIntValue);
					}
					else
					{
						throw new NotSupportedException("Cannot store type: " + Orm.GetType(value));
					}
				}
			}
		}

		class Binding
		{
			public string Name { get; set; }

			public object Value { get; set; }

			public int Index { get; set; }
		}

		object ReadCol(Sqlite3Statement stmt, int index, SQLiteIvie.ColType type, Type clrType)
		{
			if (type == SQLiteIvie.ColType.Null)
			{
				return null;
			}
			else
			{
				var clrTypeInfo = clrType.GetTypeInfo();
				if (clrTypeInfo.IsGenericType && clrTypeInfo.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					clrType = clrTypeInfo.GenericTypeArguments[0];
					clrTypeInfo = clrType.GetTypeInfo();
				}

				if (clrType == typeof(String))
				{
					return SQLiteIvie.ColumnString(stmt, index);
				}
				else if (clrType == typeof(Int32))
				{
					return (int)SQLiteIvie.ColumnInt(stmt, index);
				}
				else if (clrType == typeof(Boolean))
				{
					return SQLiteIvie.ColumnInt(stmt, index) == 1;
				}
				else if (clrType == typeof(double))
				{
					return SQLiteIvie.ColumnDouble(stmt, index);
				}
				else if (clrType == typeof(float))
				{
					return (float)SQLiteIvie.ColumnDouble(stmt, index);
				}
				else if (clrType == typeof(TimeSpan))
				{
					if (_conn.StoreTimeSpanAsTicks)
					{
						return new TimeSpan(SQLiteIvie.ColumnInt64(stmt, index));
					}
					else
					{
						var text = SQLiteIvie.ColumnString(stmt, index);
						TimeSpan resultTime;
						if (!TimeSpan.TryParseExact(text, "c", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.TimeSpanStyles.None, out resultTime))
						{
							resultTime = TimeSpan.Parse(text);
						}
						return resultTime;
					}
				}
				else if (clrType == typeof(DateTime))
				{
					if (_conn.StoreDateTimeAsTicks)
					{
						return new DateTime(SQLiteIvie.ColumnInt64(stmt, index));
					}
					else
					{
						var text = SQLiteIvie.ColumnString(stmt, index);
						DateTime resultDate;
						if (!DateTime.TryParseExact(text, _conn.DateTimeStringFormat, System.Globalization.CultureInfo.InvariantCulture, _conn.DateTimeStyle, out resultDate))
						{
							resultDate = DateTime.Parse(text);
						}
						return resultDate;
					}
				}
				else if (clrType == typeof(DateTimeOffset))
				{
					return new DateTimeOffset(SQLiteIvie.ColumnInt64(stmt, index), TimeSpan.Zero);
				}
				else if (clrTypeInfo.IsEnum)
				{
					if (type == SQLiteIvie.ColType.Text)
					{
						var value = SQLiteIvie.ColumnString(stmt, index);
						return Enum.Parse(clrType, value.ToString(), true);
					}
					else
						return SQLiteIvie.ColumnInt(stmt, index);
				}
				else if (clrType == typeof(Int64))
				{
					return SQLiteIvie.ColumnInt64(stmt, index);
				}
				else if (clrType == typeof(UInt32))
				{
					return (uint)SQLiteIvie.ColumnInt64(stmt, index);
				}
				else if (clrType == typeof(decimal))
				{
					return (decimal)SQLiteIvie.ColumnDouble(stmt, index);
				}
				else if (clrType == typeof(Byte))
				{
					return (byte)SQLiteIvie.ColumnInt(stmt, index);
				}
				else if (clrType == typeof(UInt16))
				{
					return (ushort)SQLiteIvie.ColumnInt(stmt, index);
				}
				else if (clrType == typeof(Int16))
				{
					return (short)SQLiteIvie.ColumnInt(stmt, index);
				}
				else if (clrType == typeof(sbyte))
				{
					return (sbyte)SQLiteIvie.ColumnInt(stmt, index);
				}
				else if (clrType == typeof(byte[]))
				{
					return SQLiteIvie.ColumnByteArray(stmt, index);
				}
				else if (clrType == typeof(Guid))
				{
					var text = SQLiteIvie.ColumnString(stmt, index);
					return new Guid(text);
				}
				else if (clrType == typeof(Uri))
				{
					var text = SQLiteIvie.ColumnString(stmt, index);
					return new Uri(text);
				}
				else if (clrType == typeof(StringBuilder))
				{
					var text = SQLiteIvie.ColumnString(stmt, index);
					return new StringBuilder(text);
				}
				else if (clrType == typeof(UriBuilder))
				{
					var text = SQLiteIvie.ColumnString(stmt, index);
					return new UriBuilder(text);
				}
				else
				{
					throw new NotSupportedException("Don't know how to read " + clrType);
				}
			}
		}
	}

	// public class SQLiteCommand// : DbCommand
	// {
	//     #region Fields
	//     SQLiteParameterCollection m_parameterCollection;
	//     SqliteStatementPreparer m_statementPreparer;
	//     //private XamarinSQLiteConnection _conn;
	//     #endregion

	//     #region Properties
	//     Sqlite3DatabaseHandle DatabaseHandle => ((XamarinSQLiteConnection)Connection).Handle;
	//     #endregion

	//     #region Constructors
	//     public SQLiteCommand()
	//         : this(null, null, null)
	//     {
	//     }

	//     public SQLiteCommand(string commandText)
	//         : this(commandText, null, null)
	//     {
	//     }

	//     public SQLiteCommand(XamarinSQLiteConnection connection)
	//         : this(null, connection, null)
	//     {
	//     }

	//     public SQLiteCommand(string commandText, XamarinSQLiteConnection connection)
	//         : this(commandText, connection, null)
	//     {
	//     }

	//     public SQLiteCommand(string commandText, XamarinSQLiteConnection connection, SQLiteTransaction transaction)
	//     {
	//         CommandText = commandText;
	//         DbConnection = connection;
	//         DbTransaction = transaction;
	//         m_parameterCollection = new SQLiteParameterCollection();
	//     }
	//     #endregion

	//     #region Methods
	//     internal SqliteStatementPreparer GetStatementPreparer()
	//     {
	//         m_statementPreparer.AddRef();
	//         return m_statementPreparer;
	//     }
	//     #endregion






	//     public override string CommandText { get; set; }
	//     public override int CommandTimeout { get; set; }
	//     public override CommandType CommandType
	//     {
	//         get
	//         {
	//             return CommandType.Text;
	//         }
	//         set
	//         {
	//             if (value != CommandType.Text)
	//                 throw new ArgumentException("CommandType must be Text.", "value");
	//         }
	//     }
	//     public override bool DesignTimeVisible { get; set; }
	//     public override UpdateRowSource UpdatedRowSource
	//     {
	//         get { throw new NotSupportedException(); }
	//         set { throw new NotSupportedException(); }
	//     }
	//     protected override DbConnection DbConnection { get; set; }
	//     public new SQLiteParameterCollection Parameters
	//     {
	//         get
	//         {
	//             VerifyNotDisposed();
	//             return m_parameterCollection;
	//         }
	//     }

	//     protected override DbParameterCollection DbParameterCollection => Parameters;

	//     protected override DbTransaction DbTransaction { get; set; }

	//     public override void Cancel()
	//     {
	//         Debug.WriteLine("Cancel method");
	//         //throw new NotImplementedException();
	//     }

	//     public override int ExecuteNonQuery()
	//     {
	//         var r = SQLiteIvie.Result.OK;
	//         var stmt = PrepareQuery();
	//         r = SQLiteIvie.Step(stmt);
	//         Finalize(stmt);
	//         if (r == SQLiteIvie.Result.Done)
	//         {
	//             int rowsAffected = SQLiteIvie.Changes(DatabaseHandle);
	//             return rowsAffected;
	//         }
	//         else if (r == SQLiteIvie.Result.Error)
	//         {
	//             string msg = SQLiteIvie.GetErrmsg(DatabaseHandle);
	//             throw SQLiteException.New(r, msg);
	//         }
	//         else if (r == SQLiteIvie.Result.Constraint)
	//         {
	//             if (SQLiteIvie.ExtendedErrCode(DatabaseHandle) == SQLiteIvie.ExtendedResult.ConstraintNotNull)
	//             {
	//                 throw new ArgumentException("Execute failed errcode == contraint not null");
	//             }
	//         }

	//         throw SQLiteException.New(r, SQLiteIvie.GetErrmsg(DatabaseHandle));
	//     }

	//     public override object ExecuteScalar()
	//     {
	//         using (var reader = ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SingleRow))
	//{
	//	do
	//	{
	//		if (reader.Read())
	//			return reader.GetValue(0);
	//	} while (reader.NextResult());
	//}
	//return null;
	//     }
	//     public override async Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
	//     {
	//         using (var reader = await ExecuteReaderAsync(CommandBehavior.SingleResult | CommandBehavior.SingleRow, cancellationToken))
	//         {
	//             do
	//             {
	//                 if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
	//                     return reader.GetValue(0);
	//             } while (await reader.NextResultAsync(cancellationToken).ConfigureAwait(false));
	//         }
	//         return null;
	//     }

	//     //internal SqliteStatementPreparer GetStatementPreparer()
	//     //{
	//     //    m_statementPreparer.AddRef();
	//     //    return m_statementPreparer;
	//     //}



	//     public Sqlite3Statement PrepareQuery()
	//     {
	//         var stmt = SQLiteIvie.Prepare2(DatabaseHandle, CommandText);
	//         return stmt;
	//     }
	//     private void Finalize(Sqlite3Statement stmt)
	//     {
	//         SQLiteIvie.Finalize(stmt);
	//     }

	//     protected override DbParameter CreateDbParameter()
	//     {
	//         VerifyNotDisposed();
	//         return new SQLiteParameter();
	//     }

	//     protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
	//     {
	//         VerifyValid();
	//         Prepare();
	//         return SQLiteDataReader.Create(this, behavior);
	//     }

	//     //public new SQLiteDataReader ExecuteReader()
	//     //{
	//     //    return (SQLiteDataReader)base.ExecuteReader();
	//     //}

	//     private void VerifyValid()
	//     {
	//         //VerifyNotDisposed();
	//         if (DbConnection == null)
	//             throw new InvalidOperationException("Connection property must be non-null.");
	//         if (DbConnection.State != ConnectionState.Open && DbConnection.State != ConnectionState.Connecting)
	//             throw new InvalidOperationException("Connection must be Open; current state is {0}.".FormatInvariant(DbConnection.State));
	//         if (DbTransaction != ((XamarinSQLiteConnection)DbConnection).CurrentTransaction)
	//             throw new InvalidOperationException("The transaction associated with this command is not the connection's active transaction.");
	//         if (string.IsNullOrWhiteSpace(CommandText))
	//             throw new InvalidOperationException("CommandText must be specified");
	//     }

	//     private void VerifyNotDisposed()
	//     {
	//         if (m_parameterCollection == null)
	//             throw new ObjectDisposedException(GetType().Name);
	//     }

	//     public override void Prepare()
	//     {
	//         if (m_statementPreparer == null)
	//             m_statementPreparer = new SqliteStatementPreparer(DatabaseHandle, CommandText.Trim());
	//     }
	// }
}
