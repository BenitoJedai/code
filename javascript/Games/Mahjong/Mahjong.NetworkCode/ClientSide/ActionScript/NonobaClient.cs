using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using Mahjong.NetworkCode.ClientSide.Shared;
using ScriptCoreLib.ActionScript.Nonoba.api;
using Mahjong.NetworkCode.Shared;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;

namespace Mahjong.NetworkCode.ClientSide.ActionScript
{
	[Script]
	public class NonobaClient : Client
	{
		public const int NonobaChatWidth = 200;


		string ServerEndPoint;
		public NonobaClient(string ServerEndPoint)
		{
			this.ServerEndPoint = ServerEndPoint;

			this.Element.InvokeWhenStageIsReady(Initialize);

		}



		public static void SendMessage(Connection c, Communication.Messages m, params object[] e)
		{
			var i = new Message(((int)m).ToString());

			foreach (var z in e)
			{
				i.Add(z);
			}

			c.Send(i);
		}


		private void Initialize(Stage stage)
		{
			var c = NonobaAPI.MakeMultiplayer(stage, ServerEndPoint
				//, "192.168.3.102"
				//, "192.168.1.119"
				);



			var MyEvents = new Communication.RemoteEvents();
			var MyMessages = new Communication.RemoteMessages
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
				   var type = (Communication.Messages)int.Parse(e.Type);

				   //Converter<uint, byte[]> GetMemoryStream =
				   //    index =>
				   //    {
				   //        var a = e.GetByteArray(index);

				   //        if (a == null)
				   //            throw new Exception("bytearray missing at " + index + " - " + e.ToString());

				   //        return a.ToArray();
				   //    };

				   if (MyEvents.Dispatch(type,
						 new Communication.RemoteEvents.DispatchHelper
						 {
							 GetLength = i => e.length,
							 GetInt32 = e.GetInt,
							 GetDouble = e.GetNumber,
							 GetString = e.GetString,
							 //				 GetMemoryStream = GetMemoryStream
						 }
					 ))
					   return true;

				   return false;
			   };
			#endregion

			//c.MessageDirect +=
			//    e =>
			//    {
			//        throw new Exception("got first message");
			//    };

			#region message
			c.Message +=
				e =>
				{
					InitializeMapOrSkip();


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
					 // disconnected...
				 };

		}

		bool InitializeMapOrSkipOnce;

		public void InitializeMapOrSkip()
		{
			if (InitializeMapOrSkipOnce)
				return;

			InitializeMapOrSkipOnce = true;

			InitializeMap();
		}
	}
}
