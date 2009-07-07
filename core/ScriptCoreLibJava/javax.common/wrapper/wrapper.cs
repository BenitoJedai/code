using ScriptCoreLib;

using javax.common.runtime;
using java.util;
using java.lang;
using ScriptCoreLibJava.BCLImplementation.System;

namespace csharp
{
	// exceptions really need to be reworked

    #region exceptions from java as c# friendly exceptions
    [global::System.Serializable]
    public class ThrowableException : System.Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ThrowableException() { }
        public ThrowableException(string message) : base(message) { }
        public ThrowableException(string message, System.Exception inner) : base(message, inner) { }
        protected ThrowableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [global::System.Serializable]
    public class RuntimeException : System.Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public RuntimeException() { }
        public RuntimeException(string message) : base(message) { }
        public RuntimeException(string message, System.Exception inner) : base(message, inner) { }
        protected RuntimeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [global::System.Serializable]
    public class UnsatisfiedLinkError : System.Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public UnsatisfiedLinkError() { }
        public UnsatisfiedLinkError(string message) : base(message) { }
        public UnsatisfiedLinkError(string message, System.Exception inner) : base(message, inner) { }
        protected UnsatisfiedLinkError(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
    #endregion
}


namespace javax.common.wrapper
{
    [Script(
       HasNoPrototype = true,
      Implements = typeof(csharp.ThrowableException),
      ImplementationType = typeof(java.lang.Throwable))]
    internal class ThrowableExceptionImpl : __Exception
    {
        public ThrowableExceptionImpl() { }
        public ThrowableExceptionImpl(string e) { }

    }

    [Script(
       HasNoPrototype = true,
       Implements = typeof(csharp.UnsatisfiedLinkError),
       ImplementationType = typeof(java.lang.UnsatisfiedLinkError))]
    internal class UnsatisfiedLinkErrorImpl : __Exception
    {
        public UnsatisfiedLinkErrorImpl() { }
        public UnsatisfiedLinkErrorImpl(string e) { }

    }


    [Script(
        HasNoPrototype = true,
        Implements = typeof(global::System.OutOfMemoryException),
       ImplementationType = typeof(java.lang.OutOfMemoryError))]
    internal class OutOfMemoryExceptionImpl : __Exception
    {
        public OutOfMemoryExceptionImpl() { }
        public OutOfMemoryExceptionImpl(string e) { }
    }


    [Script(
        HasNoPrototype = true,
      Implements = typeof(global::System.IndexOutOfRangeException),
      ImplementationType = typeof(java.lang.IndexOutOfBoundsException))]
    internal class IndexOutOfRangeExceptionImpl : __Exception
    {
        public IndexOutOfRangeExceptionImpl() { }
        public IndexOutOfRangeExceptionImpl(string e) { }
    }

    [Script(
    HasNoPrototype = true,
        // ExternalTarget = "java.lang.NullPointerException",
        Implements = typeof(global::System.NullReferenceException),
      ImplementationType = typeof(java.lang.NullPointerException))]
    internal class NullReferenceExceptionImpl : __Exception
    {
        public NullReferenceExceptionImpl() { }
        public NullReferenceExceptionImpl(string e) { }
    }
}




