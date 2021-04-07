using System;

namespace Yusar.Entities
{
    public class Message
    {
        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Клиентское время отправления
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Содержание
        /// </summary>
        public string Content { get; set; }
    }
}
