using System;
using System.Collections.Generic;
using Yusar.Entities;
using Yusar_API.DAL;

namespace Yusar_API.DAO
{
    public class MessagesRepository : IMessagesRepository
    {
        private List<Message> _messages;
        private readonly IDataProvider _provider;
        private static object syncRoot = new Object();
        private static MessagesRepository instance;

        private MessagesRepository()
        {
            _provider = new FileDataProvider();
            _messages = new List<Message>();
            LoadMessages();
        }

        public static MessagesRepository GetInstance()
        {
            if (instance == null)
                instance = new MessagesRepository();
            return instance;
        }

        /// <summary>
        /// Добавление пришедшего сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void AddMessage(Message message)
        {
            lock (syncRoot)
            {
                _messages.Add(message);
                SaveMessages();
            }
        }
        /// <summary>
        /// Получение всех сообщений
        /// </summary>
        /// <returns>Коллекция сообщений</returns>
        public IEnumerable<Message> GetMessages()
        {
            lock (syncRoot)
            {
                LoadMessages();
                return _messages;
            }
        }
        /// <summary>
        /// Загрузка сообщений из источника
        /// </summary>
        private void LoadMessages()
        {
            _messages = new List<Message>(_provider.LoadMessages());
        }
        /// <summary>
        /// Сохранение сообщений в источник
        /// </summary>
        private void SaveMessages()
        {
            _provider.SaveMessages(_messages);
        }
    }
}