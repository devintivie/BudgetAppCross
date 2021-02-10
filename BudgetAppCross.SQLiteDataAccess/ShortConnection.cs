using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.SQLiteDataAccess
{
    public class ShortConnection : SQLiteConnection
    {
        public const SQLiteOpenFlags flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache |
            SQLiteOpenFlags.FullMutex;
        public ShortConnection(string databasePath, SQLiteOpenFlags openFlags, bool storeDateTimeAsTicks = true) : base(databasePath, openFlags, storeDateTimeAsTicks)
        {
        }

        public ShortConnection(string databasePath) : this(databasePath, flags, true)
        {

        }
    }
}
