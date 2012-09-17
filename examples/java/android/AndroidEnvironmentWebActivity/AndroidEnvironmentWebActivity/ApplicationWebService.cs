using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using android.os;
using android.widget;
using java.io;
using ScriptCoreLib.Ultra.WebService;

namespace AndroidEnvironmentWebActivity
{

    using ystring = Action<string>;

    public delegate void Environment_DIRECTORY_callback(
        string DIRECTORY_MUSIC,
        string DIRECTORY_PODCASTS,
        string DIRECTORY_RINGTONES,
        string DIRECTORY_ALARMS,
        string DIRECTORY_NOTIFICATIONS,
        string DIRECTORY_PICTURES,
        string DIRECTORY_MOVIES,
        string DIRECTORY_DOWNLOADS,
        string DIRECTORY_DCIM
    );

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        // could we expose property Environment and have client build a
        // more complex call graph?

        public void Environment_getDataDirectory(string e, Action<string> y)
        {
            y(
                android.os.Environment.getDataDirectory().getAbsolutePath()
            );
        }

        public void Environment_getDownloadCacheDirectory(string e, Action<string> y)
        {
            y(
                android.os.Environment.getDownloadCacheDirectory().getAbsolutePath()
            );
        }

        public void Environment_getExternalStorageDirectory(string e, Action<string> y)
        {
            y(
                android.os.Environment.getExternalStorageDirectory().getAbsolutePath()
            );
        }

        // http://developer.android.com/reference/android/os/Environment.html#getExternalStoragePublicDirectory(java.lang.String)

        public void Environment_DIRECTORY(string e, Environment_DIRECTORY_callback y)
        {
            y(
                DIRECTORY_MUSIC: android.os.Environment.DIRECTORY_MUSIC,
                DIRECTORY_PODCASTS: android.os.Environment.DIRECTORY_PODCASTS,
                DIRECTORY_RINGTONES: android.os.Environment.DIRECTORY_RINGTONES,
                DIRECTORY_ALARMS: android.os.Environment.DIRECTORY_ALARMS,
                DIRECTORY_NOTIFICATIONS: android.os.Environment.DIRECTORY_NOTIFICATIONS,
                DIRECTORY_PICTURES: android.os.Environment.DIRECTORY_PICTURES,
                DIRECTORY_MOVIES: android.os.Environment.DIRECTORY_MOVIES,
                DIRECTORY_DOWNLOADS: android.os.Environment.DIRECTORY_DOWNLOADS,
                DIRECTORY_DCIM: android.os.Environment.DIRECTORY_DCIM
            );
        }

        public void Environment_getExternalStoragePublicDirectory(string DIRECTORY, Action<string> y)
        {
            y(
                android.os.Environment.getExternalStoragePublicDirectory(DIRECTORY).getAbsolutePath()
            );
        }

        public void Environment_getExternalStorageState(string DIRECTORY, Action<string> y)
        {
            y(
                android.os.Environment.getExternalStorageState()
            );
        }

        public void Environment_getRootDirectory(string DIRECTORY, Action<string> y)
        {
            y(
                android.os.Environment.getRootDirectory().getAbsolutePath()
            );
        }


        public void File_list(string path, ystring ydirectory, ystring yfile)
        {
            var f = new File(path);

            var a = f.list();

            foreach (var item in a)
            {
                if (new File(path + "/" + item).isDirectory())
                    ydirectory(item);
            }

            foreach (var item in a)
            {
                if (new File(path + "/" + item).isFile())
                    yfile(item);
            }
        }



        public void Handler(WebServiceHandler h)
        {
            var io = "/io";
            var path = h.Context.Request.Path;
            if (path.StartsWith(io))
            {


                var filepath = path.SkipUntilIfAny(io);

                filepath = filepath.Replace("%20", " ");

                var file = new File(filepath);


                if (file.exists())
                    if (file.isFile())
                        if (path.EndsWith(".jpg"))
                        {
                            var bytes = System.IO.File.ReadAllBytes(filepath);

                            h.Context.Response.ContentType = "image/jpg";


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
    }


}
