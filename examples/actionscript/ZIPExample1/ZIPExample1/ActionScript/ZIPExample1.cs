using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;
using System;

namespace ZIPExample1.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class ZIPExample1 : Sprite
    {
    

        [Embed("/assets/ZIPExample1/dude5.zip")]
        Class MyZipFile;

        [Script]
        public class ZipFileEntry
        {
            // based on http://livedocs.adobe.com/air/1/devappsflash/help.html?content=ByteArrays_3.html
            // based on http://www.pkware.com/documents/casestudies/APPNOTE.TXT

            //public const int FileHeader = 0x04034b50;

            public int file_header_signature;
            public short required_version;
            public short general_purpose_bit_flag;

            public const short DEFLATE = 8;
            public const short UNCOMPRESSED = 0;

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
            public readonly ByteArray extra_field = new ByteArray();

            public readonly ByteArray file_data = new ByteArray();

            public ZipFileEntry(ByteArray s)
            {
                this.file_header_signature = s.readInt();
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
                s.readBytes(this.extra_field, 0, (uint)this.extra_field_length);

                s.readBytes(file_data, 0, this.compressed_size);

                if (compression_method == DEFLATE)
                    file_data.uncompress();

            }


        }



        /// <summary>
        /// Default constructor
        /// </summary>
        public ZIPExample1()
        {
            var bytes = MyZipFile.ToByteArrayAsset();

            bytes.endian = Endian.LITTLE_ENDIAN;

            var file1 = new ZipFileEntry(bytes);

            throw new Exception(
                new
                {
                    file1.file_header_signature,
                    file1.compression_method,
                    file1.file_name_length,
                    file1.file_name,
                    file1.compressed_size,
                    
                }.ToString()
            );

        }
    }
}