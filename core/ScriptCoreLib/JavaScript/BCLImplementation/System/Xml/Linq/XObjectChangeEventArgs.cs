using ScriptCoreLib.Shared.BCLImplementation.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
    [Script(Implements = typeof(XObjectChangeEventArgs))]
    public class __XObjectChangeEventArgs : __EventArgs
    {
        public __XObjectChangeEventArgs(XObjectChange xObjectChange)
        {
            this.ObjectChange = xObjectChange;
        }

        public XObjectChange ObjectChange { get; set; }
    }
}
