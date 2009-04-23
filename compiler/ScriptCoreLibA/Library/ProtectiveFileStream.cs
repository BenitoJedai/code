using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Library
{
	/// <summary>
	/// ProtectiveFileStream protects against modifing a file when no changes were actually made
	/// </summary>
	public class ProtectiveFileStream : Stream
	{
		readonly MemoryStream InternalStream = new MemoryStream();
		readonly FileInfo InternalFile;

		public ProtectiveFileStream(FileInfo file)
		{
			InternalFile = file;
		}

		public event Action NotModified;

		bool InternalDisposed;
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (!InternalDisposed)
			{
				InternalDisposed = true;

				var y = InternalStream.ToArray();

				if (InternalFile.Exists)
				{
					var x = File.ReadAllBytes(InternalFile.FullName);

					if (x.Length == y.Length)
					{
						if (y.Select((k, i) => x[i] == k).All(k => k))
						{
							if (NotModified != null)
								NotModified();

							return;
						}
					}
				}

				using (var s = InternalFile.OpenWrite())
				{
					s.SetLength(0);
					s.Write(y, 0, y.Length);
				}

				
			}

			
		}

		public override bool CanRead
		{
			get { return InternalStream.CanRead; }
		}

		public override bool CanSeek
		{
			get { return InternalStream.CanSeek; }
		}

		public override bool CanWrite
		{
			get { return InternalStream.CanWrite; }
		}

		public override void Flush()
		{
			InternalStream.Flush();
		}

		public override long Length
		{
			get { return InternalStream.Length; }
		}

		public override long Position
		{
			get
			{
				return InternalStream.Position;
			}
			set
			{
				InternalStream.Position = value;
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return InternalStream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return InternalStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			InternalStream.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			InternalStream.Write(buffer, offset, count);
		}
	}
}
