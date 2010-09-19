using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.PHP.DOM;

namespace ScriptCoreLib.PHP.BCLImplementation.System.XML.XLinq
{
    [Script(Implements = typeof(XContainer))]
    internal abstract class __XContainer : __XNode
    {
        public __XName InternalElementName;

        public DOMElement InternalElement
        {
            get
            {
                DOMElement e = (DOMElement)this.InternalValue;
                return e;
            }
        }

        public void Add(params object[] content)
        {
            foreach (var item in content)
            {
                this.Add(item);
            }
        }

        public void Add(object content)
        {
        }

    }
}
