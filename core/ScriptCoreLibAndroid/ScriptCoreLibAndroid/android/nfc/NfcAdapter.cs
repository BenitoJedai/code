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
        // can we access it from NDK?

        // X:\jsc.svn\examples\javascript\android\forms\AndroidNFCEvents\AndroidNFCEvents\ApplicationWebService_poll_onnfc.cs

        public bool isEnabled() { return false; }

        public static NfcAdapter getDefaultAdapter(Context context) { return null; }
    }
}
