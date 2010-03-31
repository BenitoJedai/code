using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.WebService;

// Running in
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ScriptCoreLib.Ultra.Components.UltraWebService")]

namespace ScriptCoreLib.Ultra.Components
{
	internal delegate void StringAction(string e);


	internal sealed class UltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			result(x + DateTime.Now);
		}

		public void Serve(WebServiceHandler h)
		{
			if (h.Context.Request.Path == "/serve")
			{
				h.Context.Response.Write("hello");
				h.CompleteRequest();
			}
		}

		public void Serve2(WebServiceHandler h)
		{
			if (h.Context.Request.Path == "/serve2")
			{
				h.Default();
			}
		}
	}
}
