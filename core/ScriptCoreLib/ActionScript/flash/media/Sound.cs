using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.utils;

namespace ScriptCoreLib.ActionScript.flash.media
{
    // http://livedocs.adobe.com/flex/201/langref/flash/media/Sound.html
    [Script(IsNative = true)]
    public class Sound : EventDispatcher
    {
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
