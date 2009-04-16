using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.Stream))]
	internal abstract class __Stream
	{
		public abstract long Length { get; }

		public abstract void Write(byte[] buffer, int offset, int count);

		public abstract void WriteByte(byte value);
	}
}
