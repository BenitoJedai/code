using ScriptCoreLib;
using java.lang;

namespace java.lang.reflect
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/lang/reflect/InvocationTargetException.html
	[Script(IsNative = true)]
	public class InvocationTargetException : Exception
	{
		/// <summary>
		/// Returns the the cause of this exception (the thrown target exception, which may be null).
		/// </summary>
		/// <returns></returns>
		public Throwable getCause()
		{
			return default(Throwable);
		}


		/// <summary>
		/// Get the thrown target exception.
		/// </summary>
		/// <returns></returns>
		public Throwable getTargetException()
		{
			return default(Throwable);
		}

	}
}
