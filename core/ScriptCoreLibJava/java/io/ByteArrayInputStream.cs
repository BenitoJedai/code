using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace java.io
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/io/ByteArrayInputStream.html
	[Script(IsNative = true)]
	public class ByteArrayInputStream : InputStream
	{
		/// <summary>
		/// Creates a ByteArrayInputStream  so that it uses buf as its buffer array.
		/// </summary>
		/// <param name="buf"></param>
		public ByteArrayInputStream(sbyte[] buf)
		{

		}

		public override int read()
		{
			return default(int);
		}
	}
}
