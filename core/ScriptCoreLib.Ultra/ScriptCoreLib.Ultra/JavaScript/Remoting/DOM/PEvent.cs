using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.Remoting.DOM
{
	public interface PEvent
	{
		void dummy();
	}

	public delegate void PEventAction(PEvent e);

	public class PIEvent : PEvent
	{
		internal IEvent InternalEvent;

		#region PEvent Members

		public void dummy()
		{
		}

		#endregion
	}
}
