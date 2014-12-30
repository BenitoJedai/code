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
    public static partial class InternalGlobalExtensions
    {
        public static void InternalApplication_BeginRequest(InternalGlobal g)
        {
            // is it ROSLYN friendly? no?
            // need to compile it by 2012?


            var BeginRequestStopwatch = Stopwatch.StartNew();


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




            var WebMethods = g.GetWebMethods();



            // X:\jsc.svn\examples\javascript\test\TestBaseFieldSync\TestBaseFieldSync\ApplicationWebService.cs

            #region WriteInternalFields InternalFields -> AppendCookie
            Action<InternalWebMethodInfo> WriteInternalFields =
                x =>
                {
                    // does 304 check also look at 
                    // fields?

                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140323
                    // X:\jsc.svn\examples\javascript\forms\FormsDataBindingForEnabled\FormsDataBindingForEnabled\ApplicationWebService.cs

                    if (x.InternalFields == null)
                        return;

                    // typename instead?
                    var c = new HttpCookie("InternalFields");
                    // X:\jsc.svn\examples\javascript\Test\TestWebServiceTaskFields\TestWebServiceTaskFields\ApplicationWebService.cs


                    foreach (string InternalFieldName in x.InternalFields.Keys.ToArray())
                    {
                        //Console.WriteLine(new { InternalFieldName });

                        // for /xml post
                        if (Context.Request.HttpMethod == "POST")
                        {
                            // GetParameterValue: { key = _0600000e_field_elapsed }

                            that.Context.Response.AddHeader(
                                ".field " + InternalFieldName,
                                x.InternalFields[InternalFieldName]
                            );
                        }
                        else
                        {

                            // for / get
                            c[InternalFieldName] = x.InternalFields[InternalFieldName];
                        }
                    }

                    // Set-Cookie:InternalFields=field_Foo=7; path=/
                    //that.Context.Response.AppendCookie(c);
                    if (Context.Request.HttpMethod == "POST")
                    {

                    }
                    else
                    {
                        that.Context.Response.SetCookie(c);
                    }


                };
            #endregion



            var IsComplete = false;

            #region handler = WebServiceHandler
            var handler = new WebServiceHandler
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


                #region Default
                Default = delegate
                {
                    that.Response.ContentType = "text/html";

                    // todo: jsc: PHP workaround required
                    var apps = g.GetScriptApplications();
                    var app = apps[0];








                    var Host = that.Context.Request.Headers["Host"].TakeUntilIfAny(":");

                    var CacheManifest = false;
                    //var CacheManifest = true;

                    // should disable that for android webview?

                    //if (Host == that.Context.Request.UserHostAddress)
                    //    CacheManifest = false;

                    //// webdev?
                    //if ("127.0.0.1" == that.Context.Request.UserHostAddress)
                    //    CacheManifest = false;

                    // X:\jsc.svn\examples\javascript\test\TestServiceWorkerAssetCache\TestServiceWorkerAssetCache\Application.cs
                    app.WriteTo(Write, CacheManifest);

                    IsComplete = true;
                    that.CompleteRequest();
                },
                #endregion


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

            #region handler.WriteSource
            handler.WriteSource = app =>
            {
                handler.Context.Response.ContentType = "text/javascript";

                g.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);

                // are we not part of AppCache?
                g.Response.Cache.SetExpires(DateTime.Now.AddMinutes(15));

                //Console.WriteLine("IsConstructor WriteInternalFields");

                var Constructor = new InternalWebMethodInfo
                {
                    IsConstructor = true
                };

                // Method not found: '?'.
                // Additional information: Attempt by method 'mscorlib.<02000005IEnumerable\+ConvertToString>.ConvertToString(System.Collections.Generic.IEnumerable`1<TestIEnumerableForService.foo>)' 
                // to access method '<>f__AnonymousType6`1<System.__Canon>..ctor(System.__Canon)' failed.
                g.Invoke(Constructor);

                WriteInternalFields(Constructor);




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
                #region DiagnosticsMakeItSlowAndAddSalt
                if (app.DiagnosticsMakeItSlowAndAddSalt)
                {
                    Console.WriteLine("enter DiagnosticsMakeItSlowAndAddSalt");

                    handler.Context.Response.ContentType = "application/octet-stream";


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

                    Console.WriteLine("encrypting by splitting bytes... " + new { count });

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
                        Console.WriteLine("before flush of " + new { length });

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

                        handler.Context.Response.OutputStream.Write(xbuffer, 0, length * 2);
                        handler.Context.Response.Flush();

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
                #endregion


                #region GZipAssemblyFile
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201301/20130103

                var AcceptEncoding = handler.Context.Request.Headers["Accept-Encoding"];

                if (!string.IsNullOrEmpty(AcceptEncoding))
                    if (AcceptEncoding.Contains("gzip"))
                    {
                        g.Response.AddHeader("Content-Encoding", "gzip");



                        g.Response.WriteFile("/" + app.GZipAssemblyFile);
                        handler.CompleteRequest();

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
                    handler.Context.Response.WriteFile("/" + item.Name);
                }
                #endregion


                handler.CompleteRequest();
            };
            #endregion


            #region WebMethodMetadataToken
            if (Context.Request.HttpMethod == "POST")
            {
                // tested by
                // X:\jsc.svn\examples\javascript\Test\TestWebMethodIPAddress\TestWebMethodIPAddress\ApplicationWebService.cs
                // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\JavaScript\InternalWebMethodRequest.cs
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140110-xml

                //var WebMethodMetadataToken = Context.Request.QueryString[InternalWebMethodInfo.QueryKey];
                var WebMethodMetadataToken = Context.Request.Form["WebMethodMetadataToken"];
                //var value_Form = that.InternalContext.Request.Form[key];

                handler.WebMethod = InternalWebMethodInfo.First(
                    WebMethods,
                   WebMethodMetadataToken
                );
            }
            #endregion


            g.Serve(handler);

            if (IsComplete)
                return;


            #region POST
            if (Context.Request.HttpMethod == "POST")
            {
                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201401/20140101
                //Console.WriteLine("about to load params for " + new { handler.WebMethod });

                if (handler.WebMethod == null)
                {
                    // let user defined handler handle it..
                }
                else
                {
                    // is it also supposed to load the fields?
                    handler.WebMethod.LoadParameters(that.Context);

                    Console.WriteLine("enter invoke " + new { handler.WebMethod });

                    //about to load params for { WebMethod = { IsConstructor = false, MetadataToken = 06000002, Name = Insert, TypeFullName = WebCamAvatarsExperiment.ApplicationWebService, Parameters = 1 } }
                    //about to invoke { WebMethod = { IsConstructor = false, MetadataToken = 06000002, Name = Insert, TypeFullName = WebCamAvatarsExperiment.ApplicationWebService, Parameters = 1 } }
                    // when can we invoke the webmethod and not be at /xml?

                    //try
                    //{

                    // where is the code gen?
                    // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.WebService.cs

                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201401/20140107-dev/test
                    var WebMethodStopwatch = Stopwatch.StartNew();

                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140405/task
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140517
                    // defined by X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.WebService.cs

                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140811

                    // public abstract void Invoke(InternalWebMethodInfo e);
                    g.Invoke(handler.WebMethod);


                    // BeginRequestStopwatch
                    //that.Response.AddHeader("X-BeginRequestStopwatch", "" + BeginRequestStopwatch.ElapsedMilliseconds);

                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201401/20140110-stats
                    that.Response.AddHeader("X-ElapsedMilliseconds", "" + WebMethodStopwatch.ElapsedMilliseconds);

                    if (handler.WebMethod.AtElapsedMilliseconds != null)
                        handler.WebMethod.AtElapsedMilliseconds(WebMethodStopwatch.ElapsedMilliseconds);

                    //that.Response.AddHeader("X-WebMethodStopwatch", "" + SQLiteConnectionStringBuilderExtensions.ElapsedMilliseconds);

                    //}
                    //catch (Exception err)
                    //{
                    //    Console.WriteLine("boom! " + new { err });
                    //}

                    //Console.WriteLine("exit invoke " + new { handler.WebMethod });

                    //if (that.Context.Request.Path == "/xml")
                    if (that.Context.Request.Path != "/jsc")
                    {
                        // the diagnostics needs more attention down the road.


                        WriteInternalFields(handler.WebMethod);

                        // no yields
                        // why is handler.WebMethod.Results null??
                        #region 204
                        if (handler.WebMethod.Results.Length == 0)
                            if (handler.WebMethod.TaskResult == null)
                            {
                                // or should we send binary zip for webworker?
                                // NoContent
                                g.Context.Response.StatusCode = 204;
                                g.Context.Response.Flush();

                                that.CompleteRequest();
                                return;
                            }
                        #endregion


                        WriteXDocument(g, Write, handler.WebMethod);
                        that.CompleteRequest();
                        return;
                    }

                    that.Response.ContentType = "text/html";
                    WriteDiagnosticsResults(Write, handler.WebMethod);
                    WriteDiagnostics(g, Write, WebMethods);
                    that.CompleteRequest();
                    return;
                }
            }
            #endregion


            if (IsComplete)
                return;


            #region /favicon.ico
            if (Path == "/favicon.ico")
            {
                Context.Response.WriteFile("assets/ScriptCoreLib/jsc.ico");

                that.CompleteRequest();
                return;
            }
            #endregion



            #region /robots.txt
            if (Path == "/robots.txt")
            {
                Context.Response.StatusCode = 404;
                that.CompleteRequest();
                return;
            }
            #endregion

            #region /crossdomain.xml
            if (Path == "/crossdomain.xml")
            {
                // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalGlobalExtensions.InternalApplication_BeginRequest.cs
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140109-webclient
                // http://www.adobe.com/devnet/adobe-media-server/articles/cross-domain-xml-for-streaming.html
                // X:\jsc.smokescreen.svn\market\appengine\xmoneseAIR\xmoneseAIR\ApplicationCanvas.cs
                Context.Response.Write(@"<?xml version='1.0'?>
<!-- http://www.osmf.org/crossdomain.xml -->
<!DOCTYPE cross-domain-policy SYSTEM 'http://www.adobe.com/xml/dtds/cross-domain-policy.dtd'>
<cross-domain-policy>
    <allow-access-from domain='*' />
    <site-control permitted-cross-domain-policies='all'/>
</cross-domain-policy>");

                that.CompleteRequest();
                return;
            }
            #endregion


            // cache manifest will not be needed
            // after ServiceWorker becomes useful
            // 20141230
            // X:\jsc.svn\examples\javascript\test\TestServiceWorkerAssetCache\TestServiceWorkerAssetCache\Application.cs
            #region WriteCacheManifest
            if (Path == "/" + WebApplicationCacheManifest.ManifestName)
            {
                // <html manifest="cache-manifest">
                //WriteCacheManifest(g, that, WriteLine);

                Context.Response.StatusCode = 404;
                that.CompleteRequest();
                return;
            }
            #endregion



            if (that.Request.Path == "/jsc")
            {
                handler.Diagnostics();
                return;
            }

            #region /view-source
            if (that.Request.Path == "/view-source")
            {


                var app = handler.Applications[0];

                // can we just invoke a ctor?
                // and have the fields returne?

                handler.WriteSource(app);

                return;
            }
            #endregion


            if (handler.IsDefaultPath)
            {
                handler.Default();
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
            handler.Redirect();
            //h.Diagnostics();
        }



    }

}
