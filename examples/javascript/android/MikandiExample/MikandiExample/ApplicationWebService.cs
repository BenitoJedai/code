using android.content;
using com.mikandi.android.kandibilling;
using com.reporo.android.ads;
using java.io;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace MikandiExample
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService
    {

        const int LOGIN_REQUEST = 2323;


        //     public sealed class KandiBillingActivity : Activity
        static com.mikandi.android.kandibilling.KandiBillingActivity ref0;
        static com.reporo.android.ads.InterstitialAdActivity ref1;

        class InterstitialAdListener : ReporoAdListener
        {
            public void onAdFailed(AdView value)
            {
                System.Console.WriteLine("onAdFailed");
            }

            public void onAdReceived(AdView value)
            {
                System.Console.WriteLine("onAdReceived");
            }

            public void onInterstitialFailed(InterstitialAd value)
            {
                System.Console.WriteLine("onInterstitialFailed");
            }


            public Action<InterstitialAd> InterstitialRecieved;

            public void onInterstitialRecieved(InterstitialAd value)
            {
                System.Console.WriteLine("onInterstitialRecieved");

                if (InterstitialRecieved != null)
                    InterstitialRecieved(value);
            }
        }

        public void InterstitialAd(string e = "", Action<string> y = null)
        {

            // D/Reporo SDK(23735): Got:gif-239888-http://netdna.reporo.net/resources/images/reporoads/smrc/650c22aeab97.gif

            //             Unable to load configuration.
            // Smart Zones: null - Interstitial Zone: null - Domain: null
            // Fetching interstitial ad
            //479): START u0 {cmp=MikandiExample.Activities/com.reporo.android.ads.InterstitialAdActivity (has extras)} from pid 22861
            // Error fetching interstitial ad
            // java.lang.Exception: Interstitial zone not configured.
            //    at com.reporo.android.ads.k.a(Unknown Source)
            //    at com.reporo.android.ads.g.run(Unknown Source)
            //    at java.lang.Thread.run(Thread.java:856)
            //61): onInterstitialFailed

            var context = ThreadLocalContextReference.CurrentContext as CoreAndroidWebServiceActivity;


            var inAd = new InterstitialAd(context);
            inAd.setListener(new InterstitialAdListener
            {

                InterstitialRecieved =
                    x =>
                    {
                        inAd.showAd(context);
                    }
            });
            inAd.fetchNewAd(); //NOTE: Reporo SDK: pre-fecth ad


            //            Error fetching interstitial ad
            //java.lang.Exception: Invalid or no ad retured.
            //   at com.reporo.android.ads.k.a(Unknown Source)
            //   at com.reporo.android.ads.k.a(Unknown Source)
            //   at com.reporo.android.ads.g.run(Unknown Source)

            //            Error fetching interstitial ad
            //java.lang.ClassCastException: org.json.JSONArray cannot be cast to org.json.JSONObject
            //   at com.reporo.android.ads.k.a(Unknown Source)
            //   at com.reporo.android.ads.g.run(Unknown Source)
            //   at java.lang.Thread.run(Thread.java:856)


            //            D/Reporo SDK(23735): Smart Zones: null - Interstitial Zone: 9079 - Domain: 767a-6e78-7161-2e76.reporo.net
            //D/Reporo SDK(23735): Fetching interstitial ad
            //I/ActivityManager(  479): START u0 {cmp=MikandiExample.Activities/com.reporo.android.ads.InterstitialAdActivity (has extras)} from pid 23735
            //D/Reporo SDK(23735): Creating Interstitial Ad
            //D/Reporo SDK(23735): Unable to display insterstitial Ad null

        }

        public void Login(string e, Action<string> y)
        {
            // http://m.mikandi.com/

            // do we need the activity instead?
            var context = ThreadLocalContextReference.CurrentContext as CoreAndroidWebServiceActivity;

            //            I/System.Console(19687): #6 android.content.ActivityNotFoundException: Unable to find explicit activity class {MikandiExample.Activities/com.mikandi.android.kandibilling.KandiBillingActivity}; have you declared this activity in your AndroidManifest.xml?
            //I/ActivityManager(  479): START u0 {act=com.mikandi.android.kandibilling.LOGIN cmp=MikandiExample.Activities/com.mikandi.android.kandibilling.KandiBillingActivity (has extras)} from pid 19687
            //I/System.Console(19687): #6 android.content.ActivityNotFoundException: Unable to find explicit activity class {MikandiExample.Activities/com.mikandi.android.kandibilling.KandiBillingActivity}; have you declared this activity in your AndroidManifest.xml?
            //I/System.Console(19687):        at android.app.Instrumentation.checkStartActivityResult(Instrumentation.java:1618)
            //I/System.Console(19687):        at android.app.Instrumentation.execStartActivity(Instrumentation.java:1417)
            //I/System.Console(19687):        at android.app.Activity.startActivityForResult(Activity.java:3370)
            //I/System.Console(19687):        at android.app.Activity.startActivityForResult(Activity.java:3331)
            //I/System.Console(19687):        at com.mikandi.android.kandibilling.KandiBillingClient.login(Unknown Source)
            //I/System.Console(19687):        at MikandiExample.ApplicationWebService.Login(ApplicationWebService.java:52)


            KandiBillingClient.init(context, AppID, Secret);




            string done = "pending";
            context.ActivityResult +=
                (int requestCode, int resultCode, Intent data) =>
                {
                    done = new { requestCode, resultCode }.ToString();
                    System.Console.WriteLine(new { done });

                    //                    I/KandiBillingClient(21172): API Call result: 200
                    //I/System.out(21172): RESULT: The user successfully logged in
                    //I/System.Console(22130): { done = { requestCode = 2323, resultCode = -1 } }

                    if (requestCode == LOGIN_REQUEST)
                    {
                        if (data != null)
                        {
                            LoginResult result = KandiBillingClient.getInstance().getLoginResult(data);
                            var isSuccessful = result.isSuccessful();

                            if (isSuccessful)
                            {
                                done = new { requestCode, resultCode, isSuccessful }.ToString();

                                //refreshPurchases(result.getPurchaseIds());
                            }
                        }
                    }

                };

            //            com.mikandi.android.kandibilling.KandiBillingException: 496 App Not Found
            //   at com.mikandi.android.kandibilling.KandiBillingClient$ApiCall$1.run(Unknown Source)
            //Caused by: Failure. Error 496 from server: 496 App Not Found

            //            D/JSONReturnable(21172): Unable to parse RAW JSON
            //D/JSONReturnable(21172): java.lang.NullPointerException
            //D/JSONReturnable(21172):        at org.json.JSONTokener.nextCleanInternal(JSONTokener.java:116)
            //D/JSONReturnable(21172):        at org.json.JSONTokener.nextValue(JSONTokener.java:94)
            //D/JSONReturnable(21172):        at org.json.JSONObject.<init>(JSONObject.java:154)
            //D/JSONReturnable(21172):        at org.json.JSONObject.<init>(JSONObject.java:171)
            //D/JSONReturnable(21172):        at com.mikandi.android.v3.services.networking.JSONReturnable.preProcessData(JSONReturnable.java:67)
            //D/JSONReturnable(21172):        at com.mikandi.android.v3.services.networking.Returnable.run(Returnable.java:73)

            KandiBillingClient.getInstance().login(context, LOGIN_REQUEST);

            // Send it back to the caller.
            y(done);
        }






        public void Install(string filename = "assets/MikandiExample/mikandi-client-3.7.158.apk")
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
}
