using System;
using System.Threading;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.Extensions;

namespace AIRThreadedSound
{
    public sealed class ApplicationSprite : Sprite
    {
        // tested by nexus9

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150107
        // X:\jsc.svn\examples\actionscript\air\AIRAudioWorker\AIRAudioWorker\ApplicationSprite.cs

        // would jsc be able to translate it into
        // a property with events for cross thread sync?
        
        // or, a GC like syncer should see what fields are being read and written
        // and on the fly bridge the data flow if usage is found
        //volatile double volume;
        // http://msdn.microsoft.com/en-us/library/aa645755%28v=vs.71%29.aspx
        // http://stackoverflow.com/questions/4727068/why-not-volatile-on-system-double-and-system-long
        // http://theburningmonk.com/2010/03/threading-understanding-the-volatile-modifier-in-csharp/
        // Application ApplicationWebService sync would also benefit from such
        // usage analysis
        volatile float volume;

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

            // can we get a code analyzer to suggest a new nuget
            // 20150107, until when clippy starts selling us things?:D
            // Error	1	The type or namespace name 'net' could not be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\actionscript\air\AIRThreadedSound\AIRThreadedSound\ApplicationSprite.cs	44	17	AIRThreadedSound
            new net.hires.debug.Stats().AttachTo(this);

            new Thread(
                // jsc, whats the scope sharing analysis for this new block
                // can you show it on the other UHD display?
                // jsc zombie server, analysis server
                delegate (object scope)
            {
                // can our Console.WriteLine 
                // be redirected over udp from android
                // to the jsc studio running over at the chrome?
                // AIR has to use native plugin to do lan udp broadcast?

                // can we thread left and right audio on separate threads?
                // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/media/Sound.html
                var mySound = new Sound();

                // would it make any sense to use the async workflow instead?
                mySound.sampleData += e =>
                {
                    // does it work on tab?
                    // lets attatch the tab to find out.
                    // cmd /K c:\util\android-sdk-windows\platform-tools\adb.exe logcat
                    // works at 60fps
                    // works!
                    // could we add the nuget packages at runtime?
                    // /jsc the debugger view

                    for (var c = 0; c < 8192; c++)
                    {
                        // i wonder, can we use the orientation
                        // or magnetic north here?
                        // prep for Gear VR?
                        e.data.writeFloat(Math.Sin(((c + e.position) / Math.PI / 2)) * 0.4);
                        e.data.writeFloat(Math.Sin(((c + e.position) / Math.PI / 2)) * 0.1);
                    }
                };

                mySound.play();

            }
            ).Start(default(object));

        }

    }
}
