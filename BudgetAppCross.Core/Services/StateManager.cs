using BudgetAppCross.Models;
using BudgetAppCross.Models.Bills;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BudgetAppCross.Core.Services
{
    public class StateManager
    {
        #region Singleton
        private static readonly Lazy<StateManager> instance = new Lazy<StateManager>();
        public static StateManager Instance => instance.Value;
        public StateManager()
        {
            basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        }
        #endregion

        #region Fields
        private const string stateFilename = "budgetState.json";
        private string basePath = "";

        #endregion

        #region Properties
        public string DatabaseFilename { get; set; } = null;//"None";
        public List<string> Budgets { get; private set; }

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache |
            SQLite.SQLiteOpenFlags.FullMutex;

        public string DatabasePath
        {
            get
            {
                var currentFileAndExt = $"{DatabaseFilename}.db3";
                var fullpath = Path.Combine(basePath, currentFileAndExt);
                return fullpath;
            }
        }
        #endregion

        #region Methods
        public async Task SaveState()
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

        //public void SaveState()
        //{
        //    var path = Path.Combine(basePath, stateFilename);

        //    var state = new State
        //    {
        //        DatabaseFilename = DatabaseFilename
        //    };
        //    using (StreamWriter file = File.CreateText(path))
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        serializer.Serialize(file, state);
        //    }
        //}

        public async Task<string> LoadState()
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
                    await SaveState();
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
                var filenames = Directory.GetFiles(basePath, "*.db3");

                Budgets = filenames.Select(fn => Path.GetFileNameWithoutExtension(fn)).ToList();
            });

            return Budgets;
        }

        //public async Task SaveToFile( )
        //{
        //    await Task.Run(() =>
        //    {
        //        var budgetSave = new BudgetModel
        //        {
        //            //BillData = BillManager.AllTrackers,
        //            BankAccounts = BankAccountManager.AllAccounts

        //        };

        //        using (StreamWriter file = File.CreateText(path))
        //        {
        //            JsonSerializer serializer = new JsonSerializer();
        //            serializer.Serialize(file, budgetSave);
        //        }

        //    });
        //}

        //public async Task LoadFromFile()
        //{
        //    var text = "";
        //    await Task.Run(() => text = File.ReadAllText(path));
        //    var model = JsonConvert.DeserializeObject<BudgetModel>(text);

        //    BillManager.Clear();
        //    foreach(var bd in model.BillData)
        //    {
        //        //BillManager.AddTracker(bd);
        //    }

        //    foreach(var ba in model.BankAccounts)
        //    {
        //        BankAccountManager.AddAccount(ba);
        //    }

        //    Console.WriteLine(model.ToString());
        //}

        #endregion

    }

    [Serializable]
    public class State
    {
        public string DatabaseFilename { get; set; }
    }
}
