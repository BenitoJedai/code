using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib;
using System.Reflection;
using System.Collections;

namespace ScriptCoreLib.Archive.ZIP
{
	internal static class Extensions
	{
		public static string CombinePath(this string category, string target)
		{
			if (string.IsNullOrEmpty(category))
				return target;

			return Path.Combine(category, target);
		}


		public static void WriteUInt32(this BinaryWriter w, uint value)
		{
			w.Write((int)value);
		}

		public static void WriteUInt16(this BinaryWriter w, ushort value)
		{
			w.Write((short)value);
		}


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

			// FIXME: implement BCL type for java

			//return Encoding.UTF8.GetString(b);
			return Encoding.ASCII.GetString(b);
		}
	}
}
