using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.system
{
	[Script(IsNative = true)]
	public static class Capabilities
	{
		#region Properties
		/// <summary>
		/// [static] [read-only] Specifies whether access to the user's camera and microphone has been administratively prohibited (true) or allowed (false).
		/// </summary>
		public static bool avHardwareDisable { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system supports (true) or does not support (false) communication with accessibility aids.
		/// </summary>
		public static bool hasAccessibility { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system has audio capabilities.
		/// </summary>
		public static bool hasAudio { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system can (true) or cannot (false) encode an audio stream, such as that coming from a microphone.
		/// </summary>
		public static bool hasAudioEncoder { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system supports (true) or does not support (false) embedded video.
		/// </summary>
		public static bool hasEmbeddedVideo { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system does (true) or does not (false) have an input method editor (IME) installed.
		/// </summary>
		public static bool hasIME { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system does (true) or does not (false) have an MP3 decoder.
		/// </summary>
		public static bool hasMP3 { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system does (true) or does not (false) support printing.
		/// </summary>
		public static bool hasPrinting { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system does (true) or does not (false) support the development of screen broadcast applications to be run through Flash Media Server.
		/// </summary>
		public static bool hasScreenBroadcast { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system does (true) or does not (false) support the playback of screen broadcast applications that are being run through Flash Media Server.
		/// </summary>
		public static bool hasScreenPlayback { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system can (true) or cannot (false) play streaming audio.
		/// </summary>
		public static bool hasStreamingAudio { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system can (true) or cannot (false) play streaming video.
		/// </summary>
		public static bool hasStreamingVideo { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system supports native SSL sockets through NetConnection (true) or does not (false).
		/// </summary>
		public static bool hasTLS { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system can (true) or cannot (false) encode a video stream, such as that coming from a web camera.
		/// </summary>
		public static bool hasVideoEncoder { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the system is a special debugging version (true) or an officially released version (false).
		/// </summary>
		public static bool isDebugger { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether the player is embedded in a PDF file that is open in Acrobat 9.0 or higher (true) or not (false).
		/// </summary>
		public static bool isEmbeddedInAcrobat { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies the language code of the system on which the content is running.
		/// </summary>
		public static string language { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies whether read access to the user's hard disk has been administratively prohibited (true) or allowed (false).
		/// </summary>
		public static bool localFileReadDisable { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies the manufacturer of the running version of Flash Player or the AIR runtime, in the format "Adobe OSName".
		/// </summary>
		public static string manufacturer { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies the current operating system.
		/// </summary>
		public static string os { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies the pixel aspect ratio of the screen.
		/// </summary>
		public static double pixelAspectRatio { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies the type of runtime environment.
		/// </summary>
		public static string playerType { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies the screen color.
		/// </summary>
		public static string screenColor { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies the dots-per-inch (dpi) resolution of the screen, in pixels.
		/// </summary>
		public static double screenDPI { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies the maximum horizontal resolution of the screen.
		/// </summary>
		public static double screenResolutionX { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies the maximum vertical resolution of the screen.
		/// </summary>
		public static double screenResolutionY { get; private set; }

		/// <summary>
		/// [static] [read-only] A URL-encoded string that specifies values for each Capabilities property.
		/// </summary>
		public static string serverString { get; private set; }

		/// <summary>
		/// [static] [read-only] Specifies the Flash Player or Adobe® AIR platform and version information.
		/// </summary>
		public static string version { get; private set; }

		#endregion

	}
}
