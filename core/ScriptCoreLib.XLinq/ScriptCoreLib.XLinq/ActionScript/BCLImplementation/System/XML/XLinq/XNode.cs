using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.XML.XLinq
{
    using AS3_XML = global::ScriptCoreLib.ActionScript.XML;
    using AS3_XMLList = global::ScriptCoreLib.ActionScript.XMLList;

    [Script(Implements = typeof(XNode))]
    internal class __XNode : __XObject
    {
        internal AS3_XML InternalValue;

        public override string ToString()
        {
            return InternalValue.toXMLString();
        }
    }
}
