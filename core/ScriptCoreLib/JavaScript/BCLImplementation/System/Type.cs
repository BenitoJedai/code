using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using Reflection;
    using ScriptCoreLib.JavaScript.Runtime;
    using ScriptCoreLib.JavaScript.DOM;

    [Script(Implements = typeof(global::System.Type))]
    internal class __Type : __MemberInfo
    {
        [Script]
        internal sealed class __AttributeReflection
        {
            public IFunction Type;
            public object Value;
        }

        [Script]
        internal sealed class __TypeReflection
        {
            public IFunction GetAttributes;
        }

        public __Assembly Assembly
        {
            get
            {
                return new __Assembly
                {

                    __Value = (__AssemblyValue)Expando.InternalGetMember(AsExpando().constructor, "Assembly")
                };
            }
        }

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

        public static Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
        {
            return new __Type { TypeHandle = TypeHandle };
        }

        public static implicit operator Type(__Type e)
        {
            return (Type)(object)e;
        }

        public bool Equals(Type o)
        {
            object a = this.TypeHandle.Value;
            object b = o.TypeHandle.Value;

            return a == b;
        }

        public override string Name
        {
            get
            {
                return (string)Expando.InternalGetMember(AsExpando().constructor, "TypeName");
            }
        }

        
        __TypeReflection Reflection
        {
            get
            {
                return ((__TypeReflection)(object)AsExpando().constructor);
            }
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return GetCustomAttributes(null, false);
        }

        
        public override object[] GetCustomAttributes(Type x, bool inherit)
        {
            if (inherit)
                throw new NotSupportedException();

            if (Reflection.GetAttributes == null)
                return new object[0];

            var i = new List<object>();

            foreach (var v in (__AttributeReflection[])Reflection.GetAttributes.apply(Reflection))
            {
                var c = true;

                if (x != null)
                    if (!object.ReferenceEquals(v.Type.prototype,x.TypeHandle.Value))
                        c = false;

                // todo: rebuild to known type
                if (c)
                    i.Add(v.Value);
            }

            return i.ToArray();
        }
    }
}
