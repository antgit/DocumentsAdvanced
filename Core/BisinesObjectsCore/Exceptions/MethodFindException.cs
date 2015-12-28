using System;

namespace BusinessObjects
{
    /// <summary>
    /// Исключение при ошибках поиска метода
    /// </summary>
    [Serializable]
    public class MethodFindException : Exception
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
        public MethodFindException() { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        public MethodFindException(string message) : base(message) { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Владелец</param>
        public MethodFindException(string message, Exception inner) : base(message, inner) { }
        protected MethodFindException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
