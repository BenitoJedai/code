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
    [Script(Implements = typeof(global::System.Type))]
    internal class __Type : __MemberInfo
    {
        RuntimeTypeHandle _TypeHandle;

        public static Type GetTypeFromValue(object x)
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
        
        [Script(OptimizedCode = "return flash.utils.describeType(e);")]
        internal static XML describeType(object e)
        {
            return default(XML);
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
