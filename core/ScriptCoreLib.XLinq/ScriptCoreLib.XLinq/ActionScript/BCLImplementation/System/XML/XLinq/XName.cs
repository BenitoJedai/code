using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.XML.XLinq
{
    using AS3_QName = global::ScriptCoreLib.ActionScript.QName;
    using AS3_XML = global::ScriptCoreLib.ActionScript.XML;
    using AS3_XMLList = global::ScriptCoreLib.ActionScript.XMLList;

    [Script(Implements = typeof(XName))]
    internal class __XName
    {
		public string InternalValue;

        public string LocalName
        {
            get
            {
				return InternalValue;
            }
	
        }
        public override string ToString()
        {
            return LocalName;
        }

		public static implicit operator __XName(string e)
		{
			return new __XName { InternalValue = e };
		}

		public static XName Get(string localName, string namespaceName)
		{
			return (XName)(object)new __XName { InternalValue = localName };
		}
    }
}
