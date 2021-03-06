﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using Foundation;
using InAppPurchasing.Core;
using StoreKit;
using UIKit;

namespace InAppPurchasing.iOS
{
    public class StoreManager : IStoreManager
    {
        #region Singleton
        private static readonly Lazy<StoreManager> instance = new Lazy<StoreManager>();
        public static StoreManager Instance => instance.Value;
        #endregion

        #region Fields
        CustomPaymentObserver theObserver;
        InAppPurchaseManager iap;
        #endregion

        #region Properties
        public List<string> Products { get; set; } = new List<string>();
        #endregion

        #region Constructors
        public StoreManager()
        {
            iap = new InAppPurchaseManager();
            theObserver = new CustomPaymentObserver(iap);

            //Call this once upon startup of in-app - purchase activities
            // This also kicks off the TransactionObserver which handles the various communications
            SKPaymentQueue.DefaultQueue.AddTransactionObserver(theObserver);
            Messenger.Instance.Register<PurchaseMessage>(this, OnPurchaseMessage);
            Messenger.Instance.Register<GetPriceMessage>(this, OnGetPriceMessage);
        }
        public void Start() { }
        #endregion

        #region Methods
        private void OnGetPriceMessage(GetPriceMessage obj)
        {
            //throw new NotImplementedException();
        }
        #endregion

        #region Methods
        private void OnPurchaseMessage(PurchaseMessage obj)
        {
            //throw new NotImplementedException();
        }

        public decimal GetPrice(string product)
        {
            throw new NotImplementedException();
        }

        public bool PurchaseProduct(string product)
        {
            throw new NotImplementedException();
        }
        #endregion


    }


}