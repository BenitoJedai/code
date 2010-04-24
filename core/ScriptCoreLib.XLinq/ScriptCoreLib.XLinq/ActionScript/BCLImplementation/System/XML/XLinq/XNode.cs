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
		public void Remove()
		{
			// http://edsyrett.wordpress.com/2008/07/26/xmlremovechild/

			var p = (AS3_XML)this.InternalValue.parent();


			__delete(p.children(), this.InternalValue.childIndex());
		}


		// http://livedocs.adobe.com/flex/2/langref/flash/utils/package.html#describeType()
		[Script(OptimizedCode = "return delete(e[i]);")]
		internal static bool __delete(AS3_XMLList e, int i)
		{
			return default(bool);
		}

	
    }
}
