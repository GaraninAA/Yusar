using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Yusar.Client.DAL;
using Yusar.Entities;

namespace Yusar.Client.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            CurrentMessage = string.Empty;
            FilterDateFinish = DateTime.Now;
            FilterDateStart = DateTime.Now;
            Messages = ServerDataProvider.GetMessages(Settings.AdressServer);
        }

        private IEnumerable<Message> _messages;
        /// <summary>
        /// Коллекция отображаемых сообщений (не ObservableCollection, т.к. отдельный сообщения в контексте данной задачи не изменяются)
        /// </summary>
        public IEnumerable<Message> Messages
        {
            get
            {
                return IsFiltered ? _messages.Where(mes => mes.Date.Date >= FilterDateStart.Date && mes.Date.Date <= FilterDateFinish.Date) : _messages;
            }
            set
            {
                SetProperty(ref _messages, value, () => Messages);
            }
        }
        /// <summary>
        /// Дата "от" для фильтра
        /// </summary>
        public DateTime FilterDateStart
        {
            get
            {
                return GetProperty(() => FilterDateStart);
            }
            set
            {
                SetProperty(() => FilterDateStart, value);
                RaisePropertyChanged(() => Messages);
            }
        }
        /// <summary>
        /// Дата "до" для фильтра
        /// </summary>
        public DateTime FilterDateFinish
        {
            get
            {
                return GetProperty(() => FilterDateFinish);
            }
            set
            {
                SetProperty(() => FilterDateFinish, value);
                RaisePropertyChanged(() => Messages);
            }
        }
        /// <summary>
        /// Включение фильтрации
        /// </summary>
        public bool IsFiltered 
        {
            get
            {
                return GetProperty(() => IsFiltered);
            }
            set
            {
                SetProperty(() => IsFiltered, value);
                RaisePropertyChanged(() => Messages);
            }
        }
        /// <summary>
        /// Введённое сообщение
        /// </summary>
        public string CurrentMessage
        {
            get
            {
                return GetProperty(() => CurrentMessage);
            }
            set
            {
                SetProperty(() => CurrentMessage, value);
                RaisePropertyChanged(() => IsActiveSendButton);
            }
        }
        /// <summary>
        /// Возможность отправки
        /// </summary>
        public bool IsActiveSendButton
        {
            get
            {
                return !string.IsNullOrWhiteSpace(CurrentMessage);
            }
        }

        private ICommand _sendMessageCommand;
        /// <summary>
        /// Отправка сообщения
        /// </summary>
        public ICommand SendMessageCommand => _sendMessageCommand ?? (_sendMessageCommand = new AsyncCommand(async () =>
        {
            var result = await ServerDataProvider.AddValuesAsync(Settings.AdressServer,
                new Message() 
                {
                    Content = CurrentMessage, 
                    Date = DateTime.Now, 
                    User = Environment.UserName 
                });
            Messages = await ServerDataProvider.GetMessagesAsync(Settings.AdressServer);
            CurrentMessage = string.Empty;
        }));
    }
}
