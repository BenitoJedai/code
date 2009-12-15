using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SimpleChat2.ServerProvider;

namespace SimpleChat2.Buffer.Server
{
	public class sendmessage_handler : RequestHandler
	{
		public class sendmessage_response : ChatRequest.Requests.sendmessage, RequestHandler.IResponse
		{
			public string Content { get; set; }

			public Action InvokeAction { get; set; }

			public void Invoke()
			{
				this.InvokeAction();
			}

		}

		public delegate void sendmessage_delegate(sendmessage_response e);

		public event sendmessage_delegate Request;

		public override Type GetTargetType
		{
			get
			{
				return typeof(sendmessage_response);
			}

		}

		public override void RegisterInvokeAction(IResponse e)
		{
			e.InvokeAction =
				delegate
				{
					var x = (sendmessage_response)e;

					// rude last minute filter hack
					if (x.request == "sendmessage")
						RaiseRequest(x);
				};

		}

		public void RaiseRequest(sendmessage_response x)
		{
			Request(x);
		}
	}
}
