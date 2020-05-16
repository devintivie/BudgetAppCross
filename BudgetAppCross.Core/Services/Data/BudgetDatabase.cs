using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BudgetAppCross.Models;
using SQLite;

namespace BudgetAppCross.Core.Services
{
    public class BudgetDatabase
    {

        static readonly Lazy<BudgetDatabase> instance = new Lazy<BudgetDatabase>();
        public static BudgetDatabase Instance => instance.Value;

        static readonly Lazy<SQLiteAsyncConnection> database = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => database.Value;
        static bool initialized = false;

        public BudgetDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            //File.Delete(Constants.DatabasePath);
            //await Database.DeleteAsync  (Database.TableMappings.First());
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Bill).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Bill)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }
        public async Task<List<Bill>> GetBillsAsync()
        {
            var list = await Database.Table<Bill>().ToListAsync();
            return list;
        }

        //public Task<List<Bill>> GetItemsNotDoneAsync()
        //{
        //    // SQL queries are also possible
        //    return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}

        public async Task<Bill> GetBillAsync(int id)
        {
            return await Database.Table<Bill>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveBillAsync(Bill bill)
        {
            if (bill.ID != 0)
            {
                return Database.UpdateAsync(bill);
            }
            else
            {
                return Database.InsertAsync(bill);
            }
        }

        public Task<int> DeleteBillAsync(Bill bill)
        {
            return Database.DeleteAsync(bill);
        }
    }
}
