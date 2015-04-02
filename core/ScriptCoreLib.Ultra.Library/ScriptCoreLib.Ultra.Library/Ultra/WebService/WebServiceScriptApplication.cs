using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Delegates;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.WebService
{
    public static class WebApplicationIcon
    {
        [Obsolete]
        public const string Icon = "assets/ScriptCoreLib/jsc.ico";

        public const string Image = "assets/ScriptCoreLib/jsc.png";
    }


	[Obsolete("service worker")]
    public static class WebApplicationCacheManifest
    {
        public const string ManifestContentType = "text/cache-manifest";
        public const string ManifestName = "cache-manifest";
        //public const string ManifestName = "cache.manifest";
    }

    public class WebServiceScriptApplication
    {
        public string TypeName;
        public string TypeFullName;

        public class Reference
        {
            public string AssemblyFile;
        }

        public Reference[] References;


        public string GZipAssemblyFile;


        // <html>
        public string PageSource;


        // tested by
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.adminshell\com.abstractatech.adminshell\ApplicationWebService.cs
        public bool DiagnosticsMakeItSlowAndAddSalt;



        public string baseURI;

        public void WriteTo(
            StringAction Write, bool CacheManifest = true)
        {
            var app = this;

            // http://validator.w3.org/check?uri=

            StringAction WriteLine = k => Write(k + Environment.NewLine);

            // this function is running in .net, google app engine java and php
            // this function is based on JavaScript.EntrypointProvider
            // we could show a cool loading animation?
            // can we have XElement support already`?

            // <html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
            //WriteLine(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">");

            // http://www.w3.org/TR/html5/offline.html

            // can we detect who is looking at us? is it a 3D view?
            // https://www.oculusvr.com/order/

            //WriteLine(@"<!DOCTYPE HTML>");
            //            WriteLine(@"<!-- 
            //
            //Hello curious person, welcome to the source code. I hope you enjoy your time here. Please close the door after you've gone. 
            //
            //--> ");

            // view-source:http://skycraft.io/


            // should we provde some statistics?

                //this.References.
            WriteLine(@"<!doctype html>
<!-- 


Hi there!

Glad to see you here, we must have some interests in common.
Welcome to the source code. I hope you enjoy your time here. Please close the door after you've gone. 

Visit http://my.jsc-solutions.net to gear up!


--> 
");


            // if XElement
            var html = XElement.Parse(this.PageSource);

            if (CacheManifest)
            {
                html.Add(
                    new XAttribute("manifest", WebApplicationCacheManifest.ManifestName)
                );

                //html.SetAttributeValue("manifest", WebApplicationCacheManifest.ManifestName);

                //  method: Void SetAttributeValue(System.Xml.Linq.XName, System.Object)
            }

            if (!string.IsNullOrEmpty(baseURI))
            {
                // X:\jsc.smokescreen.svn\market\appengine\xmoneseservicesweb\xmoneseserviceswebredirector\ApplicationWebService.cs

                var head = html.Elements("head").FirstOrDefault();
                if (head != null)
                    head.Add(
                        new XElement("base",
                            new XAttribute("href", baseURI)
                        )
                    );

            }

            // add first not yet available!
            //html.AddFirst(
            html.Add(
                new XElement("script",
                    // will jvm autoclose this element?
                    new XAttribute("src",
                        //global::ScriptCoreLib.j
                        "view-source"), " "
                )
            );

            WriteLine(html.ToString());
            return;



            //<script src='view-source'></script>

            if (CacheManifest)
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201303/20130330-cache-manifest
                WriteLine(@"<html manifest=""" + WebApplicationCacheManifest.ManifestName + @""">");
            }
            else
            {
                WriteLine(@"<html>");
            }

            // flash cannot be reloaded for some reason? why?
            WriteLine(@"<head>");

            // do we need this?
            //WriteLine(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />");

            // whats the title going to be? :)
            WriteLine(@"<title>" + app.TypeName + "</title>");
            //WriteLine(@"<title>Loading...</title>");

            // http://www.whatwg.org/specs/web-apps/current-work/multipage/links.html#rel-icon

            WriteLine(@"<link rel=""icon"" href=""" + WebApplicationIcon.Icon + @""" sizes=""32x32 96x96"" type=""image/vnd.microsoft.icon"" />");
            WriteLine(@"<link rel=""icon"" href=""" + WebApplicationIcon.Image + @""" sizes=""96x96"" type=""image/png"" />");

            // http://developer.apple.com/library/safari/#documentation/appleapplications/reference/SafariHTMLRef/Articles/MetaTags.html
            // http://www.viaboxxsystems.de/html-fullscreen-apps-for-the-ipad

            //WriteLine(@"<meta name='viewport' content='width=device-width, initial-scale=1, maximum-scale=1' />");


            WriteLine(@"<meta name='apple-mobile-web-app-capable' content='yes' />");

            //WriteLine(@"<link rel=""shortcut icon"" href=""favicon""  type=""image/x-icon"" />");

            //WriteLine(@"<title>" + this.TypeFullName + "</title>");

            //WriteLine("<meta name='google-site-verification' content='uMipBZ74jD_65lTkiAVKRHM1HSJRo_NAgpk6NChQuOA' />");

            //WriteLine(@"<script></script>");
            WriteLine(@"</head>");

            if (string.IsNullOrEmpty(this.PageSource))
            {
                WriteDefaultPageSource(WriteLine);
            }
            else
            {
                Write(this.PageSource);
            }

            // jsc bootstraps always
            //WriteLine(@"<script type='text/xml' class='" + app.TypeName + "'></script>");

            //foreach (var item in app.References)
            //{
            //    Write(@"<script type='text/javascript' src='/" + item.AssemblyFile + @".js'></script>");

            //}

            // prepeare for ChromeOS deployment. primary app script:
            WriteLine("");

            Write("<script src='view-source'></script>");


            WriteLine(@"</html>");
        }

        [Obsolete]
        private static void WriteDefaultPageSource(StringAction WriteLine)
        {
            WriteLine(@"<body style='margin: 0; overflow: hidden;'><noscript>ScriptApplication cannot run without JavaScript!</noscript>");

            // should we display custom logo?
            // only the first image will be fetched, then the script...
            //WriteLine(@"<div style='border-style: none; position: absolute; left: 50%; top: 50%;' >");
            //WriteLine(@"<img class='LoadingAnimation' src='/assets/ScriptCoreLib/jsc.png' title='jsc' style='border-style: none; margin-left: -48px; margin-top: -48px; ' /> ");
            //WriteLine(@"</div>");

            // http://www.ajaxload.info/
            WriteLine(@"<div style='border-style: none; position: absolute; left: 50%; top: 50%;' >");
            WriteLine(@"<img class='LoadingAnimation' src='/assets/ScriptCoreLib/loading.gif' title='loading...'  style='border-style: none; margin-left: -16px; margin-top: -16px; ' /> ");
            WriteLine(@"</div>");



            WriteLine(@"</body>");
        }


    }
}
