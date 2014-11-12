using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/System/Xml/Linq/XText.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System.Xml.Linq/System.Xml.Linq/XText.cs

    [Script(Implements = typeof(XText))]
    internal class __XText : __XNode
    {
        public string Value
        {
            get
            {
                return ((ITextNode)this.InternalValue).text;
            }
            set
            {
                var o = this.InternalValue;
                var n = this.InternalValue.ownerDocument.createTextNode(value);

                var parentNode = this.InternalValue.parentNode;

                parentNode.replaceChild(
                    n,
                    o
                );
            }
        }

    }
}
