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

    [Script]
    internal class __ConvertToInt64 : IConvertToInt64
    {
        public long Pointer;

        public long ToInt64()
        {
            return Pointer;
        }
    }

    [Script(Implements = typeof(global::System.IntPtr))]
    internal class __IntPtr
    {
        public static readonly __IntPtr Zero = InternalGetZero();

        public static explicit operator __IntPtr(long value)
        {
            return new __IntPtr(value);
        }

        public static explicit operator long(__IntPtr value)
        {
            return value.ToInt64();
        }

        public __IntPtr()
        {
            this.PointerToken = new ZeroConvertToInt64();
        }

        public __IntPtr(long Pointer)
        {
            this.PointerToken = new __ConvertToInt64 { Pointer = Pointer };
        }

        public static __IntPtr InternalGetZero()
        {
            return new __IntPtr();
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

        public global::java.lang.Class ClassToken
        {
            get
            {
                return this.PointerToken as global::java.lang.Class;
            }
            set
            {
                this.PointerToken = value;
            }
        }

        public global::java.lang.reflect.Method MethodToken
        {
            get
            {
                return this.PointerToken as global::java.lang.reflect.Method;
            }
            set
            {
                this.PointerToken = value;
            }
        }

        public object PointerToken;


        public static __IntPtr Of(
            global::java.lang.Class Target,
            string MethodName,
            global::java.lang.Class[] Parameters
            )
        {
            var MethodToken = default(Method);

            try
            {
                MethodToken = Target.getDeclaredMethod(MethodName, Parameters);
            }
            catch
            {
                Console.WriteLine("error: " + new { Target, MethodName });

                foreach (var Parameter in Parameters)
                {
                    Console.WriteLine("error: " + new { Parameter });
                }

                foreach (var Method in ScriptCoreLibJava.Extensions.BCLImplementationExtensions.ToType(Target).GetMethods())
                {
                    Console.WriteLine("error: " + new { Method });
                }



                throw;
            }

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

            var __int64 = ToInt64();

            if (format != "x8")
                throw new NotImplementedException("format");

            return __Int32.InternalToString(format, (int)__int64);
        }

        public long ToInt64()
        {
            var value = this.PointerToken as IConvertToInt64;

            if (value == null)
                throw new NotImplementedException("IntPtr could not make use of IConvertToInt64");

            var __int64 = value.ToInt64();

            return __int64;
        }
    }
}
