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
        public StateManager() { }
        #endregion

        #region Fields
        private const string stateFilename = "budgetState.json";
        private string basePath = "";

        #endregion

        #region Properties
        public string DatabaseFilename { get; set; } = null;
        [JsonIgnore]
        public List<string> Budgets { get; private set; }

        [JsonIgnore]
        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        [JsonIgnore]
        public string DatabasePath
        {
            get
            {
                basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
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
            await Task.Run(() =>
            {
                using (StreamWriter file = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, this);
                }
            });
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
}
