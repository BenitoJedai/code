using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.lang.reflect;
using ScriptCoreLib.Android.BCLImplementation.System.Reflection;
using ScriptCoreLibJava.BCLImplementation.System;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Type))]
    internal class __Type : __MemberInfo
    {
        RuntimeTypeHandle _TypeHandle;

        public static Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
        {
            var e = new __Type
            {
                _TypeHandle = TypeHandle
            };

            return (Type)(object)e;
        }

        public global::java.lang.Class InternalTypeDescription
        {
            get
            {
                var a = this._TypeHandle.Value;
                var b = (object)a;
                var c = (__IntPtr)b;

                return c.ClassToken;
            }
        }

        public static Type GetTypeFromValue(object x)
        {
            return (__Type)global::java.lang.Object.getClass(x); ;
        }

   
    
        public static implicit operator __Type(global::java.lang.Class e)
        {
            if (e == null)
                return null;

            object i = (__RuntimeTypeHandle)e;

            return GetTypeFromHandle((RuntimeTypeHandle)i);
        }

        public static implicit operator __Type(Type e)
        {
            return (__Type)(object)e;
        }

        public static implicit operator Type(__Type e)
        {
            return (Type)(object)e;
        }

    }
}
