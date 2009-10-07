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


		public long Length
		{
			get
			{
				return 0;
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
