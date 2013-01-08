using android.app;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;
using android.widget;

namespace CallSetContentView
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
            // Send it back to the caller.
            y(e);
        }

        static __InitializeAndroidActivity __crazy_workaround;

        public void Hander(WebServiceHandler h)
        {
            if (__crazy_workaround == null)
            {
                Console.WriteLine("__crazy_workaround");
                __crazy_workaround = new __InitializeAndroidActivity();
            }
        }

    }
    class __InitializeAndroidActivity
    {
        static __InitializeAndroidActivity()
        {
            Console.WriteLine("StaticInvoke");

            //  Exception Ljava/lang/RuntimeException; thrown while initializing LTryHideActionbarExperiment/StaticInvoke;
            try
            {


                // https://groups.google.com/forum/?fromgroups=#!topic/android-developers/suLMCWiG0D8
                var c = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;


                (ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext as Activity).runOnUiThread(
                    a =>
                    {
                        // http://stackoverflow.com/questions/4451641/change-android-layout-programatically

                        var sv = new ScrollView(a);
                        var ll = new LinearLayout(a);
                        //ll.setOrientation(LinearLayout.VERTICAL);
                        sv.addView(ll);

                        var b = new Button(a).AttachTo(ll);



                        b.WithText("before AtClick");
                        b.AtClick(
                            v =>
                            {
                                b.setText("AtClick");
                            }
                        );

                        var b2 = new Button(a);
                        b2.setText("The other button!");
                        ll.addView(b2);

                        a.setContentView(sv);
                    }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + new { ex.Message, ex.StackTrace });
            }
        }
    }

}
