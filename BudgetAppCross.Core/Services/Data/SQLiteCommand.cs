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
        //SQLiteParameterCollection m_parameterCollection;
        //SqliteStatementPreparer m_statementPreparer;
        private XamarinSQLiteConnection _conn;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public SQLiteCommand(string commandText, XamarinSQLiteConnection connection)
        {
            CommandText = commandText;
            DbConnection = connection;
            _conn = (XamarinSQLiteConnection)DbConnection;
        }
        #endregion

        #region Methods

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
        public override UpdateRowSource UpdatedRowSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        protected override DbConnection DbConnection { get; set; }

        protected override DbParameterCollection DbParameterCollection => throw new NotImplementedException();

        protected override DbTransaction DbTransaction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
                    throw new ArgumentException("Execute failed errcode == contraint not null");
                }
            }

            throw SQLiteException.New(r, SQLiteIvie.GetErrmsg(_conn.Handle));
        }

        public override object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public override void Prepare()
        {
            //var stmt = SQLiteIvie.Prepare2(_conn.Handle, CommandText);
            //return stmt;
            throw new NotImplementedException();
        }

        public Sqlite3Statement PrepareQuery()
        {
            var stmt = SQLiteIvie.Prepare2(_conn.Handle, CommandText);
            return stmt;
        }
        private void Finalize(Sqlite3Statement stmt)
        {
            SQLiteIvie.Finalize(stmt);
        }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }
    }
}
