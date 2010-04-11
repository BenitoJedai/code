using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
	// http://www.w3schools.com/DOM/dom_attribute.asp
	[Script(HasNoPrototype = true)]
	public class IAttr // : INode
	{
		public string name;
		public string value;
	}
}
