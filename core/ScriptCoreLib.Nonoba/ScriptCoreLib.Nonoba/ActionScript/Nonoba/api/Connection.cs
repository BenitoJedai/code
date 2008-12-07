using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Nonoba.api
{
	[Script(IsNative = true)]
	public class Connection : EventDispatcher
	{
		#region Events
		// http://msdn.microsoft.com/en-us/library/858x0ycc.aspx
#pragma warning disable 0067
		/// <summary>
		/// 
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<object> Disconnect;

		/// <summary>
		/// 
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<object> Init;

		#endregion

		#region Events
		/// <summary>
		/// 
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<MessageEvent> Message;


		[method: Script(NotImplementedHere = true)]
		public event Action<object> MessageDirect;
#pragma warning restore 0067
		#endregion

		public void Send(/* params */ object args)
		{
		}

	}
}
