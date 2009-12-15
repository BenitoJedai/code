using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SimpleChat2.ServerProvider;

namespace SimpleChat2.Buffer.Server
{
	public class sendname_handler : RequestHandler
	{
		public class sendname_response : ChatRequest.Requests.sendname, RequestHandler.IResponse
		{
			public string Content { get; set; }

			public Action InvokeAction { get; set; }

			public void Invoke()
			{
				this.InvokeAction();
			}

		}

		public delegate void sendname_delegate(sendname_response e);

		public event sendname_delegate Request;

		public override Type GetTargetType
		{
			get
			{
				return typeof(sendname_response);
			}

		}

		public override void RegisterInvokeAction(IResponse e)
		{
			e.InvokeAction =
				delegate
				{
					var x = (sendname_response)e;

					// rude last minute filter hack
					if (x.request == "sendname")
						RaiseRequest(x);
				};

		}

		public void RaiseRequest(sendname_response x)
		{
			Request(x);
		}
	}
}
