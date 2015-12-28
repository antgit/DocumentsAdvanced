using System;

namespace BusinessObjects
{
    [Serializable]
    public class HierarchyContentException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public HierarchyContentException() { }
        public HierarchyContentException(string message) : base(message) { }
        public HierarchyContentException(string message, Exception inner) : base(message, inner) { }
        protected HierarchyContentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
