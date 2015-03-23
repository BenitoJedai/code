using android.content;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLibJava.Extensions;
using android.app;
using android.nfc;
using ScriptCoreLib.Ultra.WebService;
using System.Threading;
using android.nfc.tech;

namespace AndroidNFCExperiment
{
	public class ApplicationWebService
	{
		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140223-nfc
		// http://blog.atlasrfidstore.com/rfid-vs-nfc

		static ApplicationWebService()
		{
			// http://lifehacker.com/run-an-action-when-you-remove-your-phone-from-an-nfc-ta-1208446359
			// https://groups.google.com/forum/#!topic/android-developers/8-17f6ZLYJY

			//  android:launchMode="singleTop"
			// http://www.intridea.com/blog/2011/6/16/android-understanding-activity-launchmode
			Console.WriteLine("enter ApplicationWebService " + new { Thread.CurrentThread.ManagedThreadId });

			// http://mobile.tutsplus.com/tutorials/android/reading-nfc-tags-with-android/
			// http://stackoverflow.com/questions/10848134/android-on-nfc-read-close-activity-not-the-main-activity
			// http://stackoverflow.com/questions/17989055/nfc-not-able-to-detect-a-tag
			// http://stackoverflow.com/questions/5685946/nfc-broadcastreceiver-problem

			var activity = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext as ScriptCoreLib.Android.CoreAndroidWebServiceActivity;
			var adapter = android.nfc.NfcAdapter.getDefaultAdapter(activity);
			var isEnabled = adapter.isEnabled();

			Console.WriteLine(new { isEnabled });

			var intent = new Intent(
				activity,
				activity.GetType().ToClass()
				);

			// /ActivityManager(  510): startActivity called from non-Activity context; forcing Intent.FLAG_ACTIVITY_NEW_TASK for: Intent { act=android.nfc.action.TECH_DISCOVERED cmp=AndroidNFCExperiment.Activities/.ApplicationWebServiceActivity (has extras) }
			intent.addFlags(Intent.FLAG_ACTIVITY_SINGLE_TOP);
			//intent.addFlags(Intent.flag);
			//intent.addFlags(Intent.FLAG_ACTIVITY_NO_ANIMATION);

			// http://comments.gmane.org/gmane.comp.handhelds.android.devel/165860

			// https://code.google.com/p/android/issues/detail?id=4155
			//            Well onNewIntent will only be called when the activity has "singleTop" property and
			//exists in the activity stack(not destroyed)
			//intent.addFlags(Intent.fl);
			//com.p2.A2 is an Activity with launchMode="singleTop".


			//But without Intent.FLAG_ACTIVITY_SINGLE_TOP being set,
			//A2.onNewIntent() will not be invoked.
			// http://comments.gmane.org/gmane.comp.handhelds.android.devel/165860
			var pendingIntent = PendingIntent.getActivity(
				activity,
				0,
				intent,
				0
			);

			var techList = new[]
			{
				//  [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.Ndef]
				//  dispatch tag: TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.NdefFormatable] message: null
				new [] { typeof(android.nfc.tech.MifareClassic).FullName },
				new [] {     typeof(android.nfc.tech.NfcA).FullName},
				new [] {     typeof(android.nfc.tech.Ndef).FullName},
				new [] {     typeof(android.nfc.tech.NdefFormatable).FullName },

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
			var filters = new IntentFilter[1];

			filters[0] = new IntentFilter();

			// https://code.google.com/p/android/issues/detail?id=37673
			// http://www.xda-developers.com/android/activate-actions-upon-removal-of-nfc-tags/
			//filters[0].addAction(NfcAdapter.ACTION_TAG_LOST);

			// https://android.googlesource.com/platform/frameworks/base.git/+/android-4.2.2_r1/core/java/android/nfc/NfcAdapter.java
			filters[0].addAction("android.nfc.action.TAG_LOST");

			filters[0].addAction(NfcAdapter.ACTION_NDEF_DISCOVERED);
			filters[0].addAction(NfcAdapter.ACTION_TAG_DISCOVERED);
			filters[0].addAction(NfcAdapter.ACTION_TECH_DISCOVERED);


			// https://code.google.com/p/ndef-tools-for-android/source/browse/ndeftools-util/src/org/ndeftools/util/activity/NfcDetectorActivity.java

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

			//            D/NfcDispatcher(  747): Set Foreground Dispatch
			//D/dalvikvm(20170): GC_CONCURRENT freed 488K, 8% free 7978K/8584K, paused 4ms+2ms, total 27ms
			//D/dalvikvm(  581): GC_CONCURRENT freed 4091K, 44% free 12001K/21124K, paused 3ms+5ms, total 32ms
			//D/NfcDispatcher(  747): dispatch tag: TAG: Tech [android.nfc.tech.MifareClassic, android.nfc.tech.NfcA, android.nfc.tech.Ndef] message: NdefMessage [NdefRecord tnf=4 type=70696C65742E65653A656B616172743A32 payload=66195F26063133303130385904202020205F28033233335F2701316E1B5A13333038363439303039303030333032313336315304FDCCD727, NdefRecord tnf=1 type=536967 payload=01020080B489DEDA8C2271386B7962250063A7C7C8612C3D58C8CD44D674F9D1615E80C72D961F8AC822C3188D48EFC7DA9DA3FF5C306E1EF54E0610F66D1C891CC59428A27CAA4211D4040527CF9BCD16F20E0B3116966AFC2390B7EF30CCC877B8532281CA3CBE286D295AECEA4447FD62874872A46099D6CEED99ED6766B829FD3FDF800025687474703A2F2F70696C65742E65652F6372742F33303836343930302D303030312E637274]
			//D/dalvikvm(  747): GC_CONCURRENT freed 491K, 10% free 8255K/9160K, paused 3ms+1ms, total 33ms
			//D/dalvikvm(  747): WAIT_FOR_CONCURRENT_GC blocked 8ms
			//D/NfcHandover(  747): tryHandover(): NdefMessage [NdefRecord tnf=4 type=70696C65742E65653A656B616172743A32 payload=66195F26063133303130385904202020205F28033233335F2701316E1B5A13333038363439303039303030333032313336315304FDCCD727, NdefRecord tnf=1 type=536967 payload=01020080B489DEDA8C2271386B7962250063A7C7C8612C3D58C8CD44D674F9D1615E80C72D961F8AC822C3188D48EFC7DA9DA3FF5C306E1EF54E0610F66D1C891CC59428A27CAA4211D4040527CF9BCD16F20E0B3116966AFC2390B7EF30CCC877B8532281CA3CBE286D295AECEA4447FD62874872A46099D6CEED99ED6766B829FD3FDF800025687474703A2F2F70696C65742E65652F6372742F33303836343930302D303030312E637274]
			//I/ActivityManager(  440): START u0 {flg=0x10008000 cmp=com.android.nfc/.NfcRootActivity (has extras)} from pid 747
			//D/NfcDispatcher(  747): Set Foreground Dispatch
			//I/NfcDispatcher(  747): matched single TECH
			//I/ActivityManager(  440): START u0 {act=android.nfc.action.TECH_DISCOVERED cmp=com.google.android.tag/com.android.apps.tag.TagViewer (has extras)} from pid 747
			//I/ActivityManager(  440): Displayed com.google.android.tag/com.android.apps.tag.TagViewer: +100ms (total +114ms)
			//W/IInputConnectionWrapper(20170): showStatusIcon on inactive InputConnection
			//I/CalendarProvider2(17732): Sending notification intent: Intent { act=android.intent.action.PROVIDER_CHANGED dat=content://com.android.calendar }
			//W/ContentResolver(17732): Failed to get type for: content://com.android.calendar (Unknown URL content://com.android.calendar)

			// await?
			activity.AtNewIntent +=
				i =>
				{
					var action = i.getAction();





					Console.WriteLine("AndroidNFCExperiment AtNewIntent " + new { action });

					//I/System.Console(25300): AtNewIntent { action = android.nfc.action.TECH_DISCOVERED }

					#region android.nfc.action.TECH_DISCOVERED
					if (action == "android.nfc.action.TECH_DISCOVERED")
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

												   if (tech == typeof(android.nfc.tech.IsoDep).FullName)
												   {
													   android.nfc.tech.IsoDep.get(tag).With(
														   m =>
														   {
															   Console.WriteLine("Isodep exists");

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
					#endregion
				};

			// who is using it?
			activity.AtResume +=
				delegate
				{

					Console.WriteLine("AndroidNFCExperiment AtResume");
					adapter.enableForegroundDispatch(activity, pendingIntent, filters, techList);
				};

			//     Caused by: java.lang.IllegalStateException: Foreground dispatch can only be enabled when your activity is resumed
			//at android.nfc.NfcAdapter.enableForegroundDispatch(NfcAdapter.java:1135)

			Console.WriteLine("before AndroidNFCExperiment enableForegroundDispatch ?");
			//adapter.enableForegroundDispatch(activity, pendingIntent, filters, techList);


			activity.AtPause +=
			   delegate
			   {
				   Console.WriteLine("AndroidNFCExperiment AtPause");
				   adapter.disableForegroundDispatch(activity);
			   };
		}

	}


	//D/NativeNfcTag(  870): Connect to a tech with a different handle
	//D/NativeNfcTag(  870): Check NDEF Failed - status = 3
	//D/NativeNfcTag(  870): Starting background presence check
	//I/ActivityManager(  449): START u0 {act=android.nfc.action.TECH_DISCOVERED flg=0x20000000 cmp=AndroidNFCExperiment.Activities/.ApplicationWebServiceActivity (has extras)} from uid 10118 on display 0
	//W/ActivityManager(  449): startActivity called from non-Activity context; forcing Intent.FLAG_ACTIVITY_NEW_TASK for: Intent { act=android.nfc.action.TECH_DISCOVERED flg=0x20000000 cmp=AndroidNFCExper
	//I/System.Console(13162): AndroidNFCExperiment AtPause
	//I/System.Console(13162): AndroidNFCExperiment AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED }}
	//W/AudioTrack(  870): AUDIO_OUTPUT_FLAG_FAST denied by client
	//I/System.Console(13162): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.ID }}
	//I/System.Console(13162): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.ID, p = [B@1d579d27, FullName = [B }}
	//I/System.Console(13162): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.ID, p = [B@1d579d27, FullName = [B, HexString = 55a1c2c6 }}
	//I/System.Console(13162): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG }}
	//I/System.Console(13162): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, p = TAG: Tech [android.nfc.tech.NfcA], FullName = android.nfc.Tag }}
	//I/System.Console(13162): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, id = 55a1c2c6, ManagedThreadId = 1 }}
	//I/System.Console(13162): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, id = 55a1c2c6, tech = android.nfc.tech.NfcA }}
	//I/System.Console(13162): CoreAndroidWebServiceActivity onResume
	//I/System.Console(13162): AndroidNFCExperiment AtResume
	//D/NativeNfcTag(  870): Tag lost, restarting polling loop


	//D/NativeNfcTag(  870): Starting background presence check
	//I/System.Console(13318): AndroidNFCExperiment AtPause
	//I/System.Console(13318): AndroidNFCExperiment AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED }}
	//I/System.Console(13318): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.ID }}
	//I/System.Console(13318): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.ID, p = [B@1d579d27, FullName = [B }}
	//I/System.Console(13318): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.ID, p = [B@1d579d27, FullName = [B, HexString = 1459b33c }}
	//I/System.Console(13318): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG }}
	//I/System.Console(13318): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, p = TAG: Tech [android.nfc.tech.IsoDep, android.nfc.tech.NfcB], FullName = android.nfc.Tag }}
	//I/System.Console(13318): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, id = 1459b33c, ManagedThreadId = 1 }}
	//I/System.Console(13318): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, id = 1459b33c, tech = android.nfc.tech.IsoDep }}
	//I/System.Console(13318): Isodep exists
	//I/System.Console(13318): AtNewIntent {{ action = android.nfc.action.TECH_DISCOVERED, current = android.nfc.extra.TAG, id = 1459b33c, tech = android.nfc.tech.NfcB }}
	//I/System.Console(13318): CoreAndroidWebServiceActivity onResume
	//I/System.Console(13318): AndroidNFCExperiment AtResume
}
