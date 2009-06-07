using ScriptCoreLib;

namespace java.io
{
	// http://java.sun.com/javase/6/docs/api/java/io/InputStreamReader.html
	[Script(IsNative = true)]
	public class InputStreamReader : Reader
	{
		/// <summary>
		/// Creates an InputStreamReader that uses the named charset.
		/// </summary>
		/// <param name="in"></param>
		/// <param name="charsetName"></param>
		public InputStreamReader(InputStream @in, string charsetName)
		{
		}


		public InputStreamReader(InputStream e)
		{
		}

		#region methods
		/// <summary>
		/// Close the stream.
		/// </summary>
		public override void close()
		{
		}

		/// <summary>
		/// Return the name of the character encoding being used by this stream.
		/// </summary>
		public string getEncoding()
		{
			return default(string);
		}


		/// <summary>
		/// Read characters into a portion of an array.
		/// </summary>
		public override int read(char[] cbuf, int offset, int length)
		{
			return default(int);
		}



		#endregion

	}
}
