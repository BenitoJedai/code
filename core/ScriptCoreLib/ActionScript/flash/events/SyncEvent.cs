using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
	// http://livedocs.adobe.com/flex/3/langref/flash/events/SyncEvent.html
    [Script(IsNative=true)]
    public class SyncEvent : Event
    {

		#region Constants
		/// <summary>
		/// [static] Defines the value of the type property of a sync event object.
		/// </summary>
		public static readonly string SYNC = "sync";

		#endregion

    }
}

