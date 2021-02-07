using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

using Sqlite3DatabaseHandle = SQLitePCL.sqlite3;
using Sqlite3BackupHandle = SQLitePCL.sqlite3_backup;
using Sqlite3Statement = SQLitePCL.sqlite3_stmt;
using Sqlite3 = SQLitePCL.raw;

namespace BudgetAppCross.Core.Services
{
    public class SQLiteCommand : DbCommand
    {
        #region Fields
        SQLiteParameterCollection m_parameterCollection;
        SqliteStatementPreparer m_statementPreparer;
        //private XamarinSQLiteConnection _conn;
        #endregion

        #region Properties
        Sqlite3DatabaseHandle DatabaseHandle => ((XamarinSQLiteConnection)Connection).Handle;
        #endregion

        #region Constructors
        public SQLiteCommand()
            : this(null, null, null)
        {
        }

        public SQLiteCommand(string commandText)
            : this(commandText, null, null)
        {
        }

        public SQLiteCommand(XamarinSQLiteConnection connection)
            : this(null, connection, null)
        {
        }

        public SQLiteCommand(string commandText, XamarinSQLiteConnection connection)
            : this(commandText, connection, null)
        {
        }

        public SQLiteCommand(string commandText, XamarinSQLiteConnection connection, SQLiteTransaction transaction)
        {
            CommandText = commandText;
            DbConnection = connection;
            DbTransaction = transaction;
            m_parameterCollection = new SQLiteParameterCollection();
        }
        #endregion

        #region Methods
        internal SqliteStatementPreparer GetStatementPreparer()
        {
            m_statementPreparer.AddRef();
            return m_statementPreparer;
        }
        #endregion






        public override string CommandText { get; set; }
        public override int CommandTimeout { get; set; }
        public override CommandType CommandType
        {
            get
            {
                return CommandType.Text;
            }
            set
            {
                if (value != CommandType.Text)
                    throw new ArgumentException("CommandType must be Text.", "value");
            }
        }
        public override bool DesignTimeVisible { get; set; }
        public override UpdateRowSource UpdatedRowSource
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }
        protected override DbConnection DbConnection { get; set; }
        public new SQLiteParameterCollection Parameters
        {
            get
            {
                VerifyNotDisposed();
                return m_parameterCollection;
            }
        }

        protected override DbParameterCollection DbParameterCollection => Parameters;

        protected override DbTransaction DbTransaction { get; set; }

        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override int ExecuteNonQuery()
        {
            var r = SQLiteIvie.Result.OK;
            var stmt = PrepareQuery();
            r = SQLiteIvie.Step(stmt);
            Finalize(stmt);
            if (r == SQLiteIvie.Result.Done)
            {
                int rowsAffected = SQLiteIvie.Changes(DatabaseHandle);
                return rowsAffected;
            }
            else if (r == SQLiteIvie.Result.Error)
            {
                string msg = SQLiteIvie.GetErrmsg(DatabaseHandle);
                throw SQLiteException.New(r, msg);
            }
            else if (r == SQLiteIvie.Result.Constraint)
            {
                if (SQLiteIvie.ExtendedErrCode(DatabaseHandle) == SQLiteIvie.ExtendedResult.ConstraintNotNull)
                {
                    throw new ArgumentException("Execute failed errcode == contraint not null");
                }
            }

            throw SQLiteException.New(r, SQLiteIvie.GetErrmsg(DatabaseHandle));
        }

        public override object ExecuteScalar()
        {
            using (var reader = ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SingleRow))
			{
				do
				{
					if (reader.Read())
						return reader.GetValue(0);
				} while (reader.NextResult());
			}
			return null;
        }

        //internal SqliteStatementPreparer GetStatementPreparer()
        //{
        //    m_statementPreparer.AddRef();
        //    return m_statementPreparer;
        //}



        public Sqlite3Statement PrepareQuery()
        {
            var stmt = SQLiteIvie.Prepare2(DatabaseHandle, CommandText);
            return stmt;
        }
        private void Finalize(Sqlite3Statement stmt)
        {
            SQLiteIvie.Finalize(stmt);
        }

        protected override DbParameter CreateDbParameter()
        {
            VerifyNotDisposed();
            return new SQLiteParameter();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            VerifyValid();
            Prepare();
            return SQLiteDataReader.Create(this, behavior);
        }

        private void VerifyValid()
        {
            //VerifyNotDisposed();
            if (DbConnection == null)
                throw new InvalidOperationException("Connection property must be non-null.");
            if (DbConnection.State != ConnectionState.Open && DbConnection.State != ConnectionState.Connecting)
                throw new InvalidOperationException("Connection must be Open; current state is {0}.".FormatInvariant(DbConnection.State));
            if (DbTransaction != ((XamarinSQLiteConnection)DbConnection).CurrentTransaction)
                throw new InvalidOperationException("The transaction associated with this command is not the connection's active transaction.");
            if (string.IsNullOrWhiteSpace(CommandText))
                throw new InvalidOperationException("CommandText must be specified");
        }

        private void VerifyNotDisposed()
        {
            if (m_parameterCollection == null)
                throw new ObjectDisposedException(GetType().Name);
        }

        public override void Prepare()
        {
            if (m_statementPreparer == null)
                m_statementPreparer = new SqliteStatementPreparer(DatabaseHandle, CommandText.Trim());
        }
    }
}
