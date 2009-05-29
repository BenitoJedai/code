using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.emitters;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.particles;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.actions;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.actions
{
	[Script(IsNative = true)]
	public class Move : ActionBase
	{
		#region Methods
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
		public void update(Emitter emitter, Particle particle, double time)
		{
		}

		#endregion

		#region Constructors
		/// <summary>
		/// The constructor creates a Move action for use by an emitter.
		/// </summary>
		public Move()
		{
		}

		#endregion

	}
}
