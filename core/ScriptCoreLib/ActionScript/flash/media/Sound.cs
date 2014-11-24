using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.utils;
using System.Threading.Tasks;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.BCLImplementation.System;


namespace ScriptCoreLib.ActionScript.Extensions.flash.media
{
    // if a type implements a type that is set to be native, then only implementation
    // which is marked with NotImplementedHere applies

    internal static partial class __Sound
    {




        // X:\jsc.svn\examples\actionscript\test\TestResolveNativeImplementationExtension\TestResolveNativeImplementationExtension\Class1.cs
        public static SoundTasks get_async(Sound that)
        {
            //Console.WriteLine("InteractiveObject get_async");

            if (!SoundTasks.InternalLookup.ContainsKey(that))
                SoundTasks.InternalLookup[that] = new SoundTasks { that_Sound = that };


            return SoundTasks.InternalLookup[that];
        }
    }
}

namespace ScriptCoreLib.ActionScript.flash.media
{
    [Script]
    [Obsolete("experimental")]
    public class SoundTasks
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\IHTMLElement.async.cs
        // X:\jsc.svn\core\ScriptCoreLib\ActionScript\Extensions\flash\display\InteractiveObject.cs

        internal Sound that_Sound;

        internal static Dictionary<Sound, SoundTasks> InternalLookup = new Dictionary<Sound, SoundTasks>();

        SoundChannel sc;
        bool listening_sampleData;
        TaskCompletionSource<SampleDataEvent> awaiting_sampleData;
        int awaiting_sampleData_i;

        [System.Obsolete("should jsc expose events as async tasks until C# chooses to allow that?")]
        // shall we add on prefix or not?
        public virtual Task<SampleDataEvent> sampleData
        {
            get
            {

                //await sampleData { listening_sampleData = false, awaiting_sampleData_i = 0 }
                //  at sampleData { awaiting_sampleData_i = 1, awaiting_sampleData = [object __TaskCompletionSource_1] }
                //  at exit sampleData { awaiting_sampleData_i = 1, awaiting_sampleData =  }
                //await sampleData exit { awaiting_sampleData =  }
                //frame1 complete
                //await sampleData { listening_sampleData = true, awaiting_sampleData_i = 1 }
                //await sampleData exit { awaiting_sampleData = [object __TaskCompletionSource_1] }

                //Console.WriteLine("await sampleData " + new { listening_sampleData, awaiting_sampleData_i });
                var x = new TaskCompletionSource<SampleDataEvent>();
                // sampleData not fired the second time?

                var later_play = false;
                #region listening_sampleData
                if (!listening_sampleData)
                {
                    listening_sampleData = true;

                    that_Sound.sampleData +=
                        e =>
                        {
                            awaiting_sampleData_i++;

                            //Console.WriteLine("  at sampleData " + new { awaiting_sampleData_i, awaiting_sampleData });


                            if (awaiting_sampleData != null)
                            {

                                var xx = awaiting_sampleData;
                                awaiting_sampleData = null;

                                // allow awaiting_sampleData to be set again 
                                xx.SetResult(e);
                            }

                            //Console.WriteLine("  at exit sampleData " + new { awaiting_sampleData_i, awaiting_sampleData });
                        };

                    //mySound.play();
                    later_play = true;
                }
                #endregion


                // X:\jsc.svn\examples\actionscript\air\AIRThreadedSound\AIRThreadedSound\ApplicationSprite.cs



                // we support one awaiter at the same time for now
                awaiting_sampleData = x;

                if (later_play)
                {
                    //that_Sound.play();

                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/2014
                    // await for the next frame
                    __Task.Delay(1).ContinueWith(
                        delegate { sc = that_Sound.play(); }
                    );
                }

                //Console.WriteLine("await sampleData exit " + new { awaiting_sampleData });
                return x.Task;
            }
        }
    }


    // http://livedocs.adobe.com/flex/201/langref/flash/media/Sound.html
    [Script(IsNative = true)]
    public class Sound : EventDispatcher
    {
        // cymatics
        // sound is a very important type. 
        // with the correct speakers we should be aveble to levitate particles

        // jsc, are you already keeping track of what is being used and how?

        // X:\jsc.svn\examples\actionscript\air\AIRAudioWorker\AIRAudioWorker\ApplicationSprite.cs
        // X:\jsc.svn\examples\actionscript\air\AIRThreadedSound\AIRThreadedSound\ApplicationSprite.cs

        [Obsolete("experimental")]
        public SoundTasks async
        {
            [method: Script(NotImplementedHere = true)]
            get { return default(SoundTasks); }
        }

        #region Properties
        /// <summary>
        /// [read-only] Returns the currently available number of bytes in this sound object.
        /// </summary>
        public uint bytesLoaded { get; private set; }

        /// <summary>
        /// [read-only] Returns the total number of bytes in this sound object.
        /// </summary>
        public int bytesTotal { get; private set; }

        /// <summary>
        /// [read-only] Provides access to the metadata that is part of an MP3 file.
        /// </summary>
        public ID3Info id3 { get; private set; }

        /// <summary>
        /// [read-only] Returns the buffering state of external MP3 files.
        /// </summary>
        public bool isBuffering { get; private set; }

        /// <summary>
        /// [read-only] The length of the current sound in milliseconds.
        /// </summary>
        public double length { get; private set; }

        /// <summary>
        /// [read-only] The URL from which this sound was loaded.
        /// </summary>
        public string url { get; private set; }

        #endregion


        #region Methods
        /// <summary>
        /// Closes the stream, causing any download of data to cease.
        /// </summary>
        public void close()
        {
        }

        /// <summary>
        /// Extracts raw sound data from a Sound object.
        /// </summary>
        public double extract(ByteArray target, double length, double startPosition)
        {
            return default(double);
        }

        /// <summary>
        /// Extracts raw sound data from a Sound object.
        /// </summary>
        public double extract(ByteArray target, double length)
        {
            return default(double);
        }

        /// <summary>
        /// Initiates loading of an external MP3 file from the specified URL.
        /// </summary>
        public void load(URLRequest stream, SoundLoaderContext context)
        {
        }

        /// <summary>
        /// Initiates loading of an external MP3 file from the specified URL.
        /// </summary>
        public void load(URLRequest stream)
        {
        }

        /// <summary>
        /// Generates a new SoundChannel object to play back the sound.
        /// </summary>
        public SoundChannel play(double startTime, int loops, SoundTransform sndTransform)
        {
            return default(SoundChannel);
        }

        /// <summary>
        /// Generates a new SoundChannel object to play back the sound.
        /// </summary>
        public SoundChannel play(double startTime, int loops)
        {
            return default(SoundChannel);
        }

        /// <summary>
        /// Generates a new SoundChannel object to play back the sound.
        /// </summary>
        public SoundChannel play(double startTime)
        {
            return default(SoundChannel);
        }

        /// <summary>
        /// Generates a new SoundChannel object to play back the sound.
        /// </summary>
        public SoundChannel play()
        {
            return default(SoundChannel);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new Sound object.
        /// </summary>
        public Sound(URLRequest stream, SoundLoaderContext context)
        {
        }

        /// <summary>
        /// Creates a new Sound object.
        /// </summary>
        public Sound(URLRequest stream)
        {
        }

        /// <summary>
        /// Creates a new Sound object.
        /// </summary>
        public Sound()
        {
        }

        #endregion


        #region Events
        /// <summary>
        /// Dispatched when data has loaded successfully.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> complete;

        /// <summary>
        /// Dispatched by a Sound object when ID3 data is available for an MP3 sound.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> onid3;

        /// <summary>
        /// Dispatched when an input/output error occurs that causes a load operation to fail.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<IOErrorEvent> ioError;

        /// <summary>
        /// Dispatched when a load operation starts.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> open;

        /// <summary>
        /// Dispatched when data is received as a load operation progresses.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<ProgressEvent> progress;

        /// <summary>
        /// Dispatched when the player requests new audio data.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<SampleDataEvent> sampleData;

        #endregion


    }
}
