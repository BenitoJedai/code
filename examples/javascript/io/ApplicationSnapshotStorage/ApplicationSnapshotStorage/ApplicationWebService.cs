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
    public class ApplicationWebService
    {
        // currently created also for handler requests.
        //public 
        #region snapshot
        AppSnapshot snapshot = new AppSnapshot();


        // http://blog.slaks.net/2011/10/caller-info-attributes-vs-stack-walking.html
        //In fact, if the attacker invokes your delegate on a UI thread (by calling BeginInvoke), his assembly won’t show up on the callstack at all.  The attacker can also compile a dynamic assembly that calls your function and call into that assembly.

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
        #endregion


        //public const string prefix = "/snapshot+";
        public const string prefix = "/view-source+";

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

            #region /view-source+
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
                return;
            }
            #endregion

        }


    }
}
