using ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using android.content;
using System.Threading;
using android.app;

namespace AndroidFormsActivity
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //button2.Text = "Clicked!";


            var value = MessageBox.Show("text", "caption");

            button2.Text = new { value }.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // http://stackoverflow.com/questions/2028697/dialogs-alertdialogs-how-to-block-execution-while-dialog-is-up-net-style
            // http://mindfiremobile.wordpress.com/2014/04/21/displaying-alert-dialog-in-android-using-xamarin/

            //button1.Text = "Clicked!";

            // X:\jsc.svn\examples\java\android\forms\FormsMessageBox\FormsMessageBox\ApplicationActivity.cs
            // X:\jsc.svn\examples\java\android\Test\TestAlertDialog\TestAlertDialog\ApplicationActivity.cs
            var alertDialog = new AlertDialog.Builder(ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext);

            alertDialog.setTitle("Reset...");
            alertDialog.setMessage("Are you sure?");

            alertDialog.setOnDismissListener(
                new xDialogInterface_OnDismissListener
                {
                    yield = delegate
                    {
                        throw null;
                    }
                }
            );

            alertDialog.setPositiveButton("OK",
                new xOnClickListener
            {
                yield = delegate
                {
                    button1.Text = "clicked! " + new
                    {


                        Thread.CurrentThread.ManagedThreadId
                    };



                }
            }

                );

            // skip icons?
            //alertDialog.setIcon(android.R.drawable.star_off);

            // can we do async yet?
            alertDialog.create().show();

            // http://stackoverflow.com/questions/13974661/runonuithread-vs-looper-getmainlooper-post-in-android

            //android.os.Looper.getMainLooper().loop();

            // http://developer.android.com/reference/android/os/Looper.html

            try
            {
                // loop until we throw null
                android.os.Looper.loop();
            }
            catch
            {

            }

            button1.Text = "clicked! after looper " + new
            {


                Thread.CurrentThread.ManagedThreadId
            };

            //alertDialog.create().show();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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

    class xDialogInterface_OnDismissListener : DialogInterface_OnDismissListener
    {
        public Action<DialogInterface> yield;

      

        public void onDismiss(DialogInterface value)
        {
            yield(value);
        }
    }
}
