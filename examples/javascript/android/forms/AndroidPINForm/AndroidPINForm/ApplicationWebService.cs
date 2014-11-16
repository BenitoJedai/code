using android.app;
using AndroidPINForm.Library;
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
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptCoreLib.Android.Extensions;
using System.Windows.Forms;

namespace AndroidPINForm
{
    // we do get the PIN pad, yet no halo.. add halo?
    // will it run on galaxy s? now it does.
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    public class ApplicationWebService
    {
        /// <summary>
        /// The static content defined in the HTML file will be update to the dynamic content once application is running.
        /// </summary>
        public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

        public Task<string> WebMethod2()
        {
            // why isnt jsc doing automatic ref?
            { var ref0 = default(global::ScriptCoreLib.Android.Windows.Forms.IAssemblyReferenceToken_Forms); }


            var value = "";

            var c = (Activity)ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;

            // #5 java.lang.RuntimeException: Can't create handler inside thread that has not called Looper.prepare()

            var a = new AutoResetEvent(false);

            c.runOnUiThread(
                delegate
                {
                    var f = new Form1();

                    // how does CLR behave? can we call ShowDialog from backgrond thread?
                    var r = f.ShowDialog();

                    value = f.textBox1.Text;

                    MessageBox.Show(new { value }.ToString());

                    a.Set();
                }
            );


            a.WaitOne();

            // report service thread
            return new { Thread.CurrentThread.ManagedThreadId, value }.ToString().AsResult();
        }




    }
}
