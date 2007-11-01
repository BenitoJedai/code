using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Net;
using ScriptCoreLib.JavaScript.DOM.XML;

namespace MonthSchedule.js
{
    [Script]
    public static class LocalsExtension
    {
        public static string Localize(this string e)
        {
            var doc = Locals.Default.Content;
            var dictonary = doc.documentElement;

            System.Diagnostics.Debugger.Break();

            // to unqoute!
            var res = doc.selectNodes("//*[.='" + e + "']");
            
            return e;
        }
    }

    [Script]
    public class Locals
    {
        public readonly string Source;
        public readonly IXMLHttpRequest Request;

        public IXMLDocument Content;

        public Locals(string source)
        {
            this.Source = source;

            this.Request = new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.GET, this.Source,
                (IXMLHttpRequest e) =>
                {
                    Content = e.responseXML;
                }
            );

        }

        public void WhenDownloadComplete(Action<Locals> done)
        {
            this.Request.InvokeOnComplete(
                delegate
                {
                    done(this);
                }
            );
        }

        static Locals _Default;
        public static Locals Default
        {
            get
            {
                if (_Default == null)
                    _Default = new Locals("assets/MonthSchedule/locals.xml");

                return _Default;
            }
        }
    }
}
