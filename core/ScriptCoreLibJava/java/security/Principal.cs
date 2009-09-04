// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.security.Principal

using ScriptCoreLib;
using java.lang;

namespace java.security
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/security/Principal.html
	[Script(IsNative = true)]
	public interface Principal
	{
		/// <summary>
		/// Compares this principal to the specified object.
		/// </summary>
		bool Equals(object @another);

		/// <summary>
		/// Returns the name of this principal.
		/// </summary>
		string getName();

		/// <summary>
		/// Returns a hashcode for this principal.
		/// </summary>
		int GetHashCode();

		/// <summary>
		/// Returns a string representation of this principal.
		/// </summary>
		string ToString();

	}
}
