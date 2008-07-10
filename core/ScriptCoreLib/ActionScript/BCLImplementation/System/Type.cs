using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.ActionScript.Extensions;
using System.Reflection;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [ScriptImportsType("flash.utils.describeType")]
    [ScriptImportsType("flash.utils.getDefinitionByName")]
    [Script(Implements = typeof(global::System.Type))]
    internal class __Type : __MemberInfo
    {
        
        RuntimeTypeHandle _TypeHandle;

        public static Type GetType(string x)
        {
            var e = new __Type
            {
                _TypeDescription = describeType(getDefinitionByName(x))
            };

            return (Type)(object)e;
        }

        internal static Type GetTypeFromValue(object x)
        {
            var e = new __Type
            {
                _TypeDescription = describeType(x)
            };

            return (Type)(object)e;
        }

        public static Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
        {
            var e = new __Type
            {
                _TypeHandle = TypeHandle
            };

            return (Type)(object)e;
        }

        XML _TypeDescription;

        XML TypeDescription
        {
            get
            {
                if (_TypeDescription == null)
                    _TypeDescription = describeType(this._TypeHandle.Value.ToClassToken());

                return _TypeDescription;
            }
        }

        // http://livedocs.adobe.com/flex/2/langref/flash/utils/package.html#describeType()
        [Script(OptimizedCode = "return flash.utils.describeType(e);")]
        internal static XML describeType(object e)
        {
            return default(XML);
        }

        // http://livedocs.adobe.com/flex/2/langref/flash/utils/package.html#getDefinitionByName()
        [Script(OptimizedCode = "return flash.utils.getDefinitionByName(e);")]
        internal static object getDefinitionByName(string e)
        {
            return default(object);
        }

        public override string ToString()
        {
            return TypeDescription.ToString();
        }

        internal string InternalFullName
        {
            get
            {
                return TypeDescription.attribute("name").ToString();
            }
        }

        public __FieldInfo[] GetFields()
        {
            var f = this.TypeDescription.child("factory")[0].child("variable");
            var r = new __FieldInfo[f.length()];

            for (int i = 0; i < r.Length; i++)
            {
                r[i] = new __FieldInfo { _FieldDescription = f[i] };
            }

            return r;
        }

        public override string Name
        {
            get
            {
                var v = InternalFullName;
                var z = "::";
                var i = v.IndexOf(z);

                if (i < 0)
                    return v; // Top Level Name


                return v.Substring(i + z.Length);
            }
        }

        public string FullName
        {
            get
            {
                // fixme: should return .net styled names
                return InternalFullName;
            }
        }

        public bool Equals(__Type e)
        {
            return this.InternalFullName == e.InternalFullName;

            //return e.TypeDescription == this.TypeDescription;
        }

    }
}
