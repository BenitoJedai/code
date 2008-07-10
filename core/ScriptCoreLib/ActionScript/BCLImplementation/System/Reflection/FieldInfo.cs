using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.FieldInfo))]
    internal class __FieldInfo : __MemberInfo
    {
        internal XML _FieldDescription;
        // <variable name="Value" type="int"/>

        public override string Name
        {
            get { return _FieldDescription.attribute("name").ToString(); }
        }

        internal string InternalTypeName
        {
            get { return _FieldDescription.attribute("type").ToString(); }
        }

        public Type FieldType
        {
            get
            {
                return Type.GetType(InternalTypeName);
            }
        }

        public override string ToString()
        {
            return _FieldDescription.toXMLString();
        }
    }
}
