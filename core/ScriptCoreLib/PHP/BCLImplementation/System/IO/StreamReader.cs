using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
	//public static class __StreamReader_Test
	//{
	//    public static void Assert()
	//    {
	//        var input = new MemoryStream(Encoding.ASCII.GetBytes("aa\rbbb\ncccc\r\nuuuu"));
	//        var r = new __StreamReader(input);
	//        var a = r.ReadLine();
	//        var b = r.ReadLine();
	//        var c = r.ReadLine();
	//        var u = r.ReadLine();
	//        var n = r.ReadLine();
	//    }
	//}

	[Script(Implements = typeof(global::System.IO.StreamReader))]
	internal class __StreamReader : __TextReader
	{
		public virtual Stream BaseStream { get; set; }




		public __StreamReader(Stream BaseStream)
		{
			this.BaseStream = BaseStream;
		}

		

		const int InternalBufferCapacity = 0x100;
		byte[] InternalBuffer = new byte[InternalBufferCapacity];
		int InternalBufferCount = 0;


		public override string ReadToEnd()
		{
			var a = new StringBuilder();

			while (true)
			{
				for (int i = 0; i < this.InternalBufferCount; i++)
				{
					a.Append((char)this.InternalBuffer[i]);
				}

				this.InternalBufferCount = this.BaseStream.Read(this.InternalBuffer, 0, InternalBufferCapacity);

				if (this.InternalBufferCount > 0)
					continue;

				return a.ToString();
			}
		}

		void DiscardBuffer(int bytes)
		{
			if (bytes < 1)
				return;

			for (int i = bytes; i < this.InternalBufferCount; i++)
			{
				this.InternalBuffer[i - bytes] = this.InternalBuffer[i];
			}

			this.InternalBufferCount -= bytes;
		}

		public override string ReadLine()
		{
			var a = new StringBuilder();

			var LineFeedExcpected = false;

			while (true)
			{
				for (int i = 0; i < this.InternalBufferCount; i++)
				{
					var c = (char)this.InternalBuffer[i];

					if (c == '\n')
					{
						DiscardBuffer(i + 1);
						return a.ToString();
					}
					else if (LineFeedExcpected)
					{
						DiscardBuffer(i);

						return a.ToString();
					}

					if (c == '\r')
					{
						LineFeedExcpected = true;
						continue;
					}

					a.Append(c);
				}

				this.InternalBufferCount = this.BaseStream.Read(this.InternalBuffer, 0, InternalBufferCapacity);

				if (this.InternalBufferCount > 0)
					continue;

				return a.ToString();
			}
		}
	}
}
