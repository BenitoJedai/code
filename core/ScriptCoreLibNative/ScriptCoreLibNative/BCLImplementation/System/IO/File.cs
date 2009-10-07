using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System.IO;

namespace ScriptCoreLibNative.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.File))]
	internal class __File
	{
		// http://www.cplusplus.com/reference/clibrary/cstdio/fopen.html

		public static void WriteAllText(string path, string contents)
		{
			var handle = stdio_h.fopen(path, "w+");
			stdio_h.fputs(contents, handle);
			stdio_h.fclose(handle);
		}


		public static FileStream OpenWrite(string path)
		{
			var f = new __FileStream
			{
				InternalHandle = stdio_h.fopen(path, "w+")
			};

			return (FileStream)(object)f;
		}
	}

}
