using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace org.w3c.dom
{
	// http://java.sun.com/j2se/1.5.0/docs/api/org/w3c/dom/Node.html
	[Script(IsNative = true)]
	public class Node
	{
		/// <summary>
		/// A NamedNodeMap containing the attributes of this node (if it is an Element) or null otherwise.
		/// </summary>
		/// <returns></returns>
		public NamedNodeMap getAttributes()
		{
			return default(NamedNodeMap);
		}

		/// <summary>
		/// The value of this node, depending on its type; see the table above.
		/// </summary>
		/// <returns></returns>
		public string getNodeValue()
		{
			return default(string);
		}
	}
}
