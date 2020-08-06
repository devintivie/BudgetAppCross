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

            Messenger.Instance.Unregister(this);
        }

        //private void agendaList_Scrolled(object sender, ScrolledEventArgs e)
        //{
        //    Console.WriteLine("WTF");
        //}

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