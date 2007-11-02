using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Net;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript;

namespace MonthSchedule.js
{
    [Script]
    public static class LocalsExtension
    {

        public static string Localize(this string e)
        {
            var doc = Locals.Default.Content;

            if (doc == null)
                throw new Exception("Localize cannot find xml document");

            var dictonary = doc.documentElement;

            //System.Diagnostics.Debugger.Break();

            // to unqoute!
            // //text/eng[.='year']/
            var res = doc.selectSingleNode("//text/eng[.='" + e + "']");
            
            if (res == null)
                return e;

            res = res.parentNode.childNodes.Where(x => x.nodeName == Locals.Default.Language).Single();

            return res.text;
        }
    }

    [Script]
    public class Locals
    {
        public string Language
        {
            get
            {
                var x = Native.Document.location.ArgumentsToDictonary().GetValueOrDefault("lang");

                if (x.IsNullOrEmpty())
                    return "eng";

                return x;
            }
        }

        public readonly string Source;
        public readonly IXMLHttpRequest Request;

        public IXMLDocument Content
        {
            get
            {
                return Request.responseXML;
            }
        }

        public Locals(string source)
        {
            this.Source = source;

            this.Request = new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.GET, this.Source,
                delegate
                {
                    // xml document is not set at this moment? why is that

                    
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
