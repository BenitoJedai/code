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
        class CompositeStream
        {
            public readonly IEnumerable<Func<Stream>> s;


            public CompositeStream(IEnumerable<Func<Stream>> s)
            {
                this.s = s;
            }


            // tested by ?


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
                return this.s.SelectMany(
                    x =>
                    {
                        var ss = x();


                        // Error	11	'System.Func<System.IO.Stream>' does not contain a definition for 'Read' and no extension method 'Read' accepting a first argument of type 'System.Func<System.IO.Stream>' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalGlobalExtensions.cs	70	36	ScriptCoreLib.Ultra.Library

                        var blocks = 1 + (int)(
                            ss.Length / buffer.Length
                        );

                        //var y = ss.Read(buffer, 0, buffer.Length);
                        //Console.WriteLine("after ReadByte " + new { y });
                        //var z = y > 0;

                        return Enumerable.Range(0, blocks).Select(
                            i => ss.Read(buffer, 0, buffer.Length)
                        );
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




        private static void WriteCacheManifest(InternalGlobal g, System.Web.HttpApplication that, StringAction WriteLine)
        {
            // should the app be able to control manifest on its own?

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201303/20130330-cache-manifest
            // http://html5doctor.com/go-offline-with-application-cache/
            that.Response.ContentType = WebApplicationCacheManifest.ManifestContentType;
            that.Response.AddHeader("Cache-Control", "no-cache, private");
            that.Response.AddHeader("Expires", "0");
            // http://stackoverflow.com/questions/1715568/how-to-properly-invalidate-an-html5-cache-manifest-for-online-offline-web-apps
            // Cache-Control: no-cache, private

            #region w
            var w = new StringBuilder();

            // http://www.whatwg.org/specs/web-apps/current-work/multipage/offline.html

            w.AppendLine("CACHE MANIFEST");

            var files = g.GetFiles();
            var bytes = 0;

            // do we need these?
            //w.AppendLine(WebApplicationIcon.Icon);
            //w.AppendLine(WebApplicationIcon.Image);

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

                // There’s no technical benefit to WOFF over TTF.
                if (Command.EndsWith(".ttf"))
                    w.AppendLine(Command);
            }

            w.AppendLine("");


            // what about fake pages used by historic api?
            // X:\jsc.svn\examples\javascript\async\AsyncHistoricActivities\AsyncHistoricActivities\Application.cs
            //w.AppendLine("FALLBACK:");
            //w.AppendLine("/fake-right /#/fake-right");

            // Application Cache Error event: Resource fetch failed (-1) http://192.168.43.252:30821/fake-right 
            // http://alistapart.com/article/application-cache-is-a-douchebag


            w.AppendLine("SETTINGS:");
            w.AppendLine("prefer-online");

            // http://html5doctor.com/go-offline-with-application-cache/
            // The first value is the request URI to match, and the second is the resource sent upon matching. It caches the resource on the right for offline use, so this should be an explicit path.
            //w.AppendLine("FALLBACK:");
            //w.AppendLine("/ /#offline");




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

                Write("<h2>No results from " + WebMethod.MethodName + "</h2>");
            }
            else
            {
                Write("<h2>" + WebMethod.Results.Length + " results from " + WebMethod.MethodName + "</h2>");
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

            #region document
            var w = new StringBuilder();


            w.Append("<document>");



            if (WebMethod.Results == null)
            {
                //Console.WriteLine("WriteXDocument Results null");
            }
            else
            {
                //Console.WriteLine("WriteXDocument Results " + new { WebMethod.Results.Length });

                foreach (var item in WebMethod.Results)
                {
                    w.Append("<" + item.MethodName + ">");

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
                                w.Append("<" + p.Name + ">");
                                w.Append(escapeXML(p.Value));
                                w.Append("</" + p.Name + ">");
                            }


                        }
                    }

                    w.Append("</" + item.MethodName + ">");

                }
            }

            if (WebMethod.TaskComplete)
            {
                w.Append("<TaskComplete>");

                if (WebMethod.TaskResult != null)
                {
                    //Console.WriteLine(new { WebMethod.TaskResult });

                    w.Append("<TaskResult>");
                    w.Append(escapeXML(WebMethod.TaskResult));
                    w.Append("</TaskResult>");
                }

                w.Append("</TaskComplete>");
            }

            w.Append("</document>");
            #endregion



            var ws = w.ToString();
            var wsbytes = Encoding.UTF8.GetBytes(ws);


            // http://stackoverflow.com/questions/1999824/whats-the-shortest-pair-of-strings-that-causes-an-md5-collision

            var ETagbytes = wsbytes.ToMD5Bytes();
            var newETag = ETagbytes.ToHexString();
            // does the client already have a copy of the same response?
            // if so, send 304 ?
            // http://www.codeproject.com/Articles/23857/The-Performance-Woe-of-Binary-XML
            // are we ready for encrypted binary xml?
            // http://msdn.microsoft.com/en-us/library/cc219210.aspx
            // file:///C:/Users/Arvo/Downloads/03-002r9_Binary_Extensible_Markup_Language_BXML_Encoding_Specification.pdf
            // is this the beginning of a binary diff service?


            // allow http to https calls
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            Context.Response.AddHeader("ETag", Convert.ToBase64String(ETagbytes));

            var hasETag = Context.Request.Form.AllKeys.Contains("ETag");
            if (hasETag)
            {
                var oldETag64 = Context.Request.Form["ETag"];
                var oldETagbytes = Convert.FromBase64String(oldETag64);
                var clientETag = oldETagbytes.ToHexString();

                //{ MethodName = Gravatar, MetadataToken = 06000001, oldETag = 1706b464adc232a9df3dd4539a206569 }
                //{ MethodName = Gravatar, MetadataToken = 06000001, ETag = 1706b464adc232a9df3dd4539a206569 }

                //Console.WriteLine(new { WebMethod.MethodName, WebMethod.MetadataToken, clientETag });

                //I/System.Console( 3988): #17 POST /xml/Gravatar HTTP/1.1
                //D/FastDormancy(  220):  before ======= ENTER DORMANCY =======
                //W/Threeg  (  918): Failed to read packet and byte counts from wifi interface
                //D/dalvikvm( 3988): GC_CONCURRENT freed 819K, 56% free 3157K/7111K, external 2013K/2108K, paused 2ms+2ms
                //I/System.Console( 3988): { MethodName = Gravatar, MetadataToken = 06000001, oldETag = d41d8cd98f00b204e9800998ecf8427e }
                //I/System.Console( 3988): { MethodName = Gravatar, MetadataToken = 06000001, ETag = 5a62b5d768653ae632ba4ff7e0b7a03c }
                //I/Web Console( 3988): %c0:312066ms InternalWebMethodRequest.Complete { Name = Gravatar, Length = 0 } at http://192.168.43.1:14691/view-source:37081
                //I/Web Console( 3988): 0:312076ms { Name = Gravatar, MetadataToken = 06000001, ETag = d41d8cd98f00b204e9800998ecf8427e, ElapsedMilliseconds = 5 } at http://192.168.43.1:14691/view-source:37040

                if (clientETag == newETag)
                {
                    //Console.WriteLine("Client already has the answer, sending 304");
                    // X:\jsc.svn\examples\javascript\forms\FormsDataBindingForEnabled\FormsDataBindingForEnabled\ApplicationControl.cs

                    // what will web client do with 304?
                    g.Context.Response.StatusCode = 304;
                    g.Context.Response.Flush();

                    that.CompleteRequest();
                    return;
                }

            }

            //Console.WriteLine(new { WebMethod.MethodName, WebMethod.MetadataToken, newETag, ws.Length });

            //I/System.Console( 4563): #4 POST /xml/Gravatar HTTP/1.1
            //I/System.Console( 4563): { MethodName = Gravatar, MetadataToken = 06000001, newETag = 1706b464adc232a9df3dd4539a206569, Length = 239 }

            //I/Web Console( 4563): %c0:22236ms InternalWebMethodRequest.Complete { Name = Gravatar, Length = 0 } at http://192.168.43.1:6686/view-source:37081
            //I/Web Console( 4563): 0:22345ms { Name = Gravatar, MetadataToken = 06000001, ETag = d41d8cd98f00b204e9800998ecf8427e, ElapsedMilliseconds = 25 } at http://192.168.43.1:6686/view-source:37040


            Context.Response.ContentType = "text/xml";

            // https://developer.mozilla.org/en-US/docs/HTTP/Access_control_CORS?redirectlocale=en-US&redirectslug=HTTP_access_control


            Write(ws);

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
                Write(" <code>" + item.MethodName + "</code>");

            }
            else
            {
                Write("<img src='http://i.msdn.microsoft.com/deshae98.pubmethod(en-us,VS.90).gif' /> <code><a style='text-decoration: none;' href='" + item.ToQueryString() + "'>" + item.MethodName + "</a></code>");
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
