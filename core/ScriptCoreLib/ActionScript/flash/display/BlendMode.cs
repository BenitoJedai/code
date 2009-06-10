using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
	// http://livedocs.adobe.com/flex/3/langref/flash/display/BlendMode.html
	[Script(IsNative = true)]
	public static class BlendMode
	{
		#region Constants
		/// <summary>
		/// [static] Adds the values of the constituent colors of the display object to the colors of its background, applying a ceiling of 0xFF.
		/// </summary>
		public static readonly string ADD = "add";

		/// <summary>
		/// [static] Applies the alpha value of each pixel of the display object to the background.
		/// </summary>
		public static readonly string ALPHA = "alpha";

		/// <summary>
		/// [static] Selects the darker of the constituent colors of the display object and the colors of the background (the colors with the smaller values).
		/// </summary>
		public static readonly string DARKEN = "darken";

		/// <summary>
		/// [static] Compares the constituent colors of the display object with the colors of its background, and subtracts the darker of the values of the two constituent colors from the lighter value.
		/// </summary>
		public static readonly string DIFFERENCE = "difference";

		/// <summary>
		/// [static] Erases the background based on the alpha value of the display object.
		/// </summary>
		public static readonly string ERASE = "erase";

		/// <summary>
		/// [static] Adjusts the color of each pixel based on the darkness of the display object.
		/// </summary>
		public static readonly string HARDLIGHT = "hardlight";

		/// <summary>
		/// [static] Inverts the background.
		/// </summary>
		public static readonly string INVERT = "invert";

		/// <summary>
		/// [static] Forces the creation of a transparency group for the display object.
		/// </summary>
		public static readonly string LAYER = "layer";

		/// <summary>
		/// [static] Selects the lighter of the constituent colors of the display object and the colors of the background (the colors with the larger values).
		/// </summary>
		public static readonly string LIGHTEN = "lighten";

		/// <summary>
		/// [static] Multiplies the values of the display object constituent colors by the constituent colors of the background color, and normalizes by dividing by 0xFF, resulting in darker colors.
		/// </summary>
		public static readonly string MULTIPLY = "multiply";

		/// <summary>
		/// [static] The display object appears in front of the background.
		/// </summary>
		public static readonly string NORMAL = "normal";

		/// <summary>
		/// [static] Adjusts the color of each pixel based on the darkness of the background.
		/// </summary>
		public static readonly string OVERLAY = "overlay";

		/// <summary>
		/// [static] Multiplies the complement (inverse) of the display object color by the complement of the background color, resulting in a bleaching effect.
		/// </summary>
		public static readonly string SCREEN = "screen";

		/// <summary>
		/// [static] Uses a shader to define the blend between objects.
		/// </summary>
		public static readonly string SHADER = "shader";

		/// <summary>
		/// [static] Subtracts the values of the constituent colors in the display object from the values of the background color, applying a floor of 0.
		/// </summary>
		public static readonly string SUBTRACT = "subtract";

		#endregion

	}
}
