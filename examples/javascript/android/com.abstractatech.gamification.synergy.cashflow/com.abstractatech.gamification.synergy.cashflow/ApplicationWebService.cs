using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace com.abstractatech.gamification.synergy.cashflow
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
	[DesignerCategory("code")]
    public sealed class ApplicationWebService : Component
    {
        public Task<string> poll_onnfc(string last_id, Action<XElement> yield)
        {
#if DEBUG
            Thread.Sleep(500);

            return Task.FromResult(last_id);
#else

            var c = new TaskCompletionSource<string>();


            //I/System.Console( 6683): enter poll_onnfc { last_id = 0 }
            //D/NfcDispatcher(  804): dispatch tag: TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.Ndef] message: NdefMessage [NdefRecord tnf=4 type=70696C65742E65653A656B616172743A32 payload=66195F26063133303130385904202020205F28033233335F2701316E1B5A13333038363439303039303030333032313336315304FDCCD727, NdefRecord tnf=1 type=536967 payload=01020080B489DEDA8C2271386B7962250063A7C7C8612C3D58C8CD44D674F9D1615E80C72D961F8AC822C3188D48EFC7DA9DA3FF5C306E1EF54E0610F66D1C891CC59428A27CAA4211D4040527CF9BCD16F20E0B3116966AFC2390B7EF30CCC877B8532281CA3CBE286D295AECEA4447FD62874872A46099D6CEED99ED6766B829FD3FDF800025687474703A2F2F70696C65742E65652F6372742F33303836343930302D303030312E637274]
            //I/System.Console( 6683):
            //I/System.Console( 6683):
            //I/System.Console( 6683): AtPause
            //D/NfcDispatcher(  804): Set Foreground Dispatch
            //I/ActivityManager(  510): START u0 {act=android.nfc.action.TECH_DISCOVERED cmp=com.abstractatech.gamification.synergy.cashflow/.ApplicationWebServiceActivity (has extras)} from pid -1
            //W/ActivityManager(  510): startActivity called from non-Activity context; forcing Intent.FLAG_ACTIVITY_NEW_TASK for: Intent { act=android.nfc.action.TECH_DISCOVERED cmp=com.abstractatech.gamification.synergy.cashflow/.ApplicationWebServiceActivity (has extras) }
            //I/System.Console( 6683): Activity reactivated?
            //I/System.Console( 6683): Server service shutdown... { port = 16253 }
            //I/NfcDispatcher(  804): matched TECH override
            //I/System.Console( 6683): ReuseAddress... { localaddr = 0.0.0.0 }
            //I/System.Console( 6683): SendVisitMeAt
            //I/System.Console( 6683): { Preview = assets/com.abstractatech.gamification.synergy.cashflow/Preview.png, PreviewExists = true, PreviewExistsX = false }
            //V/PhoneStatusBar(  657): setLightsOn(true)
            //I/System.Console( 6683): { message = <string reason="" c="1" preview="" n="com.abstractatech.gamification.synergy.cashflow">Visit me at 192.168.1.102:4052</string> }
            //W/AwContents( 6683): nativeOnDraw failed; clearing to background color.
            //I/ActivityManager(  510): Displayed com.abstractatech.gamification.synergy.cashflow/.ApplicationWebServiceActivity: +155ms
            //W/AwContents( 6683): nativeOnDraw failed; clearing to background color.


            AndroidNFCEvents.ApplicationWebService_poll_onnfc.poll_onnfc(
                last_id, yield, c.SetResult
            );

            return c.Task;
#endif
        }

    }
}
