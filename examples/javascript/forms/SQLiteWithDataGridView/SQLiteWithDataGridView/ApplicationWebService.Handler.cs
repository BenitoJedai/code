using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Data.SQLite;
using System.Linq;
using System.Xml.Linq;

namespace SQLiteWithDataGridView
{

    public sealed partial class ApplicationWebService : IApplicationWebService
    {
        public void Handle(WebServiceHandler h)
        {
            var app = h.Applications[0];
            var appjs = app.TypeName + ".0.js";

            if (h.Context.Request.Path == "/" + appjs)
            {
                h.Context.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
                h.Context.Response.ContentType = "text/javascript";

                foreach (var item in app.References)
	            {
                    h.Context.Response.WriteFile(item.AssemblyFile + ".js");
	            }

                h.CompleteRequest();

                return;
            }

            if (h.IsDefaultPath)
            {
                h.Context.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
                h.Context.Response.ContentType = "text/html";

                var xml = XElement.Parse(app.PageSource);




                xml.Add(
                    new XElement("script",
                        new XAttribute("src", appjs),
                        " "
                    )
                );


                h.Context.Response.Write(xml.ToString());

                h.CompleteRequest();
            }
        }
    }
}
