using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.renderers
{
	[Script(IsNative = true)]
	public class Camera
	{
		#region Properties
		/// <summary>
		/// The direction the camera is pointing.
		/// </summary>
		public Vector3D direction { get; set; }

		/// <summary>
		/// The distance to the camera's far plane - particles farther away than this are not rendered.
		/// </summary>
		public double farPlaneDistance { get; set; }

		/// <summary>
		/// The distance to the camera's near plane - particles closer than this are not rendered.
		/// </summary>
		public double nearPlaneDistance { get; set; }

		/// <summary>
		/// The location of the camera.
		/// </summary>
		public Vector3D position { get; set; }

		/// <summary>
		/// The distance to the camera's projection distance.
		/// </summary>
		public double projectionDistance { get; set; }

		/// <summary>
		/// [read-only] The transform matrix that converts positions in world space to positions in camera space.
		/// </summary>
		public Matrix3D spaceTransform { get; private set; }

		/// <summary>
		/// The point that the camera looks at.
		/// </summary>
		public Vector3D target { get; set; }

		/// <summary>
		/// [read-only] The transform matrix that converts positions in world space to positions in camera space.
		/// </summary>
		public Matrix3D transform { get; private set; }

		/// <summary>
		/// The up direction for the camera.
		/// </summary>
		public Vector3D up { get; set; }

		#endregion

		#region Methods
		/// <summary>
		/// Dolly or Track the camera in/out in the direction it's facing.
		/// </summary>
		public void dolly(double distance)
		{
		}

		/// <summary>
		/// Raise or lower the camera.
		/// </summary>
		public void lift(double distance)
		{
		}

		/// <summary>
		/// Orbit the camera around the target.
		/// </summary>
		public void orbit(double angle)
		{
		}

		/// <summary>
		/// Pan the camera left or right.
		/// </summary>
		public void pan(double angle)
		{
		}

		/// <summary>
		/// Roll the camera clockwise or counter-clockwise.
		/// </summary>
		public void roll(double angle)
		{
		}

		/// <summary>
		/// Tilt the camera up or down.
		/// </summary>
		public void tilt(double angle)
		{
		}

		/// <summary>
		/// Dolly or Track the camera left/right.
		/// </summary>
		public void track(double distance)
		{
		}

		#endregion

		#region Constructors
		/// <summary>
		/// The constructor creates a Camera object.
		/// </summary>
		public Camera()
		{
		}

		#endregion


	}
}
