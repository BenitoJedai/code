// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.xml.transform.ErrorListener

using ScriptCoreLib;

namespace javax.xml.transform
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/xml/transform/ErrorListener.html
	[Script(IsNative = true)]
	public interface ErrorListener
	{
		/// <summary>
		/// Receive notification of a recoverable error.
		/// </summary>
		void error(TransformerException @exception);

		/// <summary>
		/// Receive notification of a non-recoverable error.
		/// </summary>
		void fatalError(TransformerException @exception);

		/// <summary>
		/// Receive notification of a warning.
		/// </summary>
		void warning(TransformerException @exception);

	}
}
