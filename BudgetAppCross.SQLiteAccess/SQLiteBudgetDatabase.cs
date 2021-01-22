using BudgetAppCross.DataAccess;
using BudgetAppCross.StateManagers;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.SQLiteAccess
{
    public class SQLiteBudgetDatabase : IBudgetDatabase
    {
        #region Fields

        #endregion

        #region Properties
        public IAccountRepo AccountAccess { get; private set; }
        public IBillRepo BillAccess { get; private set; }
        public IBalanceRepo BalanceAccess { get; private set; }
        public string ConnectionString => StateManager.Instance.DatabasePath;

        #endregion

        #region Constructors

        #endregion

        #region Methods
        public async Task Initialize()
        {
            var accountTableName = "BankAccount";
            var balanceTableName = "Balance";
            var billTableName = "Bill";

            var accountTable = BuildBankAccountTable(accountTableName);
            var balanceTable = BuildBalanceTable(balanceTableName, accountTable);
            var billTable = BuildBillTable(billTableName, accountTable);

            CreateTable(accountTable);
            CreateTable(balanceTable);
            CreateTable(billTable);

            AccountAccess = new SQLiteAccountAccess(accountTableName);
            BalanceAccess = new SQLiteBalanceAccess(balanceTableName);
            BillAccess = new SQLiteBillAccess(billTableName);
        }

        private void CreateTable(SQLiteTable table)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var cmd = new SQLiteCommand(table.BuildTableScript(), connection);
                cmd.ExecuteNonQuery();
            }
        }

        private SQLiteTable BuildBankAccountTable(string tableName)
        {
            //Build BankAccount Table
            var accountTable = new SQLiteTable(tableName);
            accountTable.AddColumn(new SQLiteColumn("AccountId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            accountTable.AddColumn(new SQLiteColumn("NickName").WithDatatype("TEXT").IsUnique());
            accountTable.AddColumn(new SQLiteColumn("AccountNumber").WithDatatype("TEXT").AsNullable(true));
            accountTable.AddColumn(new SQLiteColumn("BankName").WithDatatype("TEXT").AsNullable(true));

            return accountTable;
        }

        private SQLiteTable BuildBalanceTable(string tableName, SQLiteTable accountTable)
        {
            //Build Balance Table
            var balanceTable = new SQLiteTable(tableName);
            balanceTable.AddColumn(new SQLiteColumn("BalanceId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            balanceTable.AddColumn(new SQLiteColumn("Amount").WithDatatype("FLOAT"));
            balanceTable.AddColumn(new SQLiteColumn("Timestamp").WithDatatype("TEXT"));
            balanceTable.AddColumn(new SQLiteColumn("AccountId"));
            balanceTable.AddForeignKey(new ForeignKey("FK_BalanceToBank", "AccountId", accountTable.TableName, "AccountId")
                .HasUpdateAction(ForeignKeyAction.CASCADE)
                .HasDeleteAction(ForeignKeyAction.CASCADE));
            return balanceTable;
        }

        private SQLiteTable BuildBillTable(string tableName, SQLiteTable accountTable)
        {
            //Build Bill Table
            var billTable = new SQLiteTable(tableName);
            billTable.AddColumn(new SQLiteColumn("BillId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            billTable.AddColumn(new SQLiteColumn("Date").WithDatatype("TEXT"));
            billTable.AddColumn(new SQLiteColumn("Amount").WithDatatype("FLOAT"));
            billTable.AddColumn(new SQLiteColumn("Payee").WithDatatype("TEXT"));
            billTable.AddColumn(new SQLiteColumn("IsPaid"));
            billTable.AddColumn(new SQLiteColumn("IsAuto"));
            billTable.AddColumn(new SQLiteColumn("AccountId"));
            billTable.AddForeignKey(new ForeignKey("FK_BillToBank", "AccountId", accountTable.TableName)
                .HasUpdateAction(ForeignKeyAction.CASCADE)
                .HasDeleteAction(ForeignKeyAction.SET_DEFAULT));

            return billTable;
        }


        #endregion
    }
}
