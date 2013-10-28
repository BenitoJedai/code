using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Xml.Linq
{
    [Script(Implements = typeof(global::System.Xml.Linq.XText))]
    internal class __XText : __XNode
	{
        public string Value
        {
            get
            {
                var x = (org.w3c.dom.Text)this.InternalValue;

                return x.getData();
            }
            set
            {
                var x = (org.w3c.dom.Text)this.InternalValue;

                x.setData(value);
            }
        }
	}
}
