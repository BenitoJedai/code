using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.geom
{
	[Script(IsNative = true)]
	public class Quaternion
	{
		#region Fields
		/// <summary>
		/// [read-only] The conjugate of this quaternion.
		/// </summary>
		public readonly Quaternion conjugate;

		/// <summary>
		/// [static]
		/// </summary>
		public static Quaternion IDENTITY;

		/// <summary>
		/// [read-only] The inverse of this quaternion.
		/// </summary>
		public readonly Quaternion inverse;

		/// <summary>
		/// [read-only] The magnitude of this quaternion.
		/// </summary>
		public readonly double magnitude;

		/// <summary>
		/// [read-only] The square of the magnitude of this quaternion.
		/// </summary>
		public readonly double magnitudeSquared;

		/// <summary>
		/// The w coordinate of the quaternion.
		/// </summary>
		public double w;

		/// <summary>
		/// The x coordinate of the quaternion.
		/// </summary>
		public double x;

		/// <summary>
		/// The y coordinate of the quaternion.
		/// </summary>
		public double y;

		/// <summary>
		/// The z coordinate of the quaternion.
		/// </summary>
		public double z;

		/// <summary>
		/// [static]
		/// </summary>
		public static Quaternion ZERO;

		#endregion

	}
}
