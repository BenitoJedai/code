// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.xml.transform.stream.StreamResult

using ScriptCoreLib;
using java.io;
using java.lang;

namespace javax.xml.transform.stream
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/xml/transform/stream/StreamResult.html
	[Script(IsNative = true)]
	public class StreamResult : Result
	{
		/// <summary>
		/// Zero-argument default constructor.
		/// </summary>
		public StreamResult()
		{
		}

		/// <summary>
		/// Construct a StreamResult from a File.
		/// </summary>
		public StreamResult(File @f)
		{
		}

		/// <summary>
		/// Construct a StreamResult from a byte stream.
		/// </summary>
		public StreamResult(OutputStream @outputStream)
		{
		}

		/// <summary>
		/// Construct a StreamResult from a URL.
		/// </summary>
		public StreamResult(string @systemId)
		{
		}

		/// <summary>
		/// Construct a StreamResult from a character stream.
		/// </summary>
		public StreamResult(Writer @writer)
		{
		}

		/// <summary>
		/// Get the byte stream that was set with setOutputStream.
		/// </summary>
		public OutputStream getOutputStream()
		{
			return default(OutputStream);
		}

		/// <summary>
		/// Get the system identifier that was set with setSystemId.
		/// </summary>
		public string getSystemId()
		{
			return default(string);
		}

		/// <summary>
		/// Get the character stream that was set with setWriter.
		/// </summary>
		public Writer getWriter()
		{
			return default(Writer);
		}

		/// <summary>
		/// Set the ByteStream that is to be written to.
		/// </summary>
		public void setOutputStream(OutputStream @outputStream)
		{
		}

		/// <summary>
		/// Set the system ID from a File reference.
		/// </summary>
		public void setSystemId(File @f)
		{
		}

		/// <summary>
		/// Set the systemID that may be used in association
		/// with the byte or character stream, or, if neither is set, use
		/// this value as a writeable URI (probably a file name).
		/// </summary>
		public void setSystemId(string @systemId)
		{
		}

		/// <summary>
		/// Set the writer that is to receive the result.
		/// </summary>
		public void setWriter(Writer @writer)
		{
		}

	}
}
