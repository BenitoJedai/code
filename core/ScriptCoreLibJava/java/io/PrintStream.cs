// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.io.PrintStream

using ScriptCoreLib;
using java.lang;

namespace java.io
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/io/PrintStream.html
	[Script(IsNative = true)]
	public class PrintStream : FilterOutputStream
	{
		/// <summary>
		/// Create a new print stream.
		/// </summary>
		public PrintStream(OutputStream @out) : base(@out)
		{
		}

		/// <summary>
		/// Create a new print stream.
		/// </summary>
		public PrintStream(OutputStream @out, bool @autoFlush)
			: base(@out)
		{
		}

		/// <summary>
		/// Create a new print stream.
		/// </summary>
		public PrintStream(OutputStream @out, bool @autoFlush, string @encoding)
			: base(@out)
		{
		}

		/// <summary>
		/// Flush the stream and check its error state.
		/// </summary>
		public bool checkError()
		{
			return default(bool);
		}

		/// <summary>
		/// Close the stream.
		/// </summary>
		public void close()
		{
		}

		/// <summary>
		/// Flush the stream.
		/// </summary>
		public void flush()
		{
		}

		/// <summary>
		/// Print a boolean value.
		/// </summary>
		public void print(bool @b)
		{
		}

		/// <summary>
		/// Print a character.
		/// </summary>
		public void print(char @c)
		{
		}

		/// <summary>
		/// Print an array of characters.
		/// </summary>
		public void print(char[] @s)
		{
		}

		/// <summary>
		/// Print a double-precision floating-point number.
		/// </summary>
		public void print(double @d)
		{
		}

		/// <summary>
		/// Print a floating-point number.
		/// </summary>
		public void print(float @f)
		{
		}

		/// <summary>
		/// Print an integer.
		/// </summary>
		public void print(int @i)
		{
		}

		/// <summary>
		/// Print a long integer.
		/// </summary>
		public void print(long @l)
		{
		}

		/// <summary>
		/// Print an object.
		/// </summary>
		public void print(object @obj)
		{
		}

		/// <summary>
		/// Print a string.
		/// </summary>
		public void print(string @s)
		{
		}

		/// <summary>
		/// Terminate the current line by writing the line separator string.
		/// </summary>
		public void println()
		{
		}

		/// <summary>
		/// Print a boolean and then terminate the line.
		/// </summary>
		public void println(bool @x)
		{
		}

		/// <summary>
		/// Print a character and then terminate the line.
		/// </summary>
		public void println(char @x)
		{
		}

		/// <summary>
		/// Print an array of characters and then terminate the line.
		/// </summary>
		public void println(char[] @x)
		{
		}

		/// <summary>
		/// Print a double and then terminate the line.
		/// </summary>
		public void println(double @x)
		{
		}

		/// <summary>
		/// Print a float and then terminate the line.
		/// </summary>
		public void println(float @x)
		{
		}

		/// <summary>
		/// Print an integer and then terminate the line.
		/// </summary>
		public void println(int @x)
		{
		}

		/// <summary>
		/// Print a long and then terminate the line.
		/// </summary>
		public void println(long @x)
		{
		}

		/// <summary>
		/// Print an Object and then terminate the line.
		/// </summary>
		public void println(object @x)
		{
		}

		/// <summary>
		/// Print a String and then terminate the line.
		/// </summary>
		public void println(string @x)
		{
		}

		/// <summary>
		/// Set the error state of the stream to <code>true</code>.
		/// </summary>
		protected void setError()
		{
		}

		/// <summary>
		/// Write <code>len</code> bytes from the specified byte array starting at
		/// offset <code>off</code> to this stream.
		/// </summary>
		public void write(sbyte[] @buf, int @off, int @len)
		{
		}

		/// <summary>
		/// Write the specified byte to this stream.
		/// </summary>
		public void write(int @b)
		{
		}

	}
}
