using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace WavePlayer.Library
{
	[Script]
	public static class MyExtensions
	{
		public static byte[] ToArray(this Stream s)
		{
			var a = new byte[s.Length];

			s.Seek(0, SeekOrigin.Begin);
			s.Read(a, 0, (int)s.Length);

			return a;
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
