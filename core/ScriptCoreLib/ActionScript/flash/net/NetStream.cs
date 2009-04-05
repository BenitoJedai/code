using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.media;

namespace ScriptCoreLib.ActionScript.flash.net
{
	// http://livedocs.adobe.com/flex/3/langref/flash/net/NetStream.html
	[Script(IsNative = true)]
	public class NetStream : EventDispatcher
	{
		#region Properties
		/// <summary>
		/// [read-only] The number of seconds of data currently in the buffer.
		/// </summary>
		public double bufferLength { get; private set; }

		/// <summary>
		/// Specifies how long to buffer messages before starting to display the stream.
		/// </summary>
		public double bufferTime { get; set; }

		/// <summary>
		/// [read-only] The number of bytes of data that have been loaded into the application.
		/// </summary>
		public uint bytesLoaded { get; private set; }

		/// <summary>
		/// [read-only] The total size in bytes of the file being loaded into the application.
		/// </summary>
		public uint bytesTotal { get; private set; }

		/// <summary>
		/// Specifies whether the application should try to download a cross-domain policy file from the loaded video file's server before beginning to load the video file.
		/// </summary>
		public bool checkPolicyFile { get; set; }

		/// <summary>
		/// Specifies the object on which callback methods are invoked to handle streaming or FLV file data.
		/// </summary>
		public object client { get; set; }

		/// <summary>
		/// [read-only] The number of frames per second being displayed.
		/// </summary>
		public double currentFPS { get; private set; }

		/// <summary>
		/// [read-only] The identifier of the far end that is connected to this NetStream instance.
		/// </summary>
		public string farID { get; private set; }

		/// <summary>
		/// [read-only] A value chosen substantially by the other end of this stream, unique to this connection.
		/// </summary>
		public string farNonce { get; private set; }

		/// <summary>
		/// [read-only] Returns a NetStreamInfo object whose properties contain statistics about the quality of service.
		/// </summary>
		public NetStreamInfo info { get; private set; }

		/// <summary>
		/// [read-only] The number of seconds of data in the subscribing stream's buffer in live (unbuffered) mode.
		/// </summary>
		public double liveDelay { get; private set; }

		/// <summary>
		/// Specifies how long to buffer messages during pause mode, in seconds.
		/// </summary>
		public double maxPauseBufferTime { get; set; }

		/// <summary>
		/// [read-only] A value chosen substantially by this end of the stream, unique to this connection.
		/// </summary>
		public string nearNonce { get; private set; }

		/// <summary>
		/// [read-only] The object encoding (AMF version) for this NetStream object.
		/// </summary>
		public uint objectEncoding { get; private set; }

		/// <summary>
		/// [read-only] An object that holds all of the subscribing NetStream instances that are listening to this publishing NetStream instance.
		/// </summary>
		public NetStream[] peerStreams { get; private set; }

		/// <summary>
		/// Controls sound in this NetStream object.
		/// </summary>
		public SoundTransform soundTransform { get; set; }

		/// <summary>
		/// [read-only] The position of the playhead, in seconds.
		/// </summary>
		public double time { get; private set; }

		#endregion

		#region Methods
		/// <summary>
		/// Specifies an audio stream sent over the NetStream object, from a Microphone object passed as the source.
		/// </summary>
		public void attachAudio(Microphone microphone)
		{
		}

		/// <summary>
		/// Starts capturing video from a camera, or stops capturing if theCamera is set to null.
		/// </summary>
		public void attachCamera(Camera theCamera, int snapshotMilliseconds)
		{
		}

		/// <summary>
		/// Starts capturing video from a camera, or stops capturing if theCamera is set to null.
		/// </summary>
		public void attachCamera(Camera theCamera)
		{
		}

		/// <summary>
		/// Stops playing all data on the stream, sets the time property to 0, and makes the stream available for another use.
		/// </summary>
		public void close()
		{
		}

		/// <summary>
		/// Pauses playback of a video stream.
		/// </summary>
		public void pause()
		{
		}

		/// <summary>
		/// Begins playback of video files.
		/// </summary>
		public void play(/* params */ object arguments)
		{
		}

		/// <summary>
		/// Begins playback of video files.
		/// </summary>
		public void play()
		{
		}

		/// <summary>
		/// Sends streaming audio, video, and text messages from a client to Flash Media Server, optionally recording the stream during transmission.
		/// </summary>
		public void publish(string name, string type)
		{
		}

		/// <summary>
		/// Sends streaming audio, video, and text messages from a client to Flash Media Server, optionally recording the stream during transmission.
		/// </summary>
		public void publish(string name)
		{
		}

		/// <summary>
		/// Sends streaming audio, video, and text messages from a client to Flash Media Server, optionally recording the stream during transmission.
		/// </summary>
		public void publish()
		{
		}

		/// <summary>
		/// Specifies whether incoming audio plays on the stream.
		/// </summary>
		public void receiveAudio(bool flag)
		{
		}

		/// <summary>
		/// Specifies whether incoming video will play on the stream.
		/// </summary>
		public void receiveVideo(bool flag)
		{
		}

		/// <summary>
		/// Specifies the frame rate for incoming video.
		/// </summary>
		public void receiveVideoFPS(double FPS)
		{
		}

		/// <summary>
		/// Resumes playback of a video stream that is paused.
		/// </summary>
		public void resume()
		{
		}

		/// <summary>
		/// Seeks the keyframe (also called an I-frame in the video industry) closest to the specified location.
		/// </summary>
		public void seek(double offset)
		{
		}

		/// <summary>
		/// Sends a message on a published stream to all subscribing clients.
		/// </summary>
		public void send(string handlerName, /* params */ object arguments)
		{
		}

		/// <summary>
		/// Sends a message on a published stream to all subscribing clients.
		/// </summary>
		public void send(string handlerName)
		{
		}

		/// <summary>
		/// Pauses or resumes playback of a stream.
		/// </summary>
		public void togglePause()
		{
		}

		#endregion

		#region Constructors
		/// <summary>
		/// Creates a stream that can be used for playing video files through the specified NetConnection object.
		/// </summary>
		public NetStream(NetConnection connection)
		{
		}

		public NetStream(NetConnection connection, string peerID)
		{
		}

		#endregion

		#region Events
		/// <summary>
		/// Dispatched when an exception is thrown asynchronously — that is, from native asynchronous code.
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<AsyncErrorEvent> asyncError;

		/// <summary>
		/// Dispatched when an input or output error occurs that causes a network operation to fail.
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<IOErrorEvent> ioError;

		/// <summary>
		/// Dispatched when a NetStream object is reporting its status or error condition.
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<NetStatusEvent> netStatus;

		#endregion

		/// <summary>
		/// A static object used as a parameter to the constructor for a NetStream instance.
		/// </summary>
		public const string CONNECT_TO_FMS = "connectToFMS";

		/// <summary>
		/// Creates a peer-to-peer publisher connection.
		/// </summary>
 	 	public const string DIRECT_CONNECTIONS = "directConnections";

	}
}
