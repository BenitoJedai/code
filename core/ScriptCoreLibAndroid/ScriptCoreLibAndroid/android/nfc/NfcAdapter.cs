using android.app;
using android.content;
using android.net;
using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.nfc
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/nfc/NfcAdapter.java

    [Script(IsNative = true)]
    public class NfcAdapter
    {
        // X:\jsc.svn\examples\javascript\android\AndroidNFCExperiment\AndroidNFCExperiment\ApplicationWebService.cs

        public static string ACTION_NDEF_DISCOVERED;
        public static string ACTION_TAG_DISCOVERED;
        public static string ACTION_TECH_DISCOVERED;

        // can we access it from NDK?

        // X:\jsc.svn\examples\javascript\android\forms\AndroidNFCEvents\AndroidNFCEvents\ApplicationWebService_poll_onnfc.cs

        public bool isEnabled() { return false; }

        public static NfcAdapter getDefaultAdapter(Context context) { return null; }



        public void enableForegroundDispatch(Activity activity, PendingIntent intent,
            IntentFilter[] filters, string[][] techLists)
        { }


        public void disableForegroundDispatch(Activity activity)
        { }

        
    }
}
