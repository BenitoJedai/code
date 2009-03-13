using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.FileStream))]
	internal class __FileStream : __Stream
	{
		internal object InternalHandler;

		public override void Close()
		{
			Native.API.fclose(InternalHandler);
			InternalHandler = null;
		}

		public __FileStream(string path, FileMode mode, FileAccess access, FileShare share)
		{
			InternalHandler = Native.API.fopen(path, ToString(mode, access, share));
		}

		static internal string ToString(FileMode mode, FileAccess access, FileShare share)
		{
			// http://ee.php.net/fopen

			//'r'  	 Open for reading only; place the file pointer at the beginning of the file.
			//'r+' 	Open for reading and writing; place the file pointer at the beginning of the file.
			//'w' 	Open for writing only; place the file pointer at the beginning of the file and truncate the file to zero length. If the file does not exist, attempt to create it.
			//'w+' 	Open for reading and writing; place the file pointer at the beginning of the file and truncate the file to zero length. If the file does not exist, attempt to create it.
			//'a' 	Open for writing only; place the file pointer at the end of the file. If the file does not exist, attempt to create it.
			//'a+' 	Open for reading and writing; place the file pointer at the end of the file. If the file does not exist, attempt to create it.
			//'x' 	Create and open for writing only; place the file pointer at the beginning of the file. If the file already exists, the fopen() call will fail by returning FALSE and generating an error of level E_WARNING. If the file does not exist, attempt to create it. This is equivalent to specifying O_EXCL|O_CREAT flags for the underlying open(2) system call.
			//'x+' 	Create and open for reading and writing; place the file pointer at the beginning of the file. If the file already exists, the fopen() call will fail by returning FALSE and generating an error of level E_WARNING. If the file does not exist, attempt to create it. This is equivalent to specifying O_EXCL|O_CREAT flags for the underlying open(2) system call. 

			if (mode == FileMode.OpenOrCreate)
			{
				if (access == FileAccess.Write)
				{
					return "wb";
				}
			}

			var e = new { mode, access, share };
			throw new NotImplementedException(e.ToString());
		}

		public override void SetLength(long value)
		{
			
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return Native.API.fseek(this.InternalHandler, (int)offset, (int)origin);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException("");
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			var data = "";

			for (int i = offset; i < count; i++)
			{
				data += Native.API.chr(buffer[i]);
			}


			Native.API.fwrite(this.InternalHandler, data);
		}

		public override long Length
		{
			get { throw new NotImplementedException(""); }
		}

		public override long Position
		{
			get
			{
				throw new NotImplementedException("");
			}
			set
			{
				throw new NotImplementedException("");
			}
		}


	}
}
