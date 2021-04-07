using System.Collections.Generic;
using Yusar.Entities;

namespace Yusar_API.DAO
{
    public interface IMessagesRepository
    {
        void AddMessage(Message message);
        IEnumerable<Message> GetMessages();
    }
}