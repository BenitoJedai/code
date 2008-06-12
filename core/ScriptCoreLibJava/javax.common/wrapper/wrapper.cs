using ScriptCoreLib;

using javax.common.runtime;
using java.util;
using java.lang;

namespace csharp
{
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
    [Script(Implements = typeof(global::System.Array))]
    public class ArrayImpl
    {

    }

#if known
    [Script(Implements = typeof(global::System.RuntimeFieldHandle))]
    public class RuntimeFieldHandleImpl
    {

    }

    [Script(Implements = typeof(global::System.Runtime.CompilerServices.RuntimeHelpers))]
    public class RuntimeHelpersImpl
    {
        public static void InitializeArray(global::System.Array array, global::System.RuntimeFieldHandle fldHandle)
        {

        }
    }
#endif






    [Script(Implements = typeof(global::System.Object),
        ImplementationType = typeof(object))]
    public class ObjectImpl
    {
        [Script(ExternalTarget = "toString")]
        public new string ToString()
        {
            return default(string);
        }
    }

    [Script(IsNative = true, Implements = typeof(global::System.Type))]
    public class TypeImplementation
    {

    }

    #region num impl

    [Script(Implements = typeof(global::System.Double),
        ImplementationType = typeof(java.lang.Double))]
    public class DoubleImpl
    {
        [Script(ExternalTarget = "parseDouble")]
        public static double Parse(string e)
        {
            return default(double);
        }
    }

    [Script(Implements = typeof(global::System.Int64),
        ImplementationType = typeof(java.lang.Long))]
    public class Int64Impl
    {
    }

    [Script(Implements = typeof(global::System.Int16),
        ImplementationType = typeof(java.lang.Short))]
    public class Int16Impl
    {
    }

    [Script(Implements = typeof(global::System.Int32)
        // native type cast conflict: ,ExternalTarget="java.lang.Integer"
        , ImplementationType = typeof(java.lang.Integer)
        )]
    public class Int32Impl
    {
        [Script(ExternalTarget = "parseInt")]
        public static int Parse(string e)
        {
            return default(int);
        }
    }

    [Script(Implements = typeof(global::System.SByte)
        , ImplementationType = typeof(java.lang.Byte)
        )]
    public class SByteImpl
    {
        [Script(ExternalTarget = "parseByte")]
        public static sbyte Parse(string e)
        {
            return default(sbyte);
        }
    }
    #endregion

    [Script(Implements = typeof(global::System.Boolean),
        ImplementationType = typeof(java.lang.Boolean))]
    public class BooleanImpl
    {
        [Script(ExternalTarget = "parseBoolean")]
        public static bool Parse(string e)
        {
            return java.lang.Boolean.parseBoolean(e);
        }
    }


    [Script(
       HasNoPrototype = true,
        // ExternalTarget = "java.io.IOException",
       Implements = typeof(global::System.IO.IOException),
       ImplementationType = typeof(java.io.IOException))]
    public class IOExceptionImpl : ExceptionImpl
    {
        public IOExceptionImpl()
        {
        }

        public IOExceptionImpl(string e)
        {

        }


    }



    [Script(
       HasNoPrototype = true,
        // Use 'ImplementationType' instead of 'ExternalTarget'
        // ExternalTarget = "java.lang.RuntimeException",
       Implements = typeof(csharp.RuntimeException),
       ImplementationType = typeof(java.lang.RuntimeException))]
    public class RuntimeExceptionImpl : ExceptionImpl
    {
        public RuntimeExceptionImpl() { }
        public RuntimeExceptionImpl(string e) { }

    }

    [Script(
       HasNoPrototype = true,
        // ExternalTarget = "java.lang.Throwable",
      Implements = typeof(csharp.ThrowableException),
      ImplementationType = typeof(java.lang.Throwable))]
    public class ThrowableExceptionImpl : ExceptionImpl
    {
        public ThrowableExceptionImpl() { }
        public ThrowableExceptionImpl(string e) { }

    }

    [Script(
       HasNoPrototype = true,
        // ExternalTarget = "java.lang.UnsatisfiedLinkError",
       Implements = typeof(csharp.UnsatisfiedLinkError),
       ImplementationType = typeof(java.lang.UnsatisfiedLinkError))]
    public class UnsatisfiedLinkErrorImpl : ExceptionImpl
    {
        public UnsatisfiedLinkErrorImpl() { }
        public UnsatisfiedLinkErrorImpl(string e) { }

    }

    [Script(
       HasNoPrototype = true,
        // ExternalTarget = "java.lang.Exception",
      Implements = typeof(global::System.Exception),
      ImplementationType = typeof(java.lang.Exception))]
    public class ExceptionImpl
    {
        public ExceptionImpl() { }
        public ExceptionImpl(string e) { }
        public string Message
        {
            [Script(ExternalTarget = "getMessage")]
            get { return default(string); }
        }
    }

    [Script(
        HasNoPrototype = true,
        // ExternalTarget = "java.lang.OutOfMemoryError",
        Implements = typeof(global::System.OutOfMemoryException),
       ImplementationType = typeof(java.lang.OutOfMemoryError))]
    public class OutOfMemoryExceptionImpl : ExceptionImpl
    {
        public OutOfMemoryExceptionImpl() { }
        public OutOfMemoryExceptionImpl(string e) { }
    }


    [Script(
        HasNoPrototype = true,
        // ExternalTarget = "java.lang.IndexOutOfBoundsException",
      Implements = typeof(global::System.IndexOutOfRangeException),
      ImplementationType = typeof(java.lang.IndexOutOfBoundsException))]
    public class IndexOutOfRangeExceptionImpl : ExceptionImpl
    {
        public IndexOutOfRangeExceptionImpl() { }
        public IndexOutOfRangeExceptionImpl(string e) { }
    }

    [Script(
    HasNoPrototype = true,
        // ExternalTarget = "java.lang.NullPointerException",
        Implements = typeof(global::System.NullReferenceException),
      ImplementationType = typeof(java.lang.NullPointerException))]
    public class NullReferenceExceptionImpl : ExceptionImpl
    {
        public NullReferenceExceptionImpl() { }
        public NullReferenceExceptionImpl(string e) { }
    }
}



namespace java.lang
{
    using java.io;

    [Script(IsNative = true, ExternalTarget = "System")]
    public static class JavaSystem
    {
        public static PrintStream @out;
        public static PrintStream @err;
        public static InputStream @in;

        public static void loadLibrary(string p)
        {

        }
    }
}
