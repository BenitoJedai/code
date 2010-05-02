using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.WebService;
using ScriptCoreLib.Ultra.Library.Delegates;

namespace UltraApplicationWithAssets
{
	public sealed partial class ApplicationWebService
	{
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
			//if (h.Context.Request.Url.Query == "?serve2")
			if (h.Context.Request.Path == "/serve2")
			{
				// we could select an application here
				//h.Default();

				h.Context.Response.ContentType = "text/html";
				h.Context.Response.Write(
					UltraApplicationWithAssets.HTML.Pages.AudioSource.Text
				);

				h.CompleteRequest();
			}
		}

	}
}
