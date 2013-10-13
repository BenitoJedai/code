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
using System.Threading.Tasks;

namespace AndroidEnvironmentWebActivity
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // jsc, when can we start sending async interfaces?

        // could we expose property Environment and have client build a
        // more complex call graph?


        public Task<string> Environment_getExternalStorageState()
        {
            return Task.FromResult(
                android.os.Environment.getExternalStorageState()
            );
        }

        public Task<string> Environment_getRootDirectory()
        {
            return Task.FromResult(
                android.os.Environment.getRootDirectory().getAbsolutePath()
            );
        }


        public Task<string> Environment_getDataDirectory()
        {
            return Task.FromResult(
                android.os.Environment.getDataDirectory().getAbsolutePath()
            );
        }

        public Task<string> Environment_getDownloadCacheDirectory()
        {
            return Task.FromResult(
                android.os.Environment.getDownloadCacheDirectory().getAbsolutePath()
            );
        }

        public Task<string> Environment_getExternalStorageDirectory()
        {
            return Task.FromResult(
                android.os.Environment.getExternalStorageDirectory().getAbsolutePath()
            );
        }

        // http://developer.android.com/reference/android/os/Environment.html#getExternalStoragePublicDirectory(java.lang.String)

        public string DIRECTORY_MUSIC = android.os.Environment.DIRECTORY_MUSIC;
        public string DIRECTORY_PODCASTS = android.os.Environment.DIRECTORY_PODCASTS;
        public string DIRECTORY_RINGTONES = android.os.Environment.DIRECTORY_RINGTONES;
        public string DIRECTORY_ALARMS = android.os.Environment.DIRECTORY_ALARMS;
        public string DIRECTORY_NOTIFICATIONS = android.os.Environment.DIRECTORY_NOTIFICATIONS;
        public string DIRECTORY_PICTURES = android.os.Environment.DIRECTORY_PICTURES;
        public string DIRECTORY_MOVIES = android.os.Environment.DIRECTORY_MOVIES;
        public string DIRECTORY_DOWNLOADS = android.os.Environment.DIRECTORY_DOWNLOADS;
        public string DIRECTORY_DCIM = android.os.Environment.DIRECTORY_DCIM;
  

        public void Environment_getExternalStoragePublicDirectory(string DIRECTORY, Action<string> y)
        {
            y(
                android.os.Environment.getExternalStoragePublicDirectory(DIRECTORY).getAbsolutePath()
            );
        }



        public void File_list(string path, Action<string> ydirectory, Action<string> yfile)
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
