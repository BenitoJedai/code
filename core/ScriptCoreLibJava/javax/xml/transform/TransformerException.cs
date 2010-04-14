// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.xml.transform.TransformerException

using ScriptCoreLib;
using java.io;
using java.lang;

namespace javax.xml.transform
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/xml/transform/TransformerException.html
	[Script(IsNative = true)]
	public class TransformerException : Exception
	{
		/// <summary>
		/// Create a new TransformerException.
		/// </summary>
		public TransformerException(string @message)
		{
		}

		/// <summary>
		/// Create a new TransformerException from a message and a Locator.
		/// </summary>
		public TransformerException(string @message, SourceLocator @locator)
		{
		}

		/// <summary>
		/// Wrap an existing exception in a TransformerException.
		/// </summary>
		public TransformerException(string @message, SourceLocator @locator, Throwable @e)
		{
		}

		/// <summary>
		/// Wrap an existing exception in a TransformerException.
		/// </summary>
		public TransformerException(string @message, Throwable @e)
		{
		}

		/// <summary>
		/// Create a new TransformerException wrapping an existing exception.
		/// </summary>
		public TransformerException(Throwable @e)
		{
		}

		/// <summary>
		/// Returns the cause of this throwable or <code>null</code> if the
		/// cause is nonexistent or unknown.
		/// </summary>
		public Throwable getCause()
		{
			return default(Throwable);
		}

		/// <summary>
		/// This method retrieves an exception that this exception wraps.
		/// </summary>
		public Throwable getException()
		{
			return default(Throwable);
		}

		/// <summary>
		/// Get the location information as a string.
		/// </summary>
		public string getLocationAsString()
		{
			return default(string);
		}

		/// <summary>
		/// Method getLocator retrieves an instance of a SourceLocator
		/// object that specifies where an error occured.
		/// </summary>
		public SourceLocator getLocator()
		{
			return default(SourceLocator);
		}

		/// <summary>
		/// Get the error message with location information
		/// appended.
		/// </summary>
		public string getMessageAndLocation()
		{
			return default(string);
		}

		/// <summary>
		/// Initializes the <i>cause</i> of this throwable to the specified value.
		/// </summary>
		public Throwable initCause(Throwable @cause)
		{
			return default(Throwable);
		}

		/// <summary>
		/// Print the the trace of methods from where the error
		/// originated.
		/// </summary>
		public void printStackTrace()
		{
		}

		/// <summary>
		/// Print the the trace of methods from where the error
		/// originated.
		/// </summary>
		public void printStackTrace(PrintStream @s)
		{
		}

		/// <summary>
		/// Print the the trace of methods from where the error
		/// originated.
		/// </summary>
		public void printStackTrace(PrintWriter @s)
		{
		}

		/// <summary>
		/// Method setLocator sets an instance of a SourceLocator
		/// object that specifies where an error occured.
		/// </summary>
		public void setLocator(SourceLocator @location)
		{
		}

	}
}
