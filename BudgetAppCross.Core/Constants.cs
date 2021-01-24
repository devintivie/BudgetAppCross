//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace BudgetAppCross.Core
//{
//    public static class Constants
//    {
//        public const string DatabaseFilename = "BudgetSQLiteTemp.db3";//"BudgetSQLite.db3";

//        //public const string BudgetEFCoreFilename = "BudgetEFCore.db3";

//        //public const SQLite.SQLiteOpenFlags Flags =
//        //    // open the database in read/write mode
//        //    SQLite.SQLiteOpenFlags.ReadWrite |
//        //    // create the database if it doesn't exist
//        //    SQLite.SQLiteOpenFlags.Create |
//        //    // enable multi-threaded database access
//        //    SQLite.SQLiteOpenFlags.SharedCache;

//        public static string DatabasePath
//        {
//            get
//            {
//                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
//                var fullpath = Path.Combine(basePath, DatabaseFilename);
//                return fullpath;
//            }
//        }


//    }
//}
