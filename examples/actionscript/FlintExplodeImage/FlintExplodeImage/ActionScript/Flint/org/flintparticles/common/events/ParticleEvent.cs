using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.particles;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.events
{
	[Script(IsNative = true)]
	public class ParticleEvent
	{
		#region Fields
		/// <summary>
		/// 
		/// </summary>
		public object otherObject;

		/// <summary>
		/// The particle to which the event relates.
		/// </summary>
		public Particle particle;

		/// <summary>
		/// [static] The event dispatched by an emitter when a pre-existing particle is added to it.
		/// </summary>
		public static string PARTICLE_ADDED = "particleAdded";

		/// <summary>
		/// [static] The event dispatched by an emitter when a particle is created.
		/// </summary>
		public static string PARTICLE_CREATED = "particleCreated";

		/// <summary>
		/// [static] The event dispatched by an emitter when a particle dies.
		/// </summary>
		public static string PARTICLE_DEAD = "particleDead";

		/// <summary>
		/// [static] The event dispatched by an emitter when a particle collides with another object.
		/// </summary>
		public static string PARTICLES_COLLISION = "particlesCollision";

		#endregion

		#region Methods
		#endregion

		#region Constructors
		/// <summary>
		/// The constructor creates a ParticleEvent object.
		/// </summary>
		public ParticleEvent(string type, Particle particle, bool bubbles, bool cancelable)
		{
		}

		/// <summary>
		/// The constructor creates a ParticleEvent object.
		/// </summary>
		public ParticleEvent(string type, Particle particle, bool bubbles)
		{
		}

		/// <summary>
		/// The constructor creates a ParticleEvent object.
		/// </summary>
		public ParticleEvent(string type, Particle particle)
		{
		}

		/// <summary>
		/// The constructor creates a ParticleEvent object.
		/// </summary>
		public ParticleEvent(string type)
		{
		}

		#endregion


	}
}
