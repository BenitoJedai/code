// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.xml.transform.Templates

using ScriptCoreLib;
using java.util;

namespace javax.xml.transform
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/xml/transform/Templates.html
	[Script(IsNative = true)]
	public interface Templates
	{
		/// <summary>
		/// Get the static properties for xsl:output.
		/// </summary>
		Properties getOutputProperties();

		/// <summary>
		/// Create a new transformation context for this Templates object.
		/// </summary>
		Transformer newTransformer();

	}
}
