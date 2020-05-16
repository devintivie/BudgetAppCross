using BudgetAppCross.Models;
using BudgetAppCross.Models.Bills;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BudgetAppCross.Core.Services
{
    public class StateManager
    {
        #region Fields
        private string libFolder = FileSystem.AppDataDirectory;
        private string path;
        #endregion

        #region Properties
        public BillManager BillManager => BillManager.Instance;
        public BankAccountManager BankAccountManager => BankAccountManager.Instance;
        #endregion

        #region Singleton
        private static StateManager instance;
        public static StateManager Instance
        {
            get { return instance ?? (instance = new StateManager()); }
        }

        private StateManager()
        {
            path = $"{libFolder}/test";
        }
        #endregion 

        #region Methods
        public async Task SaveToFile( )
        {
            await Task.Run(() =>
            {
                var budgetSave = new BudgetModel
                {
                    //BillData = BillManager.AllTrackers,
                    BankAccounts = BankAccountManager.AllAccounts
                   
                };

                using (StreamWriter file = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, budgetSave);
                }

            });
        }

        public async Task LoadFromFile()
        {
            var text = "";
            await Task.Run(() => text = File.ReadAllText(path));
            var model = JsonConvert.DeserializeObject<BudgetModel>(text);

            BillManager.Clear();
            foreach(var bd in model.BillData)
            {
                //BillManager.AddTracker(bd);
            }

            foreach(var ba in model.BankAccounts)
            {
                BankAccountManager.AddAccount(ba);
            }

            Console.WriteLine(model.ToString());
        }

        #endregion

    }
}
