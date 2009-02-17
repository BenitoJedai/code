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
		Stream InternalStream;

		const int buffer_uninitialized = -2;
		const int buffer_maxsize = 4;
		byte[] buffer = new byte[buffer_maxsize];
		int buffer_datasize = buffer_uninitialized;

		public __StreamReader(Stream stream)
		{
			this.InternalStream = stream;


		}



		public override string ReadLine()
		{
			var builder = new StringBuilder();

			// copy bytes until \r\n
			// rewrite buffer

			bool cr = false;
			bool done = false;
			bool ceof = false;

			while (!done)
			{
				if (buffer_datasize > 0)
				{
					for (int i = 0; i < buffer_datasize; i++)
					{
						var x = (char)buffer[i];

						if (x == '\n')
						{
							// we are done, need to bail

							done = true;
							i++;

							// preserve buffer for next read
							for (int j = 0; j < buffer_datasize; j++)
							{
								if (j + i < buffer_datasize)
									buffer[j] = buffer[j + i];
								else
									buffer[j] = 0;
							}
							buffer_datasize -= i;

							break;
						}
						else if (cr)
						{
							// we need to bail

							// preserve buffer for next read
							for (int j = 0; j < buffer_datasize; j++)
							{
								if (j + i < buffer_datasize)
									buffer[j] = buffer[j + i];
								else
									buffer[j] = 0;
							}
							buffer_datasize -= i;

							done = true;
							break;
						}

						if (x == '\r')
						{
							cr = true;
						}
						else
						{
							builder.Append(x);
						}
					}

				}
				else
				{
					// we should return null in case of EOF
					ceof = true;
				}

				if (!done)
				{

					// fill buffer
					buffer_datasize = this.InternalStream.Read(buffer, 0, buffer_maxsize);

					if (buffer_datasize == 0)
					{
						done = true;
					}
				}

			}

			if (ceof)
				if (!cr)
					return null;

			return builder.ToString();
		}

		public override string ReadToEnd()
		{
			var builder = new StringBuilder();

			for (int i = 0; i < buffer_datasize; i++)
				builder.Append((char)buffer[i]);

			buffer_datasize = this.InternalStream.Read(buffer, 0, buffer_maxsize);

			while (buffer_datasize >= 0)
			{
				for (int i = 0; i < buffer_datasize; i++)
					builder.Append((char)buffer[i]);

				buffer_datasize = this.InternalStream.Read(buffer, 0, buffer_maxsize);
			}

			return builder.ToString();
		}
	}
}
