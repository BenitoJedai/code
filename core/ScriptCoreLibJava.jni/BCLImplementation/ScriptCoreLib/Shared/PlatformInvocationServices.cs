using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using jni;
using ScriptCoreLibJava.BCLImplementation.System;
using System.IO;
using java.security;
using java.net;
using ScriptCoreLibJava.Extensions;

namespace ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared
{
    [Script(Implements = typeof(global::ScriptCoreLib.Shared.PlatformInvocationServices))]
    internal class __PlatformInvocationServices
    {
        public static object StringOrNullCPtr(string e)
        {
            if (e == null)
                return CPtr.NULL;

            return e;
        }

        public static IDisposable CreateCMallocCollector()
        {
            return new CMalloc.Collector();
        }

        public static IntPtr OfInt32(IDisposable c, int value)
        {
            var p = ((CMalloc.Collector)c).OfInt32(value);

            return (IntPtr)p;
        }

        public static object IntPtrToPointerToken(IntPtr ptr)
        {
            return (CPtr)ptr;
        }

        [Script]
        public partial class Func
        {
            public readonly string DllName;
            public readonly string EntryPoint;
            public Func(string DllName, string EntryPoint)
            {
                // java only allows to set the field once!

                if ("dynamic" == DllName)
                    this.DllName = GetCodeSourceLocation();
                else
                    this.DllName = DllName;

                this.EntryPoint = EntryPoint;
            }

            static string f = default(string);

            static string GetCodeSourceLocation()
            {
                if (f == null)
                {
                    try
                    {
                        var cls = typeof(Func).ToClass();

                        ProtectionDomain pDomain = cls.getProtectionDomain();
                        CodeSource cSource = pDomain.getCodeSource();
                        URL loc = cSource.getLocation();


                        var ff = loc.getFile();
                        var prefix = "file:/";

                        if (prefix == ff.Substring(0, prefix.Length))
                            ff = ff.Substring(prefix.Length);

                        f = new FileInfo(ff).FullName;
                    }
                    catch
                    {
                        throw new NotSupportedException();
                    }
                }

                return f;
            }

            CFunc _Method;

            public CFunc Method
            {
                get
                {
                    if (_Method == null)
                        _Method = new CFunc(this.DllName, this.EntryPoint);

                    return _Method;
                }
            }


        }

        #region Int32Func
        [Script]
        public delegate int Int32Func(object[] e);

        partial class Func
        {
            public static implicit operator Int32Func(Func f)
            {
                var Method = f.Method;

                return e =>
                {
                    return Method.callInt(e);
                };
            }
        }

        public static int InvokeInt32(string DllName, string EntryPoint, object[] e)
        {
            Int32Func f = new Func(DllName, EntryPoint);

            return f(e);
        }
        #endregion



        #region StringFunc
        [Script]
        public delegate string StringFunc(object[] e);

        partial class Func
        {
            public static implicit operator StringFunc(Func f)
            {
                var Method = f.Method;

                return e =>
                {
                    return Method.callString(e);
                };
            }
        }

        public static string InvokeString(string DllName, string EntryPoint, object[] e)
        {
            StringFunc f = new Func(DllName, EntryPoint);

            return f(e);
        }
        #endregion

        #region BooleanFunc
        [Script]
        public delegate bool BooleanFunc(object[] e);

        partial class Func
        {
            public static implicit operator BooleanFunc(Func f)
            {
                var Method = f.Method;

                return e =>
                {
                    return Method.callBoolean(e);
                };
            }
        }

        public static bool InvokeBoolean(string DllName, string EntryPoint, object[] e)
        {
            BooleanFunc f = new Func(DllName, EntryPoint);

            return f(e);
        }
        #endregion


        #region IntPtrFunc
        [Script]
        public delegate IntPtr IntPtrFunc(object[] e);

        partial class Func
        {
            public static implicit operator IntPtrFunc(Func f)
            {
                var Method = f.Method;

                return e =>
                {
                    return (IntPtr)Method.callCPtr(e);
                };
            }
        }

        public static IntPtr InvokeIntPtr(string DllName, string EntryPoint, object[] e)
        {
            IntPtrFunc f = new Func(DllName, EntryPoint);

            return f(e);
        }
        #endregion


        #region Action
        [Script]
        public delegate void Action(object[] e);

        partial class Func
        {
            public static implicit operator Action(Func f)
            {
                var Method = f.Method;

                return e =>
                {
                     Method.callVoid(e);
                };
            }
        }

        public static void InvokeVoid(string DllName, string EntryPoint, object[] e)
        {
            Action f = new Func(DllName, EntryPoint);

            f(e);
        }
        #endregion
        
    }
}
