using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core.Services
{
    public interface IContactService
    {
        IEnumerable<Contact> FetchContacts();
    }
}
