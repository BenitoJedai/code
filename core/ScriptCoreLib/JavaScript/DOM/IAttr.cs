using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/dom/Attr.idl

	// http://www.w3schools.com/DOM/dom_attribute.asp
    // https://github.com/mono/mono/blob/master/mcs/class/Mono.WebBrowser/Mono.Mozilla/DOM/Attribute.cs

	[Script(HasNoPrototype = true)]
	public class IAttr // : INode
	{
		public string name;
		public string value;
	}
}
