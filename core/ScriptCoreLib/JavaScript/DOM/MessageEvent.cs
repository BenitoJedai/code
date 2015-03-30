using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
	// http://mxr.mozilla.org/mozilla-central/source/dom/webidl/MessageEvent.webidl

	[Script(HasNoPrototype = true, ExternalTarget = "MessageEvent")]
	public class MessageEvent : MessageEvent<object> // dynamic?
	{

	}

	[Script(HasNoPrototype = true, ExternalTarget = "MessageEvent")]
	public class MessageEvent<TData> : IEvent
	{
		// X:\jsc.svn\examples\javascript\test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs

		// idl dictates the field names, doesnt it
		// rename to window?
		public IWindow source;


		// X:\jsc.svn\examples\javascript\NewWindowMessagingExperiment\NewWindowMessagingExperiment\Application.cs
		public string origin;

		public TData data;

		public readonly MessagePort[] ports;

	}
}
