using BudgetAppCross.Models;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAppCross.Core.ViewModels
{
    public class ContactDetailsViewModel : MvxViewModel<Contact>
    {
        public Contact Contact { get; private set; }
        public override void Prepare(Contact parameter)
        {
            Contact = parameter;
        }
    }
}
