using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using android.content;
using android.app;
using android.view;


namespace TryHideActionbarExperiment
{
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
            try
            {
                var i = new StaticInvoke();

                // Send it back to the caller.
                y("done!");
            }
            catch (Exception ex)
            {
                y("error: " + ex.Message);
            }
        }



    }

    class StaticInvoke
    {
        class f : java.lang.Runnable
        {
            public Action y;

            public void run()
            {
                y();
            }
        }
        static StaticInvoke()
        {
            Console.WriteLine("StaticInvoke");

            //  Exception Ljava/lang/RuntimeException; thrown while initializing LTryHideActionbarExperiment/StaticInvoke;
            try
            {


                // https://groups.google.com/forum/?fromgroups=#!topic/android-developers/suLMCWiG0D8
                var c = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;



                (c as Activity).With(
                    a =>
                    {
                        //                 Caused by: android.view.ViewRootImpl$CalledFromWrongThreadException: Only the original thread that created a view hierarchy can touch its views.
                        //at android.view.ViewRootImpl.checkThread(ViewRootImpl.java:4746)
                        //at android.view.ViewRootImpl.recomputeViewAttributes(ViewRootImpl.java:2610)
                        //at android.view.View.setSystemUiVisibility(View.java:16016)
                        //at TryHideActionbarExperiment.StaticInvoke.__cctor_b__0(StaticInvoke.java:63)
                        //at TryHideActionbarExperiment.StaticInvoke._1__cctor_public_ldftn_0024(StaticInvoke.java:68)

                        a.runOnUiThread(
                            new f
                            {
                                y = delegate
                                {

                                    try
                                    {

                                        //                                        V/PhoneStatusBar( 5910): setLightsOn(true)
                                        //W/InputEventReceiver( 5815): Attempted to finish an input event but the input event receiver has already been disposed.

                                        // a.getWindow().getDecorView().setSystemUiVisibility(
                                        //View.SYSTEM_UI_FLAG_HIDE_NAVIGATION | View.SYSTEM_UI_FLAG_LOW_PROFILE);


                                        c.ShowToast("http://my.jsc-solutions.net");

                                        //error: { Message = requestFeature() must be called before adding content, StackTrace = android.util.AndroidRuntimeException: requestFeature() must be called before adding content
                                        //       at com.android.internal.policy.impl.PhoneWindow.requestFeature(PhoneWindow.java:229)
                                        //       at ScriptCoreLib.Android.MyExtensions.ToFullscreen(MyExtensions.java:81)

                                        //a.ToFullscreen();

                                        // go full screen
                                        // http://stackoverflow.com/questions/9023023/set-full-screen-out-oncreate
                                        WindowManager_LayoutParams attrs = a.getWindow().getAttributes();
                                        attrs.flags |= WindowManager_LayoutParams.FLAG_FULLSCREEN;
                                        a.getWindow().setAttributes(attrs);

                                        MyExtensions.HIDE_DELAY_MILLIS = 700;
                                        a.TryHideActionbar(a.getWindow().getDecorView());


                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("error: " + new { ex.Message, ex.StackTrace });
                                    }
                                }
                            }
                        );


                    }
                );


                //         error: { Message = Can't create handler inside thread that has not called Looper.prepare(), StackTrace = java.lang.RuntimeException: Can't create handler inside thread that has not called Looper.prepare()
                //at android.os.Handler.<init>(Handler.java:197)
                //at android.os.Handler.<init>(Handler.java:111)
                //at android.widget.Toast$TN.<init>(Toast.java:324)
                //at android.widget.Toast.<init>(Toast.java:91)
                //at android.widget.Toast.makeText(Toast.java:238)
                //at ScriptCoreLib.Android.MyExtensions.ShowToast(MyExtensions.java:54)
                //at TryHideActionbarExperiment.StaticInvoke.<clinit>(StaticInvoke.java:30)

            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + new { ex.Message, ex.StackTrace });
            }
        }
    }
}
