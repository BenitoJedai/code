using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ChromeTCPServer;
using ChromeTCPServer.Design;
using ChromeTCPServer.HTML.Pages;
using chrome;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;
using System.IO;
using ScriptCoreLib.JavaScript.Runtime;
using System.Threading;
using System.Collections.Generic;

namespace ChromeTCPServer
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket == null)
            {
                Native.document.body.style.backgroundColor = "cyan";



                return;
            }

            Notification.DefaultTitle = "ChromeTCPServer";

            TheServer.Invoke(AppSource.Text);

        }

    }


    sealed class x
    {
        public string path;
        public string PageSource;
        public byte[] write;
    }

    public static class TheServer
    {




        #region MulticastSend
        static int MulticastSend_c = 0;
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalAndroidWebServiceActivity.cs
        static void MulticastSend(string reason, string data, string preview, string nn)
        {
            /// http://www.daniweb.com/software-development/java/threads/424998/udp-client-server-in-java

            MulticastSend_c++;

            //var n = c + " hello world";
            var message =
                new XElement("string",
                    new XAttribute("reason", reason),
                    new XAttribute("c", "" + MulticastSend_c),
                    new XAttribute("preview", preview),
                    new XAttribute("n", nn),
                    data
                ).ToString();

            Console.WriteLine(new { message });

            Action x = async delegate
            {
                var socket = await chrome.socket.create("udp", new object());

                var port = new Random().Next(16000, 40000);
                socket.socketId.bind("0.0.0.0", port);


                // http://stackoverflow.com/questions/12253507/how-can-chrome-socket-be-used-for-broadcasting-or-multicasting
                // To send multicast packets all you need to do is bind to a local interface (0.0.0.0 with a random port works, as you've discovered), and then address a packet to the correct group/port (which is what sendTo will do).

                var xmessage = message.ToString();

                Console.WriteLine(new { socket.socketId, port, xmessage });

                var bytes = Encoding.UTF8.GetBytes(xmessage);
                var xdata = new Uint8ClampedArray(bytes);


                // Uncaught Error: Invocation of form socket.sendTo(object, string, integer, function) 
                // doesn't match definition socket.sendTo(integer socketId, binary data, string address, integer port, function callback) 

                // https://code.google.com/p/chromium/issues/detail?id=253304

                // { socketId = 68, bytesWritten = -15 } 
                var result = await socket.socketId.sendTo(
                    xdata.buffer,
                    "239.1.2.3",
                    40404
                );
                Console.WriteLine(new { socket.socketId, result.bytesWritten });

                socket.socketId.destroy();
            };

            x();

            //new Thread(
            //    delegate()
            //    {
            //        try
            //        {
            //            var socket = new DatagramSocket(); //construct a datagram socket and binds it to the available port and the localhos
            //            byte[] b = Encoding.UTF8.GetBytes(message.ToString());    //creates a variable b of type byte
            //            var dgram = new DatagramPacket((sbyte[])(object)b, b.Length, InetAddress.getByName("239.1.2.3"), 40404);//sends the packet details, length of the packet,destination address and the port number as parameters to the DatagramPacket  

            //            socket.send(dgram); //send the datagram packet from this port
            //        }
            //        catch
            //        {
            //            System.Console.WriteLine("server error");
            //        }
            //    }
            //)
            //{

            //    Name = "server"
            //}.Start();
        }
        #endregion

        //static Dictionary<string, byte[]> CachedFiles = new Dictionary<string, byte[]>();

        static x z(Tuple<IProgress<x>, x> scope)
        {
            var path = scope.Item2.path;

            Console.WriteLine("at StartNewWithProgress: " + new { scope.Item2.path, Thread.CurrentThread.ManagedThreadId });

            Action<byte[]> WriteBytes =
                bytes =>
                {
                    scope.Item1.Report(
                        new x { path = scope.Item2.path, PageSource = default(string), write = bytes }
                    );
                };

            //                                02000054 ChromeTCPServer.TheServer+<>c__DisplayClass1b+<<Invoke>b__14>d__43+<>MoveNext
            //script: error JSC1000:
            //error:
            //  statement cannot be a load instruction (or is it a bug?)
            //  [0x00d0] ldarg.0    +1 -0


            #region y
            Func<Task> y = async delegate
            {
                //nn.Title = "before bytes";

                var xbytes = default(byte[]);
                var asset = scope.Item2.path.Substring(1);

                //if (CachedFiles.ContainsKey(asset))
                //{
                //    xbytes = CachedFiles[asset];
                //}
                //else
                //{

                //Console.WriteLine(new { path } + " before bytes");
                var xhr = new IXMLHttpRequest();
                //Console.WriteLine(new { asset });

                xhr.open(ScriptCoreLib.Shared.HTTPMethodEnum.GET, asset);
                xbytes = await xhr.bytes;

                //    CachedFiles[asset] = xbytes;
                //}

                Console.WriteLine(new { asset, xbytes.Length, Thread.CurrentThread.ManagedThreadId } + " after bytes");
                //nn.Title = "after bytes";

                {
                    var o = new StringBuilder();

                    o.AppendLine("HTTP/1.0 200 OK");
                    o.AppendLine("Connection: close");
                    o.AppendLine("Content-Length: " + xbytes.Length);
                    o.AppendLine();


                    var bytes = Encoding.UTF8.GetBytes(o.ToString());

                    WriteBytes(bytes);
                }

                WriteBytes(xbytes);
                WriteBytes(null);

            };
            #endregion


            if (path == "/favicon.ico")
            {
                var outputString = "HTTP/1.0 404 go away\r\nConnection: close\r\n\r\n";

                var bytes = Encoding.UTF8.GetBytes(outputString);
                //var xx = new Uint8ClampedArray(bytes);
                WriteBytes(bytes);
                WriteBytes(null);
            }
            else if (path == "/")
            {
                #region /

                // wont work for web worker?
                //var xpage = XElement.Parse(PageSource);
                //xpage.Add(new XElement("script", new XAttribute("src", "view-source"), " "));

                //WriteString, 
                var outputString = "HTTP/1.0 200 OK\r\nConnection: close\r\n\r\n"
                    + scope.Item2.PageSource + "<script src='view-source'> </script>";

                //WriteString(output);


                var bytes = Encoding.UTF8.GetBytes(outputString);

                WriteBytes(bytes);
                WriteBytes(null);

                #endregion
            }
            else
            {
                y();
            }

            // jsc cannot return task just yet, use progress instead
            return scope.Item2;
        }

        public static void Invoke(string PageSource)
        {
            // https://code.google.com/p/chromium/issues/detail?id=179940
            // https://github.com/GoogleChrome/chrome-app-samples/blob/master/websocket-server/http.js

            #region chrome.runtime
            chrome.app.runtime.Restarted +=
          delegate
          {
              new Notification
              {
                  Message = "Restarted!"
              };
          };


            chrome.runtime.Installed += delegate
            {
                new Notification
                {
                    Message = "Installed!"
                };
            };

            chrome.runtime.Startup +=
                delegate
                {
                    new Notification
                    {
                        Message = "Startup!"
                    };
                };


            var t = new Stopwatch();
            t.Start();

            chrome.runtime.Suspend +=
                delegate
                {
                    var n = new Notification
                    {
                        Message = "Suspend! " + new { t.ElapsedMilliseconds }
                    };

                    n.Clicked += delegate
                    {
                        runtime.reload();
                    };

                };
            #endregion

            //            getNetworkList: 
            //{ name = {CE7A76DF-BCB0-4C3B-8466-D712A03F10A0}, address = fe80::55cc:63eb:5b4:60b4 }
            //{ name = {CE7A76DF-BCB0-4C3B-8466-D712A03F10A0}, address = 192.168.43.252 }
            //{ name = {4E818D17-30DD-46D2-9592-9E1F497D3D82}, address = 2001:0:5ef5:79fb:24f2:176e:3f57:d403 }
            //{ name = {4E818D17-30DD-46D2-9592-9E1F497D3D82}, address = fe80::24f2:176e:3f57:d403 }

            //            getNetworkList: 
            //{ name = {CE7A76DF-BCB0-4C3B-8466-D712A03F10A0}, address = 192.168.43.252 }
            //{ name = {CE7A76DF-BCB0-4C3B-8466-D712A03F10A0}, address = fe80::55cc:63eb:5b4:60b4 }
            //{ name = {4E818D17-30DD-46D2-9592-9E1F497D3D82}, address = 2001:0:5ef5:79fb:24f2:176e:3f57:d403 }
            //{ name = {4E818D17-30DD-46D2-9592-9E1F497D3D82}, address = fe80::24f2:176e:3f57:d403 }






            #region GetAddresss
            Func<NetworkInterface[], string> GetAddresss =
                n =>
                {
                    var a = n.OrderBy(k => k.address.Contains(":")).ToArray();

                    if (a.Length > 0)
                    {
                        return a[0].address;
                    }

                    return "127.0.0.1";
                };
            #endregion


            //Func<string, Func<string, Task<chrome.WriteInfo>>, Func<byte[], Task<chrome.WriteInfo>>, Task<object>> Handler =

            #region Handler
            Func<string, chrome.socketId, Task<string>> Handler =
                (RequestLine, socketId) =>
                {
                    //var x = new TaskCompletionSource<object>();


                    var path = RequestLine.SkipUntilIfAny(" ").TakeUntilIfAny(" ");

                    //Console.WriteLine(
                    //    new { path }
                    //);

                    //{ RequestLine = GET /view-source HTTP/1.1, path = /view-source } 

                    //var nn = new Notification
                    //{
                    //    Message = path,
                    //    Title = "ChromeTCPServer"
                    //};



                    //Console.WriteLine("before StartNewWithProgress: " + new { path, Thread.CurrentThread.ManagedThreadId });

                    var yyy = new TaskCompletionSource<string>();


                    Task.Factory.StartNewWithProgress(
                        new x { path = path, PageSource = PageSource, write = default(byte[]) },

                        function: z,

                        progress:
                            state =>
                            {
                                if (state.write == null)
                                {
                                    Console.WriteLine("progress done StartNewWithProgress: " + new { state.path, Thread.CurrentThread.ManagedThreadId });

                                    yyy.SetResult(state.path);
                                    return;
                                }


                                Console.WriteLine("progress StartNewWithProgress: " + new { state.path, state.write.Length, Thread.CurrentThread.ManagedThreadId });


                                var xx = new Uint8ClampedArray(state.write);

                                //nn.Title = "before headers";
                                socketId.write(
                                     xx.buffer
                                );
                            }
                    );





                    return yyy.Task;
                };
            #endregion

            #region doaccept
            Action<chrome.AcceptInfo> doaccept =
                async accept =>
                {
                    //Console.WriteLine("accept enter " + new { accept.socketId });


                    //var acceptn = new Notification
                    //{
                    //    Message = "accept! " + new { accept.socketId },
                    //    Title = "ChromeTCPServer"
                    //};

                    // { read = { resultCode = -2 } } 
                    var read = await accept.socketId.read();

                    // { read = { resultCode = 370 } } 
                    Console.WriteLine(new { read = new { read.resultCode } });



                    var u = new Uint8ClampedArray(read.data, 0, read.data.byteLength);
                    var input = Encoding.UTF8.GetString(u);

                    Console.WriteLine(new { input });

                    //                       { input = GET / HTTP/1.1
                    //Host: 192.168.43.252:8763
                    //Connection: keep-alive
                    //Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
                    //User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1631.0 Safari/537.36
                    //Accept-Encoding: gzip,deflate,sdch
                    //Accept-Language: en-US,en;q=0.8,et;q=0.6,cs;q=0.4,fr;q=0.2

                    // } 

                    // http://stackoverflow.com/questions/8550383/read-file-packaged-with-chrome-extension-in-content-script

                    //var xhr = new IXMLHttpRequest();
                    //xhr.open(ScriptCoreLib.Shared.HTTPMethodEnum.GET, "manifest.json");
                    //var xbytes = await xhr.bytes;

                    //var output = "HTTP/1.0 200 OK\r\n\r\nhello world\n\n"
                    //    + input + Encoding.UTF8.GetString(xbytes);

                    //GET /favicon.ico HTTP/1.1

                    var HandlerStopwatch = new Stopwatch();
                    HandlerStopwatch.Start();

                    if (string.IsNullOrEmpty(input))
                    {
                        // ??
                    }
                    else
                    {
                        var Request = new StringReader(input);
                        var RequestLine = Request.ReadLine();



                        //Console.WriteLine("accept before handler " + new { accept.socketId });
                        var xxx = Handler(RequestLine, accept.socketId);
                        await xxx;
                    }

                    Console.WriteLine("accept exit " + new { accept.socketId, HandlerStopwatch.ElapsedMilliseconds });
                    accept.socketId.destroy();
                };
            #endregion


            chrome.socket.getNetworkList().ContinueWithResult(
               async
                    n =>
               {
                   //Console.WriteLine(new { n.Length });



                   // Error in response to socket.getNetworkList: TypeError: Cannot read property 'address' of undefined


                   //foreach (var item in a)
                   //{
                   //    Console.WriteLine(new { item.name, item.address });

                   //}
                   //a.WithEach(item => Console.WriteLine(new { item.name, item.address }.ToString()));

                   // do we even have wifi?


                   var address = GetAddresss(n);




                   //                   { Location =
                   // assembly: V:\ChromeTCPServer.Application.exe
                   // type: ChromeTCPServer.Application+ctor>b__b>d__15+<>MoveNext, ChromeTCPServer.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
                   // offset: 0x001a
                   //  method:Int32 <0020> nop.try(<>MoveNext, ctor>b__b>d__15 ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[chrome.CreateInfo] ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[chrome.CreateInfo] ByRef) }
                   //script: error JSC1000: Method: <0020> nop.try, Type: ChromeTCPServer.Application+ctor>b__b>d__15+<>MoveNext; emmiting failed : System.NotImplementedException: { ParameterType = ChromeTCPServer.Application+ctor>b__b>d__15&, p = [0x001f] brtrue.s   +0 -1{[0x001a] call       +1 -4{[0x000b] ldarg.s    +1 -0} {[0x000f] ldfld      +1 -1{[0x000d] ldarg.s    +1 -0} } {[0x0014] ldsfld     +1 -0} {[0x0019] ldarg.0    +1 -0} } , m = System.Func`2[chrome.NetworkInterface,System.Boolean] <0020> nop.try.pop(chrome.NetworkInterface[], ctor>b__b>d__15 ByRef, System.Func`2[chrome.NetworkInterface,System.Boolean], <>MoveNext) }


                   var port = new Random().Next(8000, 9000);
                   var uri = "http://" + address + ":" + port;


                   var i = await socket.create("tcp", new object { });

                   var listen = await i.socketId.listen(address, port, 50);


                   //Console.WriteLine(new { i.socketId, uri });

                   //// https://code.google.com/p/chromium/issues/detail?id=253304

                   Console.WriteLine(new { listen });
                   //{ listen = -1 } 
                   if (listen >= 0)
                   {

                       var nn = new Notification
                       {
                           //Message = new { uri }.ToString(),
                           Message = uri,
                       };


                       #region advertise
                       Action advertise = delegate
                       {
                           var visitme = "Visit me at " + address + ":" + port;



                           // send one without image too...
                           MulticastSend(
                               "",
                               visitme,
                               "",
                               Notification.DefaultTitle
                           );

                           new IHTMLImage { src = Notification.DefaultIconUrl }.InvokeOnComplete(
                               preview =>
                               {
                                   MulticastSend(
                                        "",
                                       visitme,
                                        preview.toDataURL(),
                                       Notification.DefaultTitle
                                    );

                               }
                           );

                       };
                       #endregion



                       nn.Clicked +=
                           delegate
                           {
                               advertise();

                               Native.window.open(uri);
                           };

                       chrome.app.runtime.Launched +=
                            delegate
                            {
                                advertise();

                                Native.window.open(uri);
                            };


                       var forever = true;

                       var accept_gap = new Stopwatch();

                       while (forever)
                       {
                           Console.WriteLine("before accept gap: " + new { accept_gap.ElapsedMilliseconds });
                           var accept = await i.socketId.accept();
                           accept_gap.Restart();

                           // https://code.google.com/p/chromium/issues/detail?id=170595
                           //await Task.Delay(1000);

                           var delayaccept = accept;

                           Task.Delay(111).GetAwaiter().OnCompleted(
                               delegate
                               {
                                   Console.WriteLine("at accept " + new { delayaccept.socketId });
                                   doaccept(delayaccept);
                               }
                           );

                       }
                   }


                   Console.WriteLine("done!");

               }
            );
        }
    }
}
