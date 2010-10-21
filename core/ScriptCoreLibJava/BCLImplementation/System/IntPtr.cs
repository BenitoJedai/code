using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.lang.reflect;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script]
    internal interface IConvertToInt64
    {
        // compiler, does this interface exist?
        // what do you suggest?

        long ToInt64();
    }

    [Script]
    class ZeroConvertToInt64 : IConvertToInt64
    {
        public long ToInt64()
        {
            return 0;
        }
    }

    [Script(Implements = typeof(global::System.IntPtr))]
    internal class __IntPtr
    {
        public static readonly __IntPtr Zero = new __IntPtr();

        public static __IntPtr InternalGetZero()
        {
            return new __IntPtr { PointerToken = new ZeroConvertToInt64() };
        }

        public override bool Equals(object obj)
        {
            return InternalEquals(this, obj as __IntPtr);
        }

        public static bool operator !=(__IntPtr value1, __IntPtr value2)
        {
            return !InternalEquals(value1, value2);
        }

        public static bool operator ==(__IntPtr value1, __IntPtr value2)
        {
            return InternalEquals(value1, value2);
        }

        private static bool InternalEquals(__IntPtr value1, __IntPtr value2)
        {
            var a64 = value1.PointerToken as IConvertToInt64;
            var b64 = value1.PointerToken as IConvertToInt64;

            if (a64 != null)
                if (b64 != null)
                    return a64.ToInt64() == b64.ToInt64();

            return false;
        }

        public java.lang.Class ClassToken
        {
            get
            {
                return this.PointerToken as java.lang.Class;
            }
            set
            {
                this.PointerToken = value;
            }
        }

        public java.lang.reflect.Method MethodToken
        {
            get
            {
                return this.PointerToken as java.lang.reflect.Method;
            }
            set
            {
                this.PointerToken = value;
            }
        }

        public object PointerToken;


        public static __IntPtr Of(java.lang.Class Target, string MethodName, java.lang.Class[] Parameters)
        {
            var MethodToken = default(Method);
            var Methods = Target.getDeclaredMethods();

            foreach (var m in Methods)
            {
                if (MethodToken != null)
                    break;

                if (m.getName() == MethodName)
                {

                    var p = m.getParameterTypes();

                    if (p.Length == Parameters.Length)
                    {
                        MethodToken = m;

                        for (int i = 0; i < Parameters.Length; i++)
                        {
                            // name by name comparision... might not be that great!
                            if (Parameters[i].getName() != p[i].getName())
                            {
                                MethodToken = null;
                                break;
                            }
                        }
                    }
                }
            }

            // should we worry about return type overloads too?
            // this wont work in applet viewer!
            // MethodToken.setAccessible(true);

            return new __IntPtr { MethodToken = MethodToken };
        }

        public override string ToString()
        {
            return this.PointerToken.ToString();
        }

        [Script(DefineAsStatic = true)]
        public string ToString(string format)
        {
            // note: x64 support not implemented

            var value = this.PointerToken as IConvertToInt64;

            if (value == null)
                throw new NotImplementedException("IntPtr could not make use of IConvertToInt64");


            if (format != "x8")
                throw new NotImplementedException("format");


            var __int32 = (int)value.ToInt64();

            return __Int32.InternalToString(format, __int32);
        }
    }
}
