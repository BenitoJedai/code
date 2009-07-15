using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.BinaryReader))]
	internal class __BinaryReader : __IDisposable
	{
		internal Stream InternalStream;

		public __BinaryReader(Stream input)
		{
			this.InternalStream = input;
		}

		#region __IDisposable Members

		public void Dispose()
		{

		}

		#endregion

		public virtual byte ReadByte()
		{
			var i = this.InternalStream.ReadByte();

			// yay, we are throwing the wrong exception
			if (i < 0)
				throw new InvalidOperationException();

			return (byte)i;
		}
	}
}
