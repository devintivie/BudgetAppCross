using BudgetAppCross.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BudgetAppCross.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        #region Fields

        #endregion

        #region Properties
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        #endregion

        #region Constructors
        public ItemsViewModel()
        {
            Title = "Browse";

        }
        #endregion

        #region Methods
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

    }
}
