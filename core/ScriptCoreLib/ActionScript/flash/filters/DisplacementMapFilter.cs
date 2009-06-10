using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.flash.filters
{
	// http://livedocs.adobe.com/flex/3/langref/flash/filters/DisplacementMapFilter.html
	[Script(IsNative = true)]
	public class DisplacementMapFilter : BitmapFilter
	{
		#region Properties
		/// <summary>
		/// Specifies the alpha transparency value to use for out-of-bounds displacements.
		/// </summary>
		public double alpha { get; set; }

		/// <summary>
		/// Specifies what color to use for out-of-bounds displacements.
		/// </summary>
		public uint color { get; set; }

		/// <summary>
		/// Describes which color channel to use in the map image to displace the x result.
		/// </summary>
		public uint componentX { get; set; }

		/// <summary>
		/// Describes which color channel to use in the map image to displace the y result.
		/// </summary>
		public uint componentY { get; set; }

		/// <summary>
		/// A BitmapData object containing the displacement map data.
		/// </summary>
		public BitmapData mapBitmap { get; set; }

		/// <summary>
		/// A value that contains the offset of the upper-left corner of the target display object from the upper-left corner of the map image.
		/// </summary>
		public Point mapPoint { get; set; }

		/// <summary>
		/// The mode for the filter.
		/// </summary>
		public string mode { get; set; }

		/// <summary>
		/// The multiplier to use to scale the x displacement result from the map calculation.
		/// </summary>
		public double scaleX { get; set; }

		/// <summary>
		/// The multiplier to use to scale the y displacement result from the map calculation.
		/// </summary>
		public double scaleY { get; set; }

		#endregion

		#region Methods
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a DisplacementMapFilter instance with the specified parameters.
		/// </summary>
		public DisplacementMapFilter(BitmapData mapBitmap, Point mapPoint, uint componentX, uint componentY, double scaleX, double scaleY, string mode, uint color, double alpha)
		{
		}

		/// <summary>
		/// Initializes a DisplacementMapFilter instance with the specified parameters.
		/// </summary>
		public DisplacementMapFilter(BitmapData mapBitmap, Point mapPoint, uint componentX, uint componentY, double scaleX, double scaleY, string mode, uint color)
		{
		}

		/// <summary>
		/// Initializes a DisplacementMapFilter instance with the specified parameters.
		/// </summary>
		public DisplacementMapFilter(BitmapData mapBitmap, Point mapPoint, uint componentX, uint componentY, double scaleX, double scaleY, string mode)
		{
		}

		/// <summary>
		/// Initializes a DisplacementMapFilter instance with the specified parameters.
		/// </summary>
		public DisplacementMapFilter(BitmapData mapBitmap, Point mapPoint, uint componentX, uint componentY, double scaleX, double scaleY)
		{
		}

		/// <summary>
		/// Initializes a DisplacementMapFilter instance with the specified parameters.
		/// </summary>
		public DisplacementMapFilter(BitmapData mapBitmap, Point mapPoint, uint componentX, uint componentY, double scaleX)
		{
		}

		/// <summary>
		/// Initializes a DisplacementMapFilter instance with the specified parameters.
		/// </summary>
		public DisplacementMapFilter(BitmapData mapBitmap, Point mapPoint, uint componentX, uint componentY)
		{
		}

		/// <summary>
		/// Initializes a DisplacementMapFilter instance with the specified parameters.
		/// </summary>
		public DisplacementMapFilter(BitmapData mapBitmap, Point mapPoint, uint componentX)
		{
		}

		/// <summary>
		/// Initializes a DisplacementMapFilter instance with the specified parameters.
		/// </summary>
		public DisplacementMapFilter(BitmapData mapBitmap, Point mapPoint)
		{
		}

		/// <summary>
		/// Initializes a DisplacementMapFilter instance with the specified parameters.
		/// </summary>
		public DisplacementMapFilter(BitmapData mapBitmap)
		{
		}

		/// <summary>
		/// Initializes a DisplacementMapFilter instance with the specified parameters.
		/// </summary>
		public DisplacementMapFilter()
		{
		}

		#endregion

	}
}
