using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
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
				throw new NotImplementedException();
			}
		}

	}
}
