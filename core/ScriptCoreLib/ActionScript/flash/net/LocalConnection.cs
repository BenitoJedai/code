using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.net
{
	// http://livedocs.adobe.com/flex/3/langref/flash/net/LocalConnection.html
	[Script(IsNative = true)]
	public class LocalConnection : EventDispatcher
	{
		#region Properties
		/// <summary>
		/// Indicates the object on which callback methods are invoked.
		/// </summary>
		public object client { get; set; }

		/// <summary>
		/// [read-only] A string representing the domain of the location of the current file.
		/// </summary>
		public string domain { get; private set; }

		#endregion


		#region Methods
		/// <summary>
		/// Specifies one or more domains that can send LocalConnection calls to this LocalConnection instance.
		/// </summary>
		public void allowDomain(/* params */ object domains)
		{
		}

		/// <summary>
		/// Specifies one or more domains that can send LocalConnection calls to this LocalConnection instance.
		/// </summary>
		public void allowDomain()
		{
		}

		/// <summary>
		/// Specifies one or more domains that can send LocalConnection calls to this LocalConnection object.
		/// </summary>
		public void allowInsecureDomain(/* params */ object domains)
		{
		}

		/// <summary>
		/// Specifies one or more domains that can send LocalConnection calls to this LocalConnection object.
		/// </summary>
		public void allowInsecureDomain()
		{
		}

		/// <summary>
		/// Closes (disconnects) a LocalConnection object.
		/// </summary>
		public void close()
		{
		}

		/// <summary>
		/// Prepares a LocalConnection object to receive commands from a send() command (called the sending LocalConnection object).
		/// </summary>
		public void connect(string connectionName)
		{
		}

		/// <summary>
		/// Invokes the method named methodName on a connection opened with the connect(connectionName) method (the receiving LocalConnection object).
		/// </summary>
		public void send(string connectionName, string methodName, /* params */ object arguments)
		{
		}

		/// <summary>
		/// Invokes the method named methodName on a connection opened with the connect(connectionName) method (the receiving LocalConnection object).
		/// </summary>
		public void send(string connectionName, string methodName)
		{
		}

		#endregion

		#region Constructors
		/// <summary>
		/// Creates a LocalConnection object.
		/// </summary>
		public LocalConnection()
		{
		}

		#endregion

		#region Events
		/// <summary>
		/// Dispatched when an exception is thrown asynchronously — that is, from native asynchronous code.
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<AsyncErrorEvent> asyncError;

		/// <summary>
		/// Dispatched if a call to LocalConnection.send() attempts to send data to a different security sandbox.
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<SecurityErrorEvent> securityError;

		/// <summary>
		/// Dispatched when a LocalConnection object reports its status.
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<StatusEvent> status;

		#endregion

	}
}
