using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
	// http://livedocs.adobe.com/flex/2/langref/flash/display/FrameLabel.html
	[Script(IsNative = true)]
	public class FrameLabel
	{
		#region Properties
		/// <summary>
		/// [read-only] The frame number containing the label.
		/// </summary>
		public int frame { get; private set; }

		/// <summary>
		/// [read-only] The name of the label.
		/// </summary>
		public string name { get; private set; }

		#endregion

	}
}
