using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace org.w3c.dom
{
	// http://java.sun.com/j2se/1.5.0/docs/api/org/w3c/dom/NodeList.html
	[Script(IsNative = true)]
	public class NodeList
	{
		/// <summary>
		/// The number of nodes in the list.
		/// </summary>
		/// <returns></returns>
		public int getLength()
		{
			return default(int);
		}

		/// <summary>
		///  Returns the indexth item in the collection.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Node item(int index)
		{
			return default(Node);
		}
	}
}
