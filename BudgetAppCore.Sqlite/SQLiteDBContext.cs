//using BudgetAppCross.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using System.Text;

//namespace BudgetAppCore.Sqlite
//{
//    public class SQLiteDBContext : DbContext
//    {

//        #region Fields
//        const string BudgetEFCoreFilename = "BudgetEFCore.db3";
//        #endregion

//        #region Properties
//        public DbSet<BankAccount> BankAccounts { get; set; }
//        public DbSet<Bill> Bills { get; set; }
//        public DbSet<Balance> BalanceHistory { get; set; }
//        #endregion

//        #region Constructors
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            //File.Delete("CustomerDB.db");
//            //optionsBuilder.UseSqlite(@"Data Source=BudgetDB.db;");
//            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
//            var EFCorePath = Path.Combine(basePath, BudgetEFCoreFilename);
//            optionsBuilder.UseSqlite($@"Data Source={EFCorePath}");//, options =>
//            //{
//            //    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
//            //});
//            base.OnConfiguring(optionsBuilder);
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            //BankAccount Table
//            modelBuilder.Entity<BankAccount>().HasKey(t => t.AccountID);
//            modelBuilder.Entity<BankAccount>().Property(t => t.AccountID).ValueGeneratedOnAdd();
//            //modelBuilder.Entity<BankAccount>().ToTable("Accounts");

//            //Balance Table
//            modelBuilder.Entity<Balance>().HasKey(t => t.ID);
//            modelBuilder.Entity<Balance>().Property(t => t.ID).ValueGeneratedOnAdd();
//            modelBuilder.Entity<Balance>().HasOne(b => b.BankAccount).WithMany(b => b.History);
//            //modelBuilder.Entity<Balance>().ToTable("Balances");

<<<<<<< HEAD
//            //Bill Table
//            modelBuilder.Entity<Bill>().HasKey(t => t.ID);
//            modelBuilder.Entity<Bill>().Property(t => t.ID).ValueGeneratedOnAdd();
//            modelBuilder.Entity<Bill>().HasOne(b => b.BankAccount).WithMany(b => b.Bills);
//            //modelBuilder.Entity<Bill>().ToTable("Bills");
=======
            //Bill Table
            modelBuilder.Entity<Bill>().HasKey(t => t.ID);
            modelBuilder.Entity<Bill>().Property(t => t.ID).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Bill>().HasOne(b => b.BankAccount).WithMany(b => b.Bills);
            //modelBuilder.Entity<Bill>().ToTable("Bills");
>>>>>>> feature/ModifySQLite

//            base.OnModelCreating(modelBuilder);
//        }
//        #endregion

//        #region Methods

//        #endregion





//    }
//}
