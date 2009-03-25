using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.media;

namespace ScriptCoreLib.ActionScript.fl.video
{
	// http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/fl/video/FLVPlayback.html
	[Script(IsNative = true)]
	public class FLVPlayback : Sprite
	{
		#region Methods

		/// <summary>
		/// Adds an ActionScript cue point and has the same effect as adding an ActionScript cue point using the Cue Points dialog box, except that it occurs when an application executes rather than during application development.
		/// </summary>
		public object addASCuePoint(object timeOrCuePoint, string name, object parameters)
		{
			return default(object);
		}

		/// <summary>
		/// Adds an ActionScript cue point and has the same effect as adding an ActionScript cue point using the Cue Points dialog box, except that it occurs when an application executes rather than during application development.
		/// </summary>
		public object addASCuePoint(object timeOrCuePoint, string name)
		{
			return default(object);
		}

		/// <summary>
		/// Adds an ActionScript cue point and has the same effect as adding an ActionScript cue point using the Cue Points dialog box, except that it occurs when an application executes rather than during application development.
		/// </summary>
		public object addASCuePoint(object timeOrCuePoint)
		{
			return default(object);
		}

		/// <summary>
		/// Brings a video player to the front of the stack of video players.
		/// </summary>
		public void bringVideoPlayerToFront(uint index)
		{
		}

		/// <summary>
		/// Closes NetStream and deletes the video player specified by the index parameter.
		/// </summary>
		public void closeVideoPlayer(uint index)
		{
		}

		/// <summary>
		/// Sets the FLVPlayback video player to full screen.
		/// </summary>
		public void enterFullScreenDisplayState()
		{
		}

		/// <summary>
		/// Finds the cue point of the type specified by the type parameter and having the time, name, or combination of time and name that you specify through the parameters.
		/// </summary>
		public object findCuePoint(object timeNameOrCuePoint, string type)
		{
			return default(object);
		}

		/// <summary>
		/// Finds a cue point of the specified type that matches or is earlier than the time that you specify.
		/// </summary>
		public object findNearestCuePoint(object timeNameOrCuePoint, string type)
		{
			return default(object);
		}

		/// <summary>
		/// Finds the next cue point in my_cuePoint.array that has the same name as my_cuePoint.name.
		/// </summary>
		public object findNextCuePointWithName(object cuePoint)
		{
			return default(object);
		}

		/// <summary>
		/// Gets the video player specified by the index parameter.
		/// </summary>
		public VideoPlayer getVideoPlayer(double index)
		{
			return default(VideoPlayer);
		}

		/// <summary>
		/// Returns false if the FLV file embedded cue point is disabled.
		/// </summary>
		public bool isFLVCuePointEnabled(object timeNameOrCuePoint)
		{
			return default(bool);
		}

		/// <summary>
		/// Begins loading the FLV file and provides a shortcut for setting the autoPlay property to false and setting the source, totalTime, and isLive properties, if given.
		/// </summary>
		public void load(string source, double totalTime, bool isLive)
		{
		}

		/// <summary>
		/// Begins loading the FLV file and provides a shortcut for setting the autoPlay property to false and setting the source, totalTime, and isLive properties, if given.
		/// </summary>
		public void load(string source, double totalTime)
		{
		}

		/// <summary>
		/// Begins loading the FLV file and provides a shortcut for setting the autoPlay property to false and setting the source, totalTime, and isLive properties, if given.
		/// </summary>
		public void load(string source)
		{
		}

		/// <summary>
		/// Pauses playing the video stream.
		/// </summary>
		public void pause()
		{
		}

		/// <summary>
		/// Plays the video stream.
		/// </summary>
		public void play(string source, double totalTime, bool isLive)
		{
		}

		/// <summary>
		/// Plays the video stream.
		/// </summary>
		public void play(string source, double totalTime)
		{
		}

		/// <summary>
		/// Plays the video stream.
		/// </summary>
		public void play(string source)
		{
		}

		/// <summary>
		/// Plays the video stream.
		/// </summary>
		public void play()
		{
		}


		/// <summary>
		/// Plays the FLV file when enough of it has downloaded.
		/// </summary>
		public void playWhenEnoughDownloaded()
		{
		}

		/// <summary>
		/// Removes an ActionScript cue point from the currently loaded FLV file.
		/// </summary>
		public object removeASCuePoint(object timeNameOrCuePoint)
		{
			return default(object);
		}

		/// <summary>
		/// Seeks to a given time in the file, specified in seconds, with a precision of three decimal places (milliseconds).
		/// </summary>
		public void seek(double time)
		{
		}

		/// <summary>
		/// Seeks to a percentage of the file and places the playhead there.
		/// </summary>
		public void seekPercent(double percent)
		{
		}

		/// <summary>
		/// Seeks to a given time in the file, specified in seconds, with a precision up to three decimal places (milliseconds).
		/// </summary>
		public void seekSeconds(double time)
		{
		}

		/// <summary>
		/// Seeks to a navigation cue point that matches the specified time, name, or time and name.
		/// </summary>
		public void seekToNavCuePoint(object timeNameOrCuePoint)
		{
		}

		/// <summary>
		/// Seeks to the next navigation cue point, based on the current value of the playheadTime property.
		/// </summary>
		public void seekToNextNavCuePoint(double time)
		{
		}

		/// <summary>
		/// Seeks to the next navigation cue point, based on the current value of the playheadTime property.
		/// </summary>
		public void seekToNextNavCuePoint()
		{
		}

		/// <summary>
		/// Seeks to the previous navigation cue point, based on the current value of the playheadTime property.
		/// </summary>
		public void seekToPrevNavCuePoint(double time)
		{
		}

		/// <summary>
		/// Seeks to the previous navigation cue point, based on the current value of the playheadTime property.
		/// </summary>
		public void seekToPrevNavCuePoint()
		{
		}

		/// <summary>
		/// Enables or disables one or more FLV file cue points.
		/// </summary>
		public double setFLVCuePointEnabled(bool enabled, object timeNameOrCuePoint)
		{
			return default(double);
		}

		/// <summary>
		/// Sets the scaleX and scaleY properties simultaneously.
		/// </summary>
		public void setScale(double scaleX, double scaleY)
		{
		}

		/// <summary>
		/// Sets width and height simultaneously.
		/// </summary>
		public void setSize(double width, double height)
		{
		}

		/// <summary>
		/// Stops the video from playing.
		/// </summary>
		public void stop()
		{
		}

		#endregion

		#region Constructors
		#endregion

		#region Fields
		/// <summary>
		/// A number that specifies which video player instance is affected by other application programming interfaces (APIs).
		/// </summary>
		public uint activeVideoPlayerIndex;

		/// <summary>
		/// Specifies the video layout when the scaleMode property is set to VideoScaleMode.MAINTAIN_ASPECT_RATIO or VideoScaleMode.NO_SCALE.
		/// </summary>
		public string align;

		/// <summary>
		/// A Boolean value that, if set to true, causes the FLV file to start playing automatically after the source property is set.
		/// </summary>
		public bool autoPlay;

		/// <summary>
		/// A Boolean value that, if true, causes the FLV file to rewind to Frame 1 when play stops, either because the player reached the end of the stream or the stop() method was called.
		/// </summary>
		public bool autoRewind;

		/// <summary>
		/// BackButton playback control.
		/// </summary>
		public Sprite backButton;

		/// <summary>
		/// A number that specifies the bits per second at which to transfer the FLV file.
		/// </summary>
		public double bitrate;

		/// <summary>
		/// [read-only] A Boolean value that is true if the video is in a buffering state.
		/// </summary>
		public readonly bool buffering;

		/// <summary>
		/// Buffering bar control.
		/// </summary>
		public Sprite bufferingBar;

		/// <summary>
		/// If set to true, hides the SeekBar control and disables the Play, Pause, PlayPause, BackButton and ForwardButton controls while the FLV file is in the buffering state.
		/// </summary>
		public bool bufferingBarHidesAndDisablesOthers;

		/// <summary>
		/// A number that specifies the number of seconds to buffer in memory before beginning to play a video stream.
		/// </summary>
		public double bufferTime;

		/// <summary>
		/// [read-only] A number that indicates the extent of downloading, in number of bytes, for an HTTP download.
		/// </summary>
		public readonly uint bytesLoaded;

		/// <summary>
		/// [read-only] A number that specifies the total number of bytes downloaded for an HTTP download.
		/// </summary>
		public readonly uint bytesTotal;

		/// <summary>
		/// [write-only] An array that describes ActionScript cue points and disabled embedded FLV file cue points.
		/// </summary>
		public Array cuePoints;

		/// <summary>
		/// Forward button control.
		/// </summary>
		public Sprite forwardButton;

		/// <summary>
		/// Background color used when in full-screen takeover mode.
		/// </summary>
		public uint fullScreenBackgroundColor;

		/// <summary>
		/// FullScreen button control.
		/// </summary>
		public Sprite fullScreenButton;

		/// <summary>
		/// Specifies the delay time in milliseconds to hide the skin.
		/// </summary>
		public int fullScreenSkinDelay;

		/// <summary>
		/// When the stage enters full-screen mode, the FLVPlayback component is on top of all content and takes over the entire screen.
		/// </summary>
		public bool fullScreenTakeOver;

		/// <summary>
		/// A number that specifies the height of the FLVPlayback instance.
		/// </summary>
		public double height;

		/// <summary>
		/// The amount of time, in milliseconds, before Flash terminates an idle connection to Flash Media Server (FMS) because playing paused or stopped.
		/// </summary>
		public double idleTimeout;

		/// <summary>
		/// A Boolean value that is true if the video stream is live.
		/// </summary>
		public bool isLive;

		/// <summary>
		/// [read-only] A Boolean value that is true if the FLV file is streaming from Flash Media Server (FMS) using RTMP.
		/// </summary>
		public readonly bool isRTMP;

		/// <summary>
		/// [read-only] An object that is a metadata information packet that is received from a call to the NetSteam.onMetaData() callback method, if available.
		/// </summary>
		public readonly object metadata;

		/// <summary>
		/// [read-only] A Boolean value that is true if a metadata packet has been encountered and processed or if the FLV file was encoded without the metadata packet.
		/// </summary>
		public readonly bool metadataLoaded;

		/// <summary>
		/// Mute button control.
		/// </summary>
		public Sprite muteButton;

		/// <summary>
		/// [read-only] An INCManager object that provides access to an instance of the class implementing INCManager, which is an interface to the NCManager class.
		/// </summary>
		public readonly INCManager ncMgr;

		/// <summary>
		/// Pause button control.
		/// </summary>
		public Sprite pauseButton;

		/// <summary>
		/// [read-only] A Boolean value that is true if the FLV file is in a paused state.
		/// </summary>
		public readonly bool paused;

		/// <summary>
		/// Play button control.
		/// </summary>
		public Sprite playButton;

		/// <summary>
		/// A number that specifies the current playheadTime as a percentage of the totalTime property.
		/// </summary>
		public double playheadPercentage;

		/// <summary>
		/// A number that is the current playhead time or position, measured in seconds, which can be a fractional value.
		/// </summary>
		public double playheadTime;

		/// <summary>
		/// A number that is the amount of time, in milliseconds, between each playheadUpdate event.
		/// </summary>
		public double playheadUpdateInterval;

		/// <summary>
		/// [read-only] A Boolean value that is true if the FLV file is in the playing state.
		/// </summary>
		public readonly bool playing;

		/// <summary>
		/// Play/pause button control.
		/// </summary>
		public Sprite playPauseButton;

		/// <summary>
		/// [read-only] A number that specifies the height of the source FLV file.
		/// </summary>
		public readonly int preferredHeight;

		/// <summary>
		/// [read-only] Gives the width of the source FLV file.
		/// </summary>
		public readonly int preferredWidth;

		/// <summary>
		/// [write-only] Only for live preview.
		/// </summary>
		public string preview;

		/// <summary>
		/// A number that is the amount of time, in milliseconds, between each progress event.
		/// </summary>
		public double progressInterval;

		/// <summary>
		/// The height used to align the video content when autoresizing.
		/// </summary>
		public double registrationHeight;

		/// <summary>
		/// The width used to align the video content when autoresizing.
		/// </summary>
		public double registrationWidth;

		/// <summary>
		/// The x coordinate used to align the video content when autoresizing.
		/// </summary>
		public double registrationX;

		/// <summary>
		/// The y coordinate used to align the video content when autoresizing.
		/// </summary>
		public double registrationY;

		/// <summary>
		/// Specifies how the video will resize after loading.
		/// </summary>
		public string scaleMode;

		/// <summary>
		/// A number that is the horizontal scale.
		/// </summary>
		public double scaleX;

		/// <summary>
		/// A number that is the vertical scale.
		/// </summary>
		public double scaleY;

		/// <summary>
		/// [read-only] A Boolean value that is true if the user is scrubbing with the SeekBar and false otherwise.
		/// </summary>
		public readonly bool scrubbing;

		/// <summary>
		/// The SeekBar control.
		/// </summary>
		public Sprite seekBar;

		/// <summary>
		/// A number that specifies, in milliseconds, how often to check the SeekBar handle when scrubbing.
		/// </summary>
		public double seekBarInterval;

		/// <summary>
		/// A number that specifies how far a user can move the SeekBar handle before an update occurs.
		/// </summary>
		public double seekBarScrubTolerance;

		/// <summary>
		/// The number of seconds that the seekToPrevNavCuePoint() method uses when it compares its time against the previous cue point.
		/// </summary>
		public double seekToPrevOffset;

		/// <summary>
		/// A string that specifies the URL to a skin SWF file.
		/// </summary>
		public string skin;

		/// <summary>
		/// A Boolean value that, if true, hides the component skin when the mouse is not over the video.
		/// </summary>
		public bool skinAutoHide;

		/// <summary>
		/// The alpha for the background of the skin.
		/// </summary>
		public double skinBackgroundAlpha;

		/// <summary>
		/// The color for the background of the skin (0xRRGGBB).
		/// </summary>
		public uint skinBackgroundColor;

		/// <summary>
		/// The amount of time in milliseconds that it takes for the skin to fade in or fade out when hiding or showing.
		/// </summary>
		public int skinFadeTime;

		/// <summary>
		/// This property specifies the largest multiple that FLVPlayback will use to scale up its skin when it enters full screen mode with a Flash Player that supports hardware acceleration.
		/// </summary>
		public double skinScaleMaximum;

		/// <summary>
		/// Provides direct access to the VideoPlayer.soundTransform property to expose more sound control.
		/// </summary>
		public SoundTransform soundTransform;

		/// <summary>
		/// A string that specifies the URL of the FLV file to stream and how to stream it.
		/// </summary>
		public string source;

		/// <summary>
		/// [read-only] A string that specifies the state of the component.
		/// </summary>
		public readonly string state;

		/// <summary>
		/// [read-only] A Boolean value that is true if the state is responsive.
		/// </summary>
		public readonly bool stateResponsive;

		/// <summary>
		/// The Stop button control.
		/// </summary>
		public Sprite stopButton;

		/// <summary>
		/// [read-only] A Boolean value that is true if the state of the FLVPlayback instance is stopped.
		/// </summary>
		public readonly bool stopped;

		/// <summary>
		/// A number that is the total playing time for the video in seconds.
		/// </summary>
		public double totalTime;

		/// <summary>
		/// A number that you can use to manage multiple FLV file streams.
		/// </summary>
		public uint visibleVideoPlayerIndex;

		/// <summary>
		/// A number in the range of 0 to 1 that indicates the volume control setting.
		/// </summary>
		public double volume;

		/// <summary>
		/// The volume bar control.
		/// </summary>
		public Sprite volumeBar;

		/// <summary>
		/// A number that specifies, in milliseconds, how often to check the volume bar handle location when scrubbing.
		/// </summary>
		public double volumeBarInterval;

		/// <summary>
		/// A number that specifies how far a user can move the volume bar handle before an update occurs.
		/// </summary>
		public double volumeBarScrubTolerance;

		/// <summary>
		/// A number that specifies the width of the FLVPlayback instance on the Stage.
		/// </summary>
		public double width;

		/// <summary>
		/// A number that specifies the horizontal position (in pixels) of the video player.
		/// </summary>
		public double x;

		/// <summary>
		/// A number that specifies the vertical position (in pixels) of the video player.
		/// </summary>
		public double y;

		#endregion
	}
}
