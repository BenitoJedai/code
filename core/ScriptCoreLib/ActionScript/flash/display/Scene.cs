using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
	// http://livedocs.adobe.com/flex/2/langref/flash/display/Scene.html
	[Script(IsNative = true)]
	public class Scene
	{
		#region Properties
		/// <summary>
		/// [read-only] An array of FrameLabel objects for the scene.
		/// </summary>
		public FrameLabel[] labels { get; private set; }

		/// <summary>
		/// [read-only] The name of the scene.
		/// </summary>
		public string name { get; private set; }

		/// <summary>
		/// [read-only] The number of frames in the scene.
		/// </summary>
		public int numFrames { get; private set; }

		#endregion

	}
}
