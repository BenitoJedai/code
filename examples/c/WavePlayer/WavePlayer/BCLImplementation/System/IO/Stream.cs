using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace WavePlayer.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.Stream))]
	internal abstract class __Stream
	{

		public __Stream()
		{

		}


		public InternalFunc<object, long> __Stream_get_Length;
		public long Length
		{
			get
			{
				if (__Stream_get_Length == null)
				{
					Console.WriteLine("__Stream.Length __Stream_get_Length == null");
					return 0;
				}

				Console.WriteLine("__Stream.Length __Stream_get_Length != null");
				return __Stream_get_Length(this);
			}
		}

		public long Seek(long offset, SeekOrigin origin)
		{
			// no such thing as abstract in c...
			// we need a vTable?
			return 0;
		}


	}
}
