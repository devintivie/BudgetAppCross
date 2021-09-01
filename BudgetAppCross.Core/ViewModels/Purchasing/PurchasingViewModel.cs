//using InAppPurchasing.Core;
//using MvvmCross;
//using MvvmCross.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace BudgetAppCross.Core.ViewModels
//{
//    public class PurchasingViewModel : MvxViewModel
//    {
//        #region Fields
//        private IStoreManager StoreManager;
//        #endregion

//        #region Properties

//        #endregion

//        #region Constructors
//        public PurchasingViewModel()
//        {


//        }
//        #endregion

//        #region Methods

//        public override async Task Initialize()
//        {
//            StoreManager = Mvx.IoCProvider.Resolve<IStoreManager>();
//            Console.WriteLine(StoreManager);
//            await base.Initialize();
//        }
//        private async Task GetProducts()
//        {
//            //StoreManager.GetPrice("test");
//        }
//        #endregion

//    }
//}
