using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace javax.xml.parsers
{
	// http://java.sun.com/j2se/1.5.0/docs/api/javax/xml/parsers/DocumentBuilderFactory.html
	[Script(IsNative = true)]
	public abstract class DocumentBuilderFactory
	{
		/// <summary>
		/// Obtain a new instance of a DocumentBuilderFactory.
		/// </summary>
		/// <returns></returns>
		public static DocumentBuilderFactory newInstance()
		{
			return default(DocumentBuilderFactory);
		}

		/// <summary>
		/// Creates a new instance of a DocumentBuilder using the currently configured parameters.
		/// </summary>
		/// <returns></returns>
		public abstract DocumentBuilder newDocumentBuilder();
          
	}
}
