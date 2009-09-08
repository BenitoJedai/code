using System;
using System.Collections.Generic;
using System.Text;
using java.io;
using ScriptCoreLib;

namespace java.lang
{


	// http://java.sun.com/j2se/1.5.0/docs/api/java/lang/System.html
	[Script(IsNative = true, ExternalTarget = "System")]
	public static class JavaSystem
	{
		public static PrintStream @out;
		public static PrintStream @err;
		public static InputStream @in;

		/// <summary>
		/// Returns the current time in milliseconds.
		/// </summary>
		public static long currentTimeMillis()
		{
			return default(long);
		}

		public static void loadLibrary(string p)
		{

		}

		/// <summary>
		/// Copies an array from the specified source array, beginning at the specified position, to the specified position of the destination array.
		/// </summary>
		/// <param name="src"></param>
		/// <param name="srcPos"></param>
		/// <param name="dest"></param>
		/// <param name="destPos"></param>
		/// <param name="length"></param>
		public static void arraycopy(object src, int srcPos, object dest, int destPos, int length)
		{
		}


		/// <summary>
		/// Gets the system property indicated by the specified key.
		/// </summary>
		static public string getProperty(string @key)
		{
			return default(string);
		}
	}
}
