using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib;
using FlashConsoleWorm.Shared;
using FlashConsoleWorm.ActionScript;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashConsoleWorm.ActionScript.Nonoba
{
	[Script, ScriptApplicationEntryPoint]
	[SWF(width = DefaultControlWidth + NonobaChatWidth, height = DefaultControlHeight, backgroundColor = FlashConsoleWorm.ColorBlack)]
	public partial class Client : Sprite
	{
		public const int NonobaChatWidth = 200;

		public const int DefaultControlWidth = FlashConsoleWorm.DefaultWidth;
		public const int DefaultControlHeight = FlashConsoleWorm.DefaultHeight;


		public Client()
		{
			this.InvokeWhenStageIsReady(Initialize);


		}

		SharedClass1.IEvents Events;
		SharedClass1.IMessages Messages;

		public static void SendMessage(Connection c, SharedClass1.Messages m, params object[] e)
		{
			var i = new Message(((int)m).ToString());

			foreach (var z in e)
			{
				i.Add(z);
			}

			c.Send(i);
		}

		private void Initialize()
		{
			var c = NonobaAPI.MakeMultiplayer(stage
				//, "192.168.3.102"
				//, "192.168.1.119"
				);


			var MyEvents = new SharedClass1.RemoteEvents();
			var MyMessages = new SharedClass1.RemoteMessages
			{
				Send = e => SendMessage(c, e.i, e.args)
			};



			Events = MyEvents;
			Messages = MyMessages;

			this.InitializeEvents();

			#region Dispatch
			Func<Message, bool> Dispatch =
			   e =>
			   {
				   var type = (SharedClass1.Messages)int.Parse(e.Type);

				   if (MyEvents.Dispatch(type,
						 new SharedClass1.RemoteEvents.DispatchHelper
						 {
							 GetLength = i => e.length,
							 GetInt32 = e.GetInt,
							 GetDouble = e.GetNumber,
							 GetString = e.GetString,
						 }
					 ))
					   return true;

				   return false;
			   };
			#endregion


			#region message
			c.Message +=
				e =>
				{
					InitializeMap();


					var Dispatched = false;

					try
					{
						Dispatched = Dispatch(e.message);
					}
					catch (Exception ex)
					{
						System.Console.WriteLine("error at dispatch " + e.message.Type);

						throw ex;
					}

					if (Dispatched)
						return;

					System.Console.WriteLine("not on dispatch: " + e.message.Type);

				};
			#endregion

			c.Disconnect +=
				 delegate
				 {
				 };




		}
	}
}
