using android.content;
using android.content.pm;
using android.net;
using java.io;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AndroidListApplications
{
    public delegate void yield_ACTION_MAIN(string packageName, string name);

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="yield">A callback to javascript.</param>
        public void queryIntentActivities(yield_ACTION_MAIN yield, Action yield_done)
        {
            // http://stackoverflow.com/questions/2695746/how-to-get-a-list-of-installed-android-applications-and-pick-one-to-run
            // https://play.google.com/store/apps/details?id=com.flopcode.android.inspector

            var mainIntent = new Intent(Intent.ACTION_MAIN, null);

            mainIntent.addCategory(Intent.CATEGORY_LAUNCHER);

            var context = ThreadLocalContextReference.CurrentContext;



            var pkgAppsList = context.getPackageManager().queryIntentActivitiesEnumerable(mainIntent).OrderBy(k => k.activityInfo.packageName).WithEach(
                r =>
                {


                    yield(
                        r.activityInfo.applicationInfo.packageName, r.activityInfo.name
                    );
                }
            );

            yield_done();
        }


        public void Launch(
            string packageName,
            string name,

            string ExtraKey = "ExtraKey",
            string ExtraValue = "ExtraValue"
            )
        {
            // http://stackoverflow.com/questions/12504954/how-to-start-an-intent-from-a-resolveinfo
            var c = new ComponentName(packageName, name);
            Intent i = new Intent(Intent.ACTION_MAIN);

            i.addCategory(Intent.CATEGORY_LAUNCHER);
            i.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK |
                        Intent.FLAG_ACTIVITY_RESET_TASK_IF_NEEDED);
            i.setComponent(c);

            // http://stackoverflow.com/questions/11860074/start-activity-for-result
            // http://stackoverflow.com/questions/2844440/passing-arguments-from-loading-activity-to-main-activity
            i.putExtra(ExtraKey, ExtraValue);


            var context = ThreadLocalContextReference.CurrentContext;

            context.startActivity(i);
        }

        public void Remove(string packageName, string name)
        {
            // http://stackoverflow.com/questions/6813322/install-uninstall-apks-programmatically-packagemanager-vs-intents
            var context = ThreadLocalContextReference.CurrentContext;
            // http://stackoverflow.com/questions/8228365/how-do-i-remove-any-app-from-a-device-using-my-app-in-android
            // http://stackoverflow.com/questions/6049622/action-delete-android
            Intent intent = new Intent(Intent.ACTION_DELETE);
            intent.setData(android.net.Uri.parse("package:" + packageName));
            context.startActivity(intent);
        }


        public void Install(string filename)
        {
            try
            {
                System.Console.WriteLine("will install " + filename);

                // assets only?
                var context = ThreadLocalContextReference.CurrentContext;

                // extract asset
                var assets = context.getResources().getAssets();

                var s = assets.open(filename);

                var bytes = new sbyte[s.available()];

                var cc = s.read(bytes, 0, bytes.Length);

                System.Console.WriteLine(new { cc });

                var DIRECTORY = android.os.Environment.DIRECTORY_DOWNLOADS;

                var dir = android.os.Environment.getExternalStoragePublicDirectory(DIRECTORY).getAbsolutePath();
                //var dir = android.os.Environment.getDownloadCacheDirectory().getAbsolutePath();

                var apk = dir + "/AndroidListApplicationsUpdate.apk";

                System.Console.WriteLine(new { apk });


                //         Caused by: java.io.FileNotFoundException: /cache/AndroidListApplicationsUpdate.apk: open failed: EACCES (Permission denied)
                //at libcore.io.IoBridge.open(IoBridge.java:416)
                //at java.io.RandomAccessFile.<init>(RandomAccessFile.java:118)
                //at java.io.RandomAccessFile.<init>(RandomAccessFile.java:150)
                //at ScriptCoreLibJava.BCLImplementation.System.IO.__File.WriteAllBytes(__File.java:103)
                //... 18 more

                System.IO.File.WriteAllBytes(apk, (byte[])(object)bytes);

                Intent intent = new Intent(Intent.ACTION_VIEW);
                intent.setDataAndType(android.net.Uri.fromFile(new File(apk)), "application/vnd.android.package-archive");
                intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK); // without this flag android returned a intent error!

                context.startActivity(intent);
            }
            catch
            {
                throw;
            }
        }
    }

    static class X
    {
        public static IEnumerable<ResolveInfo> queryIntentActivitiesEnumerable(this  PackageManager pm, Intent mainIntent, int arg1 = 0)
        {

            var pkgAppsList = pm.queryIntentActivities(mainIntent, 0);

            //for (int i = 0; i < pkgAppsList.size(); i++)
            //{
            //    yield return (ResolveInfo)pkgAppsList.get(i);
            //}

            return Enumerable.Range(0, pkgAppsList.size()).Select(i => (ResolveInfo)pkgAppsList.get(i));

        }
    }
}
