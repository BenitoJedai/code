using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.FieldInfo))]
    internal class __FieldInfo : __MemberInfo
    {
        internal XML _FieldDescription;
        // <variable name="Value" type="int"/>

        public override string Name
        {
            get { return InternalName; }
        }



        internal string InternalName
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

        public void SetValue(object obj, object value)
        {
            DynamicContainer.SetValue(obj, InternalName, value);
        }

        public object GetValue(object obj)
        {
            return DynamicContainer.GetValue(obj, InternalName);
        }

    }
}
