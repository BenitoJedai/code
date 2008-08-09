﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.BinaryWriter))]
	internal class __BinaryWriter : __IDisposable
	{
		protected Stream OutStream;
		private byte[] _buffer;
 

 


		public __BinaryWriter(Stream output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			this.OutStream = output;
			this._buffer = new byte[0x10];

		}

		#region __IDisposable Members

		public void Dispose()
		{
		}

		#endregion

		public virtual void Write(short value)
		{
			OutStream.ToByteArray().writeShort(value);
		}

		public virtual void Write(byte value)
		{
			this.OutStream.WriteByte(value);
		}


		public virtual void Write(double value)
		{
			OutStream.ToByteArray().writeDouble(value);
		}
 


 

	}
}
