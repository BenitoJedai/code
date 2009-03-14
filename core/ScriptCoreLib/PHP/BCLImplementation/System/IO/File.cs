using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.File))]
	internal class __File
	{
		public static FileStream OpenWrite(string path)
		{
			return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		}

		public static void Delete(string path)
		{
			Native.API.unlink(path);
		}

		public static bool Exists(string path)
		{
			if (Native.API.is_file(path))
				if (Native.API.is_readable(path))
					return true;

			return false;
		}

		public static string ReadAllText(string path)
		{
			return Native.API.file_get_contents(path);
		}

		public static void WriteAllText(string path, string contents)
		{
			Native.API.file_put_contents(path, contents);
		}


		[Script(OptimizedCode = @"return call_user_func_array(pack, array_merge(array($v),(array)$a));")]
		internal static string pack_array(string v, params byte[] a)
		{
			return default(string);
		}

		internal static byte[] ToBytes(string e)
		{
			return (byte[])Native.API.array_values(Native.API.unpack("C*", e));
		}

		internal static string FromBytes(byte[] e)
		{
			return pack_array("C*", e);
		}

		public static void WriteAllBytes(string path, byte[] contents)
		{
			Native.API.file_put_contents(path, FromBytes(contents));
		}

		public static byte[] ReadAllBytes(string path)
		{
			return ToBytes(Native.API.file_get_contents(path));
		}
	}
}
