using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.File))]
	internal class __File
	{
		public static void WriteAllBytes(string path, byte[] value)
		{

			try
			{
				var stream = new java.io.RandomAccessFile(path, "rw");

				stream.setLength(0);
				stream.write(InternalByteArrayToSByteArray(value));

				stream.close();
			}
			catch
			{
				throw new csharp.RuntimeException();
			}
		}

		public static byte[] ReadAllBytes(string path)
		{
			var x = getBytesFromFile(new java.io.File(path));

			return InternalSByteArrayToByteArray(x);
		}

		[Script(OptimizedCode = @"return e;")]
		internal static byte[] InternalSByteArrayToByteArray(sbyte[] e)
		{
			return default(byte[]);
		}

		[Script(OptimizedCode = @"return e;")]
		internal static sbyte[] InternalByteArrayToSByteArray(byte[] e)
		{
			return default(sbyte[]);
		}

		// http://www.java-tips.org/java-se-tips/java.io/reading-a-file-into-a-byte-array.html
		static sbyte[] getBytesFromFile(global::java.io.File file)
		{
			try
			{

				var istream = new global::java.io.FileInputStream(file);

				// Get the size of the file
				long length = file.length();



				// Create the byte array to hold the data
				var bytes = new sbyte[(int)length];

				// Read in the bytes
				int offset = 0;
				int numRead = istream.read(bytes, offset, bytes.Length - offset);

				if (numRead >= 0)
					while (offset < bytes.Length)
					{
						offset += numRead;
						numRead = istream.read(bytes, offset, bytes.Length - offset);

						if (numRead < 0)
							break;
					}



				// Close the input stream and return bytes
				istream.close();
				return bytes;
			}
			catch
			{
				// exception mapping must be refactored
				throw new csharp.RuntimeException();
			}
		}
	}
}
