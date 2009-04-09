using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace org.w3c.dom
{


	// http://java.sun.com/j2se/1.5.0/docs/api/org/w3c/dom/Document.html
	[Script(IsNative = true)]
	public class Document
	{
		

		/// <summary>
		/// Returns a NodeList of all the Elements in document order with a given tag name and are contained in the document.
		/// </summary>
		/// <param name="tagname"></param>
		/// <returns></returns>
		public NodeList getElementsByTagName(string tagname)
		{
			return default(NodeList);
		}
	}
}
