using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;

namespace ScriptCoreLib.ActionScript.Extensions
{


    /// <summary>
    /// This class can be used to read a simple zip file
    /// </summary>
    [Script]
    public class ZipFileEntry
    {
        // based on http://livedocs.adobe.com/air/1/devappsflash/help.html?content=ByteArrays_3.html
        // based on http://www.pkware.com/documents/casestudies/APPNOTE.TXT

        [Script]
        public class Cookie<T>
        {
            public ZipFileEntry Entry;
            public T Value;
        }

        readonly int file_header_signature;
        readonly short required_version;
        readonly short general_purpose_bit_flag;

        const short DEFLATE = 8;
        const short UNCOMPRESSED = 0;

        // 2 bytes (8=DEFLATE; 0=UNCOMPRESSED)
        readonly short compression_method;
        readonly short last_modified_file_time;
        readonly short last_modified_file_date;
        readonly int crc_32;
        readonly uint compressed_size;
        readonly uint uncompressed_size;
        readonly short file_name_length;
        readonly short extra_field_length;
        readonly string file_name;

        readonly ByteArray extra_field = new ByteArray();

        readonly ByteArray file_data = new ByteArray();

        public ByteArray Bytes
        {
            get
            {
                return this.file_data;
            }
        }

        public string FileName
        {
            get
            {
                return this.file_name;
            }
        }

        public static ZipFileEntry[] Parse(ByteArray s)
        {
            const int FileHeader = 0x04034b50;

            var file_header_signature = default(int);
            var a = new List<ZipFileEntry>();
            var x = true;

            while (x)
            {
                file_header_signature = s.readInt();

                if (file_header_signature == FileHeader)
                    a.Add(new ZipFileEntry(file_header_signature, s));
                else
                    x = false;

                if (s.position == s.length)
                    x = false;
            }

            return a.ToArray();
        }

        public ZipFileEntry(int file_header_signature, ByteArray s)
        {
            this.file_header_signature = file_header_signature;
            this.required_version = s.readShort();
            this.general_purpose_bit_flag = s.readShort();
            this.compression_method = s.readShort();
            this.last_modified_file_time = s.readShort();
            this.last_modified_file_date = s.readShort();
            this.crc_32 = s.readInt();
            this.compressed_size = s.readUnsignedInt();
            this.uncompressed_size = s.readUnsignedInt();
            this.file_name_length = s.readShort();
            this.extra_field_length = s.readShort();

            this.file_name = s.readUTFBytes((uint)this.file_name_length);

            if (this.extra_field_length > 0)
                s.readBytes(this.extra_field, 0, (uint)this.extra_field_length);

            s.readBytes(file_data, 0, this.compressed_size);

            if (compression_method == DEFLATE)
            {
                try
                {
                    file_data.uncompress();
                }
                catch (Exception ex)
                {
                    throw new Exception("The file '" + file_name + "' was unable to be uncompressed. " + ex.Message);
                }
            }

        }


    }

}
