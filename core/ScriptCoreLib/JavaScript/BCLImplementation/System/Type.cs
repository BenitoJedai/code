using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Type))]
    internal class __Type
    {
        private RuntimeTypeHandle _TypeHandle;

        public RuntimeTypeHandle TypeHandle
        {
            get { return _TypeHandle; }
            set { _TypeHandle = value; }
        }


        public static Type GetTypeFromHandle(RuntimeTypeHandle handle)
        {
            __Type t = new __Type();

            t.TypeHandle = handle;

            return t;
        }

        public static implicit operator Type(__Type e)
        {
            return (Type)(object)e;
        }

        //public bool Equals(Type o)
        //{
        //    object a = this.TypeHandle;
        //    object b = o.TypeHandle;

        //    return a == b;
        //}
     }
}
