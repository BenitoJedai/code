using android.util;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace ApplicationSnapshotStorage
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public  class ApplicationWebService
    {
        // currently created also for handler requests.
        //public 
        AppSnapshot snapshot = new AppSnapshot();


        [Obsolete("jsc shall generate a callsite like this")]
        public void snapshot_Insert(string content, Action<string> key)
        {
            Console.WriteLine("snapshot_Insert");
            this.snapshot.Insert(content, key);
        }

        [Obsolete("jsc shall generate a callsite like this")]
        public void snapshot_SelectAll(Action<string> key)
        {
            Console.WriteLine("snapshot_SelectAll");
            this.snapshot.SelectAll(key);
        }

        [Obsolete("jsc shall generate a callsite like this")]
        public void snapshot_Delete(string AppSnapshotKey)
        {
            Console.WriteLine("snapshot_Delete");
            this.snapshot.Delete(int.Parse(AppSnapshotKey));
        }


        public const string prefix = "/snapshot+";

        public void Handler(WebServiceHandler e)
        {
            //#if Android
            //            Log.wtf("ApplicationSnapshotStorage", new { e.Context.Request.Path }.ToString());
            //            Log.i("ApplicationSnapshotStorage", new { e.Context.Request.Path }.ToString());

            //            Console.WriteLine("ApplicationSnapshotStorage " + new { e.Context.Request.Path }.ToString());

            //            e.Context.Response.ContentType = "text/html";
            //            e.Context.Response.Write("Android intercept");
            //            e.CompleteRequest();
            //            return;
            //#endif
            if (e.Context.Request.Path.StartsWith(prefix))
            {
                var AppSnapshotKey = int.Parse(e.Context.Request.Path.SkipUntilLastOrEmpty(prefix));
                Console.WriteLine("snapshot_SelectBytes");

                snapshot.SelectBytes(AppSnapshotKey,
                    AppSnapshotContent =>
                    {
                        e.Context.Response.ContentType = "text/html; charset=UTF-8";
                        e.Context.Response.Write(AppSnapshotContent);
                        e.CompleteRequest();
                    }
                );
            }
        }


    }
}
