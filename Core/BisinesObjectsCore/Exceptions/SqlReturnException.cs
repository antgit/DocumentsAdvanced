using System;

namespace BusinessObjects
{
    /// <summary>
    /// Исключение неизвестного типа возвращаемого результата
    /// </summary>
    [Serializable]
    public class SqlReturnException : Exception
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
        public SqlReturnException() { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        public SqlReturnException(string message) : base(message) { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Владелец</param>
        public SqlReturnException(string message, Exception inner) : base(message, inner) { }
        protected SqlReturnException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
