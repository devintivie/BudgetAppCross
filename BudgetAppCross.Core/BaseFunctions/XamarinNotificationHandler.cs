//using Acr.UserDialogs;
//using BaseClasses;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace BudgetAppCross.Core
//{
//    public class XamarinNotificationHandler : INotificationHandler
//    {
//        #region Fields

//        #endregion

//        #region Properties
//        public List<string> Notifications { get; private set; } = new List<string>();

//        #endregion

//        #region Constructors
//        public XamarinNotificationHandler(IUserDialogs dialogs)
//        {

//        }
//        #endregion

//        #region Methods

//        #endregion

//        public void AddNotification(string message)
//        {
//            throw new NotImplementedException();
//        }

//        public void ClearAllMessages()
//        {
//            throw new NotImplementedException();
//        }

//        public void DismissNotification(int index)
//        {
//            throw new NotImplementedException();
//        }

//        public void IgnoreMessage(string message)
//        {
//            throw new NotImplementedException();
//        }

//        public int IgnoreMessage(int currentIndex, string message)
//        {
//            throw new NotImplementedException();
//        }

//        public string ShowNotification(int index)
//        {
//            var config = new AlertConfig().SetMessage("Invalid Account Name");//.SetOkText(ConfirmConfig.DefaultOkText);
//            Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(config);
//            return;
//        }
//    }
//}
