using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.WebService;
using ScriptCoreLib.Ultra.Library.Delegates;
using PublishingXAML.Data;

namespace PublishingXAML
{
	public sealed class UltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			result(x + DateTime.Now);
		}

		public void Serve(WebServiceHandler h)
		{

			if (h.Context.Request.Path == "/" + new DrawingSource().Name)
			{
				h.Context.Response.ContentType = "application/xaml+xml";
				h.Context.Response.Write(new DrawingSource().Text);
				h.CompleteRequest();
			}
		}

		public void Serve2(WebServiceHandler h)
		{

			if (h.Context.Request.Path == "/" + new Drawing2Source().Name)
			{
				h.Context.Response.ContentType = "image/svg+xml";
				h.Context.Response.Write(new Drawing2Source().Text);
				h.CompleteRequest();
			}
		}
	}
}
