using BudgetAppCross.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCore.Sqlite
{
    public class SQLiteDBContext : DbContext
    {

        #region Fields

        #endregion

        #region Properties
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Balance> BalanceHistory { get; set; }
        #endregion

        #region Constructors
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=CustomerDB.db;");
        }
        #endregion

        #region Methods

        #endregion





    }
}
