﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib;

namespace ArchiveExample
{
	[Script]
	public static class Extensions
	{
		public static short AssertEqualsTo(this short value, short e)
		{
			if (value != e)
				throw new InvalidOperationException();

			return value;
		}

		public static int AssertEqualsTo(this int value, int e)
		{
			if (value != e)
				throw new InvalidOperationException();

			return value;
		}

		public static MemoryStream ReadToMemoryStream(this BinaryReader e, int length)
		{
			return new MemoryStream(e.ReadBytes(length));
		}

		public static string ReadUTF8String(this BinaryReader e, int length)
		{
			var b = new byte[length];

			e.Read(b, 0, length);

			//return Encoding.UTF8.GetString(b);
			return Encoding.ASCII.GetString(b);
		}
	}
}
