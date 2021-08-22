using BaseViewModels;
using BudgetAppCross.Models;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BudgetAppCross.Core.ViewModels
{
    public class HotelsGroupViewModel : MvxNavigationBaseViewModel
    {
        #region Fields
        private IMvxNavigationService navigationService;
        #endregion

        #region Properties
        private ObservableCollection<Hotel> items;
        public ObservableCollection<Hotel> Items
        {
            get { return items; }
            set
            {
                SetProperty(ref items, value);
            }
        }
        #endregion

        #region Constructors
        public HotelsGroupViewModel(IMvxNavigationService navigation)
        {
            navigationService = navigation;

            var Hotel1rooms = new List<Room>() {
                new Room("Jasmine", 1), new Room("Flower Suite", 2), new Room("narcissus", 1)
            };
            var Hotel2rooms = new List<Room>() {
                new Room("Princess", 1), new Room("Royale", 1), new Room("Queen", 1)
            };
            List<Room> Hotel3rooms = new List<Room>() {
                new Room("Marhaba", 1), new Room("Marhaba Salem", 1), new Room("Salem Royal", 1), new Room("Wedding Roome", 1), new Room("Wedding Suite", 2)
            };
            var hotelList = new List<Hotel>() {
                new Hotel("Yasmine Hammamet", Hotel1rooms), new Hotel("El Mouradi Hammamet", Hotel2rooms), new Hotel("Marhaba Royal Salem", Hotel3rooms)
            };

            Items = new ObservableCollection<Hotel>(hotelList);
        }
        #endregion

        #region Methods

        #endregion


    }
}
