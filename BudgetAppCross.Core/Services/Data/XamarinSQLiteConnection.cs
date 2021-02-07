using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

using Sqlite3DatabaseHandle = SQLitePCL.sqlite3;
using Sqlite3BackupHandle = SQLitePCL.sqlite3_backup;
using Sqlite3Statement = SQLitePCL.sqlite3_stmt;
using Sqlite3 = SQLitePCL.raw;
using System.Diagnostics;
using System.Linq;
using SQLitePCL;

namespace BudgetAppCross.Core.Services
{
    public class XamarinSQLiteConnection : DbConnection
    {

        #region Fields
        string m_dataSource;
        bool m_isDisposed; 
        ConnectionState m_connectionState;
        #endregion

        #region Internal
        readonly Stack<SQLiteTransaction> m_transactions;
        internal bool IsOnlyTransaction(SQLiteTransaction transaction)
        {
            return m_transactions.Count == 1 && m_transactions.Peek() == transaction;
        }
        internal SQLiteTransaction CurrentTransaction => m_transactions.FirstOrDefault();
        internal void PopTransaction()
        {
            m_transactions.Pop();
        }
        #endregion

        #region Properties
        public override string ConnectionString { get; set; }

        public override string Database => throw new NotSupportedException();

        public override string DataSource => m_dataSource;

        public override string ServerVersion => throw new NotSupportedException();

        public override ConnectionState State => m_connectionState;

        public Sqlite3DatabaseHandle Handle { get; private set; }
        static readonly Sqlite3DatabaseHandle NullHandle = default(Sqlite3DatabaseHandle);
        static readonly Sqlite3BackupHandle NullBackupHandle = default(Sqlite3BackupHandle);
        /// <summary>
		/// Gets the database path used by this connection.
		/// </summary>
		public string DatabasePath { get; private set; }

        /// <summary>
        /// Gets the SQLite library version number. 3007014 would be v3.7.14
        /// </summary>
        public int LibVersionNumber { get; private set; }
        public bool Trace { get; set; }
        public Action<string> Tracer { get; set; }
        #endregion

        #region Constructors

        static XamarinSQLiteConnection()
        {

            try
            {
                SQLitePCL.Batteries_V2.Init();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }
        public XamarinSQLiteConnection(string databasePath)
        {
            m_transactions = new Stack<SQLiteTransaction>();
            DatabasePath = databasePath;
            LibVersionNumber = SQLiteIvie.LibVersionNumber();
            Debug.WriteLine($"Lib Version {LibVersionNumber}");
        }
        #endregion

        #region Methods
        public override void ChangeDatabase(string databaseName)
        {
            throw new NotSupportedException();
        }

        public override void Close()
        {
            Dispose();
        }

        private void VerifyNotDisposed()
        {
            if (m_isDisposed) { throw new ObjectDisposedException(GetType().Name); }

            

		}

        protected override void Dispose(bool disposing)
        {
            try
            {
                //m_parameterCollection = null;
                //Utility.Dispose(ref m_statementPreparer);
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        #endregion





        public override void Open()
        {
            VerifyNotDisposed();
            SetState(ConnectionState.Connecting);

            SQLiteOpenFlags flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache |
            SQLiteOpenFlags.FullMutex;

            Sqlite3DatabaseHandle handle;
            var r = SQLiteIvie.Open(DatabasePath, out handle, (int)flags, null);

            if (r != SQLiteIvie.Result.OK)
            {
                SetState(ConnectionState.Broken);
                throw SQLiteException.New(r, String.Format("Could not open database file: {0} ({1})", DatabasePath, r));
            }
            Handle = handle;

        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            if (isolationLevel == IsolationLevel.Unspecified)
                isolationLevel = IsolationLevel.Serializable;
            if (isolationLevel != IsolationLevel.Serializable && isolationLevel != IsolationLevel.ReadCommitted)
                throw new ArgumentOutOfRangeException("isolationLevel", isolationLevel, "Specified IsolationLevel value is not supported.");

            if (m_transactions.Count == 0)
                this.ExecuteNonQuery(isolationLevel == IsolationLevel.Serializable ? "BEGIN IMMEDIATE" : "BEGIN");
            m_transactions.Push(new SQLiteTransaction(this, isolationLevel));
            return CurrentTransaction;
        }

        protected override DbCommand CreateDbCommand()
        {
            return new SQLiteCommand("", this);
        }

        private void SetState(ConnectionState newState)
        {
            if (m_connectionState != newState)
            {
                var previousState = m_connectionState;
                m_connectionState = newState;
                OnStateChange(new StateChangeEventArgs(previousState, newState));
            }
        }

        internal static byte[] ToNullTerminatedUtf8(string value)
        {
            var encoding = Encoding.UTF8;
            int len = encoding.GetByteCount(value);
            byte[] bytes = new byte[len + 1];
            encoding.GetBytes(value, 0, value.Length, bytes, 0);
            return bytes;
        }

        //internal static string FromUtf8(utf8z ptr)
        //{
        //    int length = 0;
        //    unsafe
        //    {
        //        byte* p = (byte*)ptr.ToPointer();
        //        while (*p++ != 0)
        //            length++;
        //    }

        //    return FromUtf8(ptr, length);
        //}

        //FromUtf8
    }
}
