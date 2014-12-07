using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections.Specialized;
using System.Web;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;


using java.io;


namespace ScriptCoreLibJava.BCLImplementation.System.Web
{
    [Script]
    static class __copy
    {
        public static string TakeUntilOrEmpty(this string e, string u)
        {
            var i = e.IndexOf(u);

            if (i < 0)
                return "";

            return e.Substring(0, i);
        }

        public static string SkipUntilOrEmpty(this string e, string u)
        {
            if (null == e)
                return "";

            if (u == null)
                return "";


            var i = e.IndexOf(u);

            if (i < 0)
                return "";

            return e.Substring(i + u.Length);
        }
    }

    [Script(Implements = typeof(global::System.Web.HttpRequest))]
    internal class __HttpRequest
    {
        // check sdk!
        //Y:\TestThreadManager.ApplicationWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Web\__HttpRequest.java:5: error: package javax.servlet.http does not exist
        //import javax.servlet.http.Cookie;
        //                         ^


        //[jsc.internal] UnhandledException:
        //{ FullName = System.InvalidOperationException, InnerException =  }

        //{ ExceptionObject = System.InvalidOperationException: Cannot process request because the process (5724) has exited.
        //   at System.Diagnostics.Process.GetProcessHandle(Int32 access, Boolean throwIfExited)
        //   at System.Diagnostics.Process.Kill()
        //   at jsc.meta.Commands.Configuration.ConfigurationDisposeSubst.<>c__DisplayClass2.<Monitor>b__1()
        //   at jsc.meta.Library.VolumeFunctions.VolumeFunctionsExtensions.ToVirtualDriveToDirectory.Dispose()

        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Web\HttpRequest.cs
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalHttpServlet.cs
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalAndroidWebServiceActivity.cs

        public string UserHostAddress
        {
            get
            {
                // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs
                return this.InternalContext.getRemoteAddr();
            }
        }

        public javax.servlet.http.HttpServletRequest InternalContext;

        public string Path
        {
            get
            {
                return this.InternalContext.getPathInfo();
            }
        }

        public string HttpMethod
        {
            get
            {
                // or http://msdn.microsoft.com/en-us/library/system.web.httprequest.requesttype(VS.85).aspx?

                return this.InternalContext.getMethod();
            }
        }

        public Uri Url
        {
            get
            {
                return
                    new Uri(
                        this.InternalContext.getRequestURL().toString()
                    );

            }
        }

        #region Form
        NameValueCollection InternalForm;

        public NameValueCollection Form
        {
            get
            {
                //Console.WriteLine("get_Form");

                if (InternalForm == null)
                {
                    InternalForm = new NameValueCollection();
                    InitializeForm();
                }

                //Console.WriteLine("return get_Form");

                return InternalForm;
            }
        }

        private void InitializeForm()
        {
            //Console.WriteLine("get_Form = new");


            var e = this.InternalContext.getParameterNames();

            // For HTTP servlets, parameters are contained in 
            // the query string or posted form data.

            // see: http://209.85.229.132/search?q=cache:0aKqPR_HgIUJ:g.oswego.edu/dl/classes/collections/SimpleTest.java+while+hasMoreElements&cd=1&hl=en&ct=clnk
            // see: http://java.sun.com/j2ee/1.4/docs/api/javax/servlet/ServletRequest.html#getParameter(java.lang.String)
            // see: http://java.sun.com/j2ee/1.4/docs/api/javax/servlet/http/HttpServletRequest.html#getQueryString()

            var qs = this.InternalContext.getQueryString();
            var q = new InternalQueryStringParser(qs);

            while (e.hasMoreElements())
            {
                var name = (string)e.nextElement();

                if (q[name] == null)
                {
                    var value = this.InternalContext.getParameter(name);

                    //Console.WriteLine("Form add: " + name + " = " + value);
                    InternalForm[name] = value;
                }
                else
                {
                    //Console.WriteLine("Form skip: " + name);

                }
            }
        }
        #endregion

        #region QueryString
        [Script]
        public class InternalQueryStringParser : NameValueCollection
        {
            // code duplication :)
            public readonly string QueryString;

            public InternalQueryStringParser(string QueryString)
            {
                if (null == QueryString)
                {
                    this.QueryString = "";

                    return;
                }

                this.QueryString = QueryString;

                //Console.WriteLine("InternalQueryStringParser: QueryString=" + QueryString);

                foreach (var item in QueryString.Split('&'))
                {
                    var p = item.Split('=');

                    if (p.Length == 2)
                    {
                        var value = p[0];
                        var name = p[1];

                        this[value] = name;

                        //Console.WriteLine("InternalQueryStringParser: " + value + " = " + name);
                    }

                }
            }
        }

        NameValueCollection InternalQueryString;

        public NameValueCollection QueryString
        {
            get
            {
                if (InternalQueryString == null)
                {
                    InternalQueryString = new NameValueCollection();

                    // For HTTP servlets, parameters are contained in 
                    // the query string or posted form data.
                    var e = this.InternalContext.getParameterNames();

                    // see: http://209.85.229.132/search?q=cache:0aKqPR_HgIUJ:g.oswego.edu/dl/classes/collections/SimpleTest.java+while+hasMoreElements&cd=1&hl=en&ct=clnk

                    var qs = this.InternalContext.getQueryString();
                    var q = new InternalQueryStringParser(qs);

                    while (e.hasMoreElements())
                    {
                        var name = (string)e.nextElement();

                        if (q[name] != null)
                        {
                            var value = this.InternalContext.getParameter(name);

                            //Console.WriteLine("QueryString add: " + name + " = " + value);
                            InternalQueryString[name] = value;
                        }
                        else
                        {
                            //Console.WriteLine("QueryString skip: " + name);

                        }
                    }
                }


                return InternalQueryString;
            }
        }
        #endregion

        #region Headers
        NameValueCollection InternalHeaders;
        public NameValueCollection Headers
        {
            get
            {
                if (InternalHeaders == null)
                {
                    InternalHeaders = new NameValueCollection();

                    var e = this.InternalContext.getHeaderNames();

                    while (e.hasMoreElements())
                    {
                        var name = (string)e.nextElement();

                        var value = this.InternalContext.getHeader(name);

                        InternalHeaders[name] = value;
                    }

                }

                return InternalHeaders;
            }
        }
        #endregion

        public HttpCookieCollection Cookies
        {
            get
            {
                // X:\jsc.svn\examples\java\appengine\Test\TestThreadManager\TestThreadManager\ApplicationWebService.cs

                var Cookies = new HttpCookieCollection();

                if (this.InternalContext != null)
                {
                    var i = this.InternalContext.getCookies();

                    if (i != null)
                        foreach (var item in i)
                        {
                            if (item != null)
                                Cookies.Add(
                                    new HttpCookie(item.getName(), item.getValue())
                                );
                        }
                }

                return Cookies;
            }
        }

        public __HttpRequest()
        {
        }

        public Uri UrlReferrer
        {
            get
            {
                return new Uri(this.Headers["Referer"]);
            }
        }




        public string ContentType
        {
            get
            {
                var __Request = this;
                var __RequestContentType = __Request.Headers["Content-Type"];

                return __RequestContentType;
            }
        }

        //        Implementation not found for type import :
        //type: System.Web.HttpRequest
        //method: System.Web.HttpFileCollection get_Files()
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!

        __HttpFileCollection __Files;
        public HttpFileCollection Files
        {
            get
            {
                // X:\jsc.smokescreen.svn\core\javascript\com.abstractatech.analytics\com.abstractatech.analytics\ApplicationWebService.cs
                // http://stackoverflow.com/questions/4780474/appengine-howto-see-content-from-a-post-request

                if (__Files == null)
                {
                    __Files = new __HttpFileCollection();

                    var __Request = this;
                    var __RequestContentType = ContentType;
                    Console.WriteLine("get Files " + new { __RequestContentType });
                    // get Files { ContentType = multipart/form-data; boundary=----WebKitFormBoundaryBSQTOpGkti3EcLB2 }

                    //W:\com.abstractatech.analytics.ApplicationWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Web\__HttpRequest.java:232: error: unreported exception IOException; must be caught or declared to be thrown
                    //        stream3 = InputStreamExtensions.ToNetworkStream(this.InternalContext.getInputStream());
                    //                                                                                           ^


                    var InternalStream = default(NetworkStream);


                    try
                    {
                        InternalStream = this.InternalContext.getInputStream().ToNetworkStream();
                    }
                    catch
                    {
                        // ?
                        throw;
                    }


                    var r = new SmartStreamReader(InternalStream);


                    var cid = 0;

                    // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalAndroidWebServiceActivity.cs
                    // X:\jsc.svn\core\ScriptCoreLibJava.Web\ScriptCoreLibJava.Web\BCLImplementation\System\Web\HttpRequest.cs
                    var boundary = __RequestContentType.SkipUntilOrEmpty("multipart/form-data; boundary=");

                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20130401/20130405-file-upload
                    // uploading files?
                    if (!string.IsNullOrEmpty(boundary))
                    {
                        // I/System.Console( 4561): #6 { boundary = ----WebKitFormBoundaryfk2fAcRDuAlGZWnA }
                        //Console.WriteLine("#" + cid + " " + new { boundary });

                        var header0 = r.ReadLine();

                        Console.WriteLine("#" + cid + " " + new { header0 });
                        // I/System.Console( 4561): #6 { header0 = ------WebKitFormBoundaryfk2fAcRDuAlGZWnA }

                        while (header0 == "--" + boundary)
                        {
                            // opening multipart..
                            header0 = "";

                            var header1 = r.ReadLine();

                            //Console.WriteLine("#" + cid + " " + new { header1 });
                            // I/System.Console( 4871): #2 { header1 = Content-Disposition: form-data; name="foo"; filename="a.txt" }

                            var ContentDisposition = header1.SkipUntilOrEmpty("Content-Disposition:");

                            Console.WriteLine("#" + cid + " " + new { ContentDisposition });

                            var header2 = r.ReadLine();
                            //Console.WriteLine("#" + cid + " " + new { header2 });

                            //I/System.Console( 5055): #2 { ContentDisposition =  form-data; name="foo"; filename="a.txt" }
                            //I/System.Console( 5055): #2 { header2 = Content-Type: text/plain }

                            var xContentType = header2.SkipUntilOrEmpty("Content-Type:");
                            Console.WriteLine("#" + cid + " " + new { ContentType });


                            var header3 = r.ReadLine();
                            //Console.WriteLine("#" + cid + " " + new { header3 });

                            if (header3 == "")
                            {
                                // read data
                                // http://www.w3.org/TR/html401/interact/forms.html#h-17.13.4.2
                                // X:\jsc.svn\examples\rewrite\TestReadToBoundary\TestReadToBoundary\Program.cs

                                var data = r.ReadToBoundary("\r\n--" + boundary);


                                //I/System.Console( 8169): #10 { header0 = ------WebKitFormBoundarynixQIBRKZR2TM3Om }
                                //I/System.Console( 8169): #10 { ContentDisposition =  form-data; name="foo0"; filename="textTest-app.xml" }
                                //I/System.Console( 8169): #10 { ContentType =  text/xml }
                                //I/System.Console( 8169): #10 { Length = 1297 }
                                //I/System.Console( 8169): #10 { header4 = ------WebKitFormBoundarynixQIBRKZR2TM3Om }
                                //I/System.Console( 8169): #10 { ContentDisposition =  form-data; name="foo1"; filename="test.svg" }
                                //I/System.Console( 8169): #10 { ContentType =  image/svg+xml }


                                //var __Files = (__HttpFileCollection)(object)__Request.Files;

                                var FileName = ContentDisposition.SkipUntilOrEmpty("filename=\"").TakeUntilOrEmpty("\"");
                                var name = ContentDisposition.SkipUntilOrEmpty("name=\"").TakeUntilOrEmpty("\"");

                                Console.WriteLine("#" + cid + " " + new { data.Length, FileName });

                                var n = new __HttpPostedFile
                                {
                                    ContentType = xContentType,
                                    ContentLength = (int)data.Length,
                                    InputStream = data,
                                    FileName = FileName
                                };


                                __Files.InternalDictionary[name] = n;


                                var header4 = r.ReadLine();
                                header0 = header4;
                                // which is it, the end or is there more?
                                Console.WriteLine("#" + cid + " " + new { header4 });

                                //I/System.Console( 6761): #5 { Length = 256 }
                                //I/System.Console( 6761): #5 { header4 = ------WebKitFormBoundaryrn6MyQlGvhETZkjw-- }
                                // the end?
                            }
                        }
                        // now what?
                    }


                    //this.InternalContext.getInputStream()
                    //this.InternalContext.getPathInfo

                }

                return (HttpFileCollection)(object)__Files;
            }
        }
    }

    /// <summary>
    /// A stream reader that can switch between text and binary mode.
    /// 
    /// Note this may be the first documented type for jsc developer program.
    /// 
    /// Reference: "Y:\jsc.community\zmovies\MovieAgent\MovieAgentCore\Server\Library\SmartStreamReader.cs"
    /// </summary>
    [Script]
    public class SmartStreamReader : Stream
    {

        // copy of
        // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Shared\IO\SmartStreamReader.cs

        // review: Y:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Web\SmartStreamReader.cs

        public readonly Stream BaseStream;




        public SmartStreamReader(Stream BaseStream)
        {
            this.BaseStream = BaseStream;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            throw new NotImplementedException("");
        }

        public override long Length
        {
            get { throw new NotImplementedException(""); }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException("");
            }
            set
            {
                throw new NotImplementedException("");
            }
        }



        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException("");
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException("");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException("");
        }

        //const int InternalBufferCapacity = 0x2000;
        public static int InternalBufferCapacity = 0x2000;

        byte[] InternalBuffer = new byte[InternalBufferCapacity];
        int InternalBufferCount = 0;

        public override int Read(byte[] buffer, int offset, int count)
        {
            // buffer + stream
            var value = 0;

            var InternalBufferCountToBeRead = InternalBufferCount;

            if (InternalBufferCountToBeRead > count)
                InternalBufferCountToBeRead = count;

            for (int i = 0; i < InternalBufferCountToBeRead; i++)
            {
                buffer[offset + i] = InternalBuffer[i];
            }

            value += InternalBufferCountToBeRead;
            offset += InternalBufferCountToBeRead;
            count -= InternalBufferCountToBeRead;

            DiscardBuffer(InternalBufferCountToBeRead);


            // The total number of bytes read into the buffer. 
            // This can be less than the number of bytes requested 
            // if that many bytes are not currently available, 
            // or zero (0) if the end of the stream has been reached. 

            while (count > 0)
            {
                var i = this.BaseStream.Read(buffer, offset, count);

                if (i > 0)
                {
                    value += i;
                    offset += i;
                    count -= i;


                }
                else
                {
                    // no more data, we must return
                    count = 0;
                }

            }

            return value;
        }

        public MemoryStream ReadToMemoryStream()
        {
            var target = new MemoryStream();

            var ns = this.BaseStream as NetworkStream;

            var flag = true;
            while (flag)
            {
                target.Write(this.InternalBuffer, 0, this.InternalBufferCount);

                if (ns != null)
                {
                    this.InternalBufferCount = -1;

                    if (!ns.DataAvailable)
                    {
                        Thread.Sleep(300);
                        // are we sure?
                    }

                    if (ns.DataAvailable)
                    {
                        this.InternalBufferCount = this.BaseStream.Read(this.InternalBuffer, 0, InternalBufferCapacity);
                    }
                }
                else
                    this.InternalBufferCount = this.BaseStream.Read(this.InternalBuffer, 0, InternalBufferCapacity);

                flag = (this.InternalBufferCount > 0);
            }
            return target;
        }

        public string ReadToEnd()
        {
            var a = new StringBuilder();

            var flag = true;
            while (flag)
            {
                for (int i = 0; i < this.InternalBufferCount; i++)
                {
                    a.Append((char)this.InternalBuffer[i]);
                }

                this.InternalBufferCount = this.BaseStream.Read(this.InternalBuffer, 0, InternalBufferCapacity);

                flag = (this.InternalBufferCount > 0);
            }

            return a.ToString();
        }

        void DiscardBuffer(int bytes)
        {
            if (bytes < 1)
                return;

            for (int i = bytes; i < this.InternalBufferCount; i++)
            {
                this.InternalBuffer[i - bytes] = this.InternalBuffer[i];
            }

            this.InternalBufferCount -= bytes;
        }

        public string ReadLine()
        {
            var a = new StringBuilder();

            var LineFeedExcpected = false;
            var flag = true;
            while (flag)
            {
                for (int i = 0; i < this.InternalBufferCount; i++)
                {
                    // jsc cannot handle byte to char for java?
                    var b = (int)this.InternalBuffer[i];
                    var c = (char)b;

                    if (c == '\n')
                    {
                        DiscardBuffer(i + 1);
                        return a.ToString();
                    }
                    else if (LineFeedExcpected)
                    {
                        DiscardBuffer(i);

                        return a.ToString();
                    }

                    if (c == '\r')
                    {
                        LineFeedExcpected = true;
                        continue;
                    }

                    a.Append(c);
                }

                //this.InternalBufferCount = this.BaseStream.Read(this.InternalBuffer, 0, InternalBufferCapacity);
                var len = InternalBufferCapacity - this.InternalBufferCount;

                this.InternalBufferCount += this.BaseStream.Read(
                    this.InternalBuffer, this.InternalBufferCount, len

                    );

                flag = (this.InternalBufferCount > 0);
            }
            return a.ToString();

        }

        public void ReadBlockTo(int length, StringBuilder w)
        {
            var bytes = new byte[length];
            this.Read(bytes, 0, length);

            for (int i = 0; i < length; i++)
            {
                w.Append((char)bytes[i]);
            }
        }



        public MemoryStream ReadToBoundary(string e)
        {
            var BoundaryBytes = Encoding.UTF8.GetBytes(e);


            return ReadToBoundary(BoundaryBytes);
        }

        public MemoryStream ReadToBoundary(byte[] BoundaryBytes)
        {
            var m = new MemoryStream();

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20130401/20130405-file-upload


            if (BoundaryBytes.Length >= this.InternalBuffer.Length)
                throw new Exception("Buffer too small");

            var flag = true;
            while (flag)
            {


                if (this.InternalBufferCount > BoundaryBytes.Length)
                {
                    // we now have enough data to look at

                    int i = 0;

                    // how much of the buffer can we accept?
                    for (; i < this.InternalBufferCount - BoundaryBytes.Length + 1; i++)
                    {
                        // is this the start of the boundary?

                        if (InternalCompareBytes(this.InternalBuffer, i, BoundaryBytes))
                        {
                            // we found waldo!
                            flag = false;
                            break;
                        }

                    }

                    m.Write(this.InternalBuffer, 0, i);
                    DiscardBuffer(i);
                }


                if (flag)
                {
                    var len = InternalBufferCapacity - this.InternalBufferCount;

                    this.InternalBufferCount += this.BaseStream.Read(
                        this.InternalBuffer, this.InternalBufferCount, len

                        );

                    flag = (this.InternalBufferCount > 0);
                }
            }

            return m;
        }

        public static bool InternalCompareBytes(byte[] a, int aoffset, byte[] b)
        {
            var r = true;

            for (int i = 0; i < b.Length; i++)
            {
                if (a[aoffset + i] != b[i])
                {
                    r = false;
                }

            }

            return r;
        }
    }
}
