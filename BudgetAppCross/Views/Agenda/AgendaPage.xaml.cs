using BaseClasses;
using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Binding.Extensions;
using MvvmCross.Forms.Presenters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetAppCross.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true, Title = "Agenda")]
    public partial class AgendaPage
    {
        public AgendaPage()
        {
            InitializeComponent();

            Messenger.Instance.Register<UpdateViewMessage>(this, OnUpdateView);
            
        }

        private void OnUpdateView(UpdateViewMessage obj)
        {
            //DateTime data = DateTime.Today;
            int mostRecent = 0;
            foreach (var item in agendaList.ItemsSource)
            {
                //dynamic mostRecent = data;
                var data = (DateTime)item.GetType().GetProperty("Key").GetValue(item);
                if(data.Date <= DateTime.Today)
                {
                    mostRecent++;
                }
                else
                {
                    dynamic selected = agendaList.ItemsSource.ElementAt(mostRecent);
                    agendaList.ScrollTo(selected[0], ScrollToPosition.Start, true);
                    //agendaList.ScrollTo(mostRecent[0], ScrollToPosition.Center, true);
                    return;
                }


            }

            
            ////var dates = agendaList.ItemsSource.GetEnumerator();
            //var dates = new List<DateTime>();
            //foreach (var item in agendaList.ItemsSource)
            //{
            //    var data = item.GetType().GetProperty("Key").GetValue(item);

            //    dates.Add((DateTime)data);
            //}

            //var pastDates = dates.Where(x => x <= DateTime.Today).OrderByDescending(x => x).ToList();
            //if(pastDates.Count != 0)
            //{
            //    var selectedDate = pastDates.First();
            //    if(selectedDate != null)
            //    {


            //        //dynamic selected = agendaList.ItemsSource[selectedDate]
            //        //agendaList.ScrollTo(selected, ScrollToPosition.Center, true);
            //    }
                
            //    //dynamic test = agendaList.ItemsSource.ElementAt(8);
            //    //agendaList.ScrollTo(test[0], ScrollToPosition.Start, true);
            //}


        }

        private void agendaList_Scrolled(object sender, ScrolledEventArgs e)
        {
            Console.WriteLine("WTF");
        }

        //private void ListView_BindingContextChanged(object sender, EventArgs e)
        //{
        //    var list = (ListView)sender;

        //    //var dates = e.
        //    //var date = ViewModel.GetNavigationDate();

        //    ////list.ItemsSource.
        //    //    list.ScrollTo()
        //}

        //private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        //{
        //    var list = (ListView)sender;

        //    //var dates = e.
        //    //var date = ViewModel.GetNavigationDate();

        //    ////list.ItemsSource.
        //    //    list.ScrollTo()
        //}
    }
}