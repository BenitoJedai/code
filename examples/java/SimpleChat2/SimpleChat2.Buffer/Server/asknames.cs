using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SimpleChat2.ServerProvider;

namespace SimpleChat2.Buffer.Server
{
	public class asknames_handler : RequestHandler
	{
		public class asknames_response : ChatRequest.Requests.asknames, RequestHandler.IResponse
		{
			public string Content { get; set; }

			public Action InvokeAction { get; set; }

			public void Invoke()
			{
				this.InvokeAction();
			}

		}

		public delegate void asknames_delegate(asknames_response e);

		public event asknames_delegate Request;

		public override Type GetTargetType
		{
			get
			{
				return typeof(asknames_response);
			}

		}

		public override void RegisterInvokeAction(IResponse e)
		{
			e.InvokeAction =
				delegate
				{
					var x = (asknames_response)e;

					// rude last minute filter hack
					if (x.request == "asknames")
						RaiseRequest(x);
				};

		}

		public void RaiseRequest(asknames_response x)
		{
			Request(x);
		}
	}
}
