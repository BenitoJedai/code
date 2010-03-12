using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.XML.XLinq
{

    [Script(Implements = typeof(XNode))]
    internal class __XNode : __XObject
    {
        internal INode InternalValue;

        public override string ToString()
        {
            return InternalValue.ToString();
        }
    }
}
