using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using android.app;
using android.widget;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Form))]
    internal class __Form : __ContainerControl
    {
        // "Microsoft is not planning to open source the client side .NET stack, which means certain pieces like the Windows Presentation Foundation (WPF) and Windows Forms won't be going open source, Somasegar confirmed."
        // http://tech.slashdot.org/story/14/11/15/1442201/new-trial-brings-skype-to-some-browsers

        // tested by
        // X:\jsc.svn\examples\javascript\forms\FormsConfiguredAtWebService\FormsConfiguredAtWebService\ApplicationWebService.cs
        // X:\jsc.svn\examples\java\android\forms\FormsMessageBox\FormsMessageBox\ApplicationActivity.cs
        // X:\jsc.svn\examples\javascript\android\AndroidPINForm\AndroidPINForm\ApplicationWebService.cs


        public override string Text { get; set; }

        public event EventHandler Load;

        public Size ClientSize { get; set; }

        //protected override void Dispose(bool disposing)
        public override void Dispose(bool disposing)
        {

            // ?
        }




        //public ScrollView InternalScrollView;
        public LinearLayout InternalLinearLayout;

        //public override android.view.View InternalGetElement()
        //{
        //    // invalid
        //    return InternalScrollView;
        //}

        public override android.view.ViewGroup InternalGetContainer()
        {
            return InternalLinearLayout;
        }

        // called by?
        public override void InternalBeforeSetContext(android.content.Context c)
        {
            //InternalScrollView = new ScrollView(c);
            InternalLinearLayout = new LinearLayout(c);

            InternalLinearLayout.setOrientation(1);

            //InternalScrollView.addView(InternalLinearLayout);
        }

        //public Action InternalClose = delegate { };
        public Action InternalClose = null;
        public void Close()
        {
            if (InternalClose != null)
                InternalClose();
        }

        public DialogResult ShowDialog()
        {
            this.InternalSetContext(
                global::ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext
            );

            // X:\jsc.svn\examples\java\android\forms\FormsMessageBox\FormsMessageBox\Library\ApplicationControl.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201410/20141025
            // X:\jsc.svn\examples\java\android\Test\TestAlertDialog\TestAlertDialog\ApplicationActivity.cs

            var value = default(global::System.Windows.Forms.DialogResult);
            var alertDialog = new AlertDialog.Builder(ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext);

            alertDialog.setTitle(this.Text);



            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201410/20141026

            //E/AndroidRuntime( 1380): Caused by: java.lang.NoSuchMethodError: android.app.AlertDialog$Builder.setOnDismissListener
            //E/AndroidRuntime( 1380):        at ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms.__Form.ShowDialog(__Form.java:110)

            // ?
           // alertDialog.setOnDismissListener(
           //    new xDialogInterface_OnDismissListener()
           //);

            //alertDialog.setPositiveButton("OK",
            //        new xDialogInterface_OnClickListener
            //{
            //    yield = delegate
            //    {
            //        value = global::System.Windows.Forms.DialogResult.OK;
            //    }
            //}
            //    );

            alertDialog.setView(this.InternalGetContainer());

            // skip icons?
            //alertDialog.setIcon(android.R.drawable.star_off);
            var dialog = alertDialog.create();

            dialog.setOnDismissListener(
               new xDialogInterface_OnDismissListener()
            );

            dialog.show();

            // http://stackoverflow.com/questions/13974661/runonuithread-vs-looper-getmainlooper-post-in-android
            // http://developer.android.com/reference/android/os/Looper.html

            __dismissOnClose(dialog);

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

        private void __dismissOnClose(AlertDialog dialog)
        {
            this.InternalClose = delegate
            {
                dialog.dismiss();
            };
        }
    }
}
