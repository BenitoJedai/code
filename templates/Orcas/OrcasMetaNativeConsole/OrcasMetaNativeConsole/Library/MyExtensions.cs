using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OrcasMetaNativeConsole.Library
{
	public static class MyExtensions
	{
		public static byte[] ToArray(this Stream s)
		{
			var a = new byte[s.Length];

			s.Seek(0, SeekOrigin.Begin);
			s.Read(a, 0, (int)s.Length);

			return a;
		}

		public static void WriteTo(this Stream e, string path)
		{
			var w = File.OpenWrite(path);
			w.Write(e.ToArray(), 0, (int)e.Length);
			w.Close();
		}

		public static void WriteTo(this string e, BinaryWriter w)
		{
			var a = new byte[e.Length];

			for (int i = 0; i < e.Length; i++)
			{
				a[i] = (byte)e[i];
			}

			w.BaseStream.Write(a, 0, e.Length);
		}
	}
}
