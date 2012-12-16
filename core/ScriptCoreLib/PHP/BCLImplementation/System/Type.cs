using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib.PHP.BCLImplementation.System.Reflection;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Type))]
    internal class __Type : __MemberInfo
    {
        public static class API
        {
            #region string gettype ( mixed var )

            /// <summary>  
            /// Returns the type of the PHP variable var.   
            /// </summary>  
            /// <param name="_var">mixed var</param>  
            [Script(IsNative = true)]
            public static string gettype(object _var) { return default(string); }

            #endregion

            [Script(IsNative = true)]
            public static ScriptCoreLib.PHP.Runtime.IArray get_class_vars(string n) { return default(ScriptCoreLib.PHP.Runtime.IArray); }

            [Script(IsNative = true)]
            public static string get_parent_class(object e) { return default(string); }

            [Script(IsNative = true)]
            public static string get_class(object e) { return default(string); }


        }

        public static implicit operator __Type(Type e)
        {
            return (__Type)(object)e;
        }

        RuntimeTypeHandle _TypeHandle;

        public static Type InternalGetTypeFromClassTokenName(string ClassTokenName)
        {
            return GetTypeFromHandle((RuntimeTypeHandle)(object)(__RuntimeTypeHandle)ClassTokenName);
        }


        public static Type InternalGetTypeFromValue(object x)
        {
            var is_string = x is string;

            if (is_string)
                return typeof(string);

            // http://php.net/manual/en/function.is-object.php

            // <b>Warning</b>:  get_class() expects parameter 1 to be object, string given in
            var c = API.get_class(x);

            return InternalGetTypeFromClassTokenName(c);
        }

        public static Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
        {
            var e = new __Type
            {
                _TypeHandle = TypeHandle
            };

            return (Type)(object)e;
        }

        public FieldInfo[] GetFields()
        {
            var a = new List<FieldInfo>();

            var ClassTokenName = ((__IntPtr)(object)(this._TypeHandle.Value)).ClassTokenName;

            var f = (string[])ScriptCoreLib.PHP.Runtime.IArray.API.array_keys(API.get_class_vars(ClassTokenName));

            foreach (var k in f)
            {
                var n = new __FieldInfo
                {
                    InternalDeclaringType = (Type)(object)this,
                    InternalName = k
                };

                a.Add((FieldInfo)(object)n);
            }

            return a.ToArray();
        }

        public override Type DeclaringType
        {
            get { throw new Exception("DeclaringType"); }
        }

        public override string Name
        {
            get
            {
                var ClassTokenName = ((__IntPtr)(object)(this._TypeHandle.Value)).ClassTokenName;

                return ClassTokenName;
            }
        }

        public string Namespace
        {
            get
            {
                // jsc does not yet emit namespace info
                return "<Namespace>";
            }
        }

        public string FullName
        {
            get
            {
                return Namespace + "." + Name;
            }
        }


        public Type BaseType
        {
            get
            {
                var ClassTokenName = ((__IntPtr)(object)(this._TypeHandle.Value)).ClassTokenName;

                return GetTypeFromHandle((RuntimeTypeHandle)(object)(__RuntimeTypeHandle)API.get_parent_class(ClassTokenName));
            }
        }

        public bool Equals(Type o)
        {
            return InternalEquals(this, (__Type)(object)o);
        }

        public static bool operator !=(__Type left, __Type right)
        {
            return !InternalEquals(left, right);
        }

        public static bool operator ==(__Type left, __Type right)
        {
            return InternalEquals(left, right);
        }

        public bool Equals(__Type e)
        {
            return InternalEquals(this, e);
        }

        private static bool InternalEquals(__Type x, __Type e)
        {
            var x_ClassTokenName = ((__IntPtr)(object)(x._TypeHandle.Value)).ClassTokenName;
            var e_ClassTokenName = ((__IntPtr)(object)(e._TypeHandle.Value)).ClassTokenName;

            return x_ClassTokenName == e_ClassTokenName;
        }

        public override string ToString()
        {
            return this.FullName;
        }
    }


}
