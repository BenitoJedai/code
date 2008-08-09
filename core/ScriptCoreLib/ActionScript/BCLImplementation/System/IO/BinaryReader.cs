using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.BinaryReader))]
	internal class __BinaryReader
	{
		private Stream m_stream;

		private byte[] m_buffer;





		public __BinaryReader(Stream input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.m_stream = input;
			this.m_buffer = new byte[0x10];

		}

		public virtual short ReadInt16()
		{
			return m_stream.ToByteArray().readShort();
		}

		private void FillBuffer(int p)
		{
			m_stream.Read(m_buffer, 0, p);
		}


		public virtual byte ReadByte()
		{
			if (this.m_stream == null)
			{
				throw new Exception("FileNotOpen");
			}
			int num = this.m_stream.ReadByte();
			if (num == -1)
			{
				var ms = this.m_stream as MemoryStream;


				if (ms != null)
				{
					
					throw new Exception("MemoryStreamEndOfFile: " + new { this.m_stream.Position, this.m_stream.Length, num, value = ms.ToArray() }.ToString());
				}
				else
					throw new Exception("EndOfFile: " + new { this.m_stream.Position, this.m_stream.Length, num }.ToString());
			}
			return (byte)num;
		}

		public virtual double ReadDouble()
		{
			return m_stream.ToByteArray().readDouble();

		}



	}
}
