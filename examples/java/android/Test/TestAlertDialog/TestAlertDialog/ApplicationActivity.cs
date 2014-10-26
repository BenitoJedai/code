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

namespace TestAlertDialog.Activities
{
    public class ApplicationActivity : Activity
    {



        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // cmd /K c:\util\android-sdk-windows\platform-tools\adb.exe logcat
            // Camera PTP

            // http://developer.android.com/guide/topics/ui/notifiers/notifications.html

            base.onCreate(savedInstanceState);

            ScrollView sv = new ScrollView(this);

            LinearLayout ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);


            Button b = new Button(this);

            b.setText("AlertDialog! " + new { id = Thread.currentThread().getId() });
            int counter = 0;

            // ScriptCoreLib.Ultra ?
            b.AtClick(
                delegate
            {
                counter++;

                // http://www.tomswebdesign.net/Articles/Android/number-pad-input-class.html
                // https://android.googlesource.com/platform/frameworks/base/+/b896b9f/packages/Keyguard/src/com/android/keyguard/KeyguardSimPinView.java
                // http://xmlstackoverflow.blogspot.com/2014/07/how-to-use-alertdialog-to-prompt-for-pin.html
                // http://incidencias-ctt.administracionelectronica.gob.es/websvn/filedetails.php?repname=clienteafirma&path=%2Fproject%2Fafirma-mobile%2Fafirma-android%2Ftrunk%2Fafirma-ui-android%2Fsrc%2Fes%2Fgob%2Fafirma%2Fandroid%2Fgui%2FPinDialog.java&peg=4040

                // X:\jsc.svn\examples\java\android\forms\FormsMessageBox\FormsMessageBox\ApplicationActivity.cs
                // X:\jsc.svn\examples\java\android\Test\TestAlertDialog\TestAlertDialog\ApplicationActivity.cs
                AlertDialog alertDialog = new AlertDialog.Builder(this).create();

                alertDialog.setTitle("Reset...");
                alertDialog.setMessage("Are you sure?");
                alertDialog.setButton("OK",
                    new xOnClickListener
                {
                    yield = delegate
                    {
                        b.setText("clicked! " + new { id = Thread.currentThread().getId() });
                    }
                }

                    );

                // skip icons?
                //alertDialog.setIcon(android.R.drawable.star_off);

                // can we do async yet?
                alertDialog.show();


            }
            );

            ll.addView(b);

            this.setContentView(sv);

            // X:\jsc.svn\examples\java\android\HelloOpenGLES20Activity\HelloOpenGLES20Activity\ScriptCoreLib.Android\Shader.cs

            // Error	1	'TestAlertDialog.Activities.ApplicationActivity' does not contain a definition for 'ShowLongToast' and no extension method 'ShowLongToast' accepting a first argument of type 'TestAlertDialog.Activities.ApplicationActivity' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\java\android\TestAlertDialog\TestAlertDialog\ApplicationActivity.cs	80	18	TestAlertDialog
            //this.ShowLongToast("http://jsc-solutions.net");
            //this.ShowToast("http://jsc-solutions.net");


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
