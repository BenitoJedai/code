using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.filters
{
	// http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/filters/ColorMatrixFilter.html#methodSummary
	[Script(IsNative = true)]
	public class ColorMatrixFilter : BitmapFilter
	{
		#region Properties
		/// <summary>
		/// An array of 20 items for 4 x 5 color transform.
		/// </summary>
		public double[] matrix { get; set; }

		#endregion

		#region Methods
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new ColorMatrixFilter instance with the specified parameters.
		/// </summary>
		public ColorMatrixFilter(params double[] matrix)
		{
		}

		/// <summary>
		/// Initializes a new ColorMatrixFilter instance with the specified parameters.
		/// </summary>
		public ColorMatrixFilter()
		{
		}

		#endregion

	}
}
