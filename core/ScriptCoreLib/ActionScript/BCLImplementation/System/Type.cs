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
        static XML describeType(object e)
        {
            return default(XML);
        }

        public override string ToString()
        {
            return TypeDescription.ToString();
        }

        public string FullName
        {
            get
            {
                return TypeDescription.attribute("name").ToString();
            }
        }


    }
}
