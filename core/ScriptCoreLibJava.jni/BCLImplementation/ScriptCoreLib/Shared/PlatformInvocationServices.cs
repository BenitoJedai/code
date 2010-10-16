using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using jni;
using ScriptCoreLibJava.BCLImplementation.System;

namespace ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared
{
    [Script(Implements = typeof(global::ScriptCoreLib.Shared.PlatformInvocationServices))]
    internal class __PlatformInvocationServices
    {
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
            return ((__IntPtr)(object)ptr).PointerToken;
        }

        [Script]
        public partial class Func
        {
            public readonly string DllName;
            public readonly string EntryPoint;
            public Func(string DllName, string EntryPoint)
            {
                this.DllName = DllName;
                this.EntryPoint = EntryPoint;
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

       

        
    }
}
