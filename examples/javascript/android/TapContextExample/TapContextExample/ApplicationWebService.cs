using android.app;
using android.content;
using com.tapcontext;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

// runOnUiThread has the wrong namespace?
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;

namespace TapContextExample
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    [ApplicationMetaData(name = "com.tapcontext.DEVELOPERKEY", value = "waa7vp9nz7")]
    public sealed class ApplicationWebService
    {
        static TapContextActivity ref0;


        // what does internal mean? jsc are you tracking csharp comments yet?
        public void DisplayingAnInterstitialAd()
        {
            // https://www.tapcontext.com/dashboard

            (ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext as Activity).runOnUiThread(
                (Activity app) =>
                {
                    // http://forums.makingmoneywithandroid.com/advertising-networks/1677-anyone-using-tapcontext-3.html
                    // is this SDK safe to use?

                    var sdk = new TapContextSDK(app);

                    sdk.initialize();
                    sdk.showAd();


                    //                    E/AndroidRuntime( 4738): FATAL EXCEPTION: main
                    //E/AndroidRuntime( 4738): android.content.ActivityNotFoundException: Unable to find explicit activity class {TapContextExample.Activities/com.tapcontext.TapContextActivity}; have you declared this activity in your AndroidManifest.xml?
                    //E/AndroidRuntime( 4738):        at android.app.Instrumentation.checkStartActivityResult(Instrumentation.java:1405)
                    //E/AndroidRuntime( 4738):        at android.app.Instrumentation.execStartActivity(Instrumentation.java:1379)
                    //E/AndroidRuntime( 4738):        at android.app.ContextImpl.startActivity(ContextImpl.java:727)
                    //E/AndroidRuntime( 4738):        at android.content.ContextWrapper.startActivity(ContextWrapper.java:258)
                    //E/AndroidRuntime( 4738):        at com.tapcontext.TapContextSDK$1.run(TapContextSDK.java:78)
                    //E/AndroidRuntime( 4738):        at android.os.Handler.handleCallback(Handler.java:587)
                    //E/AndroidRuntime( 4738):        at android.os.Handler.dispatchMessage(Handler.java:92)
                    //E/AndroidRuntime( 4738):        at android.os.Looper.loop(Looper.java:123)
                    //E/AndroidRuntime( 4738):        at android.app.ActivityThread.main(ActivityThread.java:3687)
                    //E/AndroidRuntime( 4738):        at java.lang.reflect.Method.invokeNative(Native Method)
                    //E/AndroidRuntime( 4738):        at java.lang.reflect.Method.invoke(Method.java:507)
                    //E/AndroidRuntime( 4738):        at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:842)
                    //E/AndroidRuntime( 4738):        at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:600)
                    //E/AndroidRuntime( 4738):        at dalvik.system.NativeStart.main(Native Method)
                    //W/ActivityManager(  129):   Force finishing activity TapContextExample.Activities/.ApplicationWebServiceActivity
                    //E/        (  129): Dumpstate > /data/log/dumpstate_app_error
                    //I/dumpstate( 4941): begin


                    // E/TapContextExample.Activities$TapContextSDK( 4202): Failed to register. Status Code: 400



                    //                    I/TapContextSDK( 4202): Cannot post message to TapContext Servers (May Retry)
                    //I/TapContextSDK( 4202): java.net.UnknownHostException: analytics.tapcontext.com
                    //I/TapContextSDK( 4202):         at java.net.InetAddress.lookupHostByName(InetAddress.java:506)
                    //I/TapContextSDK( 4202):         at java.net.InetAddress.getAllByNameImpl(InetAddress.java:294)
                    //I/TapContextSDK( 4202):         at java.net.InetAddress.getAllByName(InetAddress.java:256)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.conn.DefaultClientConnectionOperator.openConnection(DefaultClientConnectionOperator.java:136)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.conn.AbstractPoolEntry.open(AbstractPoolEntry.java:164)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.conn.AbstractPooledConnAdapter.open(AbstractPooledConnAdapter.java:119)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.client.DefaultRequestDirector.execute(DefaultRequestDirector.java:359)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.client.AbstractHttpClient.execute(AbstractHttpClient.java:555)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.client.AbstractHttpClient.execute(AbstractHttpClient.java:487)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.client.AbstractHttpClient.execute(AbstractHttpClient.java:465)
                    //I/TapContextSDK( 4202):         at com.tapcontext.analytics.tcmetrics.HttpPoster.postHttpRequest(HttpPoster.java:86)
                    //I/TapContextSDK( 4202):         at com.tapcontext.analytics.tcmetrics.HttpPoster.postData(HttpPoster.java:67)
                    //I/TapContextSDK( 4202):         at com.tapcontext.analytics.tcmetrics.AnalyticsMessages$Worker$AnalyticsMessageHandler.sendData(AnalyticsMessages.java:310)
                    //I/TapContextSDK( 4202):         at com.tapcontext.analytics.tcmetrics.AnalyticsMessages$Worker$AnalyticsMessageHandler.sendAllData(AnalyticsMessages.java:299)
                    //I/TapContextSDK( 4202):         at com.tapcontext.analytics.tcmetrics.AnalyticsMessages$Worker$AnalyticsMessageHandler.handleMessage(AnalyticsMessages.java:251)
                    //I/TapContextSDK( 4202):         at android.os.Handler.dispatchMessage(Handler.java:99)
                    //I/TapContextSDK( 4202):         at android.os.Looper.loop(Looper.java:123)
                    //I/TapContextSDK( 4202):         at com.tapcontext.analytics.tcmetrics.AnalyticsMessages$Worker$1.run(AnalyticsMessages.java:187)
                    //I/TapContextSDK( 4202): Cannot post message to TapContext Servers (May Retry)
                    //I/TapContextSDK( 4202): java.net.UnknownHostException: analytics.tapcontext.com
                    //I/TapContextSDK( 4202):         at java.net.InetAddress.lookupHostByName(InetAddress.java:497)
                    //I/TapContextSDK( 4202):         at java.net.InetAddress.getAllByNameImpl(InetAddress.java:294)
                    //I/TapContextSDK( 4202):         at java.net.InetAddress.getAllByName(InetAddress.java:256)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.conn.DefaultClientConnectionOperator.openConnection(DefaultClientConnectionOperator.java:136)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.conn.AbstractPoolEntry.open(AbstractPoolEntry.java:164)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.conn.AbstractPooledConnAdapter.open(AbstractPooledConnAdapter.java:119)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.client.DefaultRequestDirector.execute(DefaultRequestDirector.java:359)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.client.AbstractHttpClient.execute(AbstractHttpClient.java:555)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.client.AbstractHttpClient.execute(AbstractHttpClient.java:487)
                    //I/TapContextSDK( 4202):         at org.apache.http.impl.client.AbstractHttpClient.execute(AbstractHttpClient.java:465)
                    //I/TapContextSDK( 4202):         at com.tapcontext.analytics.tcmetrics.HttpPoster.postHttpRequest(HttpPoster.java:86)
                    //I/TapContextSDK( 4202):         at com.tapcontext.analytics.tcmetrics.HttpPoster.postData(HttpPoster.java:71)
                    //I/TapContextSDK( 4202):         at com.tapcontext.analytics.tcmetrics.AnalyticsMessages$Worker$AnalyticsMessageHandler.sendData(AnalyticsMessages.java:310)
                    //I/TapContextSDK( 4202):         at com.tapcontext.analytics.tcmetrics.AnalyticsMessages$Worker$AnalyticsMessageHandler.sendAllData(AnalyticsMessages.java:299)
                    //I/TapContextSDK( 4202):         at com.tapcontext.analytics.tcmetrics.AnalyticsMessages$Worker$AnalyticsMessageHandler.handleMessage(AnalyticsMessages.java:251)
                    //I/TapContextSDK( 4202):         at android.os.Handler.dispatchMessage(Handler.java:99)
                    //I/TapContextSDK( 4202):         at android.os.Looper.loop(Looper.java:123)
                    //I/TapContextSDK( 4202):         at com.tapcontext.analytics.tcmetrics.AnalyticsMessages$Worker$1.run(AnalyticsMessages.java:187)
                }
            );

            //I/System.Console( 4009): #4 POST /xml?WebMethod=06000002 HTTP/1.1
            //E/dalvikvm( 4009): Could not find class 'android.app.Notification$Builder', referenced from method com.tapcontext.TapContextSDK.showNotification
            //W/dalvikvm( 4009): VFY: unable to resolve new-instance 458 (Landroid/app/Notification$Builder;) in Lcom/tapcontext/TapContextSDK;
            //D/dalvikvm( 4009): VFY: replacing opcode 0x22 at 0x0245
            //D/dalvikvm( 4009): VFY: dead code 0x0247-02cb in Lcom/tapcontext/TapContextSDK;.showNotification (Landroid/content/Context;Landroid/content/Intent;Landroid/os/Bundle;Landroid/os/Bundle;)V
            //D/dalvikvm( 4009): VFY: dead code 0x033e-0368 in Lcom/tapcontext/TapContextSDK;.showNotification (Landroid/content/Context;Landroid/content/Intent;Landroid/os/Bundle;Landroid/os/Bundle;)V
            //I/ApplicationPackageManager( 4009): cscCountry is not German : SEB
            //D/dalvikvm( 3218): GC_EXPLICIT freed 3K, 42% free 4503K/7751K, external 1596K/2108K, paused 177ms
            //D/dalvikvm( 4009): GC_CONCURRENT freed 870K, 55% free 3057K/6727K, external 1973K/2108K, paused 4ms+3ms
            //W/dalvikvm( 4009): Exception Ljava/lang/RuntimeException; thrown while initializing Landroid/os/AsyncTask;
            //I/System.Console( 4009): #4 POST /xml?WebMethod=06000002 HTTP/1.1 error:
            //I/System.Console( 4009): #4 java.lang.ExceptionInInitializerError
            //I/System.Console( 4009): #4 java.lang.ExceptionInInitializerError
            //I/System.Console( 4009):        at com.tapcontext.Registration.register(Registration.java:35)
            //I/System.Console( 4009):        at com.tapcontext.TapContextSDK.internalInitialize(TapContextSDK.java:898)
            //I/System.Console( 4009):        at com.tapcontext.TapContextSDK.initialize(TapContextSDK.java:860)
            //I/System.Console( 4009):        at TapContextExample.ApplicationWebService.DisplayingAnInterstitialAd(ApplicationWebService.java:29)
            //I/System.Console( 4009):        at TapContextExample.Global.Invoke(Global.java:175)

            //I/System.Console( 4009): Caused by: java.lang.RuntimeException: Can't create handler inside thread that has not called Looper.prepare()
            //I/System.Console( 4009):        at android.os.Handler.<init>(Handler.java:121)
            //I/System.Console( 4009):        at android.os.AsyncTask$InternalHandler.<init>(AsyncTask.java:421)
            //I/System.Console( 4009):        at android.os.AsyncTask$InternalHandler.<init>(AsyncTask.java:421)
            //I/System.Console( 4009):        at android.os.AsyncTask.<clinit>(AsyncTask.java:152)
            //I/System.Console( 4009):        ... 27 more



        }
    }
}
