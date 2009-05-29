using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.emitters;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.particles;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.actions;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.geom;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.actions
{
	[Script(IsNative = true)]
	public class Explosion : ActionBase
	{
		#region Fields
		/// <summary>
		/// The center of the explosion.
		/// </summary>
		public Vector3D center;

		/// <summary>
		/// The strength of the explosion - larger numbers produce a stronger force.
		/// </summary>
		public double depth;

		/// <summary>
		/// The minimum distance for which the explosion force is calculated.
		/// </summary>
		public double epsilon;

		/// <summary>
		/// The strength of the explosion - larger numbers produce a stronger force.
		/// </summary>
		public double expansionRate;

		/// <summary>
		/// The strength of the explosion - larger numbers produce a stronger force.
		/// </summary>
		public double power;

		#endregion

		#region Methods
		/// <summary>
		/// This method does nothing.
		/// </summary>
		public void addedToEmitter(Emitter emitter)
		{
		}

		/// <summary>
		/// Called every frame before the particles are updated.
		/// </summary>
		public void frameUpdate(Emitter emitter, double time)
		{
		}

		/// <summary>
		/// This method does nothing.
		/// </summary>
		public void removedFromEmitter(Emitter emitter)
		{
		}

		/// <summary>
		/// This method does nothing.
		/// </summary>
		public void update(Emitter emitter, Particle particle, double time)
		{
		}

		#endregion

		#region Constructors
		/// <summary>
		/// The constructor creates an Explosion action for use by an emitter.
		/// </summary>
		public Explosion(double power, Vector3D center, double expansionRate, double depth, double epsilon)
		{
		}

		/// <summary>
		/// The constructor creates an Explosion action for use by an emitter.
		/// </summary>
		public Explosion(double power, Vector3D center, double expansionRate, double depth)
		{
		}

		/// <summary>
		/// The constructor creates an Explosion action for use by an emitter.
		/// </summary>
		public Explosion(double power, Vector3D center, double expansionRate)
		{
		}

		/// <summary>
		/// The constructor creates an Explosion action for use by an emitter.
		/// </summary>
		public Explosion(double power, Vector3D center)
		{
		}

		#endregion

	}
}
