using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.CSharp.Extensions;

namespace ScriptCoreLib.Shared.Archive.Extensions
{
	public static class ArchiveExtensions
	{
		// actionscript has its own implementation
		// for methods in this class

		public static MemoryStream ReadToMemoryStream(this BinaryReader e, int length)
		{
			return new MemoryStream(e.ReadBytes(length));
		}

		public static string ReadUTF8String(this BinaryReader e, int length)
		{
			var b = new byte[length];

			e.Read(b, 0, length);

			return Encoding.UTF8.GetString(b);
		}

		public static MemoryStream ToMemoryStream(this string e)
		{
			var s = e.ToManifestResourceStream().Stream;
			var b = new byte[s.Length];
			s.Read(b, 0, b.Length);

			
			return new MemoryStream(b);
		}
	}
}
