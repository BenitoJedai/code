using System;
using System.Threading;
using System.Threading.Tasks;
using ScriptCoreLib.ActionScript.BCLImplementation.System;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace AIRThreadedSoundAsync
{
    public sealed class ApplicationSprite : Sprite
    {
        //      method: CompareTo
        //      Object reference not set to an instance of an object.
        // at System.Collections.Generic.Dictionary`2.Insert(TKey key, TValue value, Boolean add)
        //at System.Collections.Generic.Dictionary`2.set_Item(TKey key, TValue value)
        //at jsc.Script.CompilerBase.DIACache.GetVariableName(Type t, MethodBase m, LocalVariableInfo var, CompilerBase z) in x:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.DIA.cs:line 264
        //at jsc.Script.CompilerBase.<WriteVariableName>b__0(Type t, MethodBase m, LocalVariableInfo v) in x:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.DIA.cs:line 289
        //at jsc.Script.CompilerBase.<>c__DisplayClass5.<>c__DisplayClass8.<WriteVariableName>b__2(LocalVariableInfo vx) in x:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.DIA.cs:line 303


        // X:\jsc.svn\examples\actionscript\air\AIRAudioWorker\AIRAudioWorker\ApplicationSprite.cs

        // would jsc be able to translate it into
        // a property with events for cross thread sync?
        // Error	1	'AIRThreadedSoundAsync.ApplicationSprite.volume': 
        // a volatile field cannot be of the type 'double'	X:\jsc.svn\examples\actionscript\air\AIRThreadedSoundAsync\AIRThreadedSoundAsync\ApplicationSprite.cs	13	25	AIRThreadedSoundAsync
        // or, a GC like syncer should see what fields are being read and written
        // and on the fly bridge the data flow if usage is found
        //volatile double volume;
        // http://msdn.microsoft.com/en-us/library/aa645755%28v=vs.71%29.aspx
        // http://stackoverflow.com/questions/4727068/why-not-volatile-on-system-double-and-system-long
        // http://theburningmonk.com/2010/03/threading-understanding-the-volatile-modifier-in-csharp/
        // Application ApplicationWebService sync would also benefit from such
        // usage analysis
        //volatile float volume;

        public ApplicationSprite()
        {
            // GearVR native api available
            // for AIR via http://www.adobe.com/devnet/air/articles/extending-air.html

            // http://blog.aboutme.be/2011/12/14/udp-native-extension-for-air-mobile-now-with-android-support/
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140310
            // http://16bit.signt.com/post/31487077697/extendable-as3-worker-class
            // http://performancebydesign.blogspot.com/2011/11/measuring-thread-execution-state-using.html
            // http://16bit.signt.com/post/31601682385/utilizing-multiple-worker-in-as3
            // http://www.blixtsystems.com/2010/11/audio-mixing-on-air-for-android/
            // http://coenraets.org/blog/2010/07/voicenotes-for-android-sample-app-using-flex-air-and-the-microphone-api/


            var t = new TextField
            {
                multiline = true,

                autoSize = TextFieldAutoSize.LEFT,


                text = "..."
                //}.AttachTo(__Thread.InternalPrimordialSprite);

                // .AsConsole
            }.AttachToSprite().AsConsole();

            //new Thread(
            //    // jsc, whats the scope sharing analysis for this new block
            //    // can you show it on the other UHD display?
            //    // jsc zombie server, analysis server
            //    //async 
            //    delegate (object scope)
            //{
            // can our Console.WriteLine 
            // be redirected over udp from android
            // to the jsc studio running over at the chrome?
            // AIR has to use native plugin to do lan udp broadcast?

            // can we thread left and right audio on separate threads?
            // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/media/Sound.html
            var mySound = new Sound();

            var awaiting_sampleData_i = 0;

            var awaiting_sampleData = default(TaskCompletionSource<SampleDataEvent>);

            Action<SampleDataEvent> y = e =>
            {
                awaiting_sampleData_i++;
                ////t.text = new { i } + " sampleData";
                Console.WriteLine(new { awaiting_sampleData_i } + " sampleData");



                // can we have framerate as audio?
                for (var c = 0; c < 8192; c++)
                {
                    // i wonder, can we use the orientation
                    // or magnetic north here?
                    // prep for Gear VR?
                    e.data.writeFloat(Math.Sin(((c + e.position) / Math.PI / 2)) * 0.4);
                    e.data.writeFloat(Math.Sin(((c + e.position) / Math.PI / 2)) * 0.1);
                }
            };

            var x_init = default(Action);

            x_init = delegate
            {
                Console.WriteLine("await sampleData " + new
                {
                    //listening_sampleData,
                    awaiting_sampleData_i
                });
                awaiting_sampleData = new TaskCompletionSource<SampleDataEvent>();



                Console.WriteLine("await sampleData exit " + new { awaiting_sampleData });
                // ??
                awaiting_sampleData.Task.ContinueWith(
                    tt =>
                    {
                        y(tt.Result);

                        x_init();
                    }
                );
            };



            //reusable TaskCompletionSource ?

            x_init();

            //can we have only the event in another thread?
            mySound.sampleData += e =>
            {
                Console.WriteLine("  at sampleData " + new { awaiting_sampleData_i, awaiting_sampleData });


                //y(e);

                // why does this work yet the async variant not?
                awaiting_sampleData.SetResult(e);

                Console.WriteLine("  at exit sampleData " + new { awaiting_sampleData_i, awaiting_sampleData });
            };

            mySound.play();


            //11ms await sampleData { listening_sampleData = false, awaiting_sampleData_i = 0 }
            //16ms   at sampleData { awaiting_sampleData_i = 1, awaiting_sampleData = [object __TaskCompletionSource_1] }
            //17ms   at exit sampleData { awaiting_sampleData_i = 1, awaiting_sampleData =  }
            //17ms await sampleData exit { awaiting_sampleData =  }
            //20ms frame1 enter
            //25ms frame1 complete
            //25ms await sampleData { listening_sampleData = true, awaiting_sampleData_i = 1 }
            //26ms await sampleData exit { awaiting_sampleData = [object __TaskCompletionSource_1] }

            //await sampleData { listening_sampleData = true, awaiting_sampleData_i = 1 }
            //await exit sampleData 

#if xx
            new { }.With(
                async scope =>
                {
                    //          sampleData { listening_sampleData = false, awaiting_sampleData_i = 0 }
                    //sampleData { listening_sampleData = true, awaiting_sampleData_i = 1 }



                    {
                        var e = await mySound.async.sampleData;
                        Console.WriteLine("frame1 enter");
                        y(e);
                    }
                    Console.WriteLine("frame1 complete");
                    {

                        var e = await mySound.async.sampleData;
                        Console.WriteLine("frame2 enter");
                        y(e);
                    }
                    Console.WriteLine("frame2 complete");

                    {

                        var e = await mySound.async.sampleData;
                        y(e);
                    }
                    Console.WriteLine("frame3 complete");

                    {

                        // await mySound.async.sampleData2
                        //Console.WriteLine("await mySound.async.sampleData2");
                        var e = await mySound.async.sampleData;
                        y(e);
                    }
                }
            );
#endif




            // call play automatically after subscribing?
            //mySound.play();


            //var go = default(Action);


            //go = delegate
            //{
            //    t.text = "enter go";

            //    mySound.async.sampleData.ContinueWith(
            //        tt =>
            //            {
            //                // ?
            //                t.text = "enter sampleData";


            //                var e = tt.Result;

            //                for (var c = 0; c < 8192; c++)
            //                {
            //                    // i wonder, can we use the orientation
            //                    // or magnetic north here?
            //                    // prep for Gear VR?
            //                    e.data.writeFloat(Math.Sin(((c + e.position) / Math.PI / 2)) * 0.4);
            //                    e.data.writeFloat(Math.Sin(((c + e.position) / Math.PI / 2)) * 0.1);
            //                }

            //                go();
            //            }
            //    );


            //};

            //go();

            //while (true)
            //{
            //    // would it make any sense to use the async workflow instead?
            //    var e = await mySound.async.sampleData;

            //    // does it work on tab?
            //    // lets attatch the tab to find out.
            //    // cmd /K c:\util\android-sdk-windows\platform-tools\adb.exe logcat
            //    // works at 60fps
            //    // works!
            //    // could we add the nuget packages at runtime?
            //    // /jsc the debugger view

            //    for (var c = 0; c < 8192; c++)
            //    {
            //        // i wonder, can we use the orientation
            //        // or magnetic north here?
            //        // prep for Gear VR?
            //        e.data.writeFloat(Math.Sin(((c + e.position) / Math.PI / 2)) * 0.4);
            //        e.data.writeFloat(Math.Sin(((c + e.position) / Math.PI / 2)) * 0.1);
            //    }

            //    // can we plot it?
            //}

            //}
            //).Start(default(object));

        }

    }
}
