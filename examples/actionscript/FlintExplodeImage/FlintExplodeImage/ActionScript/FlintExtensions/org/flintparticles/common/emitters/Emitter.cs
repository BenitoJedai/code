using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.emitters;
using ScriptCoreLib.ActionScript.Extensions;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.events;

namespace FlintExplodeImage.ActionScript.FlintExtensions.org.flintparticles.common.emitters
{
	[Script(Implements = typeof(Emitter))]
	internal static class __Emitter
	{
		#region Implementation for methods marked with [Script(NotImplementedHere = true)]
		#region emitterEmpty
		public static void add_emitterEmpty(Emitter that, Action<EmitterEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, EmitterEvent.EMITTER_EMPTY);
		}

		public static void remove_emitterEmpty(Emitter that, Action<EmitterEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, EmitterEvent.EMITTER_EMPTY);
		}
		#endregion

		#region emitterUpdated
		public static void add_emitterUpdated(Emitter that, Action<EmitterEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, EmitterEvent.EMITTER_UPDATED);
		}

		public static void remove_emitterUpdated(Emitter that, Action<EmitterEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, EmitterEvent.EMITTER_UPDATED);
		}
		#endregion

		#region particleAdded
		public static void add_particleAdded(Emitter that, Action<ParticleEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, ParticleEvent.PARTICLE_ADDED);
		}

		public static void remove_particleAdded(Emitter that, Action<ParticleEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, ParticleEvent.PARTICLE_ADDED);
		}
		#endregion

		#region particleCreated
		public static void add_particleCreated(Emitter that, Action<ParticleEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, ParticleEvent.PARTICLE_CREATED);
		}

		public static void remove_particleCreated(Emitter that, Action<ParticleEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, ParticleEvent.PARTICLE_CREATED);
		}
		#endregion

		#region particleDead
		public static void add_particleDead(Emitter that, Action<ParticleEvent> value)
		{
			CommonExtensions.CombineDelegate(that, value, ParticleEvent.PARTICLE_DEAD);
		}

		public static void remove_particleDead(Emitter that, Action<ParticleEvent> value)
		{
			CommonExtensions.RemoveDelegate(that, value, ParticleEvent.PARTICLE_DEAD);
		}
		#endregion

		#endregion

	}
}
