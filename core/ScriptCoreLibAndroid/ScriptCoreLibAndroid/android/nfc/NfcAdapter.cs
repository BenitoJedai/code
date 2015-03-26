using android.app;
using android.content;
using android.net;
using android.os;
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


        public  const int FLAG_READER_SKIP_NDEF_CHECK = 0x80;
        public  const string EXTRA_READER_PRESENCE_CHECK_DELAY = "presence";

        // can we access it from NDK?

        // X:\jsc.svn\examples\javascript\android\forms\AndroidNFCEvents\AndroidNFCEvents\ApplicationWebService_poll_onnfc.cs

        public bool isEnabled() { return false; }

        public static NfcAdapter getDefaultAdapter(Context context) { return null; }



        public void enableForegroundDispatch(Activity activity, PendingIntent intent,
            IntentFilter[] filters, string[][] techLists)
        { }


        public void disableForegroundDispatch(Activity activity)
        { }

        public void enableReaderMode(Activity activity, ReaderCallback callback, int flags,
            Bundle extras)
        {
        }

        [Script(IsNative = true)]
        public interface ReaderCallback
        {
             void onTagDiscovered(Tag tag);
        }

    }
}
