using android.app;
using android.content;
using android.nfc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLib.Extensions;
using System.Xml.Linq;
using System.Diagnostics;

namespace AndroidNFCEvents
{
    public static class ApplicationWebService_poll_onnfc
    {
        // inspired by
        // X:\jsc.svn\examples\javascript\android\forms\ReinstallNotification\ReinstallNotification\AtInstall.cs

        public static List<string> History = new List<string>();

        public static void poll_onnfc(
         string last_id,
         Action<XElement> yield,
         Action<string> yield_last_id,
         int sync_SelectContentUpdates_timeout = 5000,
         int sync_SelectContentUpdates_waitmin = 10,
         int sync_SelectContentUpdates_waitrandom = 30
         )
        {
            Console.WriteLine("enter poll_onnfc " + new { last_id });

            if (last_id == "")
            {
                yield_last_id("" + ApplicationWebService_poll_onnfc.History.Count);
                return;
            }



            var int_last_id = int.Parse(last_id);

            var sw = new Stopwatch();
            sw.Start();

            var random = new Random();


            // will this compile?


            while (sw.IsRunning)
            {
                var id = int_last_id;

                //sync.SelectTransaction(
                //    nid => id = (int)nid
                //);

                id = ApplicationWebService_poll_onnfc.History.Count;


                //                type: System.Random
                //method: Int32 Next(Int32)
                var wait = sync_SelectContentUpdates_waitmin
                    + random.Next(0, sync_SelectContentUpdates_waitrandom);

                //Console.WriteLine("SelectTransaction " + new { id, int_last_id, sw.ElapsedMilliseconds });
                if (id == int_last_id)
                {
                    Thread.Sleep(wait);
                }
                else
                {
                    // dont stop reading...
                    //sw.Stop();

                    //var value = new PointerSyncQueries.SelectContentUpdates
                    //{
                    //    FromTransaction = int_last_id,
                    //    ToTransaction = (int)id
                    //};

                    //sync.SelectContentUpdates(
                    //    value: value,
                    //    yield: message =>
                    //    {

                    //        yield(XElement.Parse(message));
                    //    }
                    //);

                    Console.WriteLine("raise oninstall " + new { int_last_id, id });

                    ApplicationWebService_poll_onnfc.History.ToArray().Skip(int_last_id).Take(id - int_last_id).WithEach(
                        xid =>
                        {
                            var xml = new XElement("ApplicationWebService_poll_onnfc",
                                new XAttribute("id", xid
                                    )
                            );

                            // raise oninstall { int_last_id = 1, id = 2 }

                            Console.WriteLine("yield " + new { xml });
                            yield(xml);

                            // force end of stream for now.
                            // as we are not using event stream yet
                            sw.Stop();
                        }
                    );

                    int_last_id = (int)id;
                }

                if (sw.ElapsedMilliseconds >= sync_SelectContentUpdates_timeout)
                    sw.Stop();
            }

            yield_last_id("" + int_last_id);
        }


        static ApplicationWebService_poll_onnfc()
        {
            // http://lifehacker.com/run-an-action-when-you-remove-your-phone-from-an-nfc-ta-1208446359
            // https://groups.google.com/forum/#!topic/android-developers/8-17f6ZLYJY

            //  enter ApplicationWebService { ManagedThreadId = 2029 }
            Console.WriteLine("enter ApplicationWebService_poll_onnfc " + new { Thread.CurrentThread.ManagedThreadId });

            // http://mobile.tutsplus.com/tutorials/android/reading-nfc-tags-with-android/
            // http://stackoverflow.com/questions/10848134/android-on-nfc-read-close-activity-not-the-main-activity
            // http://stackoverflow.com/questions/17989055/nfc-not-able-to-detect-a-tag
            // http://stackoverflow.com/questions/5685946/nfc-broadcastreceiver-problem

            var activity = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext as ScriptCoreLib.Android.CoreAndroidWebServiceActivity;

            var adapter = android.nfc.NfcAdapter.getDefaultAdapter(
                activity
            );

            var isEnabled = adapter.isEnabled();

            Console.WriteLine(new { isEnabled });




            var intent = new Intent(
                activity,
                activity.GetType().ToClass()
                );

            intent.addFlags(Intent.FLAG_ACTIVITY_SINGLE_TOP);

            var pendingIntent = PendingIntent.getActivity(
                activity,
                0,
                intent,
                0
            );

            var filters = new IntentFilter[1];
            var techList = new[]
            { 
                //  [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.Ndef]
                //  dispatch tag: TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.NdefFormatable] message: null
                new [] { typeof(android.nfc.tech.MifareClassic).FullName },
                new [] { typeof(android.nfc.tech.NfcA).FullName},
                new [] { typeof(android.nfc.tech.Ndef).FullName},
                new [] { typeof(android.nfc.tech.NdefFormatable).FullName },

				// current javacards? (ISO 14443-4)
				new [] {     typeof(android.nfc.tech.IsoDep).FullName },
			};

            // http://124.16.139.131:24080/lxr/source/packages/apps/Nfc/src/com/android/nfc/NfcDispatcher.java?v=android-4.0.4


            //String[][] techList = new String[][] { new String[] { NfcA.class.getName(),
            //NfcB.class.getName(), NfcF.class.getName(),
            //NfcV.class.getName(), IsoDep.class.getName(),
            //MifareClassic.class.getName(),
            //MifareUltralight.class.getName(), Ndef.class.getName() } };



            // Notice that this is the same filter as in our manifest.
            filters[0] = new IntentFilter();

            // https://code.google.com/p/android/issues/detail?id=37673
            // http://www.xda-developers.com/android/activate-actions-upon-removal-of-nfc-tags/
            //filters[0].addAction(NfcAdapter.ACTION_TAG_LOST);

            // https://android.googlesource.com/platform/frameworks/base.git/+/android-4.2.2_r1/core/java/android/nfc/NfcAdapter.java
            filters[0].addAction("android.nfc.action.TAG_LOST");

            filters[0].addAction(NfcAdapter.ACTION_NDEF_DISCOVERED);
            filters[0].addAction(NfcAdapter.ACTION_TAG_DISCOVERED);
            filters[0].addAction(NfcAdapter.ACTION_TECH_DISCOVERED);


            filters[0].addCategory(Intent.CATEGORY_DEFAULT);

            //V:\src\AndroidNFCExperiment\ApplicationWebService.java:57: unreported exception android.content.IntentFilter.MalformedMimeTypeException; must be caught or declared to be thrown
            try
            {
                filters[0].addDataType("*/*");
            }
            catch
            {
                throw;
            }

            activity.AtNewIntent +=
                i =>
                {
                    var action = i.getAction();

                    Console.WriteLine("AtNewIntent " + new { action });

                    //I/System.Console(25300): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED }

                    if (action == NfcAdapter.ACTION_TECH_DISCOVERED)
                    {
                        //                    U:\src\AndroidNFCExperiment\ApplicationWebService___c__DisplayClass4.java:93: <identifier> expected
                        //private static Tag _<.cctor>b__0_Isinst_0064(Object _0064)
                        //                     ^

                        //                        I/System.Console(26970): AtPause
                        //D/NfcDispatcher(  747): Set Foreground Dispatch
                        //I/System.Console(26970): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED }
                        //I/System.Console(26970): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, tag =  }
                        //D/AndroidRuntime(26970): Shutting down VM
                        //W/dalvikvm(26970): threadid=1: thread exiting with uncaught exception (group=0x419dc700)
                        //I/ActivityManager(  440): START u0 {act=android.nfc.action.TECH_DISCOVERED flg=0x20000000 cmp=AndroidNFCExperiment.Activities/.ApplicationWebServiceActivity (has extras)} from pid -1
                        //W/ActivityManager(  440): startActivity called from non-Activity context; forcing Intent.FLAG_ACTIVITY_NEW_TASK for: Intent { act=android.nfc.action.TECH_DISCOVERED flg=0x20000000 cmp=AndroidNFCExperiment.Activities/.ApplicationWebServiceActivity (has extras) }

                        //var tag = (Tag)intent.getParcelableExtra(NfcAdapter.EXTRA_TAG);

                        //Console.WriteLine("AtNewIntent " + new { action, tag });

                        // D/NfcDispatcher(  747): dispatch tag: TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.Ndef] 
                        // message: NdefMessage 
                        // [NdefRecord tnf=4 type=70696C65742E65653A656B616172743A32 payload=66195F26063133303130385904202020205F28033233335F2701316E1B5A13333038363439303039303030333032313336315304FDCCD727, 
                        //  NdefRecord tnf=1 type=536967 payload=01020080B489DEDA8C2271386B7962250063A7C7C8612C3D58C8CD44D674F9D1615E80C72D961F8AC822C3188D48EFC7DA9DA3FF5C306E1EF54E0610F66D1C891CC59428A27CAA4211D4040527CF9BCD16F20E0B3116966AFC2390B7EF30CCC877B8532281CA3CBE286D295AECEA4447FD62874872A46099D6CEED99ED6766B829FD3FDF800025687474703A2F2F70696C65742E65652F6372742F33303836343930302D303030312E637274
                        // ]



                        //                        I/ActivityManager(  440): START u0 {act=android.nfc.action.TECH_DISCOVERED flg=0x20000000 cmp=AndroidNFCExperiment.Activities/.ApplicationWebServiceActivity (has extras)} from pid -1
                        //W/ActivityManager(  440): startActivity called from non-Activity context; forcing Intent.FLAG_ACTIVITY_NEW_TASK for: Intent { act=android.nfc.action.TECH_DISCOVERED flg=0x20000000 cmp=AndroidNFCExperiment.Activities/.ApplicationWebServiceActivity (has extras) }
                        //I/System.Console(30978): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED }
                        //I/NfcDispatcher(  747): matched TECH override
                        //I/System.Console(30978): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG }
                        //I/System.Console(30978): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, p = TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.NdefFormatable], FullName = android.nfc.Tag }
                        //I/System.Console(30978): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.ID }
                        //I/System.Console(30978): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.ID, p = [B@4212cfc8, FullName = [B }
                        //I/System.Console(30978): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.ID, p = [B@4212cfc8, FullName = [B, HexString = fc4969f9 }
                        //I/System.Console(30978): AtResume

                        // https://groups.google.com/forum/#!topic/android-developers/8-17f6ZLYJY
                        //http://stackoverflow.com/questions/9009043/android-nfc-intercept-all-tags
                        var extras = i.getExtras();
                        var ks = extras.keySet();
                        var iterator = ks.iterator();
                        while (iterator.hasNext())
                        {
                            var current = (string)iterator.next();

                            Console.WriteLine("AtNewIntent " + new { action, current });



                            // I/System.Console(29237): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, p = TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.Ndef], FullName = android.nfc.Tag }
                            // http://stackoverflow.com/questions/5968896/listing-all-extras-of-an-intent
                            extras.get(current).With(
                                p =>
                                {
                                    Console.WriteLine("AtNewIntent " + new { action, current, p, p.GetType().FullName });

                                    (p as Tag).With(
                                       tag =>
                                       {
                                           var id = (byte[])(object)tag.getId();

                                           Console.WriteLine("AtNewIntent " + new { action, current, id = id.ToHexString(), Thread.CurrentThread.ManagedThreadId });

                                           History.Add(
                                                id.ToHexString()
                                            );

                                           //I/System.Console(32331): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, p = TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.Ndef], id = fdccd727, tech = android.nfc.tech.MifareClassic }
                                           //I/System.Console(32331): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, p = TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.Ndef], id = fdccd727, tech = android.nfc.tech.NfcA }
                                           //I/System.Console(32331): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, p = TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.Ndef], id = fdccd727, tech = android.nfc.tech.Ndef }

                                           //I/System.Console(32331): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, p = TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.NdefFormatable], id = fc4969f9, tech = android.nfc.tech.MifareClassic }
                                           //I/System.Console(32331): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, p = TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.NdefFormatable], id = fc4969f9, tech = android.nfc.tech.NfcA }
                                           //I/System.Console(32331): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, p = TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.NdefFormatable], id = fc4969f9, tech = android.nfc.tech.NdefFormatable }

                                           tag.getTechList().WithEach(
                                               tech =>
                                               {
                                                   Console.WriteLine("AtNewIntent " + new { action, current, id = id.ToHexString(), tech });

                                                   if (tech == typeof(android.nfc.tech.MifareClassic).FullName)
                                                   {
                                                       android.nfc.tech.MifareClassic.get(tag).With(
                                                           m =>
                                                           {


                                                           }
                                                       );
                                                   }


                                                   if (tech == typeof(android.nfc.tech.NfcA).FullName)
                                                   {
                                                       android.nfc.tech.NfcA.get(tag).With(
                                                           m =>
                                                           {

                                                           }
                                                       );
                                                   }
                                               }
                                           );

                                           //tag.
                                       }
                                    );

                                    (p as byte[]).With(
                                        bytes =>
                                        {
                                            var HexString = bytes.ToHexString();

                                            Console.WriteLine("AtNewIntent " + new { action, current, p, p.GetType().FullName, HexString });
                                        }
                                    );

                                    // https://android.googlesource.com/platform/packages/apps/Nfc/+/android-4.2.1_r1.2/nci/src/com/android/nfc/dhimpl/NativeNfcTag.java

                                    //(p as android.os.Parcelable[]).With(
                                    //     m =>
                                    //     {

                                    //         Console.WriteLine("AtNewIntent " + new { action, current, records = m.getRecords().Length });

                                    //         m.getRecords().WithEach(
                                    //             r =>
                                    //             {
                                    //                 Console.WriteLine("AtNewIntent " + new { action, current, id = ((byte[])(object)r.getId()).ToHexString() });

                                    //             }
                                    //         );
                                    //     }
                                    // );
                                }
                            );

                        }

                        //                        I/System.Console(29527): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED }
                        //I/System.Console(29527): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG }
                        //I/System.Console(29527): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, p = TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.Ndef], FullName = android.nfc.Tag }
                        //I/System.Console(29527): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.ID }
                        //I/System.Console(29527): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.ID, p = [B@42156c88, FullName = [B }
                        //I/System.Console(29527): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.NDEF_MESSAGES }
                        //I/System.Console(29527): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.NDEF_MESSAGES, p = [Landroid.os.Parcelable;@42156e98, FullName = [Landroid.os.Parcelable; }
                        //I/System.Console(29527): AtResume





                        //                        I/System.Console(28867): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED }
                        //I/System.Console(28867): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG }
                        //I/System.Console(28867): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.ID }
                        //I/System.Console(28867): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.NDEF_MESSAGES }




                        //var id = tag.getId();

                        //Console.WriteLine("AtNewIntent " + new { action, tag, id.Length, id = ((byte[])(object)id).ToHexString() });

                        //tag.getTechList().WithEach(
                        //    tech =>
                        //    {
                        //        Console.WriteLine("AtNewIntent " + new { action, tech });
                        //    }
                        //);
                        //tag.get
                    }
                };

            // who is using it?
            activity.AtResume +=
                delegate
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("AtResume");
                    Console.WriteLine();
                    adapter.enableForegroundDispatch(activity, pendingIntent, filters, techList);
                };

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("before enableForegroundDispatch");
            adapter.enableForegroundDispatch(activity, pendingIntent, filters, techList);


            activity.AtPause +=
               delegate
               {
                   Console.WriteLine();
                   Console.WriteLine();
                   Console.WriteLine("AtPause");
                   adapter.disableForegroundDispatch(activity);
               };
        }

    }
}
