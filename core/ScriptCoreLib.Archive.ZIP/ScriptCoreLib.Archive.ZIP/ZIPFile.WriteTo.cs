using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using System.Collections;

namespace ScriptCoreLib.Archive.ZIP
{
	partial class ZIPFile
	{
		public static uint ToMsDosDateTime(DateTime dateTime)
		{
			uint num = 0;
			num |= (uint)((dateTime.Second / 2) & 0x1f);
			num |= (uint)((dateTime.Minute & 0x3f) << 5);
			num |= (uint)((dateTime.Hour & 0x1f) << 11);
			num |= (uint)((dateTime.Day & 0x1f) << 0x10);
			num |= (uint)((dateTime.Month & 15) << 0x15);
			num |= (uint)(((dateTime.Year - 0x7bc) & 0x7f) << 0x19);

			return num;
		}

		public static DateTime FromMsDosDateTime(uint dosDateTime)
		{
			// jsc should prevent javac from reporting possible loss of precision

			long year = (0x7bc + (((int)(dosDateTime >> 0x19)) & 0x7f));
			long second = ((int)((dosDateTime & 0x1f) << 1));
			long minute = (((int)(dosDateTime >> 5)) & 0x3f);
			long hour = (((int)(dosDateTime >> 11)) & 0x1f);
			long day = (((int)(dosDateTime >> 0x10)) & 0x1f);
			long month = (((int)(dosDateTime >> 0x15)) & 15);
			return new DateTime((int)year, (int)month, (int)day, (int)hour, (int)minute, (int)second);
		}

		public void Add(string FileName, params byte[] Data)
		{
			Add(FileName, new MemoryStream(Data));
		}

		public void Add(string FileName, MemoryStream Data)
		{
			this.Add(new Entry { FileName = FileName, Data = Data });
		}

		private void Add(Entry entry)
		{
			this.Items.Add(entry);
		}


		public void WriteTo(BinaryWriter w)
		{
			// http://www.pkware.com/documents/casestudies/APPNOTE.TXT

			var offsets = new Queue();
			var Items = this.Entries;
			
			#region Local file header:
			foreach (var v in Items)
			{
				// if we box unit, it will actually represent Long 
				// this is for tostring functionality

				offsets.Enqueue((uint)w.BaseStream.Position);

				//var h = v.Header;
				var file_name = Encoding.ASCII.GetBytes(v.FileName);

				//        local file header signature     4 bytes  (0x04034b50)
				w.Write(ZIPFileEntryHeader.FileHeader);
				//        version needed to extract       2 bytes
				w.Write((short)0x000A);
				//        general purpose bit flag        2 bytes
				w.Write((short)0);

				//        compression method              2 bytes
				w.Write(ZIPFileEntryHeader.UNCOMPRESSED);
				//        last mod file time              2 bytes
				//        last mod file date              2 bytes
				w.WriteUInt32(ToMsDosDateTime(DateTime.Now));

				//        crc-32                          4 bytes
				w.WriteUInt32((uint)Crc32Helper.GetCrc32(v.Data.ToArray()));

				//        compressed size                 4 bytes
				w.WriteUInt32((uint)v.Data.Length);
				//        uncompressed size               4 bytes
				w.WriteUInt32((uint)v.Data.Length);


				//        file name length                2 bytes
				w.Write((short)file_name.Length);

				//        extra field length              2 bytes
				w.Write((short)0);

				//        file name (variable size)
				w.Write(file_name);

				//        extra field (variable size)

				v.Data.WriteTo(w.BaseStream);

			}
			#endregion

			var p = w.BaseStream.Position;


			#region Central directory structure
			foreach (var v in Items)
			{
				//var h = v.Header;
				var offset = (uint)offsets.Dequeue();

				var file_name = Encoding.ASCII.GetBytes(v.FileName);


				//       central file header signature   4 bytes  (0x02014b50)
				w.Write((int)0x02014b50);
				//       version made by                 2 bytes
				w.WriteUInt16((ushort)0x0014);
				//       version needed to extract       2 bytes
				w.WriteUInt16((ushort)0x000A);
				//       general purpose bit flag        2 bytes
				w.WriteUInt16((ushort)0x0000);
				//       compression method              2 bytes
				w.Write(ZIPFileEntryHeader.UNCOMPRESSED);
				//       last mod file time              2 bytes
				//       last mod file date              2 bytes
				w.WriteUInt32(ToMsDosDateTime(DateTime.Now));
				//       crc-32                          4 bytes
				w.WriteUInt32((uint)Crc32Helper.GetCrc32(v.Data.ToArray()));
				//       compressed size                 4 bytes
				w.WriteUInt32((uint)v.Data.Length);
				//       uncompressed size               4 bytes
				w.WriteUInt32((uint)v.Data.Length);
				//       file name length                2 bytes
				w.WriteUInt16((ushort)file_name.Length);
				//       extra field length              2 bytes
				w.WriteUInt16((ushort)0x0000);
				//       file comment length             2 bytes
				w.WriteUInt16((ushort)0x0000);
				//       disk number start               2 bytes
				w.WriteUInt16((ushort)0x0000);
				//       internal file attributes        2 bytes
				w.WriteUInt16((ushort)0x0000);
				//       external file attributes        4 bytes
				w.Write((int)0x0000);
				//       relative offset of local header 4 bytes
				w.WriteUInt32((uint)offset);
				//       file name (variable size)
				w.Write(file_name);
				//       extra field (variable size)
				//       file comment (variable size)
			}
			#endregion

			//      

			var q = w.BaseStream.Position;
			var z = q - p;

			#region I.  End of central directory record:
			//end of central dir signature    4 bytes  (0x06054b50)
			w.WriteUInt32((uint)0x06054b50);

			//number of this disk             2 bytes
			w.WriteUInt16((ushort)0);
			//number of the disk with the
			//start of the central directory  2 bytes
			w.WriteUInt16((ushort)0);
			//total number of entries in the
			//central directory on this disk  2 bytes
			w.WriteUInt16((ushort)Items.Length);
			//total number of entries in
			//the central directory           2 bytes
			w.WriteUInt16((ushort)Items.Length);
			//size of the central directory   4 bytes
			w.WriteUInt32((uint)z);
			//offset of start of central
			//directory with respect to
			//the starting disk number        4 bytes
			w.WriteUInt32((uint)p);
			//.ZIP file comment length        2 bytes
			w.WriteUInt16((ushort)0);
			//.ZIP file comment       (variable size)
			#endregion


		}

	}

}
