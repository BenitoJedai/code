// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.xml.transform.URIResolver

using ScriptCoreLib;
using java.lang;

namespace javax.xml.transform
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/xml/transform/URIResolver.html
	[Script(IsNative = true)]
	public interface URIResolver
	{
		/// <summary>
		/// Called by the processor when it encounters
		/// an xsl:include, xsl:import, or document() function.
		/// </summary>
		Source resolve(string @href, string @base);

	}
}
