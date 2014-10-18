using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

namespace JVMCLRTCPMultiplex
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // X:\jsc.svn\examples\java\hybrid\JVMCLRSSLTCPListener\JVMCLRSSLTCPListener\Program.cs
            // https://www.npmjs.org/package/port-mux
            // http://c-skills.blogspot.com/
            // http://httpd.apache.org/docs/trunk/ssl/ssl_faq.html


            //// match HTTP GET requests (using a prefix string match) and forward them to localhost:80
            //.addRule('GET ', 80)

            //// match TLS (HTTPS) requests (versions 3.{0,1,2,3}) using a regular expression
            //.addRule(/^\x16\x03[\x00 -\x03] /, '192.168.1.1:443') // regex match

            // f you wanted to be really clever, you could use a connection proxy thing to sniff the first couple of bytes of the incoming data stream, and hand off the connection based on the contents of byte 0: if it's 0x16 (the SSL/TLS 'handshake' byte), pass the connection to the SSL side, if it's an alphabetical character, do normal HTTP. My comment about port numbering applies.
            // http://serverfault.com/questions/47876/handling-http-and-https-requests-using-a-single-port-with-nginx
            // http://www.pond-weed.com/multiplex/

            var port = new Random().Next(8000, 12000);

            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            Process.Start(@"https://localhost:" + port); //.WaitForExit();
            //Process.Start(@"http://localhost:" + port); //.WaitForExit();

            // "X:\jsc.svn\examples\java\hybrid\JVMCLRSSLTCPListener\JVMCLRSSLTCPListener\bin\Debug\serverCert.cer.pfx"

            // http://stackoverflow.com/questions/11749036/networkstream-doesnt-support-seek-operations

            Action<TcpClient> yield =
                clientSocket =>
                {
                    // why cannot i peek?

                    var p = new Eugene.PeekableStream(clientSocket.GetStream(), 1);

                    var zbuffer = new byte[1];
                    var z = p.Peek(zbuffer, 0, 1);

                    // 47 HTTP
                    // 16 HTTPS
                    Console.WriteLine(zbuffer[0].ToString("x2"));

                };

            while (true)
                yield(
                    listener.AcceptTcpClient()
                );
            CLRProgram.CLRMain();

        }


    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );


            MessageBox.Show("click to close");

        }
    }


}
