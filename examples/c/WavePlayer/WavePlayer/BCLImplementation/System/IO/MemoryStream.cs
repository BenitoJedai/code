using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace WavePlayer.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.MemoryStream))]
	internal class __MemoryStream : __Stream
	{
		public __MemoryStream() : this(null)
		{

		}

		public __MemoryStream(Stream s)
		{
			// we are doing manual virtual calls.. feel the pain yet?
			this.__Stream_get_Length = k => ((__MemoryStream)k).__Length;
		}

		private long __Length
		{
			get
			{
				Console.WriteLine("__MemoryStream.get_Length");
				return 0;
			}
		}

	

		public byte[] ToArray()
		{
			return new byte[0];
		}
	}
}
