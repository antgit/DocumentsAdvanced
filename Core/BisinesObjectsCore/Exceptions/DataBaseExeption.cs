using System;

namespace BusinessObjects
{
    /// <summary>Ошибка на уровне базы данных</summary>
    [Serializable]
    public class DatabaseException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //
        /// <summary>
        /// Конструктор
        /// </summary>
        public DatabaseException() 
        { 
        }
        /// <summary>Конструктор</summary>
        /// <param name="message">Сообщение</param>
        public DatabaseException(string message) : base(message) 
        { 
        }
        /// <summary>Конструктор</summary>
        /// <param name="message">Сообщение</param>
        /// <param name="id">Идентификатор</param>
        public DatabaseException(string message, int id)
            : base(message)
        {
            Id = id;
        }
        /// <summary>Конструктор</summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Владелец</param>
        public DatabaseException(string message, Exception inner) : base(message, inner) { }
        /// <summary>Конструктор</summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DatabaseException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) 
        {
        }

        /// <summary>Идентификатор</summary>
        public int Id { get; set; }
    }
}
