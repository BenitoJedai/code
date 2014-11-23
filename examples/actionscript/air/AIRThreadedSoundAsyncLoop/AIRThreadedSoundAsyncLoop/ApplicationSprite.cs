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
using System.Math;

namespace AIRThreadedSoundAsyncLoop
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
        // Error	1	'AIRThreadedSoundAsyncLoop.ApplicationSprite.volume': 
        // a volatile field cannot be of the type 'double'	X:\jsc.svn\examples\actionscript\air\AIRThreadedSoundAsyncLoop\AIRThreadedSoundAsyncLoop\ApplicationSprite.cs	13	25	AIRThreadedSoundAsyncLoop
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


            new net.hires.debug.Stats().AttachToSprite();



            var t = new Thread(
                async arg0 =>
            {
                var s = new Sound();
                //var sw = System.Diagnostics.Stopwatch.StartNew();

                while (true)
                {
                    var e = await s.async.sampleData;

                    // can we have framerate as audio?
                    for (var c = 0; c < 8192; c++)
                    {
                        // i wonder, can we use the orientation
                        // or magnetic north here?
                        // prep for Gear VR?
                        e.data.writeFloat(Sin(((c + e.position) / PI * 0.3)) * 0.4);
                        e.data.writeFloat(Sin(((c + e.position) / PI * 0.3)) * 0.1);
                    }
                }
            }
            );
            
            t.Start(null);





        }

    }
}
