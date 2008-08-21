using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.BCLImplementation.System.IO;
using ScriptCoreLib.ActionScript.flash.utils;

namespace ScriptCoreLib.ActionScript.Archive.Extensions
{
	[Script(Implements = typeof(global::ScriptCoreLib.Shared.Archive.Extensions.ArchiveExtensions))]
	internal static class __ArchiveExtensions
	{
		public static MemoryStream ReadToMemoryStream(this BinaryReader e, int length)
		{
			var o = new ByteArray();

			e.BaseStream.ToByteArray().readBytes(o, 0, (uint)length);

			return o.ToMemoryStream();
		}

		public static MemoryStream ToMemoryStream(this string e)
		{
			var c = KnownEmbeddedResources.Default[e];

			if (c == null)
				throw new Exception(e);

			return c.ToByteArrayAsset().ToMemoryStream();
		}

		public static string ReadUTF8String(this BinaryReader e, int length)
		{

			return e.BaseStream.ToByteArray().readUTFBytes((uint)length);
		}
	}
}
