using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.WebService;
using ScriptCoreLib.Ultra.Library.Delegates;
using System.Xml.Linq;
using XElementAsSerializer.Data;

namespace XElementAsSerializer
{
	public sealed class UltraWebService
	{
		public void Swap(Class1 c_, Class1Action y)
		{


			y(
				new Class1
				{
					Foo = c_.Bar,
					Bar = c_.Foo
				}
			);
		}

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
			//if (h.Context.Request.Url.Query == "?serve2")
			if (h.Context.Request.Path == "/serve2")
			{
				// we could select an application here
				//h.Default();

				h.Context.Response.ContentType = "text/html";
				h.Context.Response.Write(
					XElementAsSerializer.HTML.Pages.AudioSource.Text
				);

				h.CompleteRequest();
			}
		}

	}
}
