using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.counters;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.particles;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.activities;
using ScriptCoreLib.ActionScript;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.initializers;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.events;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.emitters
{
	[Script(IsNative = true)]
	public class Emitter : DisplayObject
	{
		#region Properties
		/// <summary>
		/// The Counter for the Emitter.
		/// </summary>
		public Counter counter { get; set; }

		/// <summary>
		/// Indicates a fixed time (in seconds) to use for every frame.
		/// </summary>
		public double fixedFrameTime { get; set; }

		/// <summary>
		/// The maximum duration for a single update frame, in seconds.
		/// </summary>
		public double maximumFrameTime { get; set; }

		/// <summary>
		/// This is the particle factory used by the emitter to create and dispose of particles.
		/// </summary>
		public ParticleFactory particleFactory { get; set; }

		/// <summary>
		/// [read-only] The array of all particles created by this emitter.
		/// </summary>
		public Particle[] particles { get; private set; }

		/// <summary>
		/// [read-only] Indicates if the emitter is currently running.
		/// </summary>
		public bool running { get; private set; }

		/// <summary>
		/// Indicates whether the emitter should manage its own internal update tick.
		/// </summary>
		public bool useInternalTick { get; set; }

		#endregion

		#region Methods
		/// <summary>
		/// Adds an Action to the Emitter.
		/// </summary>
		public void addAction(Action action, double priority)
		{
		}

		/// <summary>
		/// Adds an Activity to the Emitter.
		/// </summary>
		public void addActivity(Activity activity, double priority)
		{
		}

		/// <summary>
		/// Adds existing particles to the emitter.
		/// </summary>
		public void addExistingParticles(Particle[] particles, bool applyInitializers)
		{
		}

		/// <summary>
		/// Adds existing particles to the emitter.
		/// </summary>
		public void addExistingParticles(Particle[] particles)
		{
		}

		/// <summary>
		/// Adds an Initializer object to the Emitter.
		/// </summary>
		public void addInitializer(Initializer initializer, double priority)
		{
		}

		/// <summary>
		/// Detects if the emitter is using a particular action or not.
		/// </summary>
		public bool hasAction(Action action)
		{
			return default(bool);
		}

		/// <summary>
		/// Detects if the emitter is using an action of a particular class.
		/// </summary>
		public bool hasActionOfType(Class actionClass)
		{
			return default(bool);
		}

		/// <summary>
		/// Detects if the emitter is using a particular activity or not.
		/// </summary>
		public bool hasActivity(Activity activity)
		{
			return default(bool);
		}

		/// <summary>
		/// Detects if the emitter is using an activity of a particular class.
		/// </summary>
		public bool hasActivityOfType(Class activityClass)
		{
			return default(bool);
		}

		/// <summary>
		/// Detects if the emitter is using a particular initializer or not.
		/// </summary>
		public bool hasInitializer(Initializer initializer)
		{
			return default(bool);
		}

		/// <summary>
		/// Detects if the emitter is using an initializer of a particular class.
		/// </summary>
		public bool hasInitializerOfType(Class initializerClass)
		{
			return default(bool);
		}

		/// <summary>
		/// 
		/// </summary>
		public void killAllParticles()
		{
		}

		/// <summary>
		/// Pauses the emitter.
		/// </summary>
		public void pause()
		{
		}

		/// <summary>
		/// Removes an Action from the Emitter.
		/// </summary>
		public void removeAction(Action action)
		{
		}

		/// <summary>
		/// Removes an Activity from the Emitter.
		/// </summary>
		public void removeActivity(Activity activity)
		{
		}

		/// <summary>
		/// Removes an Initializer from the Emitter.
		/// </summary>
		public void removeInitializer(Initializer initializer)
		{
		}

		/// <summary>
		/// Resumes the emitter after a pause.
		/// </summary>
		public void resume()
		{
		}

		/// <summary>
		/// Makes the emitter skip forwards a period of time with a single update.
		/// </summary>
		public void runAhead(double time, double frameRate)
		{
		}

		/// <summary>
		/// Makes the emitter skip forwards a period of time with a single update.
		/// </summary>
		public void runAhead(double time)
		{
		}

		/// <summary>
		/// Starts the emitter.
		/// </summary>
		public void start()
		{
		}

		/// <summary>
		/// Stops the emitter, killing all current particles and returning them to the particle factory for reuse.
		/// </summary>
		public void stop()
		{
		}

		/// <summary>
		/// Used to update the emitter.
		/// </summary>
		public void update(double time)
		{
		}

		#endregion

		#region Constructors
		/// <summary>
		/// The constructor creates an emitter.
		/// </summary>
		public Emitter()
		{
		}

		#endregion

		#region Events
		/// <summary>
		/// Dispatched when an emitter attempts to update the particles' state but it contains no particles.
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<EmitterEvent> emitterEmpty;

		/// <summary>
		/// Dispatched when the particle system has updated and the state of the particles has changed.
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<EmitterEvent> emitterUpdated;

		/// <summary>
		/// Dispatched when a pre-existing particle is added to the emitter.
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<ParticleEvent> particleAdded;

		/// <summary>
		/// Dispatched when a particle is created and has just been added to the emitter.
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<ParticleEvent> particleCreated;

		/// <summary>
		/// Dispatched when a particle dies and is about to be removed from the system.
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<ParticleEvent> particleDead;

		#endregion


	}
}
