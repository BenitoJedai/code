// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.xml.transform.dom.DOMSource

using ScriptCoreLib;
using java.lang;
using org.w3c.dom;

namespace javax.xml.transform.dom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/xml/transform/dom/DOMSource.html
	[Script(IsNative = true)]
	public class DOMSource : Source
	{
		/// <summary>
		/// Zero-argument default constructor.
		/// </summary>
		public DOMSource()
		{
		}

		/// <summary>
		/// Create a new input source with a DOM node.
		/// </summary>
		public DOMSource(Node @n)
		{
		}

		/// <summary>
		/// Create a new input source with a DOM node, and with the
		/// system ID also passed in as the base URI.
		/// </summary>
		public DOMSource(Node @node, string @systemID)
		{
		}

		/// <summary>
		/// Get the node that represents a Source DOM tree.
		/// </summary>
		public Node getNode()
		{
			return default(Node);
		}

		/// <summary>
		/// Get the base ID (URL or system ID) from where URLs
		/// will be resolved.
		/// </summary>
		public string getSystemId()
		{
			return default(string);
		}

		/// <summary>
		/// Set the node that will represents a Source DOM tree.
		/// </summary>
		public void setNode(Node @node)
		{
		}

		/// <summary>
		/// Set the base ID (URL or system ID) from where URLs
		/// will be resolved.
		/// </summary>
		public void setSystemId(string @baseID)
		{
		}

	}
}
