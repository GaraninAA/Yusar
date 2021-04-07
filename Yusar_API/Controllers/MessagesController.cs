using System.Web.Http;
using Yusar.Entities;
using Yusar_API.DAO;

namespace Yusar_API.Controllers
{
    public class MessagesController : ApiController
    {
        private readonly IMessagesRepository _messagesRepository;

        public MessagesController()
        {
            _messagesRepository = MessagesRepository.GetInstance(); //при наличии DI получение репозитория оттуда
        }

        /// <summary>
        /// Get api/messages получение всех сообщений
        /// </summary>
        /// <returns>Коллекция сообщений</returns>
        public IHttpActionResult Get()
        {
            return Ok(_messagesRepository.GetMessages());
        }

        /// <summary>
        /// Post api/messages отправка сообщения
        /// </summary>
        /// <param name="value">Сообщение</param>
        /// <returns>Успешность выполнения</returns>
        public IHttpActionResult Post([FromBody] Message value)
        {
            _messagesRepository.AddMessage(value);
            return Ok();
        }
    }
}
