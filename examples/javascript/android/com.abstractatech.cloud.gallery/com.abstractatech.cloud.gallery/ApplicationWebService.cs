using java.io;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;
using android.graphics;
using System.Text;
using java.io;


namespace com.abstractatech.cloud.gallery
{
    using ystring = Action<string>;


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
#if Android
            var DIRECTORY_DCIM = global::android.os.Environment.DIRECTORY_DCIM;

            path = global::android.os.Environment.getExternalStoragePublicDirectory(DIRECTORY_DCIM).getAbsolutePath();
            path += "/Camera";

            var f = new File(path);

            //Caused by: java.lang.Runtim
            //eException: Implement IComparable for java.lang.Long vs java.lang.Long
            //       at ScriptCoreLibJava.BCLImplementation.System.Collections.__Comparer.Compare(__Comparer.java:136)

            var a = f.listFiles().Where(k => k.getName().EndsWith(".jpg")).OrderByDescending(k => (double)k.lastModified()).ToArray();

            //foreach (var item in a)
            //{
            //    if (new File(path + "/" + item).isDirectory())
            //        ydirectory(path + "/" + item);
            //}

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


                        yfile(path + "/" + item.getName());

                    }
                    else
                    {
                        break;
                    }
                }
            }
#endif

            if (done != null)
                done("");
        }

        const string thumb = "/thumb";
        const string io = "/io";


        // refactor this into separate partial class file
        public void Handler(WebServiceHandler h)
        {
            var Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":");

            var path = h.Context.Request.Path;

            if (h.IsDefaultPath)
            {
                if (!h.Context.Request.QueryString.AllKeys.Contains("about"))
                    if (Host == h.Context.Request.UserHostAddress)
                    {
                        //h.Context.Response.SetCookie(new System.Web.HttpCookie("about", "me"));

                        //System.Console.WriteLine("open this in laptop");

                        h.Context.Response.Redirect("/?about=me");
                        h.CompleteRequest();

                        return;
                    }
            }



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

#if Android
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
#endif


                h.Context.Response.ContentType = "text/html";
                h.Context.Response.Write("what ya lookin for?");
                h.Context.Response.Write(new XElement("pre", filepath).ToString());
                h.CompleteRequest();
                return;
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
}
