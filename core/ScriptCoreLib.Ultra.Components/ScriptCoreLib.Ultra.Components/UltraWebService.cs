using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.WebService;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using System.Web.UI;

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

		//public void Serve2(WebServiceHandler h)
		//{
		//    if (h.IsDefaultPath)
		//    {
		//        h.Context.Response.ContentType = "text/html";
		//        var app = h.Applications[0];

		//        var m = new HtmlTextWriter(h.Context.Response.Output);

		//        m.WriteFullBeginTag("html");
		//        m.WriteFullBeginTag("head");
		//        m.WriteFullBeginTag("title");

		//        m.Write(app.TypeName);
		//        m.WriteEndTag("title");

		//        m.WriteEndTag("head");

		//        m.WriteLine(app.PageSource);


		//        m.WriteLine(@"<script type='text/xml' class='" + app.TypeName + @"'></script>");


		//        foreach (var item in app.References)
		//        {
		//            m.WriteLine(@"<script type='text/javascript' src='" + item.AssemblyFile + @".js'></script>");
		//        }


		//        m.WriteEndTag("html");

		//        h.CompleteRequest();
		//    }
		//}
	}
}
