using System;

namespace BusinessObjects
{
    /// <summary>
    /// Ошибка проверки объекта
    /// </summary>
    [Serializable]
    public class ValidateException : Exception
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
        public ValidateException() { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        public ValidateException(string message) : base(message) { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Владелец</param>
        public ValidateException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ValidateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
