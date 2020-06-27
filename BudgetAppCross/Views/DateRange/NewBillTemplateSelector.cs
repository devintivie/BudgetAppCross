using BudgetAppCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BudgetAppCross.Views
{
    public class NewBillTemplateSelector : DataTemplateSelector
    {
        //public DataTemplate SingleAddTemplate { get; set; }
        //public DataTemplate MultiAddTemplate { get; set; }

        private readonly DataTemplate singleAddTemplate;
        private readonly DataTemplate multiAddTemplate;

        public NewBillTemplateSelector()
        {
            singleAddTemplate = new DataTemplate(typeof(NewBillView));
            multiAddTemplate = new DataTemplate(typeof(NewMultiBillView));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //throw new NotImplementedException();
            if(item.GetType() == typeof(NewBillViewModel))
            {
                return singleAddTemplate; 
            }
            return multiAddTemplate;
        }
    }
}
