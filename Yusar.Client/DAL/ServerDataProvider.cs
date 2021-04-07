using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Yusar.Entities;

namespace Yusar.Client.DAL
{
    public class ServerDataProvider
    {
        /// <summary>
        /// Создание HTTP клиента
        /// </summary>
        /// <returns></returns>
        private static HttpClient CreateClient()
        {
            var client = new HttpClient();
            return client;
        }
        /// <summary>
        /// Получение всех сообщений с сервера
        /// </summary>
        /// <param name="server">Адрес сервера</param>
        /// <returns></returns>
        public static IEnumerable<Message> GetMessages(string server)
        {
            using (var client = CreateClient())
            {
                try
                {
                    var response = client.GetAsync($"{server}/api/messages").Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        return JsonConvert.DeserializeObject<IEnumerable<Message>>(response.Content.ReadAsStringAsync().Result);
                    else return new List<Message>();
                }
                catch
                {
                    //обработка ошибок получения, логирование и т.д.
                    return new List<Message>();
                }
            }
        }
        /// <summary>
        /// Ассинхронное получение сообщений с сервера
        /// </summary>
        /// <param name="server">Адрес сервера</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Message>> GetMessagesAsync(string server)
        {
            using (var client = CreateClient())
            {
                try
                {
                    var response = await client.GetAsync($"{server}/api/messages");
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        return JsonConvert.DeserializeObject<IEnumerable<Message>>(await response.Content.ReadAsStringAsync());
                    else return new List<Message>();
                }
                catch
                {
                    //обработка ошибок получения, логирование и т.д.
                    return new List<Message>();
                }
            }
        }
        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="server">Адрес сервера</param>
        /// <param name="message">Сообщение</param>
        /// <returns></returns>
        public static async Task<bool> AddValuesAsync(string server, Message message)
        {
            using (var client = CreateClient())
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{server}/api/messages", content);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        return true;
                    else return false;
                }
                catch
                {
                    //обработка ошибок получения, логирование и т.д.
                    return false;
                }
            }
        }
    }
}
