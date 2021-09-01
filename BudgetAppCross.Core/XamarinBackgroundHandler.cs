using BaseClasses;
using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppCross.Core
{
    public class XamarinBackgroundHandler : IBackgroundHandler
    {
        #region Fields
        private readonly IMessenger _messenger;
        private readonly IUserDialogs _dialogs;
        private readonly ILogManager _logger;
        //private readonly INotificationHandler _notificationHandler;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public XamarinBackgroundHandler(IMessenger messenger, ILogManager logger, IUserDialogs dialogs)
        {
            _messenger = messenger;
            _dialogs = dialogs;
            _logger = logger;
        }
        #endregion

        #region Logger

        #endregion

        #region Messenger

        #endregion

        #region Notifications

        #endregion



        public string CurrentNotification => throw new NotImplementedException();

        public int NotificationIndex => throw new NotImplementedException();

        public int NotificationCount => throw new NotImplementedException();

        public bool HasNotifications => throw new NotImplementedException();

        public Task ClearAllMessages()
        {
            throw new NotImplementedException();
        }

        public Task DismissCurrentMessage()
        {
            throw new NotImplementedException();
        }

        public Task IgnoreAllMessagesWithCurrentMessage()
        {
            throw new NotImplementedException();
        }

        public void Log(LogMessage log)
        {
            _logger.Add(log);
        }

        public void Notify(string message)
        {
            var config = new AlertConfig().SetMessage("Invalid Budget Name");//.SetOkText(ConfirmConfig.DefaultOkText);
            _dialogs.Alert(config);
            return;
        }

        public void RegisterMessage<T>(object recipient, Action<T> action)
        {
            _messenger.Register(recipient, action);
        }

        public void SendMessage<T>(T message)
        {
            _messenger.Send(message);
        }

        public void SendMessage<T>(T message, object context)
        {
            _messenger.Send(message, context);
        }

        public Task ShowNextMessage()
        {
            throw new NotImplementedException();
        }

        public Task ShowPreviousMessage()
        {
            throw new NotImplementedException();
        }

        public void UnregisterMessages(object recipient)
        {
            _messenger.Unregister(recipient);
        }
    }
}
