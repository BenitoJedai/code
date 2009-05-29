using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.actions;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.zones;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.emitters;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.particles;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.actions
{
	[Script(IsNative = true)]
	public class DeathZone : ActionBase
	{
		#region Fields
		/// <summary>
		/// The zone.
		/// </summary>
		public Zone3D zone;

		/// <summary>
		/// If true, the zone is treated as the safe area and particles ouside the zone are killed.
		/// </summary>
		public bool zoneIsSafe;

		#endregion

		#region Methods
		/// <summary>
		/// Returns a value of -20, so that the DeathZone executes after all movement has occured.
		/// </summary>
		public double getDefaultPriority()
		{
			return default(double);
		}

		/// <summary>
		/// Checks whether the particle is inside the zone and kills it if it is in the DeathZone region.
		/// </summary>
		public void update(Emitter emitter, Particle particle, double time)
		{
		}

		#endregion

		#region Constructors
		/// <summary>
		/// The constructor creates a DeathZone action for use by an emitter.
		/// </summary>
		public DeathZone(Zone3D zone, bool zoneIsSafe)
		{
		}

		/// <summary>
		/// The constructor creates a DeathZone action for use by an emitter.
		/// </summary>
		public DeathZone(Zone3D zone)
		{
		}

		#endregion

	}
}
