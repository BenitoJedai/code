﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Web;

namespace ScriptCoreLib.Ultra.WebService
{
    public static class InternalGlobalExtensions
    {
        public static InternalFileInfo ToCurrentFile(this InternalGlobal g)
        {
            var that = g.InternalApplication;

            var x = default(InternalFileInfo);
            foreach (var item in g.GetFiles())
            {
                if (that.Request.Path == "/" + item.Name)
                {
                    x = item;
                    break;
                }
            }
            return x;
        }

        public static bool FileExists(InternalGlobal g)
        {
            return g.ToCurrentFile() != null;
        }

        public static string escapeXML(string s)
        {
            return s.ToXMLString();
        }


        public static void InternalApplication_BeginRequest(InternalGlobal g)
        {
            var that = g.InternalApplication;
            var Context = that.Context;

            var Path = Context.Request.Path;

            var CurrentFile = g.ToCurrentFile();

            if (CurrentFile != null)
            {
                // http://betterexplained.com/articles/how-to-optimize-your-site-with-http-caching/


                //// http://www.mombu.com/programming/xbase/t-outputcache-directive-vs-responsecachesetcacheability-624773.html
                g.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
                g.Response.Cache.SetExpires(DateTime.Now.AddMinutes(15));

                //g.EndRequest +=
                //    (_s, _e) =>
                //    {
                //Console.WriteLine("cache " + CurrentFile.Name);


                //        // http://forums.asp.net/t/1123505.aspx
                //        HttpApplication application = (HttpApplication)_s;
                //        HttpContext context = application.Context;
                //g.Response.ExpiresAbsolute = DateTime.Now.AddDays(1);
                //context.Response.AddHeader("pragma", "no-cache");
                //g.Response.AddHeader("cache-control", "public");
                g.Response.AddHeader("x-handler", "http://jsc-solutions.net");

                // to root
                Context.Response.WriteFile("/" + CurrentFile.Name);

                that.CompleteRequest();

                //context.Response.CacheControl = "no-cache";
                //};

                // fake lag
                //if (that.Request.Path.EndsWith(".js"))
                //    System.Threading.Thread.Sleep(1000);
                return;
            }



            if (Path == "/favicon.ico")
            {
                Context.Response.WriteFile("assets/ScriptCoreLib/jsc.ico");

                that.CompleteRequest();
                return;
            }



            if (Path == "/robots.txt")
            {
                Context.Response.StatusCode = 404;
                that.CompleteRequest();
                return;
            }

            if (Path == "/crossdomain.xml")
            {
                Context.Response.StatusCode = 404;
                that.CompleteRequest();
                return;
            }

            StringAction Write =
                e =>
                {
                    // could we take the method pointer implicitly?
                    Context.Response.Write(e);
                };

            StringAction WriteLine =
                e =>
                {
                    // could we take the method pointer implicitly?
                    Write(e + Environment.NewLine);
                };

            if (Path == "/" + WebApplicationCacheManifest.ManifestName)
            {
                WriteCacheManifest(g, that, WriteLine);
                return;
            }



            var WebMethods = g.GetWebMethods();

            //Console.WriteLine();

            foreach (var item in WebMethods)
            {
                item.LoadParameters(that.Context);
            }

            if (Context.Request.HttpMethod == "POST")
            {
                var WebMethod = InternalWebMethodInfo.First(WebMethods, Context.Request.QueryString[InternalWebMethodInfo.QueryKey]);
                if (WebMethod == null)
                {
                    // let user defined handler hangle it..
                }
                else
                {
                    g.Invoke(WebMethod);

                    if (that.Context.Request.Path == "/xml")
                    {
                        WriteXDocument(g, Write, WebMethod);
                        that.CompleteRequest();
                        return;
                    }

                    that.Response.ContentType = "text/html";
                    WriteDiagnosticsResults(Write, WebMethod);
                    WriteDiagnostics(g, Write, WebMethods);
                    that.CompleteRequest();
                    return;
                }
            }

            var IsComplete = false;

            var h = new WebServiceHandler
            {
                Context = that.Context,

                CompleteRequest = delegate
                {
                    IsComplete = true;
                    that.CompleteRequest();
                },

                Applications = g.GetScriptApplications(),

                Default = delegate
                {
                    that.Response.ContentType = "text/html";

                    // todo: jsc: PHP workaround required
                    var apps = g.GetScriptApplications();
                    var app = apps[0];

                    app.WriteTo(Write);

                    IsComplete = true;
                    that.CompleteRequest();
                },

                Diagnostics = delegate
                {
                    that.Response.ContentType = "text/html";
                    WriteDiagnostics(g, Write, WebMethods);

                    IsComplete = true;
                    that.CompleteRequest();
                },

                Redirect = delegate
                {
                    that.Response.Redirect("/#" + that.Request.Path);

                    IsComplete = true;
                    that.CompleteRequest();
                }
            };


            g.Serve(h);

            if (!IsComplete)
            {
                if (that.Request.Path == "/jsc")
                {
                    h.Diagnostics();
                    return;
                }

                if (h.IsDefaultPath)
                {
                    h.Default();
                    return;
                }

                if (Context.Request.HttpMethod == "POST")
                {
                    // we dont know what to do with this POST..
                    Context.Response.StatusCode = 404;
                    that.CompleteRequest();
                    return;
                }

                // we could invoke web service handler now?
                h.Redirect();
                //h.Diagnostics();
            }
        }



        private static void WriteCacheManifest(InternalGlobal g, System.Web.HttpApplication that, StringAction WriteLine)
        {
            that.Response.ContentType = WebApplicationCacheManifest.ManifestContentType;

            // http://www.whatwg.org/specs/web-apps/current-work/multipage/offline.html

            WriteLine("CACHE MANIFEST");

            var files = g.GetFiles();
            var bytes = 0;

            WriteLine(WebApplicationIcon.Icon);
            WriteLine(WebApplicationIcon.Image);

            foreach (var item in files)
            {
                WriteLine("# " + item.Length + " bytes");

                var Command = item.Name;

                // webkit seems to have 5MB limit.
                // http://groups.google.com/a/chromium.org/group/chromium-html5/browse_thread/thread/e911f18b905d28ee/9f54c8cc1e8afb5d
                // http://stackoverflow.com/questions/2908459/mobile-safari-5mb-html5-application-cache-limit
                // http://www.yuiblog.com/blog/2010/07/12/mobile-browser-cache-limits-revisited/

                // hack.

                // we need to figure out how to make the application fit to the cache limits.
                // we could be optimizing javascript.

                if (Command.EndsWith(".deploy"))
                    Command = "# " + Command;
                else if (Command.EndsWith(".swf"))
                    Command = "# " + Command;
                else
                {
                    bytes += item.Length;
                }

                WriteLine(Command);
            }

            var now = DateTime.Now;

            WriteLine("# jsc: have good day! files: " + files.Length + " bytes: " + bytes);

            that.CompleteRequest();
        }



        public static bool IsDefaultPathOrSpecialPath(string e)
        {
            if (WebServiceHandler.InternalIsDefaultPath(e))
                return true;

            if (e == "/jsc")
                return true;

            if (e == "/xml")
                return true;

            return false;
        }


        private static void WriteDiagnosticsResults(StringAction Write, InternalWebMethodInfo WebMethod)
        {
            if (WebMethod.Results == null)
            {

                Write("<h2>No Results</h2>");
            }
            else
            {
                Write("<h2>" + WebMethod.Results.Length + " Results</h2>");

                foreach (var item in WebMethod.Results)
                {
                    WriteWebMethod(Write, item,
                        Parameter =>
                        {
                            if (Parameter == null)
                                return;

                            Write(" = '<code style='color: red'>" + escapeXML(Parameter.Value) + "</code>'");
                        }
                    );
                }

                Write("<br />");
            }
        }

        private static void WriteDiagnostics(InternalGlobal g, StringAction Write, InternalWebMethodInfo[] WebMethods)
        {
            // should the diagnostics be a separate rich Browser Application? :)

            var Context = g.InternalApplication.Context;

            Write("<title>jsc-solutions.net</title>");

            Write("<a href='http://jsc-solutions.net'><img border='0' src='/assets/ScriptCoreLib/jsc.png' /></a>");


            Write("<h2>Special pages</h2>");

            Write("<br /> " + "special page: " + "<a href='/robots.txt'>/robots.txt</a>");
            Write("<br /> " + "special page: " + "<a href='/xml'>/xml</a>");
            Write("<br /> " + "special page: " + "<a href='/crossdomain.xml'>/crossdomain.xml</a>");
            Write("<br /> " + "special page: " + "<a href='/favicon.ico'>/favicon.ico</a>");
            Write("<br /> " + "special page: " + "<a href='/jsc'>/jsc</a>");

            Write("<h2>WebMethods</h2>");



            foreach (var item in WebMethods)
            {
                WriteWebMethodForm(g, Write, item);
            }


            Write("<br /> Path: '" + Context.Request.Path + "'");
            Write("<br /> HttpMethod: '" + Context.Request.HttpMethod + "'");

            Write("<h2>Form</h2>");
            foreach (var item in Context.Request.Form.AllKeys)
            {
                Write("<br /> " + "<img src='http://i.msdn.microsoft.com/w144atby.pubproperty(en-us,VS.90).gif' /> <code>");
                Write(item);
                Write(" = ");
                Write(escapeXML(Context.Request.Form[item]));
                Write("</code>");
            }

            Write("<h2>QueryString</h2>");
            foreach (var item in Context.Request.QueryString.AllKeys)
            {
                Write("<br /> " + "<img src='http://i.msdn.microsoft.com/w144atby.pubproperty(en-us,VS.90).gif' /> <code>");
                Write(item);
                Write(" = ");
                Write(escapeXML(Context.Request.QueryString[item]));
                Write("</code>");
            }

            Write("<h2>Script Applications</h2>");

            foreach (var item in g.GetScriptApplications())
            {
                Write("<br /> " + "<img  script application: " + item.TypeName);

                foreach (var r in item.References)
                {
                    Write("<br /> &nbsp;&nbsp;&nbsp;&nbsp;");

                    Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' /> reference: ");
                    Write(r.AssemblyFile);

                }
            }

            Write("<h2>Files</h2>");

            foreach (var item in g.GetFiles())
            {
                Write("<br /> " + " file: <a href='" + item.Name + "'>" + item.Name + "</a>");
            }



        }

        private static void WriteXDocument(InternalGlobal g, StringAction Write, InternalWebMethodInfo WebMethod)
        {
            var that = g.InternalApplication;
            var Context = that.Context;

            Context.Response.ContentType = "text/xml";

            Write("<document>");

            if (WebMethod.Results != null)
                foreach (var item in WebMethod.Results)
                {
                    Write("<" + item.Name + ">");

                    foreach (var p in item.Parameters)
                    {
                        Write("<" + p.Name + ">");
                        Write(escapeXML(p.Value));
                        Write("</" + p.Name + ">");

                    }

                    Write("</" + item.Name + ">");

                }

            Write("</document>");

            that.CompleteRequest();
        }

        private static void WriteWebMethodForm(InternalGlobal that, StringAction Write, InternalWebMethodInfo WebMethod)
        {
            Write("<form target='_blank' action='" + WebMethod.ToQueryString() + "' method='POST'>");
            WriteWebMethod(Write, WebMethod,
                Parameter =>
                {
                    if (Parameter == null)
                    {
                        Write("<input type='submit' value='Invoke'  />");

                        return;
                    }

                    var key = "_" + WebMethod.MetadataToken + "_" + Parameter.Name;

                    Write(" = ");
                    Write("<input type='text'  name='" + key + "' value='" + Parameter.Value.Replace("'", "&apos;") + "' />");
                }
            );
            Write("</form>");
        }

        public delegate void InternalWebMethodParameterInfoAction(InternalWebMethodParameterInfo p);

        private static void WriteWebMethod(StringAction Write, InternalWebMethodInfo item, InternalWebMethodParameterInfoAction more)
        {
            if (string.IsNullOrEmpty(item.MetadataToken))
            {
                Write("<br /> ");
                Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubdelegate(en-us,VS.90).gif' />");
                Write(" method: <code>" + item.Name + "</code>");

            }
            else
            {
                Write("<br /> <img src='http://i.msdn.microsoft.com/deshae98.pubmethod(en-us,VS.90).gif' /> method: <code><a href='" + item.ToQueryString() + "'>" + item.Name + "</a></code>");
            }

            if (more != null)
                more(null);

            if (item.Parameters != null)
                foreach (var p in item.Parameters)
                {
                    Write("<br /> &nbsp;&nbsp;&nbsp;&nbsp;");

                    if (p.IsDelegate)
                    {
                        Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubdelegate(en-us,VS.90).gif' />");
                        Write(" parameter: <code>" + p.Name + "</code>");


                    }
                    else
                    {
                        Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' />");
                        Write(" parameter: <code>" + p.Name + "</code>");

                        if (more != null)
                            more(p);

                    }

                }


        }



        public static DefaultProfile InternalGetProfile(InternalGlobal g)
        {
            var that = g.InternalApplication;
            return (DefaultProfile)that.Context.Profile;
        }

    }

}
