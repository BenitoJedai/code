using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.particles
{
	[Script(IsNative = true)]
	public interface ParticleFactory
	{
		#region Methods
		/// <summary>
		/// To obtain a new Particle object.
		/// </summary>
		Particle createParticle();

		/// <summary>
		/// Indicates a particle is no longer required.
		/// </summary>
		void disposeParticle(Particle particle);

		#endregion

	}
}
