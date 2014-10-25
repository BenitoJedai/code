using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;
using android.app;
using android.content;
using android.view;
using android.widget;
using ScriptCoreLib.Android.Extensions;

namespace TestPINDialog
{
    // X:\jsc.svn\examples\javascript\android\Test\TestPINLayoutDialog\TestPINLayoutDialog\ApplicationActivity.cs
    // will jsc pick em up here?
    // http://developer.android.com/guide/topics/manifest/uses-sdk-element.html
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    public class ApplicationWebService
    {
        /// <summary>
        /// The static content defined in the HTML file will be update to the dynamic content once application is running.
        /// </summary>
        public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        //public async Task<string> WebMethod2()
        public Task<string> WebMethod2()
        {
            // http://stackoverflow.com/questions/25003121/how-to-use-alertdialog-to-prompt-for-pin
            // X:\jsc.svn\examples\java\android\Test\TestAlertDialog\TestAlertDialog\ApplicationActivity.cs

            // https://android.googlesource.com/platform/frameworks/base/+/b896b9f/packages/Keyguard/src/com/android/keyguard/KeyguardSimPinView.java
            // http://seek-for-android.googlecode.com/svn-history/r172/trunk/applications/SecureFileManager/SecurityFileManager/src/org/openintents/filemanager/FileManagerActivity.java
            // https://github.com/Paldom/PinDialog

            var c = (Activity)ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;

            // #5 java.lang.RuntimeException: Can't create handler inside thread that has not called Looper.prepare()

            var a = new AutoResetEvent(false);


            c.runOnUiThread(
                delegate
            {

                //// the context. lets find it
                //var alertDialogBuilder = new AlertDialog.Builder(c);


                //LayoutInflater inflater = LayoutInflater.from(c);

                // https://github.com/chinloong/Android-PinView/blob/master/res/layout/activity_pin_entry_view.xml
                // http://lifehacker.com/three-ways-to-improve-your-androids-lock-screen-securi-1293317441

                var alertDialog = new AlertDialog.Builder(c);

                alertDialog.setTitle("Authentication");
                alertDialog.setMessage("PIN1");



                var xll = new LinearLayout(c);
                xll.setOrientation(LinearLayout.VERTICAL);

                var xt = new EditText(c);

                //http://stackoverflow.com/questions/6443286/type-number-variation-password-not-present-in-inputtype-class
                // https://groups.google.com/forum/#!topic/android-developers/UZuZjEbAnLE


                // http://kmansoft.com/2011/02/27/an-edittext-for-entering-ip-addresses/
                xt.setInputType(

                    android.text.InputType.TYPE_CLASS_NUMBER |
                    android.text.InputType.TYPE_NUMBER_VARIATION_PASSWORD);
                xt.setTransformationMethod(android.text.method.PasswordTransformationMethod.getInstance());
                xll.addView(xt);


                alertDialog.setPositiveButton("OK",
               new xOnClickListener
                {
                    yield = delegate
                    {
                        // I/System.Console(23890): OK {{ ManagedThreadId = 1 }}
                        Console.WriteLine(
                            "OK " +
                 new { Thread.CurrentThread.ManagedThreadId }

                            );

                        a.Set();
                    }
                }

               );

                //{
                //    var xb = new Button(this);
                //    xb.setText("1");
                //    xll.addView(xb);
                //}

                //{
                //    var xb = new Button(this);
                //    xb.setText("2");
                //    xll.addView(xb);
                //}

                //{
                //    var xb = new Button(this);
                //    xb.setText("3");
                //    xll.addView(xb);
                //}



                alertDialog.setView(xll);


                // skip icons?
                //alertDialog.setIcon(android.R.drawable.star_off);

                // can we do async yet?
                alertDialog.create().show();

            }
                );


            a.WaitOne();

            // report service thread
            return new { Thread.CurrentThread.ManagedThreadId }.ToString().AsResult();
        }

    }

    class xOnClickListener : DialogInterface_OnClickListener
    {
        public Action<DialogInterface, int> yield;

        public void onClick(DialogInterface arg0, int arg1)
        {
            yield(arg0, arg1);
        }
    }
}
