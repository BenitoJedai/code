// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.xml.transform.SourceLocator

using ScriptCoreLib;
using java.lang;

namespace javax.xml.transform
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/xml/transform/SourceLocator.html
	[Script(IsNative = true)]
	public interface SourceLocator
	{
		/// <summary>
		/// Return the character position where the current document event ends.
		/// </summary>
		int getColumnNumber();

		/// <summary>
		/// Return the line number where the current document event ends.
		/// </summary>
		int getLineNumber();

		/// <summary>
		/// Return the public identifier for the current document event.
		/// </summary>
		string getPublicId();

		/// <summary>
		/// Return the system identifier for the current document event.
		/// </summary>
		string getSystemId();

	}
}
