using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/display/MovieClip.html
    [Script(IsNative = true)]
    public class MovieClip : Sprite
    {
		#region Properties
		/// <summary>
		/// [read-only] Specifies the number of the frame in which the playhead is located in the timeline of the MovieClip instance.
		/// </summary>
		public int currentFrame { get; private set; }

		/// <summary>
		/// [read-only] The current label in which the playhead is located in the timeline of the MovieClip instance.
		/// </summary>
		public string currentLabel { get; private set; }

		/// <summary>
		/// [read-only] Returns an array of FrameLabel objects from the current scene.
		/// </summary>
		public FrameLabel currentLabels { get; private set; }

		/// <summary>
		/// [read-only] The current scene in which the playhead is located in the timeline of the MovieClip instance.
		/// </summary>
		public Scene currentScene { get; private set; }

		/// <summary>
		/// A Boolean value that indicates whether a movie clip is enabled.
		/// </summary>
		public bool enabled { get; set; }

		/// <summary>
		/// [read-only] The number of frames that are loaded from a streaming SWF file.
		/// </summary>
		public int framesLoaded { get; private set; }

		/// <summary>
		/// [read-only] An array of Scene objects, each listing the name, the number of frames, and the frame labels for a scene in the MovieClip instance.
		/// </summary>
		public Scene[] scenes { get; private set; }

		/// <summary>
		/// [read-only] The total number of frames in the MovieClip instance.
		/// </summary>
		public int totalFrames { get; private set; }

		/// <summary>
		/// Indicates whether other display objects that are SimpleButton or MovieClip objects can receive mouse release events.
		/// </summary>
		public bool trackAsMenu { get; set; }

		#endregion


		#region Methods
		/// <summary>
		/// Moves the playhead in the timeline of the movie clip.
		/// </summary>
		public void play()
		{
		}

		/// <summary>
		/// Stops the playhead in the movie clip.
		/// </summary>
		public void stop()
		{
		}

		public void nextFrame()
		{
		}
		#endregion
    }
}
