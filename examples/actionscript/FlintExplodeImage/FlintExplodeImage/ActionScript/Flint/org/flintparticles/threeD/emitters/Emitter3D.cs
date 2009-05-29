using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.emitters;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.particles;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.geom;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.emitters
{
	[Script(IsNative = true)]
	public class Emitter3D : Emitter
	{
		#region Properties
		/// <summary>
		/// [static][read-only] The default particle factory used to manage the creation, reuse and destruction of particles.
		/// </summary>
		public static ParticleFactory defaultParticleFactory { get; private set; }

		/// <summary>
		/// Indicates the position of the Emitter instance relative to the local coordinate system of the Renderer.
		/// </summary>
		public Vector3D position { get; set; }

		/// <summary>
		/// Indicates the rotation of the Emitter instance relative to the local coordinate system of the Renderer.
		/// </summary>
		public Quaternion rotation { get; set; }

		/// <summary>
		/// [read-only] Indicates the rotation of the Emitter instance relative to the local coordinate system of the Renderer, as a matrix transformation.
		/// </summary>
		public Matrix3D rotationTransform { get; private set; }

		/// <summary>
		/// Identifies whether the particles should be arranged into a spacially sorted array - this speeds up proximity testing for those actions that need it.
		/// </summary>
		public bool spaceSort { get; set; }

		/// <summary>
		/// The array of particle indices sorted based on the particles' x positions.
		/// </summary>
		public Array spaceSortedX { get; set; }

		#endregion

	}
}
