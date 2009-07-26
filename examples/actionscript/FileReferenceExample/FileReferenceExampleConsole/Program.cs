using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Archive.ZIP;
using System.IO;

namespace FileReferenceExampleConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var crc = new Crc32Helper();
			crc.ComputeCrc32(new byte[] { 1, 2, 0xfe, 0xff });
			
			// Crc32Value = 0x3d414fa9

			var z = new ZIPFile
			{
				{"default.txt", "hello world"}
			};

			var m = z.ToBytes();

			var w = new StringBuilder();

			var xxi = 0;
			foreach (var xx in m)
			{
				xxi++;
				w.Append(" " + xx.ToString("x2"));
				if ((xxi % 16) == 0)
					w.AppendLine();
			}

			Console.WriteLine(w.ToString());

			/*
			 * flash
50 4b 03 04 0a 00 00 00 00 00 80 5c fa 3a 16 ff
ff ff 0b 00 00 00 0b 00 00 00 0b 00 00 00 64 65
66 61 75 6c 74 2e 74 78 74 50 4b 01 02 14 00 0a
00 00 00 00 00 80 5c fa 3a 95 2e 51 ff 29 00 00
00 29 00 00 00 0b 00 00 00 00 00 00 00 00 00 00
00 00 00 00 00 00 00 64 65 66 61 75 6c 74 2e 74
78 74 50 4b 05 06 00 00 00 00 01 00 01 00 39 00
00 00 29 00 00 00 00 00		
			 * 
			 * .net
50 4b 03 04 0a 00 00 00 00 00 fd 5c fa 3a 85 11
4a 0d 0b 00 00 00 0b 00 00 00 0b 00 00 00 64 65
66 61 75 6c 74 2e 74 78 74 68 65 6c 6c 6f 20 77
6f 72 6c 64 50 4b 01 02 14 00 0a 00 00 00 00 00
fd 5c fa 3a 85 11 4a 0d 0b 00 00 00 0b 00 00 00
0b 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
00 00 64 65 66 61 75 6c 74 2e 74 78 74 50 4b 05
06 00 00 00 00 01 00 01 00 39 00 00 00 34 00 00
00 00 00


			 */

			File.WriteAllBytes("archive2.zip", m);
		}
	}
}
