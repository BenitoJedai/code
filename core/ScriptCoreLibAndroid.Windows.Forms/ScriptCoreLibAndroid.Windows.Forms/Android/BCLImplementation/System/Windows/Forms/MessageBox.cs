using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.MessageBox))]
    internal class __MessageBox
    {
        public static global::System.Windows.Forms.DialogResult Show(string text)
        {
            return Show(
                text: text,
                // caption would be turnacated
                caption: "Message"
            );

        }

        public static global::System.Windows.Forms.DialogResult Show(string text, string caption)
        {
            // X:\jsc.svn\examples\java\android\forms\FormsMessageBox\FormsMessageBox\Library\ApplicationControl.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201410/20141025
            // X:\jsc.svn\examples\java\android\Test\TestAlertDialog\TestAlertDialog\ApplicationActivity.cs

            var value = default(global::System.Windows.Forms.DialogResult);
            var alertDialog = new AlertDialog.Builder(ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext);

            alertDialog.setTitle(caption);

            if (!string.IsNullOrEmpty(caption))
            {
                alertDialog.setMessage(text);
            }

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201410/20141026


            alertDialog.setPositiveButton("OK",
                    new xDialogInterface_OnClickListener
            {
                yield = delegate
                {
                    value = global::System.Windows.Forms.DialogResult.OK;
                }
            }
                );

            // skip icons?
            //alertDialog.setIcon(android.R.drawable.star_off);
            var dialog = alertDialog.create();

            dialog.setOnDismissListener(
                new xDialogInterface_OnDismissListener()
            );

            dialog.show();


            // http://stackoverflow.com/questions/13974661/runonuithread-vs-looper-getmainlooper-post-in-android
            // http://developer.android.com/reference/android/os/Looper.html

            try
            {
                // loop until we throw null
                android.os.Looper.loop();
            }
            catch
            {
            }

            return value;
        }



    }


    [Script]
    class xDialogInterface_OnDismissListener : DialogInterface_OnDismissListener
    {
        public Action<DialogInterface> yield;



        public void onDismiss(DialogInterface value)
        {
            //yield(value);

            throw null;
        }
    }

    [Script]
    public class xDialogInterface_OnClickListener : DialogInterface_OnClickListener
    {
        public Action<DialogInterface, int> yield;

        public void onClick(DialogInterface arg0, int arg1)
        {
            yield(arg0, arg1);
        }
    }
}
