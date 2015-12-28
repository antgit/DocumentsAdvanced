using System;
using System.Runtime.Serialization;

namespace BusinessObjects
{
    [Serializable]
    public class ConnectionException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ConnectionException()
        {
        }

        public ConnectionException(string message) : base(message)
        {
        }

        public ConnectionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ConnectionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
