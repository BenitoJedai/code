using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.XML.XLinq
{

    [Script(Implements = typeof(XObject))]
    internal class __XObject
    {
        internal virtual XElement InternalGetParent()
        {
            throw new NotSupportedException();
        }

        public XElement Parent
        {
            get
            {
                return this.InternalGetParent();
            }
        }
    }
}
