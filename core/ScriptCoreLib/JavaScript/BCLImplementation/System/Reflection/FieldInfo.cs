﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.FieldInfo))]
    internal class __FieldInfo : __MemberInfo
    {
        internal string _Name;

        public override string Name
        {
            get { return _Name; }
        }

        public object GetValue(object obj)
        {
            return Runtime.Expando.InternalGetMember(obj, _Name);
            
        }

        public void SetValue(object obj, object value)
        {
            Runtime.Expando.InternalSetMember(obj, _Name, value);
        }

        public static implicit operator global::System.Reflection.FieldInfo(__FieldInfo e)
        {
            return (global::System.Reflection.FieldInfo)(object)e;
        }


    }
}
