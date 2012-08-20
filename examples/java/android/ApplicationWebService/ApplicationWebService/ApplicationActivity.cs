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

namespace ApplicationWebService.Activities
{
    public class ApplicationActivity : Activity
    {
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

            base.onCreate(savedInstanceState);

            #region assets
            var path = "";

            var assets = this.getResources().getAssets();

            var collection = new string[0];

            try
            {
                collection = assets.list(path);
            }
            catch
            {
            }

            var a = new List<InternalFileInfo>();

            foreach (var item in collection)
            {
                a.Add(
                    new InternalFileInfo
                    {
                        FileName = item,
                        OpenFile =
                        () =>
                        {
                            var rr = default(NetworkStream);
                            try
                            {
                                rr = assets.open(item).ToNetworkStream();
                            }
                            catch
                            {
                                throw;
                            }

                            return rr;
                        }
                    }
                );
            }
            #endregion


            #region CreateServer at port
            var r = new System.Random();
            var port = r.Next(1024, 32000);
            var ip = getLocalIpAddress();
            var ipa = Dns.GetHostAddresses(getLocalIpAddress())[0];

            Class1Shared.CreateServer(
                ipa,
                port,
                x =>
                {

                }, a.ToArray()
            ).Start();
            #endregion

            #region uri
            var uri = "http://" + ip;

            uri += ":";
            uri += ((object)(port)).ToString();
            #endregion


            #region WebView as UI at uri
            var webview = new WebView(this);
            webview.setWebViewClient(new MyWebViewClient { });
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

            webview.loadUrl(uri);
            #endregion

        }

        public class InternalFileInfo
        {
            public string FileName;

            // jsc does not yet detect field generics?
            //public Func<Stream> OpenFile;
            public StreamFunc OpenFile;
        }

        #region Class1Shared
        public delegate void NetworkStreamAction(NetworkStream s);
        public delegate NetworkStream StreamFunc();

        public class Class1Shared
        {
            public class Int32Box
            {
                public int value;
            }

            public static Thread CreateServer(IPAddress ipa, int port, Action<string> Console_WriteLine, InternalFileInfo[] assets)
            {
                var random = new System.Random();

                // Error 312 (net::ERR_UNSAFE_PORT): Unknown error.
                if (port < 1024)
                    port = random.Next(1024, 32000);


                var cid = 0;

                NetworkStreamAction AtConnection =
                    s =>
                    {
                        var id = cid++;

                        Console_WriteLine("#" + cid);

                        //log("AtConnection");

                        var r = new SmartStreamReader(s);

                        var HTTP_METHOD_PATH_QUERY = r.ReadLine();

                        Console_WriteLine("#" + cid + HTTP_METHOD_PATH_QUERY);

                        var HTTP_METHOD = HTTP_METHOD_PATH_QUERY.TakeUntilOrEmpty(" ");

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

                                s.Write(oa, 0, oa.Length);

                                s.Flush();
                                s.Close();
                                return;
                            }
                        #endregion


                        var HTTP_PATH_QUERY = HTTP_METHOD_PATH_QUERY.SkipUntilOrEmpty(" ").TakeUntilLastOrEmpty(" ");
                        var HTTP_PATH = HTTP_PATH_QUERY.TakeUntilIfAny("?");
                        var HTTP_QUERY = HTTP_PATH_QUERY.SkipUntilOrEmpty("?");

                        #region HTTP_HEADERS
                        var HTTP_HEADERS = new List<string>();

                        var br = r.ReadLine();

                        while (!string.IsNullOrEmpty(br))
                        {
                            HTTP_HEADERS.Add(br);

                            br = r.ReadLine();
                        }
                        #endregion


                        var data = r.ReadStreamToEnd();


                        var selected_item = default(InternalFileInfo);

                        foreach (var item in assets)
                        {
                            // LINQ please!
                            if (HTTP_PATH == "/" + item.FileName)
                                selected_item = item;

                        }


                        {
                            var m = new MemoryStream();

                            Console_WriteLine("#" + cid + " 200");

                            if (selected_item != null)
                            {
                                var path = selected_item.FileName;

                                #region WriteLineASCII
                                Action<string> WriteLineASCII = (string e) =>
                                {
                                    var x = Encoding.ASCII.GetBytes(e + "\r\n");

                                    s.Write(x, 0, x.Length);
                                };
                                #endregion

                                WriteLineASCII("HTTP/1.1 200 OK");

                                Action<string> SetContentType =
                                    value => WriteLineASCII("Content-Type: " + value);

                                SetContentType("text/html; charset=utf-8");

                                if (path.EndsWith(".gif"))
                                    SetContentType("image/gif");
                                else if (path.EndsWith(".png"))
                                    SetContentType("image/png");
                                else if (path.EndsWith(".htm"))
                                    SetContentType("text/html");
                                else
                                    SetContentType("application/octet-stream");

                                //WriteLineASCII("Content-Length: " + data.Length);
                                WriteLineASCII("Connection: close");


                                WriteLineASCII("");

                                selected_item.OpenFile().CopyTo(s);

                                //m.Write(bytes, 0, bytes.Length);

                                // send the file!
                                //WriteLineASCII("<pre>" + selected_item.FileName + "</pre>");
                            }
                            else
                            {
                                #region WriteLineASCII
                                Action<string> WriteLineASCII = (string e) =>
                                {
                                    var x = Encoding.ASCII.GetBytes(e + "\r\n");

                                    m.Write(x, 0, x.Length);
                                };
                                #endregion

                                WriteLineASCII("HTTP/1.1 200 OK");
                                WriteLineASCII("Content-Type:	text/html; charset=utf-8");
                                //WriteLineASCII("Content-Length: " + data.Length);
                                WriteLineASCII("Connection: close");


                                WriteLineASCII("");

                                #region html index
                                WriteLineASCII("<html>");

                                WriteLineASCII("<body>");
                                //WriteLineASCII("<pre style='color: blue;'>" + new { HTTP_METHOD, HTTP_PATH, HTTP_QUERY, data = data.Length } + "</pre>");

                                WriteLineASCII("<code style='color: green;'>HTTP_METHOD: " + HTTP_METHOD + "</code><br />");
                                WriteLineASCII("<code style='color: green;'>HTTP_PATH: " + HTTP_PATH + "</code><br />");
                                WriteLineASCII("<code style='color: green;'>HTTP_QUERY: " + HTTP_QUERY + "</code><br />");
                                WriteLineASCII("<code style='color: green;'>data: " + data.Length + "</code><br />");
                                WriteLineASCII("<code style='color: green;'>data: 0x" + data.Length.ToString("x8") + "</code><br />");

                                var boundary = "";

                                #region HTTP_HEADERS
                                foreach (var item in HTTP_HEADERS.ToArray())
                                {
                                    var HeaderKey = item.TakeUntilOrEmpty(":");
                                    var HeaderValue = item.SkipUntilIfAny(":");

                                    // http://www.w3.org/TR/html401/interact/forms.html#h-17.13.4.2
                                    // http://www.w3.org/Protocols/rfc1341/7_2_Multipart.html
                                    if (HeaderKey == "Content-Type")
                                        boundary = HeaderValue.SkipUntilOrEmpty("multipart/form-data; boundary=");


                                    WriteLineASCII("<code style='color: gray;'>" + HeaderKey + "</code>:");
                                    WriteLineASCII("<code style='color: green;'>" + HeaderValue + "</code><br />");
                                }
                                #endregion

                                WriteLineASCII("<hr />");
                                WriteLineASCII("<pre>" + boundary + "</pre>");
                                WriteLineASCII("<hr />");

                                WriteLineASCII("<h1 style='color: red;'>Hello world</h2><h3>jsc</h3>");
                                WriteLineASCII("<pre>" + HTTP_METHOD_PATH_QUERY + "</pre>");


                                if (string.IsNullOrEmpty(boundary))
                                    WriteLineASCII("<pre>" + WriteHexDump(data.ToBytes()) + "</pre>");

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
                                        WriteLineASCII("<hr />");
                                        WriteLineASCII("<pre>" + WriteHexDump(chunk) + "</pre>");
                                    }

                                }
                                #endregion




                                WriteLineASCII("<hr />");

                                foreach (var item in assets)
                                {
                                    WriteLineASCII("<a href='" + item.FileName + "'>" + item.FileName + "</a>");
                                    WriteLineASCII("<hr />");
                                }

                                WriteLineASCII("<hr />");

                                WriteLineASCII("<form target='_blank' action='?WebMethod=06000048' method='POST'><br /> <img src='http://i.msdn.microsoft.com/deshae98.pubmethod(en-us,VS.90).gif' /> method: <code><a href='?WebMethod=06000048'>Hello</a></code><input type='submit' value='Invoke'  /><br /> &nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' /> parameter: <code>data</code> = <input type='text'  name='_06000048_data' value='' /><br /> &nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubdelegate(en-us,VS.90).gif' /> parameter: <code>result</code></form>");

                                WriteLineASCII("<form target='_blank' action='?WebMethod=06000048' method='POST' enctype='multipart/form-data'>");

                                WriteLineASCII("  <input type='file' name='pic' size='40' accept='image/*' />");
                                WriteLineASCII("  <input type='file' name='foo' />");
                                WriteLineASCII("  <br /> <img src='http://i.msdn.microsoft.com/deshae98.pubmethod(en-us,VS.90).gif' /> method: <code><a href='?WebMethod=06000048'>Hello</a></code><input type='submit' value='Invoke'  /><br /> &nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' /> parameter: <code>data</code> = <input type='text'  name='_06000048_data' value='' /><br /> &nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubdelegate(en-us,VS.90).gif' /> parameter: <code>result</code></form>");

                                WriteLineASCII("</body>");

                                WriteLineASCII("</html>");
                                #endregion


                                var oa = m.ToArray();

                                s.Write(oa, 0, oa.Length);


                            }


                            s.Flush();
                            s.Close();


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


}
