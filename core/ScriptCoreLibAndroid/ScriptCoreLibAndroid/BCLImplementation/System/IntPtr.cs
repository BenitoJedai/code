using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.lang.reflect;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/intptr.cs

    [Script(Implements = typeof(global::System.IntPtr))]
    internal class __IntPtr
    {
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

        public static __IntPtr Of(global::java.lang.Class Target, string MethodName, global::java.lang.Class[] Parameters)
        {
            var MethodToken = default(Method);

            try
            {
                MethodToken = Target.getDeclaredMethod(MethodName, Parameters);
            }
            catch
            {
                //throw new InvalidOperationException();
            }

            return new __IntPtr { MethodToken = MethodToken };
        }
    }
}
