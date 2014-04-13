using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Archive
{
    [Obsolete("Abstractatech.ZIPDecoder.ZIPArchive")]
    public partial class ZIPArchive
    {
        // To be removed:
        // ScriptCoreLib.Archive
        // ScriptCoreLib.Archive.ZIP

        public ZIPArchiveFile[] Files;

        public long ArchiveStartOffset;
        public long ArchiveEndOffset;

        public static ZIPArchive OpenArchive(string path)
        {
            var a = new ZIPArchive();

            try
            {

                using (var r = File.OpenRead(path))
                {
                    a.Files = GetEntries(r,
                        (ArchiveStartOffset, ArchiveEndOffset) =>
                        {
                            a.ArchiveEndOffset = ArchiveEndOffset;
                            a.ArchiveStartOffset = ArchiveStartOffset;
                        }
                    ).ToArray();
                }
            }
            catch
            {
                a = null;
            }

            return a;
        }

        public static ZIPArchiveFile[] GetFiles(string path)
        {
            using (var r = File.OpenRead(path))
            {
                return GetEntries(r).ToArray();
            }
        }

        public static IEnumerable<ZIPArchiveFile> GetEntries(
            Stream s,
            Action<long, long> NotifyArchiveBounds = null
            )
        {
            var StartPosition = s.Position;

            var r = new BinaryReader(s);

            // Find the last of 0x06054b50



            Func<ushort> u2 = () => r.ReadUInt16();
            Func<uint> u4 = () => r.ReadUInt32();
            Func<int, byte[]> bytes =
                c => Read(s, () => false, () => (byte)s.ReadByte()).Take(c).ToArray();

            Func<long, bool> seek_f = p => { /*Console.WriteLine("seek: " + p);*/ s.Seek(p, SeekOrigin.Begin); return false; };

            Func<long, int, byte[]> bytes_at = (p, c) => { seek_f(p); return bytes(c); };

            // reading from the end towards the start
            seek_f(s.Length - 20);

            return from CentralDirectoryFound in WhileReading(s)

                   // how large could the zip comment possibly be? :)
                   where (s.Length - s.Position) < 4096 || CentralDirectoryFound()

                   // should we seek backwards?
                   let NextPosition = s.Position - 1

                   where NextPosition < 0 ? CentralDirectoryFound() && false : true

                   let x50 = (byte)s.ReadByte()
                   where x50 == 0x50 ? true : seek_f(NextPosition)
                   let x4b = (byte)s.ReadByte()
                   where x4b == 0x4b ? true : seek_f(NextPosition)
                   let x05 = (byte)s.ReadByte()
                   where x05 == 0x05 ? true : seek_f(NextPosition)
                   let x06 = (byte)s.ReadByte()
                   where x06 == 0x06 ? true : seek_f(NextPosition)

                   let p = s.Position - 4



                   let Number_of_this_disk = r.ReadUInt16()
                   let Disk_where_central_directory_starts = r.ReadUInt16()
                   let Number_of_central_directory_records_on_this_disk = r.ReadUInt16()
                   let Total_number_of_central_directory_records = r.ReadUInt16()
                   let Size_of_central_directory = r.ReadUInt32()
                   let Offset_of_start_of_central_directory_relative_to_start_of_archive = r.ReadUInt32()

                   let ZIP_file_comment_length = r.ReadUInt16()
                   let ZIP_file_comment = bytes(ZIP_file_comment_length)

                   let end_of_archive = s.Position

                   let start_of_central_directory = p - Size_of_central_directory
                   let start_of_archive = start_of_central_directory - Offset_of_start_of_central_directory_relative_to_start_of_archive


                   let CDFH_Positions = new[] { start_of_central_directory }.ToList()
                   let CDFH_AddPosition = new Func<long>(() => { CDFH_Positions.Add(s.Position); return s.Position; })



                   from FileIndex in Enumerable.Range(0, (int)Number_of_central_directory_records_on_this_disk)

                   let HeaderFound = seek_f(CDFH_Positions[FileIndex])

                   // 0x02014b50
                   let CDFH_x50 = (byte)s.ReadByte()
                   let CDFH_x4b = (byte)s.ReadByte()
                   let CDFH_x01 = (byte)s.ReadByte()
                   let CDFH_x02 = (byte)s.ReadByte()

                   where CDFH_x50 == 0x50
                       && CDFH_x4b == 0x4b
                       && CDFH_x01 == 0x01
                       && CDFH_x02 == 0x02

                   let ReadingFilesFromCentralDirectory = CentralDirectoryFound()

                   let _NotifyArchiveBounds = NotifyArchiveBounds.InvokeUnit(
                        start_of_archive, end_of_archive
                   )

                   let CDFH_Position = s.Position - 4

                   let CDFH_Version_made_by = u2()
                   let CDFH_Version_deeded_to_extract = u2()
                   let CDFH_General_purpose_bit_flah = u2()
                   let CDFH_Compression_method = u2()
                   let CDFH_File_last_modification_time = u2()
                   let CDFH_File_last_modification_date = u2()
                   let CDFH_CRC32 = u4()
                   let CDFH_Compressed_size = u4()
                   let CDFH_Uncompressed_size = u4()
                   let CDFH_File_name_length = u2()
                   let CDFH_Extra_field_length = u2()
                   let CDFH_File_comment_length = u2()
                   let CDFH_Disk_number_where_file_starts = u2()
                   let CDFH_Internal_file_attributes = u2()
                   let CDFH_External_file_attributes = u4()
                   let CDFH_Relative_offset_of_local_file_header = u4()
                   let CDFH_File_name = bytes(CDFH_File_name_length)
                   let CDFH_Extra_field = bytes(CDFH_Extra_field_length)
                   let CDFH_File_comment = bytes(CDFH_File_comment_length)

                   let CDFH_NextPosition = CDFH_AddPosition()

                   let utf8_File_name = Encoding.UTF8.GetString(CDFH_File_name)

                   let offset_of_local_file_header = start_of_archive + CDFH_Relative_offset_of_local_file_header

                   let FileHeaderFound = seek_f(offset_of_local_file_header)

                   // 0x04034b50 
                   let FH_x50 = (byte)s.ReadByte()
                   let FH_x4b = (byte)s.ReadByte()
                   let FH_x03 = (byte)s.ReadByte()
                   let FH_x04 = (byte)s.ReadByte()

                   where FH_x50 == 0x50
                       && FH_x4b == 0x4b
                       && FH_x03 == 0x03
                       && FH_x04 == 0x04

                   let FH_Version_needed_to_extract = u2()
                   let FH_General_purpose_bit_flag = u2()
                   let FH_Compression_method = u2()
                   let FH_File_last_modification_time = u2()
                   let FH_File_last_modification_date = u2()
                   let FH_CRC32 = u4()
                   let FH_Compressed_size = u4()
                   let FH_Uncompressed_size = u4()
                   let FH_File_name_length = u2()
                   let FH_Extra_field_length = u2()
                   let FH_File_name = bytes(CDFH_File_name_length)
                   let FH_Extra_field = bytes(CDFH_Extra_field_length)

                   let DataPosition = s.Position
                   let DataLength = (int)Math.Max(FH_Compressed_size, CDFH_Compressed_size)

                   let FH_GetData = new Func<byte[]>(() => bytes_at(DataPosition, DataLength))

                   select new ZIPArchiveFile
                   {
                       Name = utf8_File_name
                       //GetData = FH_GetData
                   };






        }



        public static IEnumerable<Func<bool>> WhileReading(Stream s)
        {
            var _break = false;

            return Read<Func<bool>>(s, () => _break, () => () => _break = true);
        }

        public static IEnumerable<T> Read<T>(Stream s, Func<bool> _break, Func<T> yield)
        {

            while (s.Position < s.Length)
                if (_break())
                    yield break;
                else
                    yield return yield();
        }
    }
}
