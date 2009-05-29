using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.particles
{
	[Script(IsNative = true)]
	public class Particle
	{
		#region Properties
		/// <summary>
		/// The age of the particle, in seconds.
		/// </summary>
		public double age { get; set; }

		/// <summary>
		/// [read-only]
		/// </summary>
		public double alpha { get; private set; }

		/// <summary>
		/// The radius of the particle, for collision approximation
		/// </summary>
		public double collisionRadius { get; set; }

		/// <summary>
		/// The 32bit ARGB color of the particle.
		/// </summary>
		public uint color { get; set; }

		/// <summary>
		/// [read-only] A ColorTransform object that converts white to the colour of the particle.
		/// </summary>
		public ColorTransform colorTransform { get; private set; }

		/// <summary>
		/// [read-only] The dictionary object enables actions and activities to add additional properties to the particle.
		/// </summary>
		public Dictionary dictionary { get; private set; }

		/// <summary>
		/// The energy of the particle.
		/// </summary>
		public double energy { get; set; }

		/// <summary>
		/// The object used to display the image.
		/// </summary>
		public object image { get; set; }

		/// <summary>
		/// Whether the particle is dead and should be removed from the stage.
		/// </summary>
		public bool isDead { get; set; }

		/// <summary>
		/// The lifetime of the particle, in seconds.
		/// </summary>
		public double lifetime { get; set; }

		/// <summary>
		/// The mass of the particle ( 1 is the default ).
		/// </summary>
		public double mass { get; set; }

		/// <summary>
		/// The scale of the particle ( 1 is normal size ).
		/// </summary>
		public double scale { get; set; }

		#endregion

		#region Methods
		/// <summary>
		/// Creates a new particle with all the same properties as this one.
		/// </summary>
		public Particle clone(ParticleFactory factory)
		{
			return default(Particle);
		}

		/// <summary>
		/// Creates a new particle with all the same properties as this one.
		/// </summary>
		public Particle clone()
		{
			return default(Particle);
		}

		/// <summary>
		/// Sets the particle's properties to their default values.
		/// </summary>
		public void initialize()
		{
		}

		#endregion

		#region Constructors
		/// <summary>
		/// Creates a particle.
		/// </summary>
		public Particle()
		{
		}

		#endregion


	}
}
