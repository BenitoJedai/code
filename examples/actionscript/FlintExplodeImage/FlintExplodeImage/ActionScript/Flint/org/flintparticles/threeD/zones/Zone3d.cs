using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.zones
{
	[Script(IsNative = true)]
	public interface Zone3D
	{
		#region Methods
		/// <summary>
		/// The contains method determines whether a point is inside the zone.
		/// </summary>
		bool contains(Vector3D p);

		/// <summary>
		/// The getLocation method returns a random point inside the zone.
		/// </summary>
		Vector3D getLocation();

		/// <summary>
		/// The getArea method returns the size of the zone.
		/// </summary>
		double getVolume();

		#endregion


	}
}
