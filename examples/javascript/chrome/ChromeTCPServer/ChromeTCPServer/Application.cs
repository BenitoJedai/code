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

    public static class TheServer
    {
        public static void Invoke(string PageSource)
        {
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
                    new Notification
                    {
                        Message = "Suspend! " + new { t.ElapsedMilliseconds }
                    };
                };

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

            //Func<string, Func<string, Task<chrome.WriteInfo>>, Func<byte[], Task<chrome.WriteInfo>>, Task<object>> Handler =

            #region Handler
            Func<string, chrome.socketId, Task<WriteInfo>> Handler =
                (RequestLine, socketId) =>
                {
                    //var x = new TaskCompletionSource<object>();


                    var path = RequestLine.SkipUntilIfAny(" ").TakeUntilIfAny(" ");

                    Console.WriteLine(
                        new { path }
                    );

                    //{ RequestLine = GET /view-source HTTP/1.1, path = /view-source } 

                    //var nn = new Notification
                    //{
                    //    Message = path,
                    //    Title = "ChromeTCPServer"
                    //};

                    if (path == "/favicon.ico")
                    {
                        var outputString = "HTTP/1.0 404 go away\r\nConnection: close\r\n\r\n";

                        var bytes = Encoding.UTF8.GetBytes(outputString);
                        var xx = new Uint8ClampedArray(bytes);

                        return socketId.write(
                              xx.buffer
                          );
                    }

                    #region /
                    if (path == "/")
                    {
                        var xpage = XElement.Parse(PageSource);

                        xpage.Add(new XElement("script", new XAttribute("src", "view-source"), " "));

                        //WriteString, 
                        var outputString = "HTTP/1.0 200 OK\r\nConnection: close\r\n\r\n"
                            + xpage.ToString();

                        //WriteString(output);


                        var bytes = Encoding.UTF8.GetBytes(outputString);
                        var xx = new Uint8ClampedArray(bytes);

                        return socketId.write(
                              xx.buffer
                          );
                    }
                    #endregion


                    Func<Task<WriteInfo>> y = async delegate
                    {
                        //nn.Title = "before bytes";

                        //Console.WriteLine(new { path } + " before bytes");
                        var xhr = new IXMLHttpRequest();
                        var asset = path.Substring(1);
                        //Console.WriteLine(new { asset });

                        xhr.open(ScriptCoreLib.Shared.HTTPMethodEnum.GET, asset);
                        var xbytes = await xhr.bytes;
                        Console.WriteLine(new { path, xbytes.Length } + " after bytes");
                        //nn.Title = "after bytes";

                        {
                            var outputString = "HTTP/1.0 200 OK\r\nConnection: close\r\n\r\n";
                            var bytes = Encoding.UTF8.GetBytes(outputString);
                            var xx = new Uint8ClampedArray(bytes);

                            //nn.Title = "before headers";
                            await socketId.write(
                                 xx.buffer
                            );
                        }
                        //nn.Title = "after headers";
                        //Console.WriteLine(new { path } + " after headers");

                        {
                            var xx = new Uint8ClampedArray(xbytes);

                            var yy = await socketId.write(
                                 xx.buffer
                            );
                            //Console.WriteLine(new { path } + " after response");

                            return yy;
                        }
                        //nn.Title = "done!";
                    };

                    return y();
                };
            #endregion

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
                    //Console.WriteLine(new { read = new { read.resultCode } });



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

                    var Request = new StringReader(input);
                    var RequestLine = Request.ReadLine();

                    //Console.WriteLine("accept before handler " + new { accept.socketId });
                    var xxx = Handler(RequestLine, accept.socketId);
                    await xxx;

                    //Console.WriteLine("accept exit " + new { accept.socketId });
                    accept.socketId.destroy();
                };

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

                   Console.WriteLine(new { i.socketId, uri });

                   // https://code.google.com/p/chromium/issues/detail?id=253304
                   var listen = await i.socketId.listen(address, port, 50);

                   Console.WriteLine(new { listen });
                   //{ listen = -1 } 
                   if (listen >= 0)
                   {

                       var nn = new Notification
                       {
                           Message = new { uri }.ToString(),
                       };



                       nn.Clicked +=
                           delegate
                           {
                               Native.window.open(uri);
                           };

                       chrome.app.runtime.Launched +=
                            delegate
                            {
                                Native.window.open(uri);
                            };


                       var forever = true;

                       while (forever)
                       {
                           Console.WriteLine("before accept");
                           var accept = await i.socketId.accept();

                           // https://code.google.com/p/chromium/issues/detail?id=170595
                           //await Task.Delay(1000);

                           doaccept(accept);
                       }
                   }


                   Console.WriteLine("done!");

               }
            );
        }
    }
}
