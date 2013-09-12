using android.app;
//using android.content.pm;
using android.view;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Android.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.WebService;

namespace com.abstractatech.gamification.imp
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {


        //#if Android
        //        static __InitializeAndroidActivity __crazy_workaround;

        //        public void Hander(WebServiceHandler h)
        //        {
        //            if (__crazy_workaround == null)
        //            {
        //                Console.WriteLine("__crazy_workaround");
        //                __crazy_workaround = new __InitializeAndroidActivity();
        //            }
        //        }


        //#endif


    }


    //class __InitializeAndroidActivity
    //{


    //    public static int HIDE_DELAY_MILLIS = 700;

    //    class HideLater : View.OnSystemUiVisibilityChangeListener, java.lang.Runnable
    //    {
    //        public Activity that;
    //        public View view;

    //        public void run()
    //        {

    //            // http://stackoverflow.com/questions/13280253/android-dimming-navigation-buttons
    //            that.getWindow().getDecorView().setSystemUiVisibility(
    //                //              View.SYSTEM_UI_FLAG_HIDE_NAVIGATION 
    //                //|
    //                View.SYSTEM_UI_FLAG_LOW_PROFILE
    //             );
    //        }

    //        public void onSystemUiVisibilityChange(int value)
    //        {
    //            view.postDelayed(
    //                this, HIDE_DELAY_MILLIS
    //            );
    //        }
    //    }

    //    public static void TryHideActionbar(Activity that, View view)
    //    {
    //        try
    //        {
    //            // http://mobile.dzone.com/articles/60-android-hacks-excerpt

    //            //Log.wtf("AndroidGLSpiralActivity", "TryHideActionbar");
    //            var h = new HideLater { that = that, view = view };
    //            view.setOnSystemUiVisibilityChangeListener(
    //               h
    //                );

    //            h.onSystemUiVisibilityChange(0);
    //            //Log.wtf("AndroidGLSpiralActivity", "TryHideActionbar done");
    //        }
    //        catch
    //        {
    //            //Log.wtf("AndroidGLSpiralActivity", "TryHideActionbar error");

    //            //throw;
    //        }
    //    }


    //    static __InitializeAndroidActivity()
    //    {
    //        Console.WriteLine("StaticInvoke");

    //        //  Exception Ljava/lang/RuntimeException; thrown while initializing LTryHideActionbarExperiment/StaticInvoke;
    //        try
    //        {


    //            // https://groups.google.com/forum/?fromgroups=#!topic/android-developers/suLMCWiG0D8
    //            var c = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;



    //            (c as Activity).With(
    //                a =>
    //                {
    //                    //                 Caused by: android.view.ViewRootImpl$CalledFromWrongThreadException: Only the original thread that created a view hierarchy can touch its views.
    //                    //at android.view.ViewRootImpl.checkThread(ViewRootImpl.java:4746)
    //                    //at android.view.ViewRootImpl.recomputeViewAttributes(ViewRootImpl.java:2610)
    //                    //at android.view.View.setSystemUiVisibility(View.java:16016)
    //                    //at TryHideActionbarExperiment.StaticInvoke.__cctor_b__0(StaticInvoke.java:63)
    //                    //at TryHideActionbarExperiment.StaticInvoke._1__cctor_public_ldftn_0024(StaticInvoke.java:68)

    //                    a.runOnUiThread(
    //                       delegate
    //                       {

    //                           try
    //                           {

    //                               //                                        V/PhoneStatusBar( 5910): setLightsOn(true)
    //                               //W/InputEventReceiver( 5815): Attempted to finish an input event but the input event receiver has already been disposed.

    //                               // a.getWindow().getDecorView().setSystemUiVisibility(
    //                               //View.SYSTEM_UI_FLAG_HIDE_NAVIGATION | View.SYSTEM_UI_FLAG_LOW_PROFILE);


    //                               //c.ShowToast("http://my.jsc-solutions.net");

    //                               //error: { Message = requestFeature() must be called before adding content, StackTrace = android.util.AndroidRuntimeException: requestFeature() must be called before adding content
    //                               //       at com.android.internal.policy.impl.PhoneWindow.requestFeature(PhoneWindow.java:229)
    //                               //       at ScriptCoreLib.Android.MyExtensions.ToFullscreen(MyExtensions.java:81)

    //                               //a.ToFullscreen();

    //                               // go full screen
    //                               // http://stackoverflow.com/questions/9023023/set-full-screen-out-oncreate
    //                               WindowManager_LayoutParams attrs = a.getWindow().getAttributes();
    //                               attrs.flags |= WindowManager_LayoutParams.FLAG_FULLSCREEN;
    //                               a.getWindow().setAttributes(attrs);


    //                               // cant have it jumping around, thats distracting!
    //                               //TryHideActionbar(a, a.getWindow().getDecorView());




    //                               a.setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);

    //                           }
    //                           catch (Exception ex)
    //                           {
    //                               Console.WriteLine("error: " + new { ex.Message, ex.StackTrace });
    //                           }
    //                       }
    //                    );


    //                }
    //            );


    //            //         error: { Message = Can't create handler inside thread that has not called Looper.prepare(), StackTrace = java.lang.RuntimeException: Can't create handler inside thread that has not called Looper.prepare()
    //            //at android.os.Handler.<init>(Handler.java:197)
    //            //at android.os.Handler.<init>(Handler.java:111)
    //            //at android.widget.Toast$TN.<init>(Toast.java:324)
    //            //at android.widget.Toast.<init>(Toast.java:91)
    //            //at android.widget.Toast.makeText(Toast.java:238)
    //            //at ScriptCoreLib.Android.MyExtensions.ShowToast(MyExtensions.java:54)
    //            //at TryHideActionbarExperiment.StaticInvoke.<clinit>(StaticInvoke.java:30)

    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine("error: " + new { ex.Message, ex.StackTrace });
    //        }
    //    }
    //}

}
