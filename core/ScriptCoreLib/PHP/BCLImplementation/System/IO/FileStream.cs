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
			InternalHandler = Native.API.fopen(path, ToString(path, mode, access, share));

			if (InternalHandler == null)
				throw new Exception("Unable to open the file.");

		}

		static internal string ToString(string path, FileMode mode, FileAccess access, FileShare share)
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
					if (File.Exists(path))
						return "r+b";
					else
						return "x+b";
				}

				if (access == FileAccess.Read)
				{
					if (File.Exists(path))
						return "rb";
					else
						return "xb";
				}
			}

			var e = new { mode, access, share };
			throw new NotImplementedException(e.ToString());
		}

		public override void SetLength(long value)
		{
			Native.API.ftruncate(this.InternalHandler, (int)value);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return Native.API.fseek(this.InternalHandler, (int)offset, (int)origin);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			var data = Native.API.fread(this.InternalHandler, count);
			var bytes = __File.ToBytes(data);

			for (int i = 0; i < bytes.Length; i++)
			{
				buffer[i + offset] = bytes[i];
			}

			return bytes.Length;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			var bytes = new byte[count];
			for (int i = 0; i < count; i++)
			{
				bytes[i] = buffer[i + offset];
			}

			var data = __File.FromBytes(bytes);

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
				return Native.API.ftell(this.InternalHandler);
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}


	}
}
