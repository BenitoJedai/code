using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLib;

namespace ScriptCoreLibNative.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.FileStream))]
	internal class __FileStream : __Stream
	{
		internal object InternalHandle;

		public void __Write(byte[] buffer, int offset, int count)
		{
			stdio_h.fwrite(buffer, 1, count, InternalHandle);
		}

		public void __Close()
		{
			stdio_h.fclose(InternalHandle);
		}

		public __FileStream()
		{


			// we are doing manual virtual calls.. feel the pain yet?
			this.__Stream_Write = (k, buffer, offset, count) => ((__FileStream)k).__Write(buffer, offset, count);
			this.__Stream_Close = k => ((__FileStream)k).__Close();

		}
	}

}
