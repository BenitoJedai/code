using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.filters
{
	// http://livedocs.adobe.com/flex/3/langref/flash/filters/DisplacementMapFilterMode.html
	[Script(IsNative = true)]
	public static class DisplacementMapFilterMode
	{
		#region Constants
		/// <summary>
		/// [static] Clamps the displacement value to the edge of the source image.
		/// </summary>
		public static readonly string CLAMP = "clamp";

		/// <summary>
		/// [static] If the displacement value is outside the image, substitutes the values in the color and alpha properties.
		/// </summary>
		public static readonly string COLOR = "color";

		/// <summary>
		/// [static] If the displacement value is out of range, ignores the displacement and uses the source pixel.
		/// </summary>
		public static readonly string IGNORE = "ignore";

		/// <summary>
		/// [static] Wraps the displacement value to the other side of the source image.
		/// </summary>
		public static readonly string WRAP = "wrap";

		#endregion

	}
}
