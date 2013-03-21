using android.content;
using android.content.pm;
using android.graphics;
using android.graphics.drawable;
using java.io;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace com.abstractatech.appmanager
{

    public delegate void yield_ACTION_MAIN(
        string packageName,
        string name,
        string icon_base64 = "",

        string label = ""
    );

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {


        public void queryIntentActivities(
            yield_ACTION_MAIN yield,
            // jsc please start supporting int :)
            //int take = 10,
            string skip = "0",
            string take = "10",
            Action yield_done = null)
        {
            // http://stackoverflow.com/questions/2695746/how-to-get-a-list-of-installed-android-applications-and-pick-one-to-run
            // https://play.google.com/store/apps/details?id=com.flopcode.android.inspector

            var mainIntent = new Intent(Intent.ACTION_MAIN, null);

            mainIntent.addCategory(Intent.CATEGORY_LAUNCHER);

            var context = ThreadLocalContextReference.CurrentContext;

            var pm = context.getPackageManager();


            //            Caused by: java.net.SocketException: Broken pipe
            //       at org.apache.harmony.luni.platform.OSNetworkSystem.write(Native Method)
            //       at dalvik.system.BlockGuard$WrappedNetworkSystem.write(BlockGuard.java:284)
            //       at org.apache.harmo
            //ny.luni.net.PlainSocketImpl.write(PlainSocketImpl.java:472)
            //       at org.apache.harmony.luni.net.SocketOutputStream.write(SocketOutputStream.java:57)

            var pkgAppsList = pm.queryIntentActivitiesEnumerable(mainIntent)
                .OrderBy(k => k.activityInfo.packageName)
                // do we have skip yet?

 //Implementation not found for type import :
                //type: System.Linq.Enumerable
                //method: System.Collections.Generic.IEnumerable`1[android.content.pm.ResolveInfo] Skip[ResolveInfo](System.Collections.Generic.IEnumerable`1[android.content.pm.ResolveInfo], Int32)
                //Did you forget to add the [Script] attribute?
                //Please double check the signature!

                .Skip(int.Parse(skip))
                .Take(int.Parse(take))
                .WithEach(
                r =>
                {
                    // http://stackoverflow.com/questions/6344694/get-foreground-application-icon-convert-to-base64

                    var label = (string)(object)pm.getApplicationLabel(r.activityInfo.applicationInfo);

                    var icon_base64 = "";

                    try
                    {
                        var icon = pm.getApplicationIcon(r.activityInfo.applicationInfo);

                        if (icon != null)
                        {

                            ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
                            // bitmap.compress(CompressFormat.PNG, 0, outputStream); 

                            BitmapDrawable bitDw = ((BitmapDrawable)icon);
                            Bitmap bitmap = bitDw.getBitmap();
                            ByteArrayOutputStream stream = new ByteArrayOutputStream();
                            bitmap.compress(Bitmap.CompressFormat.PNG, 100, stream);
                            var bitmapByte = (byte[])(object)stream.toByteArray();

                            icon_base64 = Convert.ToBase64String(bitmapByte);

                            //bitmapByte = Base64.encode(bitmapByte,Base64.DEFAULT);
                            //System.out.println("..length of image..."+bitmapByte.length);
                        }
                    }
                    catch
                    {

                    }


                    yield(
                        r.activityInfo.applicationInfo.packageName,
                        r.activityInfo.name,

                        icon_base64: icon_base64,
                        label: label
                    );
                }
            );

            if (yield_done != null)
                yield_done();
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
