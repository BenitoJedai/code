using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
	// http://livedocs.adobe.com/flex/3/langref/flash/display/GradientType.html
	[Script(IsNative = true)]
	public static class GradientType
	{
		#region Constants
		/// <summary>
		/// [static] Value used to specify a linear gradient fill.
		/// </summary>
		public static readonly string LINEAR = "linear";

		/// <summary>
		/// [static] Value used to specify a radial gradient fill.
		/// </summary>
		public static readonly string RADIAL = "radial";

		#endregion

	}
}
