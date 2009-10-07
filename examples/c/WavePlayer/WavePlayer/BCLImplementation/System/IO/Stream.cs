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
		// A native delegate is a static function pointer
		[Script(IsNative = true)]
		public delegate long _virtual_get_Length(__Stream e);

		_virtual_get_Length __Stream_get_Length;

		public __Stream()
		{
			__Stream_get_Length = __Stream_get_Length_Default();

			if (__Stream_get_Length == null)
			{
				Console.WriteLine("__Stream.Length __Stream_get_Length == null");
			}
			else
			Console.WriteLine("__Stream.Length __Stream_get_Length != null");
		}

		static _virtual_get_Length __Stream_get_Length_Default()
		{
			return null;
		}

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
