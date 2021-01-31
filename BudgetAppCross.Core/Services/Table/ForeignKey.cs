using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core.Services
{
    public class ForeignKey : ITableAttribute
    {
        #region Properties
        /// <summary>
        /// Name of the foreign key so it can be referenced later, similar to SQL
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// ID name on this table
        /// </summary>
        public string ThisId { get; set; }

        /// <summary>
        /// Name of table to be referenced
        /// </summary>
        public string OtherTable { get; set; }

        /// <summary>
        /// Column in other table that is referenced
        /// </summary>
        public string OtherColumn { get; set; }

        public ForeignKeyAction DeleteAction { get; set; }
        public ForeignKeyAction UpdateAction { get; set; }
        #endregion

        #region Constructors
        public ForeignKey(string name, string id, string otherTable, string otherColumn = null)
        {
            KeyName = name;
            ThisId = id;
            OtherTable = otherTable;
            if (otherColumn == null)
            {
                OtherColumn = ThisId;
            }
            else
            {
                OtherColumn = otherColumn;
            }
        }
        #endregion

        #region Fluent Methods
        public ForeignKey HasDeleteAction(ForeignKeyAction action)
        {
            DeleteAction = action;
            return this;
        }
        public ForeignKey HasUpdateAction(ForeignKeyAction action)
        {
            UpdateAction = action;
            return this;
        }


        #endregion

        #region Methods
        private string UpdateActionString()
        {
            var action = string.Empty;
            switch (UpdateAction)
            {
                case ForeignKeyAction.NO_ACTION:
                    break;
                case ForeignKeyAction.RESTRICT:
                    break;
                case ForeignKeyAction.SET_NULL:
                    break;
                case ForeignKeyAction.SET_DEFAULT:
                    action = " ON UPDATE SET DEFAULT";
                    break;
                case ForeignKeyAction.CASCADE:
                    action = " ON UPDATE CASCADE";
                    break;
                default:
                    break;
            }

            return action;
        }

        private string DeleteActionString()
        {
            var action = string.Empty;
            switch (UpdateAction)
            {
                case ForeignKeyAction.NO_ACTION:
                    break;
                case ForeignKeyAction.RESTRICT:
                    break;
                case ForeignKeyAction.SET_NULL:
                    break;
                case ForeignKeyAction.SET_DEFAULT:
                    action = " ON DELETE SET DEFAULT";
                    break;
                case ForeignKeyAction.CASCADE:
                    action = " ON DELETE CASCADE";
                    break;
                default:
                    break;
            }

            return action;
        }

        public string ToDbString()
        {
            var ua = UpdateActionString();
            var da = DeleteActionString();
            return $"FOREIGN KEY({ThisId}) REFERENCES {OtherTable}({OtherColumn}){ua}{da}";
        }
        #endregion
    }
}
