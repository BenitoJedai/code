// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.util.Enumeration

using ScriptCoreLib;
using java.lang;

namespace java.util
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/util/Enumeration.html
    // http://developer.android.com/reference/java/util/Enumeration.html
	[Script(IsNative = true)]
	public interface Enumeration
	{
		/// <summary>
		/// Tests if this enumeration contains more elements.
		/// </summary>
		bool hasMoreElements();

		/// <summary>
		/// Returns the next element of this enumeration if this enumeration
		/// object has at least one more element to provide.
		/// </summary>
		object nextElement();

	}
}
