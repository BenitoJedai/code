using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.emitters;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.counters
{
	[Script(IsNative = true)]
	public interface Counter
	{
		#region Methods
		/// <summary>
		/// Resumes the emitter after a stop
		/// </summary>
		void resume();

		/// <summary>
		/// The startEmitter method is called when the emitter starts.
		/// </summary>
		uint startEmitter(Emitter emitter);

		/// <summary>
		/// Stops the emitter from emitting particles
		/// </summary>
		void stop();

		/// <summary>
		/// The updateEmitter method is called every frame after the emitter has started.
		/// </summary>
		uint updateEmitter(Emitter emitter, double time);

		#endregion

	}
}
