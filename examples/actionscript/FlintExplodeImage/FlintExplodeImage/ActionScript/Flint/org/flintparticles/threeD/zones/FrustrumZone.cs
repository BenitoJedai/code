using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.geom;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.renderers;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.zones
{
	[Script(IsNative = true)]
	public class FrustrumZone : Zone3D
	{
		#region Fields
		/// <summary>
		/// The flint camera whose frustrum should be used.
		/// </summary>
		public Camera camera;

		/// <summary>
		/// The rectangle describing the size and position of the viewing window.
		/// </summary>
		public Rectangle viewRect;

		#endregion

		#region Methods
		/// <summary>
		/// The contains method determines whether a point is inside the box.
		/// </summary>
		public bool contains(Vector3D p)
		{
			return default(bool);
		}

		/// <summary>
		/// The getLocation method returns a random point inside the box.
		/// </summary>
		public Vector3D getLocation()
		{
			return default(Vector3D);
		}

		/// <summary>
		/// The getArea method returns the volume of the box.
		/// </summary>
		public double getVolume()
		{
			return default(double);
		}

		#endregion

		#region Constructors
		/// <summary>
		/// The constructor creates a FrustrumZone zone.
		/// </summary>
		public FrustrumZone(Camera camera, Rectangle viewRect)
		{
		}

		#endregion

	}
}
