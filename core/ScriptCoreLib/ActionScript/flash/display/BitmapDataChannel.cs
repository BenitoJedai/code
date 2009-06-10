using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
	// http://livedocs.adobe.com/flex/3/langref/flash/display/BitmapDataChannel.html
	[Script(IsNative = true)]
	public static class BitmapDataChannel
	{
		#region Constants
		/// <summary>
		/// [static] The alpha channel.
		/// </summary>
		public static readonly uint ALPHA = 8;

		/// <summary>
		/// [static] The blue channel.
		/// </summary>
		public static readonly uint BLUE = 4;

		/// <summary>
		/// [static] The green channel.
		/// </summary>
		public static readonly uint GREEN = 2;

		/// <summary>
		/// [static] The red channel.
		/// </summary>
		public static readonly uint RED = 1;

		#endregion

	}
}
