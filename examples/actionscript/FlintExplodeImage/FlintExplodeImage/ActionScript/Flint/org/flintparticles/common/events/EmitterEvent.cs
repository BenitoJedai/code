using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.events
{
	[Script(IsNative = true)]
	public class EmitterEvent
	{
		#region Constants
		/// <summary>
		/// [static] The event dispatched by an emitter when it currently has no particles to display.
		/// </summary>
		public static readonly string EMITTER_EMPTY = "emitterEmpty";

		/// <summary>
		/// [static] The event dispatched by an emitter when it has updated all its particles and is ready for them to be rendered.
		/// </summary>
		public static readonly string EMITTER_UPDATED = "emitterUpdated";

		#endregion

		#region Methods
		#endregion

		#region Constructors
		/// <summary>
		/// The constructor creates a EmitterEvent object.
		/// </summary>
		public EmitterEvent(string type, bool bubbles, bool cancelable)
		{
		}

		/// <summary>
		/// The constructor creates a EmitterEvent object.
		/// </summary>
		public EmitterEvent(string type, bool bubbles)
		{
		}

		/// <summary>
		/// The constructor creates a EmitterEvent object.
		/// </summary>
		public EmitterEvent(string type)
		{
		}

		#endregion

	}
}
