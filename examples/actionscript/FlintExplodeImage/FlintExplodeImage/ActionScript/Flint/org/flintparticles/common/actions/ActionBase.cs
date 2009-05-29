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
	public class ActionBase : Action
	{
		#region Methods
		/// <summary>
		/// This method does nothing.
		/// </summary>
		public void addedToEmitter(Emitter emitter)
		{
		}

		/// <summary>
		/// Returns a default priority of 0 for this action.
		/// </summary>
		public double getDefaultPriority()
		{
			return default(double);
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
		/// The constructor creates an ActionBase object.
		/// </summary>
		public ActionBase()
		{
		}

		#endregion

	}
}
