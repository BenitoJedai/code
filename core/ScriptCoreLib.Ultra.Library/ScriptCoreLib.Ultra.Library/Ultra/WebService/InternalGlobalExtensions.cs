using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace ScriptCoreLib.Ultra.WebService
{
    public static class InternalGlobalExtensions
    {
        class CompositeStream
        {
            public readonly IEnumerable<Func<Stream>> s;


            public CompositeStream(IEnumerable<Func<Stream>> s)
            {
                this.s = s;
            }

#if JAVA_SUPPORTS_YIELD
            public IEnumerable<int> GetBytes(byte[] buffer)
            {
                Console.WriteLine("enter GetBytes");
                var x = this.s.GetEnumerator();
                //Console.WriteLine("before MoveNext");
                while (x.MoveNext())
                {
                    //Console.WriteLine("before Current");
                    var ss = x.Current();

                    var z = false;
                    //Console.WriteLine("before do");
                    do
                    {
                        //Console.WriteLine("before ReadByte " + new { buffer.Length });
                        var y = ss.Read(buffer, 0, buffer.Length);
                        //Console.WriteLine("after ReadByte " + new { y });
                        z = y > 0;

                        if (z)
                        {
                            //Console.WriteLine("before yield");
                            yield return y;
                            //Console.WriteLine("after yield");
                        }
                        //Console.WriteLine("before re do");
                    }
                    while (z);

                    //Console.WriteLine("before dispose");
                    ss.Dispose();
                }
                Console.WriteLine("exit GetBytes");
            }
#else
            public IEnumerable<int> GetBytes(byte[] buffer)
            {
                return this.s.Select(
                    x =>
                    {
                        var ss = x();


                        // Error	11	'System.Func<System.IO.Stream>' does not contain a definition for 'Read' and no extension method 'Read' accepting a first argument of type 'System.Func<System.IO.Stream>' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalGlobalExtensions.cs	70	36	ScriptCoreLib.Ultra.Library


                        var y = ss.Read(buffer, 0, buffer.Length);
                        //Console.WriteLine("after ReadByte " + new { y });
                        var z = y > 0;

                        return y;
                    }
                );
            }
#endif

        }



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

            #region WriteFile
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
                g.Response.AddHeader("Content-Length", "" + CurrentFile.Length);

                var ContentType = "application/octet-stream";
                var n = CurrentFile.Name;

                // http://www.webmaster-toolkit.com/mime-types.shtml
                if (n.EndsWith(".gif")) ContentType = "image/gif";
                else if (n.EndsWith(".htm")) ContentType = "text/html";

                else if (n.EndsWith(".png")) ContentType = "image/png";
                else if (n.EndsWith(".jpg")) ContentType = "image/jpg";
                else if (n.EndsWith(".svg")) ContentType = "image/svg+xml";

                else if (n.EndsWith(".js")) ContentType = "application/x-javascript";

                else if (n.EndsWith(".mp3")) ContentType = "audio/mpeg3";
                else if (n.EndsWith(".wav")) ContentType = "audio/wav";
                else if (n.EndsWith(".mid")) ContentType = "audio/midi";

                else if (n.EndsWith(".css")) ContentType = "text/css";

                that.Response.ContentType = ContentType;

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
            #endregion


            #region favicon
            if (Path == "/favicon.ico")
            {
                Context.Response.WriteFile("assets/ScriptCoreLib/jsc.ico");

                that.CompleteRequest();
                return;
            }
            #endregion



            #region robots
            if (Path == "/robots.txt")
            {
                Context.Response.StatusCode = 404;
                that.CompleteRequest();
                return;
            }
            #endregion

            #region crossdomain
            if (Path == "/crossdomain.xml")
            {
                Context.Response.StatusCode = 404;
                that.CompleteRequest();
                return;
            }
            #endregion


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

            #region WriteCacheManifest
            if (Path == "/" + WebApplicationCacheManifest.ManifestName)
            {
                WriteCacheManifest(g, that, WriteLine);
                return;
            }
            #endregion



            var WebMethods = g.GetWebMethods();

            //Console.WriteLine();


            // why not do it on demand?
            //foreach (var item in WebMethods)
            //{
            //    item.LoadParameters(that.Context);
            //}

            #region POST
            if (Context.Request.HttpMethod == "POST")
            {
                var WebMethod = InternalWebMethodInfo.First(
                    WebMethods,
                    Context.Request.QueryString[InternalWebMethodInfo.QueryKey]
                );
                if (WebMethod == null)
                {
                    // let user defined handler handle it..
                }
                else
                {
                    WebMethod.LoadParameters(that.Context);

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
            #endregion


            var IsComplete = false;

            #region WebServiceHandler
            var h = new WebServiceHandler
            {
                Context = that.Context,

                CompleteRequest = delegate
                {
                    IsComplete = true;
                    that.CompleteRequest();
                },

                Applications = g.GetScriptApplications(),

                // tested by
                // X:\jsc.svn\examples\javascript\synergy\webgl\EvilChopperByPer\EvilChopperByPer\ApplicationWebService.cs
                // X:\jsc.svn\examples\javascript\synergy\webgl\WebGLDoomByInt13h\WebGLDoomByInt13h\ApplicationWebService.cs
                GetFiles = g.GetFiles,

                Default = delegate
                {
                    that.Response.ContentType = "text/html";

                    // todo: jsc: PHP workaround required
                    var apps = g.GetScriptApplications();
                    var app = apps[0];








                    var Host = that.Context.Request.Headers["Host"].TakeUntilIfAny(":");

                    var CacheManifest = true;

                    // should disable that for android webview?

                    //if (Host == that.Context.Request.UserHostAddress)
                    //    CacheManifest = false;

                    //// webdev?
                    //if ("127.0.0.1" == that.Context.Request.UserHostAddress)
                    //    CacheManifest = false;

                    app.WriteTo(Write, CacheManifest);

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
            #endregion

            #region WriteSource
            h.WriteSource = app =>
            {
                h.Context.Response.ContentType = "text/javascript";

                g.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
                g.Response.Cache.SetExpires(DateTime.Now.AddMinutes(15));






                #region GetFiles
                var ff = g.GetFiles();

                // jsc packages js files? not for long:P will switch to gzip at some point!
                var app_references = app.References.Select(
                    // why wont Single work correctly?
                    // are we embedding one file multiple times?
                    item => ff.First(k => k.Name == item.AssemblyFile || k.Name == item.AssemblyFile + ".js")
                ).ToArray();
                #endregion



                app_references.WithEachIndex(
                    (app_ref, index) =>
                    {
                        // will this work an all platforms?
                        // need to test!
                        g.Response.AddHeader("X-Reference-" + index, app_ref.Name + " " + app_ref.Length);
                    }
                );


                // tested by
                // X:\jsc.svn\examples\javascript\android\com.abstractatech.adminshell\com.abstractatech.adminshell\ApplicationWebService.cs
                if (app.DiagnosticsMakeItSlowAndAddSalt)
                {
                    Console.WriteLine("enter DiagnosticsMakeItSlowAndAddSalt");

                    h.Context.Response.ContentType = "application/octet-stream";


                    #region composite
                    var composite =
                        new CompositeStream(
                           app_references.Select(
                            k =>
                            {
                                return new Func<Stream>(
                                    () =>
                                    {
                                        Console.WriteLine("composite: " + new { k.Name });
                                        return (Stream)File.OpenRead(k.Name);
                                    }
                                );
                            }
                        )
                    );
                    #endregion


                    Console.WriteLine("encrypting... ");

                    //var x = new MemoryStream();

                    var buffer = new byte[1024 * 40];
                    //var count = composite.GetBytes(buffer).Count();

                    #region count
                    var count = 0;


                    foreach (var y in composite.GetBytes(buffer))
                    {
                        count += y;

                        Console.WriteLine(new { count });
                    }

                    #endregion

                    // encrypting... { count = 58 }

                    Console.WriteLine("encrypting... " + new { count });

                    var time = new Stopwatch();
                    time.Start();

                    var bytesleft = count;

                    g.Response.AddHeader("Content-Length", "" + (count * 2));
                    g.Response.AddHeader("X-DiagnosticsMakeItSlowAndAddSalt", "ok");


                    //                    lets write DiagnosticsMakeItSlowAndAddSalt
                    //enter DiagnosticsMakeItSlowAndAddSalt
                    //encrypting...
                    //enter GetBytes
                    //exit GetBytes
                    //encrypting... { count = 2282136 }
                    //enter GetBytes

                    // ? 
                    // needs to work in .net then in android!

                    var xbuffer = new byte[buffer.Length * 2];

                    foreach (var length in composite.GetBytes(buffer))
                    {
                        for (int i = 0; i < length; i++)
                        {
                            var item = buffer[i];

                            var lo = (byte)(item & 0xf);
                            var hi = (byte)((item & 0xf0) >> 4);


                            xbuffer[i * 2 + 0] = lo;
                            xbuffer[i * 2 + 1] = hi;

                            //h.Context.Response.OutputStream.WriteByte(lo);
                            //h.Context.Response.OutputStream.WriteByte(hi);



                            bytesleft--;




                        }

                        h.Context.Response.OutputStream.Write(xbuffer, 0, length * 2);
                        h.Context.Response.Flush();

                        var timetarget = 8000 - time.ElapsedMilliseconds;

                        //                 Caused by: java.lang.ArithmeticException: divide by zero
                        //at ScriptCoreLib.Ultra.WebService.InternalGlobalExtensions___c__DisplayClasse._InternalApplication_BeginRequest_b__7(InternalGlobalExtensions___c__DisplayClasse.java:235)

                        //var ms = (int)(timetarget / bytesleft);



                        Console.WriteLine("." + new
                        {
                            bytesleft
                            //, ms 
                        });

                        //if (timetarget > 0)
                        //{
                        //    Thread.Sleep(ms);
                        //}
                    }





                    Console.WriteLine("will upload done " + new { time.ElapsedMilliseconds });

                    return;
                }


                #region GZipAssemblyFile
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201301/20130103

                var AcceptEncoding = h.Context.Request.Headers["Accept-Encoding"];

                if (!string.IsNullOrEmpty(AcceptEncoding))
                    if (AcceptEncoding.Contains("gzip"))
                    {
                        g.Response.AddHeader("Content-Encoding", "gzip");



                        g.Response.WriteFile("/" + app.GZipAssemblyFile);
                        h.CompleteRequest();

                        return;
                    }
                #endregion

                g.Response.AddHeader("X-Comment", "gzip was disabled");

                #region the old way


                var app_size = app_references.Sum(k => k.Length);

                // Accept-Encoding:gzip,deflate,sdch

                g.Response.AddHeader("Content-Length", "" + app_size);
                //g.Response.AddHeader("X-GZipAssemblyFile", "" + app.GZipAssemblyFile);


                foreach (var item in app_references)
                {
                    // asp.net needs absolute paths
                    h.Context.Response.WriteFile("/" + item.Name);
                }
                #endregion


                h.CompleteRequest();
            };
            #endregion

            g.Serve(h);


            if (!IsComplete)
            {
                if (that.Request.Path == "/jsc")
                {
                    h.Diagnostics();
                    return;
                }

                #region /view-source
                if (that.Request.Path == "/view-source")
                {
                    var app = h.Applications[0];

                    h.WriteSource(app);

                    return;
                }
                #endregion

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
            // should the app be able to control manifest on its own?

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201303/20130330-cache-manifest

            that.Response.ContentType = WebApplicationCacheManifest.ManifestContentType;
            that.Response.AddHeader("Cache-Control", "no-cache, private");

            // http://stackoverflow.com/questions/1715568/how-to-properly-invalidate-an-html5-cache-manifest-for-online-offline-web-apps
            // Cache-Control: no-cache, private

            #region w
            var w = new StringBuilder();

            // http://www.whatwg.org/specs/web-apps/current-work/multipage/offline.html

            w.AppendLine("CACHE MANIFEST");

            var files = g.GetFiles();
            var bytes = 0;

            w.AppendLine(WebApplicationIcon.Icon);
            w.AppendLine(WebApplicationIcon.Image);

            //Explicit entries

            w.AppendLine("/");
            w.AppendLine("/view-source");


            foreach (var item in files)
            {

                var Command = item.Name;

                bytes += item.Length;

                // webkit seems to have 5MB limit.
                // http://groups.google.com/a/chromium.org/group/chromium-html5/browse_thread/thread/e911f18b905d28ee/9f54c8cc1e8afb5d
                // http://stackoverflow.com/questions/2908459/mobile-safari-5mb-html5-application-cache-limit
                // http://www.yuiblog.com/blog/2010/07/12/mobile-browser-cache-limits-revisited/

                // hack.

                // we need to figure out how to make the application fit to the cache limits.
                // we could be optimizing javascript.

                if (Command.EndsWith(".css"))
                    w.AppendLine(Command);

                if (Command.EndsWith(".swf"))
                    w.AppendLine(Command);

            }

            w.AppendLine("");
            w.AppendLine("SETTINGS:");
            w.AppendLine("prefer-online");

            w.AppendLine("");
            w.AppendLine("NETWORK:");
            w.AppendLine("*");

            var now = DateTime.Now;

            // Application Cache Error event: Manifest changed during update, scheduling retry 
            w.AppendLine("");
            w.AppendLine("# " + new { bytes });
            w.AppendLine("");
            #endregion

            //            Implementation not found for type import :
            //type: System.Text.StringBuilder
            //method: Int32 get_Length()
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!


            that.Response.AddHeader("Content-Length", "" + w.Length);
            // chrome://appcache-internals/
            that.Response.Write(w.ToString());


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
            Write("<blockquote>");
            if (WebMethod.Results == null)
            {

                Write("<h2>No results from " + WebMethod.Name + "</h2>");
            }
            else
            {
                Write("<h2>" + WebMethod.Results.Length + " results from " + WebMethod.Name + "</h2>");
                Write("<blockquote>");

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
                Write("</blockquote>");
                Write("<br />");
            }
            Write("</blockquote>");
        }

        private static void WriteDiagnostics(InternalGlobal g, StringAction Write, InternalWebMethodInfo[] WebMethods)
        {
            // should the diagnostics be a separate rich Browser Application? :)

            var Context = g.InternalApplication.Context;

            Write("<title>jsc-solutions.net</title>");
            Write("<br/><center><a href='/'>Launch Application</a></center><br/>");

            Write("<a href='http://jsc-solutions.net'><img border='0' src='/assets/ScriptCoreLib/jsc.png' /></a>");


            Write("<blockquote>");
            Write("<h2>Special pages</h2>");
            Write("<blockquote>");

            // like CON in filesystem?
            Write("<br /> " + "special page: " + "<a href='/robots.txt'>/robots.txt</a>");
            Write("<br /> " + "special page: " + "<a href='/xml'>/xml</a>");
            Write("<br /> " + "special page: " + "<a href='/crossdomain.xml'>/crossdomain.xml</a>");
            Write("<br /> " + "special page: " + "<a href='/favicon.ico'>/favicon.ico</a>");
            Write("<br /> " + "special page: " + "<a href='/jsc'>/jsc</a>");
            Write("<br /> " + "special page: " + "<a href='/view-source'>/view-source</a>");
            Write("</blockquote>");

            Write("<h2>Methods</h2>");
            Write("<blockquote>");
            foreach (var item in WebMethods)
            {
                WriteWebMethodForm(Write, item);
            }
            Write("</blockquote>");


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

            var ff = g.GetFiles();

            // http://msdn.microsoft.com/en-us/library/y47ychfe.aspx

            Write("<h2>Applications</h2>");
            Write("<blockquote>");

            foreach (var app in g.GetScriptApplications())
            {
                Write("<br /> ");

                Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' />");

                Write(" <code style='color: darkcyan;'>" + app.TypeName + "</code>");

                // var app_references = app.References.Select(
                //    item => ff.First(k => k.Name == item.AssemblyFile + ".js")
                //).ToArray();

                // var app_size = app_references.Sum(k => k.Length);

                // Write(" <span style='color: gray;'>(" + app_size + " bytes)</span>");

                foreach (var r in app.References)
                {
                    Write("<br /> &nbsp;&nbsp;&nbsp;&nbsp;");

                    Write("<img src='http://i.msdn.microsoft.com/dynimg/IC477625.png' />");
                    Write(" " + r.AssemblyFile);

                }
            }
            Write("</blockquote>");

            Write("<h2>Files</h2>");
            Write("<blockquote>");

            Action<string> separator = delegate { };

            foreach (var item in g.GetFiles())
            {
                separator(item.Name);

                Write(
                    "<br /> "
                    + " file: <a href='" + item.Name + "'>" + item.Name + "</a>" + " size: " + item.Length
                );


                // do we need this?
                //var itemref_csharp4 = item;

                separator =
                    next =>
                    {
                        if (next.TakeUntilLastOrEmpty("/") == item.Name.TakeUntilLastOrEmpty("/"))
                        {
                            return;
                        }

                        Write(
                            "<br /> "
                        );

                    };

            }
            Write("</blockquote>");
            Write("</blockquote>");

        }

        private static void WriteXDocument(InternalGlobal g, StringAction Write, InternalWebMethodInfo WebMethod)
        {
            var that = g.InternalApplication;
            var Context = that.Context;

            Context.Response.ContentType = "text/xml";

            // https://developer.mozilla.org/en-US/docs/HTTP/Access_control_CORS?redirectlocale=en-US&redirectslug=HTTP_access_control

            // allow http to https calls
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");


            Write("<document>");

            if (WebMethod.Results != null)
            {
                foreach (var item in WebMethod.Results)
                {
                    Write("<" + item.Name + ">");

                    if (item.Parameters != null)
                    {
                        foreach (var p in item.Parameters)
                        {

                            if (p.Value == null)
                            {
                                // no parameter?
                                // X:\jsc.svn\examples\javascript\WebMethodXElementTransferExperiment\WebMethodXElementTransferExperiment\ApplicationWebService.cs
                            }
                            else
                            {
                                Write("<" + p.Name + ">");
                                Write(escapeXML(p.Value));
                                Write("</" + p.Name + ">");
                            }


                        }
                    }

                    Write("</" + item.Name + ">");

                }
            }

            Write("</document>");

            that.CompleteRequest();
        }

        public static void WriteWebMethodForm(StringAction Write, InternalWebMethodInfo WebMethod)
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

                    // C# named parameters style
                    Write(": ");

                    var value = "";

                    Parameter.Value.With(x => value = x.Replace("'", "&apos;"));


                    Write("<input type='text'  name='" + key + "' value='" + value + "' />");
                }
            );
            Write("</form>");
        }

        public delegate void InternalWebMethodParameterInfoAction(InternalWebMethodParameterInfo p);

        public static void WriteWebMethod(StringAction Write, InternalWebMethodInfo item, InternalWebMethodParameterInfoAction more)
        {
            Write("<br /> ");

            if (string.IsNullOrEmpty(item.MetadataToken))
            {
                // when does this happen?

                Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubdelegate(en-us,VS.90).gif' />");
                Write(" <code>" + item.Name + "</code>");

            }
            else
            {
                Write("<img src='http://i.msdn.microsoft.com/deshae98.pubmethod(en-us,VS.90).gif' /> <code><a style='text-decoration: none;' href='" + item.ToQueryString() + "'>" + item.Name + "</a></code>");
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
                        Write(" <code>" + p.Name + "</code>");


                    }
                    else
                    {
                        Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' />");
                        Write(" <code>" + p.Name + "</code>");

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
