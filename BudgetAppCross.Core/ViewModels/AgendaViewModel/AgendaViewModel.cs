using BudgetAppCross.Core.Services;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BudgetAppCross.Core.ViewModels
{
    public class AgendaViewModel : BaseViewModel// MvxViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        private ObservableCollection<DateTime> agendaDates;
        public ObservableCollection<DateTime> AgendaDates
        {
            get { return agendaDates; }
            set
            {
                SetProperty(ref agendaDates, value);
            }
        }


        #endregion

        #region Commands
        #endregion

        #region Constructors
        public AgendaViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;
            Title = "Agenda";

            var startDate = DateTime.Today.AddDays(-7);
            var endDate = DateTime.Today.AddMonths(1).AddDays(2);

            var dayCount = (endDate - startDate).TotalDays + 1;

            var tempAgendaDates = new List<DateTime>();
            for (int i = 0; i < dayCount; i++)
            {
                //var entry = new AgendaEntry(startDate.AddDays(i));
                //tempAgendaDates.Add(entry);
                tempAgendaDates.Add(startDate.AddDays(i));
            }

            AgendaDates = new ObservableCollection<DateTime>(tempAgendaDates);
        }


        #endregion

        #region Methods

        public override void ViewAppeared()
        {
            base.ViewAppeared();


        }


        #endregion

    }

}