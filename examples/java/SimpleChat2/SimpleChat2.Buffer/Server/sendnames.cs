using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SimpleChat2.ServerProvider;

namespace SimpleChat2.Buffer.Server
{
	public class sendnames_handler : RequestHandler
	{
		public class sendnames_response : ChatRequest.Requests.sendnames, RequestHandler.IResponse
		{
			public string Content { get; set; }

			public Action InvokeAction { get; set; }

			public void Invoke()
			{
				this.InvokeAction();
			}

		}

		public delegate void sendnames_delegate(sendnames_response e);

		public event sendnames_delegate Request;

		public override Type GetTargetType
		{
			get
			{
				return typeof(sendnames_response);
			}

		}

		public override void RegisterInvokeAction(IResponse e)
		{
			e.InvokeAction =
				delegate
				{
					var x = (sendnames_response)e;

					// rude last minute filter hack
					if (x.request == "sendnames")
						RaiseRequest(x);
				};

		}

		public void RaiseRequest(sendnames_response x)
		{
			Request(x);
		}
	}
}
