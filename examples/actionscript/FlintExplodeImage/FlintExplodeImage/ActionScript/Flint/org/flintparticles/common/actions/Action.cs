using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.emitters;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.particles;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.actions
{
	[Script(IsNative = true)]
	public interface Action
	{
		#region Methods
		/// <summary>
		/// The addedToEmitter method is called by the emitter when the Action is added to it.
		/// </summary>
		void addedToEmitter(Emitter emitter);

		/// <summary>
		/// The getDefaultPriority method is used to order the execution of actions.
		/// </summary>
		double getDefaultPriority();

		/// <summary>
		/// The removedFromEmitter method is called by the emitter when the Action is removed from it.
		/// </summary>
		void removedFromEmitter(Emitter emitter);

		/// <summary>
		/// The update method is used by the emitter to apply the action to every particle.
		/// </summary>
		void update(Emitter emitter, Particle particle, double time);

		#endregion

	}
}
