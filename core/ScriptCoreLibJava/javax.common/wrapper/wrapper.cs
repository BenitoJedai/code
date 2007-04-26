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

    [Script(Implements = typeof(global::System.IDisposable))]
    public interface IDisposableImplementation
    {
        void Dispose();
    }

    [Script(Implements = typeof(global::System.Object),
        ImplementationType=typeof(object))]
    public class ObjectImpl
    {
        [Script(ExternalTarget="toString")]
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
        , ImplementationType=typeof(java.lang.Integer)
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
        ImplementationType=typeof(java.lang.Boolean))]
    public class BooleanImpl
    {
        [Script(ExternalTarget = "parseBoolean")]
        public static bool Parse(string e)
        {
            return java.lang.Boolean.parseBoolean(e);
        }
    }

    [Script(Implements = typeof(global::System.String),
        ImplementationType = typeof(java.lang.String))]
    public class StringImplementation
    {
        [Script(ExternalTarget = "charAt")]
        public string get_Chars(int i)
        {
            return default(string);
        }

        [Script(ExternalTarget = "trim")]
        public string Trim()
        {
            return default(string);
        }

        [Script(DefineAsStatic = true)]
        public string PadLeft(int totalWidth, char paddingChar)
        {
            string u = (string)(object)this;
            string p =  Convert.ToString(paddingChar);

            while (u.Length < totalWidth)
                u = p + u; 

            return u;
        }


        [Script(ExternalTarget = "substring")]
        public string Substring(int start)
        {
            return default(string);
        }

        [Script(DefineAsStatic = true)]
        public string Substring(int start, int len)
        {
            java.lang.String s = (java.lang.String)(object)this;

            

            return s.substring(start, start + len);
        }

#if JAVA5

        [Script(ExternalTarget = "replace")]
#else
        [Script(DefineAsStatic=true)]
#endif
        public string Replace(string a, string b)
        {
            return Convert.ReplaceString((string)(object)this, a, b);
        }

        [Script(ExternalTarget = "replace")]
        public string Replace(char a, char b)
        {
            return default(string);
        }

        [Script(DefineAsStatic=true)]
        public bool Contains(string a)
        {
            return IndexOf(a) > -1;
        }


        [Script(ExternalTarget = "indexOf")]
        public int IndexOf(string str)
        {
            return default(int);
        }

        
        [Script(ExternalTarget = "indexOf")]
        public int IndexOf(string str, int pos)
        {
            return default(int);
        }

        [Script(ExternalTarget = "indexOf")]
        public int IndexOf(char str)
        {
            return default(int);
        }

        [Script(ExternalTarget = "indexOf")]
        public int IndexOf(char str, int pos)
        {
            return default(int);
        }

        [Script(ExternalTarget = "lastIndexOf")]
        public int LastIndexOf(string str)
        {
            return default(int);
        }

        [Script(ExternalTarget = "lastIndexOf")]
        public int LastIndexOf(char str)
        {
            return default(int);
        }

        [Script(DefineAsStatic = true)]
        public string[] Split(char[] e)
        {
            object a = (object)this;
            return Convert.SplitStringByChar((string)a, e[0]);
        }

        [Script(ExternalTarget = "startsWith")]
        public bool StartsWith(string prefix)
        {
            return default(bool);
        }

        [Script(ExternalTarget = "endsWith")]
        public bool EndsWith(string suffix)
        {
            return default(bool);
        }

        [Script(ExternalTarget = "equals")]
        public bool Equals(string e)
        {
            return default(bool);
        }

        [Script(ExternalTarget = "compareTo")]
        public int CompareTo(string e)
        {
            return default(int);
        }

        [Script(ExternalTarget = "toUpperCase")]
        public string ToUpper()
        {
            return default(string);
        }

        [Script(ExternalTarget = "toLowerCase")]
        public string ToLower()
        {
            return default(string);
        }

        [Script(ExternalTarget = "equals", DefineAsInstance=true)]
        public static bool operator ==(StringImplementation a, StringImplementation b)
        {
            return default(bool);
        }

  
        public static bool operator !=(StringImplementation a, StringImplementation b)
        {
            return !(a == b);
        }

        public int Length
        {
            [Script(ExternalTarget="length")]
            get
            {
                return default(int);
            }
        }


        public static string Concat(params object[] e)
        {
            java.lang.StringBuffer b = new java.lang.StringBuffer();

            foreach (object v in e)
            {
                b.append(v);
            }

            return b.ToString();
        }

        //[Script(DefineAsStatic = true)]
        public static string Concat(object a)
        {
            if (a == null)
            {

                    return null;
            }




            return a.ToString();
        }

        [Script(
            StringConcatOperator = "+")]
        public static string Concat(object a, object b)
        {
            if (a == null)
            {
                if (b == null)
                    return null;

                return b.ToString();
            }
            else
            {
                if (b == null)
                    return a.ToString();
            }



            return a.ToString() + b.ToString();
        }

        [Script(
            StringConcatOperator="+"
            )]
        public static string Concat(string a, string b)
        {
            return default(string);
        }

        [Script(
            StringConcatOperator="+"
            )]
        public static string Concat(string a, string b, string c)
        {
            return default(string);
        }

        [Script(
            StringConcatOperator = "+")]
        public static string Concat(string a, string b, string c, string d)
        {
            return default(string);
        }

        [Script(
            StringConcatOperator = "+")]
        public static string Concat(object a, object b, object c)
        {
            return default(string);
        }
    }

    [Script(
       HasNoPrototype = true,
       ExternalTarget = "java.io.IOException",
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
       ExternalTarget = "java.lang.RuntimeException",
       Implements = typeof(csharp.RuntimeException),
       ImplementationType=typeof(object))]
    public class RuntimeExceptionImpl : ExceptionImpl
    {
        public RuntimeExceptionImpl() { }
        public RuntimeExceptionImpl(string e) { }

    }

    [Script(
       HasNoPrototype = true,
       ExternalTarget = "java.lang.Throwable",
      Implements = typeof(csharp.ThrowableException),
      ImplementationType = typeof(java.lang.Throwable))]
    public class ThrowableExceptionImpl : ExceptionImpl
    {
        public ThrowableExceptionImpl() { }
        public ThrowableExceptionImpl(string e) { }

    }

    [Script(
       HasNoPrototype = true,
       ExternalTarget = "java.lang.UnsatisfiedLinkError",
       Implements = typeof(csharp.UnsatisfiedLinkError),
       ImplementationType=typeof(object))]
    public class UnsatisfiedLinkErrorImpl : ExceptionImpl
    {
        public UnsatisfiedLinkErrorImpl() { }
        public UnsatisfiedLinkErrorImpl(string e) { }

    }

    [Script(
       HasNoPrototype = true,
       ExternalTarget = "java.lang.Exception",
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
        ExternalTarget = "java.lang.OutOfMemoryError",
        Implements = typeof(global::System.OutOfMemoryException),
       ImplementationType=typeof(object))]
    public class OutOfMemoryExceptionImpl
    {
        public OutOfMemoryExceptionImpl() { }
        public OutOfMemoryExceptionImpl(string e) { }
        public string Message
        {
            [Script(ExternalTarget = "getMessage")]
            get { return default(string); }
        }
    }


    [Script(
        HasNoPrototype = true,
       ExternalTarget = "java.lang.IndexOutOfBoundsException",
      Implements = typeof(global::System.IndexOutOfRangeException),
      ImplementationType = typeof(object))]
    public class IndexOutOfRangeExceptionImpl
    {
        public IndexOutOfRangeExceptionImpl() { }
        public IndexOutOfRangeExceptionImpl(string e) { }
        public string Message
        {
            [Script(ExternalTarget = "getMessage")]
            get { return default(string); }
        }
    }

    [Script(
    HasNoPrototype = true,
   ExternalTarget = "java.lang.NullPointerException",
  Implements = typeof(global::System.NullReferenceException),
      ImplementationType = typeof(object))]
    public class NullReferenceExceptionImpl
    {
        public NullReferenceExceptionImpl() { }
        public NullReferenceExceptionImpl(string e) { }
        public string Message
        {
            [Script(ExternalTarget = "getMessage")]
            get { return default(string); }
        }
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
