using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using System.Net.Sockets;
using ScriptCoreLibJava.BCLImplementation.System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
	[Script(Implements = typeof(global::System.Net.Sockets.NetworkStream))]
	internal class __NetworkStream : __Stream
	{
		public java.io.OutputStream InternalOutputStream;
		public java.io.InputStream InternalInputStream;

		public override void Close()
		{
			Flush();

			try
			{
				this.InternalOutputStream.close();
				this.InternalInputStream.close();
			}
			catch
			{
				throw new InvalidOperationException();
			}
		}

		public override void Flush()
		{
			try
			{
				this.InternalOutputStream.flush();
			}
			catch
			{
				throw new InvalidOperationException();
			}
		}

		public override long Length
		{
			get { throw new NotImplementedException(); }
		}

		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			var i = -1;

			try
			{
				i = this.InternalInputStream.read((sbyte[])(object)buffer, offset, count);
			}
			catch
			{
				throw new InvalidOperationException();
			}

			return i;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			try
			{
				this.InternalOutputStream.write((sbyte[])(object)buffer, offset, count);
			}
			catch
			{
				throw new InvalidOperationException();
			}

		}


	}
}
