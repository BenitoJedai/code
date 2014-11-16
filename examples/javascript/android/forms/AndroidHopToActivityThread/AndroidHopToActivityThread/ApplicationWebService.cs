using android.app;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Android.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AndroidHopToActivityThread
{

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    public class ApplicationWebService
    {
        //I/System.Console(13300): enter InternalURLDecode { Value = /xml/WebMethod2 }
        //D/dalvikvm(13300): GC_CONCURRENT freed 486K, 7% free 8118K/8672K, paused 2ms+1ms, total 30ms

        public async Task WebMethod2(string value)
        {
            Console.WriteLine("enter WebMethod2");
            // X:\jsc.svn\examples\javascript\android\forms\AndroidPINForm\AndroidPINForm\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\Test\TestHopToThreadPoolAwaitable\TestHopToThreadPoolAwaitable\Application.cs

            // why isnt jsc doing automatic ref?
            { var ref0 = default(global::ScriptCoreLib.Android.Windows.Forms.IAssemblyReferenceToken_Forms); }

            await default(HopToActivityAwaitable);

            // basically it wrks. is worth it?

            MessageBox.Show(new { value }.ToString());
            Console.WriteLine("exit WebMethod2");
        }

    }


    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/CryptoStream.cs
    // simple awaitable that allows for hopping to the thread pool
    struct HopToActivityAwaitable : INotifyCompletion
    {
        public HopToActivityAwaitable GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }
        public void OnCompleted(Action continuation)
        {

            Console.WriteLine("HopToActivityAwaitable OnCompleted");

            var c = (Activity)ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;

            c.runOnUiThread(
                delegate
                {
                    continuation();
                }
            );

        }
        public void GetResult() { }
    }
}
