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
}
