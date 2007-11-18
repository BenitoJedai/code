using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using Reflection;
using ScriptCoreLib.JavaScript.Runtime;

    [Script(Implements = typeof(global::System.Type))]
    internal class __Type : __MemberInfo
    {
        private RuntimeTypeHandle _TypeHandle;

        public RuntimeTypeHandle TypeHandle
        {
            get { return _TypeHandle; }
            set { _TypeHandle = value; }
        }


        public __FieldInfo GetField(string name)
        {
            __FieldInfo r = null;

            foreach (var m in Runtime.Expando.Of(_TypeHandle.Value).GetFields())
            {
                if (m.Name == name)
                {
                    r = new __FieldInfo { _Name = m.Name };

                    break;
                }

            }

            return r;
        }

        Expando AsExpando()
        {
            return Runtime.Expando.Of(_TypeHandle.Value);
        }

        public __FieldInfo[] GetFields()
        {
            var a = new List<__FieldInfo>();

            foreach (var m in AsExpando().GetFields())
            {
                a.Add(new __FieldInfo { _Name = m.Name });

            }
            

            return a.ToArray();
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

        public override string Name
        {
            get 
            {
                return (string)Expando.InternalGetMember(AsExpando().constructor, "TypeName");
            }
        }
    }
}
