using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.emitters;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.activities
{
	[Script(IsNative = true)]
	public interface Activity
	{
		#region Methods
		/// <summary>
		/// The addedToEmitter method is called by the emitter when the Activity is added to it.
		/// </summary>
		void addedToEmitter(Emitter emitter);

		/// <summary>
		/// The getDefaultPriority method is used to order the execution of activities.
		/// </summary>
		double getDefaultPriority();

		/// <summary>
		/// The initialize method is used by the emitter to start the activity.
		/// </summary>
		void initialize(Emitter emitter);

		/// <summary>
		/// The removedFromEmitter method is called by the emitter when the Activity is removed from it.
		/// </summary>
		void removedFromEmitter(Emitter emitter);

		/// <summary>
		/// The update method is used by the emitter to apply the activity.
		/// </summary>
		void update(Emitter emitter, double time);

		#endregion

	}
}
