using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
	// http://livedocs.adobe.com/flex/3/langref/flash/events/DataEvent.html
	[Script(IsNative = true)]
	public class DataEvent : TextEvent
	{
		#region Constants
		/// <summary>
		/// [static] Defines the value of the type property of a data event object.
		/// </summary>
		public static readonly string DATA = "data";

		/// <summary>
		/// [static] Defines the value of the type property of an uploadCompleteData event object.
		/// </summary>
		public static readonly string UPLOAD_COMPLETE_DATA = "uploadCompleteData";

		#endregion


		#region Properties
		/// <summary>
		/// The raw data loaded into Flash Player or Adobe AIR.
		/// </summary>
		public string data { get; set; }

		#endregion

		#region Methods
		/// <summary>
		/// [override] Creates a copy of the DataEvent object and sets the value of each property to match that of the original.
		/// </summary>
		public Event clone()
		{
			return default(Event);
		}

		#endregion

		#region Constructors
		/// <summary>
		/// Creates an event object that contains information about data events.
		/// </summary>
		public DataEvent(string type, bool bubbles, bool cancelable, string data) : base(type, bubbles, cancelable)
		{
		}

		/// <summary>
		/// Creates an event object that contains information about data events.
		/// </summary>
		public DataEvent(string type, bool bubbles, bool cancelable) : base(type, bubbles, cancelable)
		{
		}

		/// <summary>
		/// Creates an event object that contains information about data events.
		/// </summary>
		public DataEvent(string type, bool bubbles)
			: base(type, bubbles)
		{
		}

		/// <summary>
		/// Creates an event object that contains information about data events.
		/// </summary>
		public DataEvent(string type)
			: base(type)
		{
		}

		#endregion

	}
}
