using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SimpleChat2.ServerProvider;

namespace SimpleChat2.Buffer.Server
{
	public class findname_handler : RequestHandler
	{
		public class findname_response : ChatRequest.Requests.findname, RequestHandler.IResponse
		{
			public string Content { get; set; }

			public Action InvokeAction { get; set; }

			public void Invoke()
			{
				this.InvokeAction();
			}

		}

		public delegate void findname_delegate(findname_response e);

		public event findname_delegate Request;

		public override Type GetTargetType
		{
			get
			{
				return typeof(findname_response);
			}

		}

		public override void RegisterInvokeAction(IResponse e)
		{
			e.InvokeAction =
				delegate
				{
					var x = (findname_response)e;

					// rude last minute filter hack
					if (x.request == "findname")
						RaiseRequest(x);
				};

		}

		public void RaiseRequest(findname_response x)
		{
			Request(x);
		}
	}
}
