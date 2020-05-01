using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core.Services
{
    public class ContactService : IContactService
    {
        public IEnumerable<Contact> FetchContacts()
        {
            var contacts = new List<Contact>();
            for (int i = 0; i < 10; i++)
            {
                contacts.Add(new Contact
                {
                    Id = i,
                    Name = $"contact{i}"
                });
            }

            return contacts;
        }
    }
}
