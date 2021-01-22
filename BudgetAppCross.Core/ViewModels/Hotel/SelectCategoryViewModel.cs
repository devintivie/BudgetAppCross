//using BudgetAppCross.Models;
//using MvvmCross.Navigation;
//using MvvmCross.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Text;

//namespace BudgetAppCross.Core.ViewModels
//{
//    public class SelectCategoryViewModel : MvxViewModel
//    {
//        #region Fields
//        private IMvxNavigationService navigationService;
//        #endregion

//        #region Properties
//        private ObservableCollection<Room> rooms;
//        public ObservableCollection<Room> Rooms
//        {
//            get { return rooms; }
//            set
//            {
//                SetProperty(ref rooms, value);
//            }
//        }

//        private Category category;
//        public Category Category
//        {
//            get { return category; }
//            set
//            {
//                SetProperty(ref category, value);
//            }
//        }

//        private bool isSelected;
//        public bool IsSelected
//        {
//            get { return isSelected; }
//            set
//            {
//                SetProperty(ref isSelected, value);
//            }
//        }



//        #endregion

//        #region Constructors
//        public SelectCategoryViewModel(IMvxNavigationService navigation)
//        {
//            navigationService = navigation;
//            //Title = "Agenda";
//        }
//        #endregion

//        #region Methods

//        #endregion

//    }
//}
