// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;

namespace java.io
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/io/DataOutput.html
	[Script(IsNative = true)]
	public interface DataOutput
	{
		/// <summary>
		/// Writes to the output stream all the bytes in array <code>b</code>.
		/// </summary>
		void write(byte[] @b);

		/// <summary>
		/// Writes <code>len</code> bytes from array
		/// <code>b</code>, in order,  to
		/// the output stream.
		/// </summary>
		void write(byte[] @b, int @off, int @len);

		/// <summary>
		/// Writes to the output stream the eight
		/// low-order bits of the argument <code>b</code>.
		/// </summary>
		void write(int @b);

		/// <summary>
		/// Writes a <code>boolean</code> value to this output stream.
		/// </summary>
		void writeBoolean(bool @v);

		/// <summary>
		/// Writes to the output stream the eight low-
		/// order bits of the argument <code>v</code>.
		/// </summary>
		void writeByte(int @v);

		/// <summary>
		/// Writes a string to the output stream.
		/// </summary>
		void writeBytes(string @s);

		/// <summary>
		/// Writes a <code>char</code> value, wich
		/// is comprised of two bytes, to the
		/// output stream.
		/// </summary>
		void writeChar(int @v);

		/// <summary>
		/// Writes every character in the string <code>s</code>,
		/// to the output stream, in order,
		/// two bytes per character.
		/// </summary>
		void writeChars(string @s);

		/// <summary>
		/// Writes a <code>double</code> value,
		/// which is comprised of eight bytes, to the output stream.
		/// </summary>
		void writeDouble(double @v);

		/// <summary>
		/// Writes a <code>float</code> value,
		/// which is comprised of four bytes, to the output stream.
		/// </summary>
		void writeFloat(float @v);

		/// <summary>
		/// Writes an <code>int</code> value, which is
		/// comprised of four bytes, to the output stream.
		/// </summary>
		void writeInt(int @v);

		/// <summary>
		/// Writes a <code>long</code> value, which is
		/// comprised of eight bytes, to the output stream.
		/// </summary>
		void writeLong(long @v);

		/// <summary>
		/// Writes two bytes to the output
		/// stream to represent the value of the argument.
		/// </summary>
		void writeShort(int @v);

		/// <summary>
		/// Writes two bytes of length information
		/// to the output stream, followed
		/// by the Java modified UTF representation
		/// of  every character in the string <code>s</code>.
		/// </summary>
		void writeUTF(string @str);

	}
}

