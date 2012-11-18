using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.XML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{

    [Script(Implements = typeof(XNode))]
    internal class __XNode : __XObject
    {
        internal INode InternalValue;

        public override string ToString()
        {
            return IXMLDocument.ToXMLString(InternalValue);
        }

        internal override XElement InternalGetParent()
        {
            return (XElement)(object)new __XElement
            {
                InternalValue = this.InternalValue.parentNode
            };
        }

        public void Remove()
        {
            this.InternalValue.parentNode.removeChild(InternalValue);
        }

    }
}
