using android.content;
using ScriptCoreLib.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace foo.bar
{
    [IntentFilter(Action = android.nfc.NfcAdapter.ACTION_NDEF_DISCOVERED)]
    [IntentFilter(Action = android.nfc.NfcAdapter.ACTION_TAG_DISCOVERED)]
    [IntentFilter(Action = android.nfc.NfcAdapter.ACTION_TECH_DISCOVERED)]
    //[IntentFilterData(scheme = "package")]
    public class AtDiscovered : BroadcastReceiver
    {
        // r(20814): /data/local/tmp/AndroidNFCBroadcastReceiver.Activities-debug.apk (at Binary XML file line #16): 
        // <receiver> does not have valid android:name
        // Failure [INSTALL_PARSE_FAILED_MANIFEST_MALFORMED]
        // http://stackoverflow.com/questions/16645632/programmatic-vs-static-broadcast-recievers-in-android

        // http://stackoverflow.com/questions/4853622/android-nfc-tag-received-with-broadcastreceiver
        // http://stackoverflow.com/questions/6829655/nfc-broadcast-problem
        // You can't capture those intents with a BroadcastReceiver, because only Activities 
        // can receive NFC intents. You can find more information about it in the NFC guide.
        public override void onReceive(Context arg0, Intent arg1)
        {
            var context = ThreadLocalContextReference.CurrentContext;

            var action = arg1.getAction();


            Console.WriteLine("AtDiscovered " + new { action });
        }
    }
}
