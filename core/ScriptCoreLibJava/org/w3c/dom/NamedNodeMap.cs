using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace org.w3c.dom
{
	// http://java.sun.com/j2se/1.5.0/docs/api/org/w3c/dom/NamedNodeMap.html
	[Script(IsNative = true)]
	public class NamedNodeMap
	{
		/// <summary>
		/// Retrieves a node specified by name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Node getNamedItem(string name)
		{
			return default(Node);
		}

	}
}
