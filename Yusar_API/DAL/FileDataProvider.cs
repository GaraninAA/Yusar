using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Yusar.Entities;

namespace Yusar_API.DAL
{
    /// <summary>
    /// Простое сохранение в файл со статичными путями, для примера, чтобы не подключать БД
    /// </summary>
    public class FileDataProvider : IDataProvider
    {
        private readonly static string _pathToFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Yosar";
        private readonly static string _path = Path.Combine(_pathToFolder, "Messages.json");

        /// <summary>
        /// Загрузка всех сообщений из файла
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Message> LoadMessages()
        {
            try
            {
                if (!File.Exists(_path)) return new List<Message>();
                using (StreamReader fstream = new StreamReader(_path, true))
                {
                    var data = fstream.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<Message>>(data);
                }
            }
            catch (Exception ex)
            {
                //Обработка ошибок, логирование и т.д.
                return new List<Message>();
            }
        }
        /// <summary>
        /// Сохранение сообщений в файл
        /// </summary>
        /// <param name="messages">Сообщения</param>
        /// <returns>Успешность сохранения</returns>
        public bool SaveMessages(IEnumerable<Message> messages)
        {
            try
            {
                if (!Directory.Exists(_pathToFolder))
                {
                    Directory.CreateDirectory(_pathToFolder);
                }
                if (!File.Exists(_path))
                {
                    var f = File.Create(_path);
                    f.Close();
                }
                using (StreamWriter fstream = new StreamWriter(_path, false))
                {
                    fstream.Write(JsonConvert.SerializeObject(messages));
                }
                return true;
            }
            catch (Exception ex)
            {
                //Обработка ошибок, логирование и т.д.
                return false;
            }
        }
    }
}