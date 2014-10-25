using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.provider;
using android.view;
using android.webkit;
using android.widget;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLibJava.Extensions;

namespace TestPINLayoutDialog.Activities
{
    // targetSdkVersion
    // http://developer.android.com/guide/topics/manifest/uses-sdk-element.html
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    public class ApplicationActivity : Activity
    {



        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // cmd /K c:\util\android-sdk-windows\platform-tools\adb.exe logcat
            // Camera PTP

            // http://developer.android.com/guide/topics/ui/notifiers/notifications.html

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);


            var b = new Button(this);

            // jsc is doing the wrong thing here
            var SDK_INT = android.os.Build.VERSION.SDK_INT;

            b.setText("Notify! " + new { SDK_INT, android.os.Build.VERSION.SDK });
            int counter = 0;



            // http://stackoverflow.com/questions/12900795/how-to-get-a-pin-number-password-keyboard-in-android
            //var t = new EditText(this);
            //t.setInputType(android.text.InputType.TYPE_NUMBER_VARIATION_PASSWORD);
            //t.setTransformationMethod(android.text.method.PasswordTransformationMethod.getInstance());
            //ll.addView(t);

            // ScriptCoreLib.Ultra ?
            b.AtClick(
                delegate
            {
                counter++;

                // X:\jsc.svn\examples\javascript\android\Test\TestPINDialog\TestPINDialog\ApplicationWebService.cs

                var alertDialog = new AlertDialog.Builder(this);

                alertDialog.setTitle("Authentication");
                alertDialog.setMessage("PIN1");



                var xll = new LinearLayout(this);
                xll.setOrientation(LinearLayout.VERTICAL);

                var xt = new EditText(this);

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
                        b.setText("clicked! " + new { id = Thread.currentThread().getId() });
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

            ll.addView(b);

            this.setContentView(sv);

            // X:\jsc.svn\examples\java\android\HelloOpenGLES20Activity\HelloOpenGLES20Activity\ScriptCoreLib.Android\Shader.cs

            // Error	1	'TestPINLayoutDialog.Activities.ApplicationActivity' does not contain a definition for 'ShowLongToast' and no extension method 'ShowLongToast' accepting a first argument of type 'TestPINLayoutDialog.Activities.ApplicationActivity' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\java\android\TestPINLayoutDialog\TestPINLayoutDialog\ApplicationActivity.cs	80	18	TestPINLayoutDialog
            //this.ShowLongToast("http://jsc-solutions.net");


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
