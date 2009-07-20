using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;

namespace ScriptCoreLib.ActionScript.flash.events
{
	// http://livedocs.adobe.com/flex/3/langref/flash/events/SampleDataEvent.html
	[Script(IsNative = true)]
	public class SampleDataEvent
	{
		#region Properties
		/// <summary>
		/// The data in the audio stream.
		/// </summary>
		public ByteArray data { get; set; }

		/// <summary>
		/// The position of the data in the audio stream.
		/// </summary>
		public double position { get; set; }

		#endregion

		#region Constants
		/// <summary>
		/// [static] Defines the value of the type property of a SampleDataEvent event object.
		/// </summary>
		public static readonly string SAMPLE_DATA = "sampleData";

		#endregion

	}
}
