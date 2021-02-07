using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


using Sqlite3DatabaseHandle = SQLitePCL.sqlite3;
using Sqlite3BackupHandle = SQLitePCL.sqlite3_backup;
using Sqlite3Statement = SQLitePCL.sqlite3_stmt;
using Sqlite3 = SQLitePCL.raw;
using System.Runtime.CompilerServices;

namespace BudgetAppCross.Core.Services
{
    internal sealed class SqliteStatementPreparer : IDisposable
    {
        readonly Sqlite3DatabaseHandle m_database;
        readonly byte[] m_commandTextBytes;
        List<Sqlite3Statement> m_statements;
        int m_bytesUsed;
        int m_refCount;


        public SqliteStatementPreparer(Sqlite3DatabaseHandle database, string commandText)
        {
            m_database = database;
            m_commandTextBytes = Encoding.UTF8.GetBytes(commandText);
            m_statements = new List<Sqlite3Statement>();
            m_refCount = 1;
        }

        public Sqlite3Statement Get(int index, CancellationToken cancellationToken)
        {
            if (m_statements == null)
                throw new ObjectDisposedException(GetType().Name);
            if (index < 0 || index > m_statements.Count)
                throw new ArgumentOutOfRangeException("index");
            if (index < m_statements.Count)
                return m_statements[index];
            if (m_bytesUsed == m_commandTextBytes.Length)
                return null;

            var commandString = Encoding.UTF8.GetString(m_commandTextBytes);
            Console.WriteLine(commandString);

            var r = SQLiteIvie.Prepare2(m_database, commandString);
            return r;


            //SQLiteIvie.Result errorCode;
            //do
            //{
            //    unsafe
            //    {
            //        fixed (byte* sqlBytes = &m_commandTextBytes[m_bytesUsed])
            //        {
            //            byte* remainingSqlBytes;

            //            string s = Encoding.UTF8.GetString((byte*)Unsafe.AsPointer(ref sqlBytes.GetPinnableReference()));

            //            Sqlite3Statement statement;
            //            errorCode = Sqlite3.sqlite3_prepare_v2(m_database, sqlBytes, out statement);
            //            switch (errorCode)
            //            {
            //                case SQLiteIvie.Result.OK:
            //                    m_bytesUsed += (int)(remainingSqlBytes - sqlBytes);
            //                    m_statements.Add(statement);
            //                    break;

            //                case SQLiteIvie.Result.Busy:
            //                case SQLiteIvie.Result.Locked:
            //                case SQLiteIvie.Result.CannotOpen:
            //                    if (cancellationToken.IsCancellationRequested)
            //                        return null;
            //                    Thread.Sleep(20);
            //                    break;

            //                default:
            //                    throw new SQLiteException(errorCode, m_database);
            //            }
            //        }
            //    }
            //} while (errorCode != SQLiteIvie.Result.OK);

            //return m_statements[index];
        }

        public void AddRef()
        {
            if (m_refCount == 0)
                throw new ObjectDisposedException(GetType().Name);
            m_refCount++;
        }

        public void Dispose()
        {
            m_refCount--;
            if (m_refCount == 0)
            {
                foreach (var statement in m_statements)
                    statement.Dispose();
                m_statements = null;
            }
            else if (m_refCount < 0)
            {
                throw new InvalidOperationException("SqliteStatementList ref count decremented below zero.");
            }
        }


    }
}
