﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace ScriptCoreLib.Archive.ZIP
{
	public class ZIPFileEntryHeader
	{
        // see also: http://en.wikipedia.org/wiki/ZIP_(file_format)
        // "PK"0304
		internal const int FileHeader = 0x04034b50;
        internal const int ZIP_data_descriptor = 0x08074b50;

		public int file_header_signature;
		public short required_version;
		public short general_purpose_bit_flag;

		internal const short DEFLATE = 8;
		internal const short UNCOMPRESSED = 0;

		// 2 bytes (8=DEFLATE; 0=UNCOMPRESSED)
		public short compression_method;
		public short last_modified_file_time;
		public short last_modified_file_date;
		public int crc_32;
		public uint compressed_size;
		public uint uncompressed_size;
		public short file_name_length;
		public short extra_field_length;
		public string file_name;

		public MemoryStream extra_field;
		public MemoryStream file_data;

        public override string ToString()
        {
            return this.file_name;
        }

		public static implicit operator ZIPFileEntryHeader(BinaryReader r)
		{
			var p = r.BaseStream.Position;

			var file_header_signature = r.ReadInt32();

			if (file_header_signature != FileHeader)
			{
				// we have found something else than a file

				r.BaseStream.Position = p;
				return null;
			}

			var n = new ZIPFileEntryHeader
			{
				file_header_signature = file_header_signature,
				required_version = r.ReadInt16(),
				general_purpose_bit_flag = r.ReadInt16(),

				// http://en.wikipedia.org/wiki/DEFLATE
				// no compression implemented yet and
				// the builtin variant does not seem to do the trick
				compression_method = r.ReadInt16()/*.AssertEqualsTo(UNCOMPRESSED)*/,
				last_modified_file_time = r.ReadInt16(),
				last_modified_file_date = r.ReadInt16(),
				crc_32 = r.ReadInt32(),
				compressed_size = r.ReadUInt32(),
				uncompressed_size = r.ReadUInt32(),
				file_name_length = r.ReadInt16(),
				extra_field_length = r.ReadInt16(),
			};

			n.file_name = r.ReadUTF8String(n.file_name_length);

			if (n.extra_field_length > 0)
				n.extra_field = r.ReadToMemoryStream(n.extra_field_length);

     
            if (((n.general_purpose_bit_flag >> 3) & 1) == 1)
            {
                // If bit 3 (0x08) of the general-purpose flags field is set, then the CRC-32 and 
                // file sizes are not known when the header is written. The fields in the local 
                // header are filled with zero, and the CRC-32 and size are appended in a 12-byte 
                // structure immediately after the compressed data:

                var ZIP_data_descriptor_signature = r.ReadInt32();

                // pop crc
                if (ZIP_data_descriptor_signature == ZIP_data_descriptor)
                    r.ReadInt32(); 
                //else 
                //    ZIP_data_descriptor_signature;

                var _compressed_size = r.ReadUInt32();
                var _uncompressed_size = r.ReadUInt32();
            }

            if (n.compression_method == UNCOMPRESSED)
                n.file_data = r.ReadToMemoryStream((int)n.compressed_size);


			return n;
		}
	}
}
