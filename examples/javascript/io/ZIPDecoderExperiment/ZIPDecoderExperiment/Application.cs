using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZIPDecoderExperiment;
using ZIPDecoderExperiment.Design;
using ZIPDecoderExperiment.HTML.Pages;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.IO;
using Abstractatech.ZIPDecoder;


// time to publish.
namespace Abstractatech.ZIPDecoder
{
    //Error	15	The task factory "CodeTaskFactory" could not be loaded from the assembly "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Microsoft.Build.Tasks.v4.0.dll". Could not find file 'C:\Users\Arvo\AppData\Local\Temp\a1kdzdpu.dll'.	ZIPDecoderExperiment
    // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Archive\ZIPArchive.cs

    //using ScriptCoreLib.Archive;

    public class ZIPArchiveFile
    {
        public string Name;

        // compressed data?
        //public Func<byte[]> GetData;
    }

    public partial class ZIPArchive
    {
        // To be removed:
        // ScriptCoreLib.Archive
        // ScriptCoreLib.Archive.ZIP

        public ZIPArchiveFile[] Files;

        public long ArchiveStartOffset;
        public long ArchiveEndOffset;


        //public static IEnumerable<T> Read<T>(Stream s, Func<bool> _break, Func<T> yield)
        // js might not like <T> for switch rewrite
        public static IEnumerable<Func<bool>> ReadAsFuncBoolean(Stream s, Func<bool> _break, Func<Func<bool>> yield)
        {

            while (s.Position < s.Length)
                if (_break())
                    yield break;
                else
                    yield return yield();
        }

        public static IEnumerable<Func<bool>> WhileReading(Stream s)
        {
            var _break = false;

            return ReadAsFuncBoolean(s, () => _break, () => () => _break = true);
        }


        public static IEnumerable<byte> ReadAsByte(Stream s, Func<bool> _break, Func<byte> yield)
        {

            while (s.Position < s.Length)
                if (_break())
                    yield break;
                else
                    yield return yield();
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
                c => ReadAsByte(s, () => false, () => (byte)s.ReadByte()).Take(c).ToArray();

            Func<long, bool> seek_f = p => { /*Console.WriteLine("seek: " + p);*/ s.Seek(p, SeekOrigin.Begin); return false; };

            Func<long, int, byte[]> bytes_at = (p, c) => { seek_f(p); return bytes(c); };

            // reading from the end towards the start
            seek_f(s.Length - 20);


            Func<string, bool> w = text =>
            {
                Console.WriteLine(text);
                return true;
            };

            //Console.WriteLine("before WhileReading");
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

                   where w(new { p }.ToString())

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

                   let _NotifyArchiveBounds = NotifyArchiveBounds.InvokeUnit(
                         start_of_archive, end_of_archive
                    )

                   //CDFH_AddPosition { start_of_central_directory = 65011, Position = 65064, Count = 1, Number_of_central_directory_records_on_this_disk = 40 }
                   //CDFH_AddPosition { start_of_central_directory = 65011, Position = 65117, Count = 2, Number_of_central_directory_records_on_this_disk = 40 }
                   //CDFH_AddPosition { start_of_central_directory = 65011, Position = 65170, Count = 3, Number_of_central_directory_records_on_this_disk = 40 }
                   //CDFH_AddPosition { start_of_central_directory = 65011, Position = 65223, Count = 4, Number_of_central_directory_records_on_this_disk = 40 }

                   //where w(new { start_of_central_directory }.ToString())

                   let CDFH_Positions = new[] { start_of_central_directory }.ToList()
                   let CDFH_AddPosition = new Func<long>(() =>
                   {

                       //Console.WriteLine("CDFH_AddPosition " + new
                       //{
                       //    start_of_central_directory,
                       //    s.Position,
                       //    CDFH_Positions.Count,
                       //    Number_of_central_directory_records_on_this_disk
                       //});

                       CDFH_Positions.Add(s.Position); return s.Position;
                   })



                   from FileIndex in Enumerable.Range(0, (int)Number_of_central_directory_records_on_this_disk)

                   //where w(new { FileIndex, CDFH_Positions.Count }.ToString())

                   //0:4733ms before WhileReading
                   //0:4741ms { p = 67131 }
                   //0:4744ms { start_of_central_directory = 65011 }
                   //0:4745ms { FileIndex = 0, Count = 1 }
                   //0:4745ms { FileIndex = 1, Count = 1 }

                   let HeaderFound = seek_f(CDFH_Positions[FileIndex])

                   // 0x02014b50
                   let CDFH_x50 = (byte)s.ReadByte()
                   let CDFH_x4b = (byte)s.ReadByte()
                   let CDFH_x01 = (byte)s.ReadByte()
                   let CDFH_x02 = (byte)s.ReadByte()

                   //where w(new { CDFH_x50, CDFH_x4b, CDFH_x01, CDFH_x02 }.ToString())

                   // 0:7287ms { CDFH_x50 = 80, CDFH_x4b = 75, CDFH_x01 = 1, CDFH_x02 = 2 }

                   // jsc does not like it yet
                   //where CDFH_x50 == 0x50
                   //    && CDFH_x4b == 0x4b
                   //    && CDFH_x01 == 0x01
                   //    && CDFH_x02 == 0x02

                   where CDFH_x50 == 0x50
                   where CDFH_x4b == 0x4b
                   where CDFH_x01 == 0x01
                   where CDFH_x02 == 0x02

                   //where w("before ReadingFilesFromCentralDirectory")

                   let ReadingFilesFromCentralDirectory = CentralDirectoryFound()

                   //where w(new { ReadingFilesFromCentralDirectory }.ToString())



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
                   where FH_x4b == 0x4b
                   where FH_x03 == 0x03
                   where FH_x04 == 0x04

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



    }


}

namespace ZIPDecoderExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {





        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\examples\actionscript\io\ZipExample2\ZipExample2\Shared\MyCanvas.cs



            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Archive\ZIPArchive.cs

            // ScriptCoreLib.Ultra.Library.Extensions
            // X:\jsc.svn\examples\javascript\io\GIFDecoderExperiment\GIFDecoderExperiment\Application.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140413

            Action<byte[]> AtZIPBytes = bytes =>
            {
                // { type = application/x-zip-compressed, name = dude5.zip, size = 67153, md5hex = acedbcbdf70dae38a1f7a7333ba35585, ElapsedMilliseconds = 43 }

                var s = Stopwatch.StartNew();
                var e = ZIPArchive.GetEntries(
                    new MemoryStream(bytes),
                    NotifyArchiveBounds:
                        (lo, hi) =>
                        {
                            new IHTMLPre {
                                new { 
                                   lo,
                                   hi
                                }
                            }.AttachToDocument();
                        }
                );

                var a = e.ToArray();

                // { Length = 40, ElapsedMilliseconds = 136 }

                new IHTMLPre {
                    new { 
                        a.Length,
                        s.ElapsedMilliseconds
                    }
                }.AttachToDocument();

                a.WithEach(
                    x =>
                    {
                        new IHTMLPre {
                            new { 
                                x.Name
                            }
                        }.AttachToDocument();
                    }
                );
            };

            Native.document.documentElement.ondragover +=
                e =>
                {
                    e.stopPropagation();
                    e.preventDefault();

                    e.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.
                    page.body.style.backgroundColor = "cyan";
                };

            Native.document.documentElement.ondrop +=
                //async 
                  e =>
                  {
                      // X:\jsc.svn\examples\javascript\io\WebApplicationSelectingFile\WebApplicationSelectingFile\Application.cs

                      page.body.style.backgroundColor = "";

                      Console.WriteLine("ondrop");

                      e.stopPropagation();
                      e.preventDefault();
                      FileList x = e.dataTransfer.files; // FileList object.

                      for (uint i = 0; i < x.length; i++)
                      {
                          var f = x[i];

                          var s = Stopwatch.StartNew();

                          //Method not found: 'Void ScriptCoreLib.JavaScript.DOM.DataTransferItem.getAsString(ScriptCoreLib.JavaScript.DOM.IFunction)'.
                          // do redux rebuild!

                          //var bytes = await f.readAsBytes();
                          f.readAsBytes().ContinueWithResult(
                              bytes =>
                              {

                                  var md5 = bytes.ToMD5Bytes();
                                  var md5hex = md5.ToHexString();

                                  new IHTMLPre {
                                        new { 
                                            f.type,
                                        f.name, 
                                        f.size,
                                        md5hex,
                                        s.ElapsedMilliseconds
                                        }
                                    }.AttachToDocument();


                                  if (f.name.EndsWith(".zip"))
                                      AtZIPBytes(bytes);
                              }
                          );

                      }
                  };
        }

    }
}

//02000032 Abstractatech.ZIPDecoder.ZIPArchive+<ReadAsFuncBoolean>d__3
//script: error JSC1000: if block not detected correctly, opcode was { Branch = [0x0008]
//bne.un.s   +0 -2{[0x0001]
//ldfld      +1 -1{[0x0000]
//ldarg.0    +1 -0} } {[0x0006]
//ldc.i4.s   +1 -0} , Location =
// assembly: V:\ZIPDecoderExperiment.Application.exe
// type: Abstractatech.ZIPDecoder.ZIPArchive+<ReadAsFuncBoolean>d__3, ZIPDecoderExperiment.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x0008
//  method:System.Collections.Generic.IEnumerator`1[System.Func`1[System.Boolean]] System.Collections.Generic.IEnumerable<System.Func<System.Boolean>>.GetEnumerator() }