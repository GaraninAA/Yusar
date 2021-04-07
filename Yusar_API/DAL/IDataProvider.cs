using System.Collections.Generic;
using Yusar.Entities;

namespace Yusar_API.DAL
{
    public interface IDataProvider
    {
        IEnumerable<Message> LoadMessages();
        bool SaveMessages(IEnumerable<Message> messages);
    }
}
