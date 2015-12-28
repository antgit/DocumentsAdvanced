using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace StarterWpf
{
    /// <summary>Файл или модуль программы ранее установленный</summary>
    [Serializable]
    public class AplicationFile
    {
        /// <summary>Папка, относительно корня</summary>
        public string Folder { get; set; }
        /// <summary>Имя файла</summary>
        public string FileName { get; set; }
        /// <summary>Дата последнего изменения</summary>
        public DateTime Date { get; set; }
        /// <summary>Загрузка данных из файла</summary>
        /// <param name="file">Полный путь к файлу</param>
        /// <returns></returns>
        public static List<AplicationFile> Load(string file)
        {
            List<AplicationFile> coll = new List<AplicationFile>();
            XmlSerializer xs = new XmlSerializer(typeof(List<AplicationFile>));
            using (StreamReader rd = new StreamReader(file))
            {
                coll = xs.Deserialize(rd) as List<AplicationFile>;
            }
            return coll;
        }
        /// <summary>Сохранить в файл</summary>
        /// <param name="coll">Коллекция</param>
        /// <param name="file">Полное имя файла</param>
        public static void Save(List<AplicationFile> coll, string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<AplicationFile>));
            using (StreamWriter wr = new StreamWriter(file))
            {
                xs.Serialize(wr, coll);
            }
        }
    }
}