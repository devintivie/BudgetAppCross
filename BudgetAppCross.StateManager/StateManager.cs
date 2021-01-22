//using BudgetAppCross.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Xamarin.Essentials;

namespace BudgetAppCross.StateManagers
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
        public bool FullVersionPaid { get; private set; } = false;

        //Free Version Limitations
        public const int MAX_PAYEES = int.MaxValue;
        public const int MAX_ACCOUNTS = int.MaxValue;

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

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, state);
            }
        }

        public async Task<string> LoadState()
        {
            var path = Path.Combine(basePath, stateFilename);

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
                await SaveState();
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
