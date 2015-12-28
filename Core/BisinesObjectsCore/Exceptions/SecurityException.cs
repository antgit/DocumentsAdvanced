using System;
using System.Runtime.Serialization;

namespace BusinessObjects
{
    [Serializable]
    public class SecurityException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public SecurityException()
        {
        }

        public SecurityException(string message) : base(message)
        {
        }

        public SecurityException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SecurityException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
