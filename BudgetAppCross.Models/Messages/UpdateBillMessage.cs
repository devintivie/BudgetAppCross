using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross;
using MvvmCross.Plugin.Messenger;

namespace BudgetAppCross.Models
{
    public class UpdateBillMessage
    {
        public int AccountId { get; private set; }
        public UpdateBillMessage(int id)
        {
            AccountId = id;
        }
    }

    public class ChangeBillMessage
    {
        public int AccountId { get; private set; }
        public ChangeBillMessage(int id)
        {
            AccountId = id;
        }
        public ChangeBillMessage()
        {

        }
    }

    public class ChangeBalanceMessage
    {
        public int AccountId { get; private set; }
        public ChangeBalanceMessage(int id)
        {
            AccountId = id;
        }
        public ChangeBalanceMessage()
        {

        }
    }

    public class ChangeBudgetsMessage
    {

    }

    public class UpdateMenuMessage
    {

    }

    public class UpdateViewMessage
    {

    }
}

