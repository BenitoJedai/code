using android.app;
using android.os;
using android.view;
using android.webkit;
using android.widget;
using java.net;
using java.util;
using java.io;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Shared.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ScriptCoreLib.Extensions;
using System.Web;
using ScriptCoreLib.Android.BCLImplementation.System.Web;
using ScriptCoreLib.Delegates;
using android.util;
using android.content;
using ScriptCoreLib.Ultra.WebService;

namespace ApplicationWebService.Activities
{
    public class ApplicationActivity : Activity
    {
        const int FILECHOOSER_RESULTCODE = 1;

        // http://m0s-programming.blogspot.com/2011/02/file-upload-in-through-webview-on.html
        //protected void onActivityResult(int requestCode, int resultCode,
        //                           Intent intent)
        //{
        //    if (requestCode == FILECHOOSER_RESULTCODE)
        //    {
        //        if (null == mUploadMessage) return;
        //        Uri result = intent == null || resultCode != RESULT_OK ? null
        //                : intent.getData();
        //        mUploadMessage.onReceiveValue(result);
        //        mUploadMessage = null;

        //    }
        //}  

        protected override void onCreate(Bundle savedInstanceState)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20120-1/201208
            // this is a prototype implementation for ApplicationWebService on android
            // we need to serve assets at random port
            // we need a webbrowser
            // web browser works.
            // while loading we could display jsc logo, made in opengl
            // we need ip and port.
            // we need to show what assets we have
            // let's start using HttpRequest

            base.onCreate(savedInstanceState);

            #region assets
            var assets = this.getResources().getAssets();

            var aa = new List<InternalFileInfo>();
            //var a = new List<InternalFileInfo>();

            Action<string> GetAssets = null;

            GetAssets =
                (ParentDirectory) =>
                {
                    var collection = new string[0];

                    Log.wtf("ApplicationWebService", "GetAssets " + ParentDirectory);

                    try
                    {
                        collection = assets.list(ParentDirectory);
                    }
                    catch
                    {
                    }


                    foreach (var RelativeName in collection)
                    {
                        var FileName =
                            string.IsNullOrEmpty(ParentDirectory) ?
                            RelativeName : ParentDirectory + "/" + RelativeName;

                        var IsFile = false;
                        try
                        {
                            var rr = assets.open(FileName);

                            IsFile = true;
                            rr.close();
                        }
                        catch
                        {
                        }

                        if (IsFile)
                        {
                            aa.Add(
                                new InternalFileInfo
                                {
                                    Name = FileName,

                                }
                            );
                        }

                        if (FileName == "webkit")
                        {
                        }
                        else if (FileName == "sounds")
                        {
                        }
                        else if (FileName == "images")
                        {
                        }
                        else GetAssets(FileName);
                    }
                };

            GetAssets("");

            #endregion


            #region CreateServer at port
            var r = new System.Random();
            var port = r.Next(1024, 32000);
            var ip = getLocalIpAddress();
            var ipa = Dns.GetHostAddresses(getLocalIpAddress())[0];

            Class1Shared.CreateServer(
                this,
                ipa,
                port,
                x =>
                {

                }, aa.ToArray()
            ).Start();
            #endregion


            #region uri
            var uri = "http://" + ip;

            uri += ":";
            uri += ((object)(port)).ToString();

            //var height = this.getWindowManager().getDefaultDisplay().getHeight();
            //var width = this.getWindowManager().getDefaultDisplay().getWidth();


            //if (width > height)
            //{
            //    this.ToFullscreen();
            //}
            //else
            uri += "/jsc?flag=foo#bar";

            #endregion


            #region WebView as UI at uri
            var webview = new WebView(this);

            webview.getSettings().setJavaScriptEnabled(true);

            webview.setWebViewClient(new MyWebViewClient { });
            webview.setWebChromeClient(
                new MyWebChromeClient
                {
                    //AtopenFileChooser =
                    //    (ValueCallback<Uri> uploadMsg) =>
                    //    {
                    //        //mUploadMessage = uploadMsg;  
                    //        //Intent i = new Intent(Intent.ACTION_GET_CONTENT);
                    //        //i.addCategory(Intent.CATEGORY_OPENABLE);
                    //        //i.setType("image/*");
                    //        //this.startActivityForResult(
                    //        //    Intent.createChooser(i, "File Chooser"), FILECHOOSER_RESULTCODE
                    //        //);
                    //    }
                }
            );

            this.setContentView(webview);

            this.AtKeyDown =
                (keyCode, e) =>
                {

                    if (keyCode == KeyEvent.KEYCODE_BACK)
                    {
                        if (webview.canGoBack())
                        {
                            webview.goBack();
                            return true;
                        }
                    }

                    return false;
                };
            this.setTitle(uri);
            webview.loadUrl(uri);
            #endregion

        }

        //public class InternalFileInfo
        //{
        //    public string FileName;

        //    // jsc does not yet detect field generics?
        //    //public Func<Stream> OpenFile;
        //    public StreamFunc OpenFile;
        //}




        #region Class1Shared
        public delegate void NetworkStreamAction(NetworkStream s);

        // nested delegates cannot stay partial..
        //public delegate NetworkStream StreamFunc();



        public class Class1Shared
        {
            public class Int32Box
            {
                public int value;
            }

            public static Thread CreateServer(
                ContextWrapper InternalContext,
                IPAddress ipa, int port,
                Action<string> Console_WriteLine,
                InternalFileInfo[] Files)
            {
                var random = new System.Random();

                // Error 312 (net::ERR_UNSAFE_PORT): Unknown error.
                if (port < 1024)
                    port = random.Next(1024, 32000);


                var cid = 0;

                NetworkStreamAction AtConnection =
                    InternalStream =>
                    {
                        var id = cid++;

                        Console_WriteLine("#" + cid);

                        //log("AtConnection");

                        var r = new SmartStreamReader(InternalStream);

                        #region __Global
                        var __Request = new __HttpRequest();
                        var __Response = new __HttpResponse { InternalStream = InternalStream, InternalContext = InternalContext };
                        var __Global = new __Global();
                        __Global.Files = Files;
                        __Global.WebMethods =
                            new InternalWebMethodInfo[]
                            {
                                new InternalWebMethodInfo
                                {
                                    Name = "WebMethod2",
                                    TypeFullName = "TestGAE.ApplicationWebService",
                                    MetadataToken =  "06000001",

                                    Parameters = new InternalWebMethodParameterInfo[]
                                    {
                                        new InternalWebMethodParameterInfo 
                                        {
                                            Name = "e",
                                            Value = "",
                                            IsDelegate = false
                                        },
                                        
                                        new InternalWebMethodParameterInfo 
                                        {
                                            Name = "y",
                                            Value = "",
                                            IsDelegate = true
                                        }
                                    }
                                }
                            };

                        __Global.ScriptApplications =
                            new[]
                            {
                                new WebServiceScriptApplication
                                {
                                    TypeName = "Application",
                                    TypeFullName = "TestGAE.Application",

                                    PageSource = "<body>\r\n  <noscript>Error: This Application requires JavaScript.</noscript>\r\n  <link rel=\"stylesheet\" href=\"assets/TestGAE/Default.css\" />\r\n  <div id=\"PageContainer\">\r\n    <h1 id=\"Header\">JSC - The .NET crosscompiler for web platforms.</h1>\r\n    <p id=\"Content\" style=\"padding: 2em;     color: blue;\">Hello world</p>\r\n  </div>\r\n</body>",

                                    References = new []
                                    {
                                        new WebServiceScriptApplication.Reference
                                        {
                                            AssemblyFile = "ScriptCoreLib.dll"
                                        },
                                        new WebServiceScriptApplication.Reference
                                        {
                                            AssemblyFile = "TestGAE.Application.exe"
                                        }
                                    }
                                }
                            };

                        ((__HttpApplication)(object)__Global).Request = (HttpRequest)(object)__Request;
                        ((__HttpApplication)(object)__Global).Response = (HttpResponse)(object)__Response;
                        var Context = __Global.Context;
                        #endregion




                        #region __Request { HttpMethod, QueryString, Headers }
                        var HTTP_METHOD_PATH_QUERY = r.ReadLine();
                        var HTTP_METHOD = HTTP_METHOD_PATH_QUERY.TakeUntilOrEmpty(" ");
                        __Request.HttpMethod = HTTP_METHOD;

                        Console.WriteLine("#" + cid + " " + HTTP_METHOD_PATH_QUERY);

                        #region check METHOD
                        if (HTTP_METHOD != "POST")
                            if (HTTP_METHOD != "GET")
                            {
                                var m = new MemoryStream();

                                Action<string> WriteLineASCII = (string e) =>
                                {
                                    var x = Encoding.ASCII.GetBytes(e + "\r\n");

                                    m.Write(x, 0, x.Length);
                                };

                                Console_WriteLine("#" + cid + " 500");

                                WriteLineASCII("HTTP/1.1 500 OK");
                                WriteLineASCII("Connection: close");

                                var oa = m.ToArray();

                                InternalStream.Write(oa, 0, oa.Length);

                                InternalStream.Flush();
                                InternalStream.Close();
                                return;
                            }
                        #endregion


                        var HTTP_PATH_QUERY = HTTP_METHOD_PATH_QUERY.SkipUntilOrEmpty(" ").TakeUntilLastOrEmpty(" ");
                        var HTTP_PATH = HTTP_PATH_QUERY.TakeUntilIfAny("?");
                        __Request.Path = HTTP_PATH;

                        #region QueryString
                        var HTTP_QUERY = HTTP_PATH_QUERY.SkipUntilOrEmpty("?");

                        var __QueryStringItems = HTTP_QUERY.Split('&');

                        foreach (var item in __QueryStringItems)
                        {
                            var Key = item.TakeUntilOrEmpty("=");
                            if (!string.IsNullOrEmpty(Key))
                            {
                                var Value = item.SkipUntilIfAny("=");

                                __Request.QueryString[Key] = Value;
                            }
                        }
                        #endregion

                        __Request.Headers["Content-Type"] = "";

                        #region Headers
                        var NextHeader = r.ReadLine();
                        while (!string.IsNullOrEmpty(NextHeader))
                        {
                            // http://www.nextthing.org/archives/2005/08/07/fun-with-http-headers

                            var HeaderKey = NextHeader.TakeUntilOrEmpty(":");
                            var HeaderValue = NextHeader.SkipUntilIfAny(":").Trim();

                            __Request.Headers[HeaderKey] = HeaderValue;

                            NextHeader = r.ReadLine();
                        }
                        #endregion

                        #endregion

                        var data = r.ReadToMemoryStream();

                        var boundary = __Request.Headers["Content-Type"].SkipUntilOrEmpty("multipart/form-data; boundary=");

                        #region Form
                        if (__Request.Headers["Content-Type"] == "application/x-www-form-urlencoded")
                        {
                            var p = Encoding.UTF8.GetString(data.ToBytes());
                            var q = p.Split('&');

                            foreach (var item in q)
                            {
                                var Key = item.TakeUntilOrEmpty("=");
                                var Value = item.SkipUntilIfAny("=");

                                __Request.Form[Key] = Value;
                            }
                        }
                        #endregion

                        #region selected_item
                        var selected_item = default(InternalFileInfo);

                        foreach (var item in Files)
                        {
                            // LINQ please!
                            if (HTTP_PATH == "/" + item.Name)
                                selected_item = item;

                        }
                        #endregion



                        {



                            if (__Request.Path == "/jsc")
                            {
                                __InternalGlobalExtensions.InternalApplication_BeginRequest(__Global);
                            }
                            else
                            {
                                InternalGlobalExtensions.InternalApplication_BeginRequest(__Global);
                            }


                            Console.WriteLine("#" + cid + " " + HTTP_METHOD_PATH_QUERY + " done");

                            return;
                            //}

                            #region selected_item
                            if (selected_item != null)
                            {
                                var path = selected_item.Name;

                                __Response.StatusCode = 200;
                                __Response.Headers["X-Handler"] = "http://jsc-solutions.net";

                                if (path.EndsWith(".gif"))
                                    __Response.ContentType = ("image/gif");
                                else if (path.EndsWith(".png"))
                                    __Response.ContentType = ("image/png");
                                else if (path.EndsWith(".htm"))
                                    __Response.ContentType = ("text/html; charset=utf-8");
                                else
                                    __Response.ContentType = ("application/octet-stream");


                                __Response.WriteFile(path);

                                InternalStream.Close();
                                __Global.CompleteRequest();
                                return;

                            }
                            #endregion


                            __Response.StatusCode = 200;
                            __Response.ContentType = ("text/html; charset=utf-8");
                            __Response.Headers["X-Handler"] = "http://jsc-solutions.net";


                            #region index

                            #region WriteLineASCII
                            Action<string> WriteLine = (string e) =>
                            {
                                __Response.Write(e + "\r\n");
                            };
                            #endregion



                            #region html index

                            WriteLine("<html>");
                            WriteLine("<body>");

                            foreach (var HeaderKey in Context.Request.Headers.AllKeys)
                            {
                                var HeaderValue = Context.Request.Headers[HeaderKey];

                                WriteLine("<code style='color: gray;'>" + HeaderKey + "</code>:");
                                WriteLine("<code style='color: green;'>" + HeaderValue + "</code><br />");
                            }

                            WriteLine("<h3>data: 0x" + data.Length.ToString("x8") + "</h3>");


                            WriteLine("<pre>" + boundary + "</pre>");
                            WriteLine("<hr />");

                            WriteLine("<pre>" + HTTP_METHOD_PATH_QUERY + "</pre>");

                            if (string.IsNullOrEmpty(boundary))
                                WriteLine("<pre>" + WriteHexDump(data.ToBytes()) + "</pre>");




                            #region by boundary
                            if (!string.IsNullOrEmpty(boundary))
                            {
                                boundary = "--" + boundary;

                                var bytes = data.ToBytes();
                                var boundarybytes = Encoding.ASCII.GetBytes(boundary);

                                var boundaries = new List<Int32Box>();

                                for (int i = 0; i < bytes.Length - boundarybytes.Length; i++)
                                {
                                    var AtBoundary = false;

                                    // is i at boundary?
                                    for (int j = 0; j < boundarybytes.Length; j++)
                                    {
                                        if (bytes[i + j] != boundarybytes[j])
                                        {
                                            AtBoundary = false;
                                            break;
                                        }
                                        AtBoundary = true;
                                    }

                                    if (AtBoundary)
                                    {
                                        boundaries.Add(new Int32Box { value = i });
                                    }
                                }

                                var boundaries_a = boundaries.ToArray();

                                for (int i = 0; i < boundaries_a.Length - 1; i++)
                                {
                                    var start = boundaries_a[i].value + boundarybytes.Length + 2;
                                    var end = boundaries_a[i + 1].value;

                                    var chunk = new byte[end - start];

                                    Array.Copy(bytes, start, chunk, 0, chunk.Length);
                                    WriteLine("<hr />");
                                    WriteLine("<pre>" + WriteHexDump(chunk) + "</pre>");
                                }

                            }
                            #endregion

                            WriteLine("<hr />");

                            WriteLine("<form target='_blank' action='/jsc?WebMethod=06000048' method='POST'><br /> <img src='http://i.msdn.microsoft.com/deshae98.pubmethod(en-us,VS.90).gif' /> method: <code><a href='?WebMethod=06000048'>Hello</a></code><input type='submit' value='Invoke'  /><br />");
                            WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' /> parameter: <code>data</code> = <input type='text'  name='_06000048_data' value='' /><br />");
                            WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' /> parameter: <code>foo</code> = <input type='text'  name='_06000048_foo' value='' /><br />");
                            WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubdelegate(en-us,VS.90).gif' /> parameter: <code>result</code></form>");

                            WriteLine("<hr />");
                            WriteLine("<form target='_blank' action='/jsc?WebMethod=06000048' method='POST' enctype='multipart/form-data'>");
                            WriteLine("  <br /> <img src='http://i.msdn.microsoft.com/deshae98.pubmethod(en-us,VS.90).gif' /> method: <code><a href='?WebMethod=06000048'>Hello</a></code><input type='submit' value='Invoke'  /><br />");
                            WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' /> parameter: <code>data</code> = <input type='text'  name='_06000048_data' value='' /><br />");
                            WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' /> parameter: <code>foo</code> = <input type='text'  name='_06000048_foo' value='' /><br />");
                            WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubdelegate(en-us,VS.90).gif' /> parameter: <code>result</code></form>");


                            WriteLine("<hr />");
                            WriteLine("<form target='_blank' action='?WebMethod=06000048' method='POST' enctype='multipart/form-data'>");
                            WriteLine("  <input type='file' name='pic' size='40' accept='image/*' />");
                            WriteLine("  <input type='file' name='foo' />");
                            WriteLine("  <br /> <img src='http://i.msdn.microsoft.com/deshae98.pubmethod(en-us,VS.90).gif' /> method: <code><a href='?WebMethod=06000048'>Hello</a></code><input type='submit' value='Invoke'  /><br />");
                            WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' /> parameter: <code>data</code> = <input type='text'  name='_06000048_data' value='' /><br />");
                            WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' /> parameter: <code>foo</code> = <input type='text'  name='_06000048_foo' value='' /><br />");
                            WriteLine("&nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubdelegate(en-us,VS.90).gif' /> parameter: <code>result</code></form>");

                            WriteLine("</body>");

                            WriteLine("</html>");
                            #endregion

                            InternalStream.Flush();
                            InternalStream.Close();
                            #endregion




                        }
                    };


                var t = new Thread(
                        delegate()
                        {
                            Console_WriteLine(ipa + ":" + port);

                            var r = new TcpListener(ipa, port);

                            //try
                            //{
                            r.Start();

                            while (true)
                            {
                                //log("AcceptTcpClient");
                                var c = r.AcceptTcpClient();
                                //log("AcceptTcpClient done, GetStream");

                                var s = c.GetStream();
                                //log("AcceptTcpClient done, GetStream done");


                                //AtConnection(s);

                                new Thread(
                                    delegate()
                                    {
                                        //log("before AtConnection");
                                        AtConnection(s);
                                    }
                                )
                                {
                                    IsBackground = true,
                                }.Start();
                            }

                            //}
                            //catch
                            //{
                            //    log("AcceptTcpClient error!");

                            //    throw;
                            //}


                        }
                    )
                {
                    IsBackground = true,
                };
                return t;
            }

            public static string WriteHexDump(byte[] read_b, int bytes_shown = 16)
            {
                if (read_b == null)
                    return "";

                return WriteHexDump(read_b, bytes_shown, 0, read_b.Length, true, "");
            }

            public static string WriteHexDump(byte[] read_b, int bytes_shown, int offset, int length, bool show_offset = true, string prefix = "")
            {
                //Console.WriteLine(" hex dump offset " + offset + " length " + length);
                var w = new StringBuilder();


                try
                {

                    int p = 0;

                    int pi = offset;
                    string Text = "";

                    int formatx = 0;

                    while (formatx < length)
                    {

                        bool b_will_offset = (formatx) % bytes_shown == 0;


                        if (b_will_offset)
                        {

                            if (formatx == 0)
                                w.Append(prefix);
                            else
                                w.Append("".PadLeft(prefix.Length));

                            if (show_offset)
                            {
                                //System.Console.Write("0x" + Convert.ToHexString(pi, 4) + " : ");
                                w.Append("0x" + pi.ToString("x8") + " : ");
                            }
                        }

                        p = Convert.ToInt32(read_b[pi]);

                        bool isAlpha = IsVisibleChar(p);

                        if (isAlpha)
                        {
                            Text += new string((char)p, 1);
                        }
                        else
                            Text += ".";


                        w.Append((p & 0xFF).ToString("x2") + " ");

                        pi++;
                        formatx++;

                        if (formatx % bytes_shown == 0)
                        {


                            w.AppendLine(" | " + Text);

                            Text = "";
                        }
                    }

                    if (Text != "")
                    {
                        while (formatx++ % bytes_shown != 0)
                            w.Append("   ");

                        w.AppendLine(" | " + Text);
                    }
                }
                catch
                {
                }

                return w.ToString();
            }



            public static bool IsVisibleChar(int p)
            {
                bool bA = p >= 'a';
                bool aZ = p <= 'z';
                bool lA = (bA && aZ);

                bool buA = p >= 'A';
                bool auZ = p <= 'Z';
                bool uA = (buA && auZ);


                bool b0 = p >= '0';
                bool a9 = p <= '9';
                bool uN = (b0 && a9);

                bool x = "_\'\"=[]()+-;:.?@/".IndexOf((char)p) > -1;

                bool isAlpha = lA || uA || uN || x;
                return isAlpha;

            }
        }
        #endregion

        #region goBack
        Func<int, KeyEvent, bool> AtKeyDown;

        public override bool onKeyDown(int keyCode, KeyEvent e)
        {
            // http://android-coding.blogspot.com/2011/08/handle-back-button-in-webview-to-back.html

            if (AtKeyDown != null)
                if (AtKeyDown(keyCode, e))
                    return true;

            return base.onKeyDown(keyCode, e);
        }
        #endregion

        #region MyWebChromeClient
        class MyWebChromeClient : WebChromeClient
        {
            //public Action<ValueCallback<Uri>> AtopenFileChooser;

            //public override void openFileChooser(ValueCallback<Uri> uploadMsg)
            //{
            //    AtopenFileChooser(uploadMsg);
            //}
        }

        #endregion

        #region MyWebViewClient
        class MyWebViewClient : WebViewClient
        {
            public override bool shouldOverrideUrlLoading(WebView view, string url)
            {
                view.loadUrl(url);
                return true;
            }


        }
        #endregion

        #region getLocalIpAddress
        public static string getLocalIpAddress()
        {
            var value = "";

            try
            {
                for (Enumeration en = NetworkInterface.getNetworkInterfaces(); en.hasMoreElements(); )
                {
                    NetworkInterface intf = (NetworkInterface)en.nextElement();
                    for (Enumeration enumIpAddr = intf.getInetAddresses(); enumIpAddr.hasMoreElements(); )
                    {
                        InetAddress inetAddress = (InetAddress)enumIpAddr.nextElement();

                        //Log.wtf("getLocalIpAddress", inetAddress.getHostAddress().ToString());

                        var v6 = inetAddress is Inet6Address;

                        if (v6)
                        {
                        }
                        else if (!inetAddress.isLoopbackAddress())
                        {
                            if (value == "")
                                value = inetAddress.getHostAddress().ToString();
                        }
                    }
                }
            }
            catch
            {
            }

            if (value == "")
            {
                // no wifi
                value = "127.0.0.1";
            }

            return value;
        }
        #endregion
    }

    class __Global : InternalGlobal
    {
        public InternalFileInfo[] Files;

        public override InternalFileInfo[] GetFiles()
        {
            return Files;
        }

        public InternalWebMethodInfo[] WebMethods;

        public override InternalWebMethodInfo[] GetWebMethods()
        {
            return WebMethods;
        }

        public WebServiceScriptApplication[] ScriptApplications;

        public override WebServiceScriptApplication[] GetScriptApplications()
        {
            return ScriptApplications;
        }

        public override void Invoke(InternalWebMethodInfo e)
        {
        }

        public override void Serve(WebServiceHandler h)
        {
        }
    }







    public static class __InternalGlobalExtensions
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
                //g.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
                //g.Response.Cache.SetExpires(DateTime.Now.AddMinutes(15));


                g.Response.AddHeader("x-handler", "http://jsc-solutions.net");

                // to root
                Context.Response.WriteFile("/" + CurrentFile.Name);

                that.CompleteRequest();

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

            var WebMethods = g.GetWebMethods();

            var IsComplete = false;

            that.Response.ContentType = "text/html";
            WriteDiagnostics(g, Write, WebMethods);


            IsComplete = true;
            that.CompleteRequest();
        }

        public static string ToXMLString(this string xml)
        {
            return xml
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;")
            ;
        }

        public static string FromXMLString(this string xml)
        {
            return xml
                .Replace("&apos;", "'")
                .Replace("&quot;", "\"")
                .Replace("&gt;", ">")
                .Replace("&lt;", "<")
                .Replace("&amp;", "&")
            ;
        }

        public static string escapeXML(string s)
        {
            return s.ToXMLString();
        }

        public static void WriteDiagnostics(InternalGlobal g, StringAction Write, InternalWebMethodInfo[] WebMethods)
        {
            // should the diagnostics be a separate rich Browser Application? :)

            var Context = g.InternalApplication.Context;

            Write("<title>jsc-solutions.net</title>");

            Write("<center>");
            Write("<div style='background-color: black; color: white; padding: 2em;'>");
            Write("&laquo; Rotate your device to left to <b>launch</b>");
            Write("</div>");
            Write("</center>");

            Write("<br/><center><a href='/'>Launch Application</a></center><br/>");

            //Write("<h1>" + Context.Request.Headers["Host"] + "</h1>");

            foreach (var HeaderKey in Context.Request.Headers.AllKeys)
            {
                var HeaderValue = Context.Request.Headers[HeaderKey];

                Write("<code style='color: gray;'>" + HeaderKey + "</code>:");
                Write("<code style='color: green;'>" + HeaderValue + "</code><br />");
            }


            Write("<a href='http://jsc-solutions.net'><img border='0' src='/assets/ScriptCoreLib/jsc.png' /></a>");

            #region Special pages
            Write("<h2>Special pages</h2>");


            Write("<br /> " + "special page: " + "<a href='/robots.txt'>/robots.txt</a>");
            Write("<br /> " + "special page: " + "<a href='/xml'>/xml</a>");
            Write("<br /> " + "special page: " + "<a href='/crossdomain.xml'>/crossdomain.xml</a>");
            Write("<br /> " + "special page: " + "<a href='/favicon.ico'>/favicon.ico</a>");
            Write("<br /> " + "special page: " + "<a href='/jsc'>/jsc</a>");
            #endregion

            Write("<h2>WebMethods (" + WebMethods.Length + ")</h2>");



            foreach (var item in WebMethods)
            {
                InternalGlobalExtensions.WriteWebMethodForm(Write, item);
            }


            //Write("<br /> Path: '" + Context.Request.Path + "'");
            //Write("<br /> HttpMethod: '" + Context.Request.HttpMethod + "'");

            Write("<h2>Form</h2>");
            foreach (var item in Context.Request.Form.AllKeys)
            {
                Write("<br /> " + "<img src='http://i.msdn.microsoft.com/w144atby.pubproperty(en-us,VS.90).gif' /> <code>");
                Write(item);
                Write(" = ");
                Write(escapeXML(Context.Request.Form[item]));
                Write("</code>");
            }

            #region QueryString
            Write("<h2>QueryString</h2>");
            foreach (var item in Context.Request.QueryString.AllKeys)
            {
                Write("<br /> " + "<img src='http://i.msdn.microsoft.com/w144atby.pubproperty(en-us,VS.90).gif' /> <code>");
                Write(item);
                Write(" = ");
                Write(escapeXML(Context.Request.QueryString[item]));
                Write("</code>");
            }
            #endregion


            Write("<h2>Script Applications</h2>");

            foreach (var item in g.GetScriptApplications())
            {
                Write("<br /> " + "script application: " + item.TypeName);

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

    }
}
