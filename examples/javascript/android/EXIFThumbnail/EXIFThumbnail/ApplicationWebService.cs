using com.drew.imaging;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLibJava.Extensions;

namespace EXIFThumbnail
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // jsc, on android how do I get the exif thumbnail of the pictures?
            // firs let make sure we have .android referenced


            var DIRECTORY_DCIM = global::android.os.Environment.DIRECTORY_DCIM;

            var path = global::android.os.Environment.getExternalStoragePublicDirectory(DIRECTORY_DCIM).getAbsolutePath();
            path += "/Camera";

            var s = new Stopwatch();

            s.Start();

            var dir = new java.io.File(path);

            //  { Elapsed = 14595.0ms }
            var a = dir.listFiles().OrderByDescending(k => (double)k.lastModified()).Take(10).ToArray();


            a.WithEach(
                f =>
                {
                    //y(f.getName());



                    try
                    {
                        // http://stackoverflow.com/questions/8578441/can-the-android-sdk-work-with-jdk-1-7
                        // http://stackoverflow.com/questions/15848332/does-adt-support-java-7-api
                        // https://code.google.com/p/android/issues/detail?id=22970
                        //                        [dx] trouble processing:
                        //[dx] bad class file magic (cafebabe) or version (0033.0000)
                        //[dx] ...while parsing com/drew/imaging/bmp/BmpMetadataReader.class
                        //[dx] ...while processing com/drew/imaging/bmp/BmpMetadataReader.class

                        //{ tag = [Exif Thumbnail] Thumbnail Compression - JPEG (old-style) }
                        //{ tag = [Exif Thumbnail] Orientation - Top, left side (Horizontal / normal) }
                        //{ tag = [Exif Thumbnail] X Resolution - 72 dots per inch }
                        //{ tag = [Exif Thumbnail] Y Resolution - 72 dots per inch }
                        //{ tag = [Exif Thumbnail] Resolution Unit - Inch }
                        //{ tag = [Exif Thumbnail] Thumbnail Offset - 1292 bytes }
                        //{ tag = [Exif Thumbnail] Thumbnail Length - 9092 bytes }

                        // http://drewnoakes.com/code/exif/
                        var m = ImageMetadataReader.readMetadata(f);

                        // http://stackoverflow.com/questions/10166373/metadata-extraction-java

                        var t = typeof(com.drew.metadata.exif.ExifThumbnailDirectory).ToClass();
                        if (m.containsDirectory(t))
                        {
                            var x = (com.drew.metadata.exif.ExifThumbnailDirectory)m.getDirectory(t);

                            Console.WriteLine(
                               f.getName()
                            );

                            var data = (byte[])(object)x.getThumbnailData();

                            if (data != null)
                            {
                                var src = "data:image/jpg;base64," +
                                    Convert.ToBase64String(
                                        data
                                    );



                                y(src);
                            }
                        }

                        //com.drew.metadata.exif.ExifThumbnailDirectory.

                        //var i = m.getDirectories().iterator();

                        //while (i.hasNext())
                        //{
                        //    var directory = (com.drew.metadata.Directory)i.next();
                        //    var tags = directory.getTags().toArray();

                        //    foreach (com.drew.metadata.Tag tag in tags)
                        //    {
                        //        tag.
                        //        y(new { tag }.ToString());
                        //    }



                        //}


                    }
                    catch
                    {
                        throw;
                    }
                }
            );

            s.Stop();

            Console.WriteLine(
                new { s.Elapsed }
            );
        }

    }
}
