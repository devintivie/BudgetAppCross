//using BudgetAppCross.Models;
//using BudgetAppCross.Models.Bills;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Xamarin.Essentials;

namespace BudgetAppCross.StateManagers//ore.Services
{
    public class StateManager
    {
        #region Singleton
        private static readonly Lazy<StateManager> instance = new Lazy<StateManager>();
        public static StateManager Instance => instance.Value;
        public StateManager()
        {
            basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        }
        #endregion

        #region Fields
        private const string stateFilename = "budgetState.json";
        private string basePath = "";



        #endregion

        #region Properties
        public string DatabaseFilename { get; set; } = null;
        public List<string> Budgets { get; private set; }
        public bool FullVersionPaid { get; private set; } = false;

        //Free Version Limitations
        public const int MAX_PAYEES = int.MaxValue;
        public const int MAX_ACCOUNTS = int.MaxValue;

        //public const SQLite.SQLiteOpenFlags Flags =
        //    // open the database in read/write mode
        //    SQLite.SQLiteOpenFlags.ReadWrite |
        //    // create the database if it doesn't exist
        //    SQLite.SQLiteOpenFlags.Create |
        //    // enable multi-threaded database access
        //    SQLite.SQLiteOpenFlags.SharedCache |
        //    SQLite.SQLiteOpenFlags.FullMutex;

        public string DatabasePath
        {
            get
            {
                //var currentFileAndExt = $"{DatabaseFilename}.sqlite3";
                var fullpath = Path.Combine(basePath, DatabaseFilename);
                var connectionString = $"DataSource={fullpath}.sqlite3;Version=3;";
                //var fullpath = Path.Combine(basePath, currentFileAndExt);
                return connectionString;
            }
        }
        #endregion

        #region Methods
        public void SaveState()
        {
            var path = $"{basePath}/{stateFilename}";

            var state = new State
            {
                DatabaseFilename = DatabaseFilename
            };
            //await Task.Run(() =>
            //{
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, state);
            }
            //});
        }

        public string LoadState()
        {
            var path = Path.Combine(basePath, stateFilename);


            //await Task.Run(() =>
            //{
            //File.Delete(path);
            var state = new State();
            try
            {
                var filenames = Directory.GetFiles(basePath);
                foreach (var item in filenames)
                {
                    Console.WriteLine(item);
                }
                var str = File.ReadAllText(path);
                state = JsonConvert.DeserializeObject<State>(str);
                DatabaseFilename = state.DatabaseFilename;
            }
            catch (FileNotFoundException e)
            {
                SaveState();
                var filenames2 = Directory.GetFiles(basePath);
                foreach (var item in filenames2)
                {
                    Console.WriteLine(item);
                }
                var str = File.ReadAllText(path);
                state = JsonConvert.DeserializeObject<State>(str);

                DatabaseFilename = state.DatabaseFilename;
            }
            //});
            return DatabaseFilename;
        }


        public async Task<List<string>> FindBudgetFiles()
        {
            await Task.Run(() =>
            {
                var filenames = Directory.GetFiles(basePath, "*.sqlite3");

                Budgets = filenames.Select(fn => Path.GetFileNameWithoutExtension(fn)).ToList();
            });

            return Budgets;
        }

        public async Task DeleteBudgetFile(string budgetName)
        {
            await Task.Run(() =>
            {
                var filename = $"{budgetName}.db3";
                var fullpath = Path.Combine(basePath, filename);
                File.Delete(fullpath);
            });

            if (DatabaseFilename == budgetName)
            {
                DatabaseFilename = null;
                SaveState();
            }
        }

        #endregion

    }

    [Serializable]
    public class State
    {
        public string DatabaseFilename { get; set; }
    }
}
