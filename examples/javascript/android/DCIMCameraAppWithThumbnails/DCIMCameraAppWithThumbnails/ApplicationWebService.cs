using java.io;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;
using android.graphics;

namespace DCIMCameraAppWithThumbnails
{
    using ystring = Action<string>;
    using com.drew.imaging;
    using com.drew.metadata;
    using System.Text;

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
            // Send it back to the caller.
            y(e);
        }

        public void File_list(
            string path,
            ystring ydirectory,
            ystring yfile,
            string sskip = "0",
            string stake = "10",
            ystring done = null)
        {
            var DIRECTORY_DCIM = android.os.Environment.DIRECTORY_DCIM;

            path = android.os.Environment.getExternalStoragePublicDirectory(DIRECTORY_DCIM).getAbsolutePath();
            path += "/Camera";

            var f = new File(path);

            var a = f.list();

            foreach (var item in a)
            {
                if (new File(path + "/" + item).isDirectory())
                    ydirectory(path + "/" + item);
            }

            int skip = int.Parse(sskip);
            int take = int.Parse(stake);

            foreach (var item in a)
            {
                if (skip > 0)
                {
                    skip--;
                }
                else
                {
                    if (take > 0)
                    {
                        take--;

                        if (new File(path + "/" + item).isFile())
                        {
                            yfile(path + "/" + item);
                        }

                    }
                    else
                    {
                        break;
                    }
                }
            }

            done("");
        }

        const string thumb = "/thumb";
        const string io = "/io";

        // refactor this into separate partial class file
        public void Handler(WebServiceHandler h)
        {


            var path = h.Context.Request.Path;

            var is_io = path.StartsWith(io);
            var is_thumb = path.StartsWith(thumb);

            if (is_io || is_thumb)
            {


                var filepath = path.SkipUntilIfAny(io);

                if (is_thumb)
                {
                    filepath = path.SkipUntilIfAny(thumb);
                }

                // is this still a problem?
                filepath = filepath.Replace("%20", " ");

                var file = new File(filepath);


                if (file.exists())
                    if (file.isFile())
                        if (path.EndsWith(".jpg"))
                        {
                            var bytes = InternalReadBytes(filepath, is_thumb);

                            h.Context.Response.ContentType = "image/jpg";

                            // http://www.webscalingblog.com/performance/caching-http-headers-cache-control-max-age.html
                            h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");

                            // send all the bytes

                            h.Context.Response.OutputStream.Write(bytes, 0, bytes.Length);



                            h.CompleteRequest();
                            return;
                        }

                h.Context.Response.ContentType = "text/html";
                h.Context.Response.Write("what ya lookin for?");
                h.Context.Response.Write(new XElement("pre", filepath).ToString());
                h.CompleteRequest();
                return;
            }
        }

        public void GetEXIF(string path, ystring yield)
        {

            var is_io = path.StartsWith(io);
            var is_thumb = path.StartsWith(thumb);

            var filepath = path.SkipUntilIfAny(io);

            if (is_thumb)
            {
                filepath = path.SkipUntilIfAny(thumb);
            }

            // is this still a problem?
            filepath = filepath.Replace("%20", " ");

            var file = new File(filepath);


            if (file.exists())
                if (file.isFile())
                    if (path.EndsWith(".jpg"))
                    {

                        file.print(yield);

                    }
        }



        private static byte[] InternalReadBytes(string filepath, bool thumb = true)
        {
            var mImageData = (sbyte[])(object)System.IO.File.ReadAllBytes(filepath);

            if (thumb)
            {
                // http://stackoverflow.com/questions/2577221/android-how-to-create-runtime-thumbnail
                int THUMBNAIL_HEIGHT = 96;

                //int THUMBNAIL_WIDTH = 66;

                var imageBitmap = BitmapFactory.decodeByteArray(mImageData, 0, mImageData.Length);
                float width = imageBitmap.getWidth();
                float height = imageBitmap.getHeight();
                float ratio = width / height;
                imageBitmap = Bitmap.createScaledBitmap(imageBitmap, (int)(THUMBNAIL_HEIGHT * ratio), THUMBNAIL_HEIGHT, false);

                //int padding = (THUMBNAIL_WIDTH - imageBitmap.getWidth()) / 2;
                //imageView.setPadding(padding, 0, padding, 0);
                //imageView.setImageBitmap(imageBitmap);



                ByteArrayOutputStream baos = new ByteArrayOutputStream();
                // http://developer.android.com/reference/android/graphics/Bitmap.html
                imageBitmap.compress(Bitmap.CompressFormat.PNG, 0, baos);
                mImageData = baos.toByteArray();

            }

            return (byte[])(object)mImageData;
        }
    }

    public static class X
    {
        public static void print(this File file, ystring yield)
        {
            // https://code.google.com/p/metadata-extractor/wiki/GettingStarted

            var w = new StringBuilder();

            w.AppendLine("metadata: ");

            try
            {

                // http://drewnoakes.com/code/exif/
                Metadata m = ImageMetadataReader.readMetadata(file);


                //            [javac] V:\src\DCIMCameraAppWithThumbnails\ApplicationWebService.java:191: unreported exception com.drew.imaging.ImageProcessingException; must be caught or declared to be thrown
                //[javac]                     metadata4 = ImageMetadataReader.readMetadata(file3);
                //[javac]                                                                 ^



                var i = m.getDirectories().iterator();

                while (i.hasNext())
                {
                    var directory = (Directory)i.next();

                    var tags = directory.getTags().toArray();

                    foreach (Tag tag in tags)
                    {

                        w.AppendLine(new { tag }.ToString());
                    }


                    if (directory.hasErrors())
                    {

                        var ierror = directory.getErrors().iterator();

                        while (ierror.hasNext())
                        {
                            var error = (string)ierror.next();

                            w.AppendLine(new { error }.ToString());
                        }
                    }
                }

                w.AppendLine("end of metadata");

            }
            catch (Exception ex)
            {
                w.AppendLine("error " + new { ex.Message, ex.StackTrace });
            }
            yield(w.ToString());

        }
    }
}
