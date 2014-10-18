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
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.Extensions;

namespace JVMCLRSSLTCPListener
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // X:\jsc.svn\examples\java\hybrid\JVMCLRTCPMultiplex\JVMCLRTCPMultiplex\Program.cs

            // Error	1	Referenced assembly 'ScriptCoreLibA, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null' does not have a strong name.	X:\jsc.svn\examples\java\hybrid\JVMCLRSSLTCPListener\JVMCLRSSLTCPListener\CSC	JVMCLRSSLTCPListener


            // will this work on android?

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            // http://stackoverflow.com/questions/19958829/where-can-i-find-makecert-exe-visual-studio-ultimate-2012

            // "C:\Program Files (x86)\Windows Kits\8.0\bin\x64\makecert.exe"

            // To generate a certificate with private key, you have to use the option -pe. But this is not suficient. 
            // Private key will only be created if your certificate destination is a store. So you'll have to use the command like this:

            // https://social.msdn.microsoft.com/Forums/vstudio/en-US/1367551d-3448-49d7-bcea-6d96d04d1acb/rsacryptoserviceprovider-errors?forum=clr


            //            Error: Save encoded certificate to store failed => 0x5(5)
            //Failed

            // certmgr.msc
            // http://certificateerror.blogspot.com/2011/08/access-local-machine-certificates.html
            // http://devproconnections.com/development/working-certificates
            // http://rickardrobin.wordpress.com/2012/12/05/specifying-a-friendly-name-to-a-certificate/
            // http://myousufali.wordpress.com/2012/05/29/create-a-self-signed-server-certificate/


            // logical store name
            //Process.Start(
            //    new ProcessStartInfo(
            //    @"C:\Program Files (x86)\Windows Kits\8.0\bin\x64\makecert.exe",
            //    //"-r  -n \"CN=localhost\" -m 12 -sky exchange -sv serverCert.pvk -pe -ss my serverCert.cer"
            //    //"-r  -n \"CN=localhost\" -m 12 -sky exchange -pe -ss my serverCert.cer -sr localMachine"
            //    //"-r  -n \"CN=localhost\" -m 12 -sky exchange -pe -ss my serverCert.cer -sr currentuser"
            //    "-r  -n \"CN=localhost\" -m 12 -sky exchange -pe -ss my -sr currentuser"
            //    )

            //{
            //    UseShellExecute = false

            //}

            //    ).WaitForExit();


            // Additional information: The specified network password is not correct.

            X509Certificate2 xcertificate = new X509Certificate2("serverCert.cer.pfx", "xxx");

            Console.WriteLine(
                new
            {
                xcertificate.HasPrivateKey
            }
            );

            // http://www.dib0.nl/code/343-using-ssl-over-tcp-as-client-and-server-with-c
            // http://msdn.microsoft.com/en-us/library/system.net.security.sslstream.aspx

            // random NIC ip and random port?
            // then patch the io bridge?
            // then remove webdev dependency?
            TcpListener listener = new TcpListener(IPAddress.Any, 1300);
            listener.Start();


            Process.Start(@"https://localhost:1300"); //.WaitForExit();

            // https://github.com/stealth/qdns
            // https://github.com/stealth/qdns/blob/master/qdns.cc
            // http://docs-legacy.fortinet.com/fos50hlp/50/index.html#page/FortiOS%205.0%20Help/ldb.134.19.html
            // http://blog.stalkr.net/2012/02/sshhttps-multiplexing-with-sshttp.html
            // https://www.npmjs.org/package/port-mux
            // How?
            //The muxer basically sniffs the initial data packet sent by the client to determine (using a rule set) where to forward the request to.



            Action<TcpClient> yield =
                clientSocket =>
                {



                    //makecert -r -pe -n "CN=localhost" -m 12 -sky exchange -ss my serverCert.cer.  This command created a self-signed certificate with "localhost" for the certificate subject and it makes the certificate valid for 12 months.

                    // jsc, when was the last time we used makecert?
                    // where is makecert?

                    // http://stackoverflow.com/questions/23044914/c-sharp-ssl-server-mode-must-use-a-certificate-with-the-corresponding-private-ke


                    // Additional information: The specified network password is not correct.

                    // can we use async ?

                    // Create a stream to decrypt the data

                    // http://security.stackexchange.com/questions/12426/secure-communication-between-c-client-and-java-server-using-certificates
                    // http://ishare2learn.wordpress.com/2012/05/22/ssl-communication-in-c/
                    // http://blogs.msdn.com/b/joncole/archive/2007/06/13/sample-asynchronous-sslstream-client-server-implementation.aspx


                    // http://c-skills.blogspot.com/2014/05/quantum-dns-trickery.html

                    using (SslStream sslStream = new SslStream(
                        innerStream: clientSocket.GetStream(),
                        leaveInnerStreamOpen: false,

                        userCertificateSelectionCallback:
                            new LocalCertificateSelectionCallback(
                                (object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers) =>
                        {
                            return localCertificates[0];
                        }
                            ),
                        userCertificateValidationCallback:
                            new RemoteCertificateValidationCallback(
                                (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
                        {
                            return true;
                        }
                            ),
                        encryptionPolicy: EncryptionPolicy.RequireEncryption

                        ))
                    {
                        // http://blogs.msdn.com/b/joncole/archive/2007/06/13/sample-asynchronous-sslstream-client-server-implementation.aspx
                        // http://stackoverflow.com/questions/6356070/c-sslstream-and-local-proxy

                        // Additional information: The handshake failed due to an unexpected packet format.

                        // !!!
                        // https://localhost:1300/
                        // Additional information: Authentication failed because the remote party has closed the transport stream.
                        // Additional information: The server mode SSL must use a certificate with the associated private key.
                        // You need to combine the certificate and private key into one PKCS12 package as described here: http://www.dylanbeattie.net/docs/openssl_iis_ssl_howto.html

                        // Additional information: A call to SSPI failed, see inner exception.

                        // The client and server cannot communicate, because they do not possess a common algorithm

                        try
                        {
                            sslStream.AuthenticateAsServer(xcertificate,
                                //clientCertificateRequired: true,
                                clientCertificateRequired: false,
                                // chrome for android does not like IIS TLS 1.2
                                enabledSslProtocols: System.Security.Authentication.SslProtocols.Tls12,
                                checkCertificateRevocation: false
                            );
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(new { ex.Message });

                            if (ex.InnerException != null)
                                Console.WriteLine(new { ex.InnerException.Message });

                            return;
                        }

                        //var x = sslStream.RemoteCertificate;

                        // ... Send and read data over the stream

                        // NET::ERR_CERT_AUTHORITY_INVALID



                        // issue NIC private key pfx?

                        // Error code: ERR_CONNECTION_REFUSED

                        // Your connection is not private
                        // NET::ERR_CERT_AUTHORITY_INVALID


                        //var x = sslStream.ReadByte();

                        Console.WriteLine("read " + sslStream.GetHashCode());

                        //read 1707556
                        //read 15368010
                        //read 4094363
                        //GET / HTTP/1.1
                        //Host: localhost:1300
                        //Connection: keep-alive
                        //Cache-Control: max-age=0
                        //Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
                        //User-Agent: Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2188.2 Safari/537.36
                        //Accept-Encoding: gzip, deflate, sdch
                        //Accept-Language: en-US,en;q=0.8

                        // Additional information: Stream was not readable.
                        var rx = new StreamReader(sslStream);
                        Action y = delegate { };

                        while (true)
                        {
                            var rxl = rx.ReadLine();

                            if (string.IsNullOrEmpty(rxl))
                                break;

                            Console.WriteLine(rxl);

                            if (rxl == "GET / HTTP/1.1")
                                y = delegate
                                {
                                    // Error code: ERR_EMPTY_RESPONSE

                                    // how many times have we played http server?
                                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServer\ChromeTCPServer\Application.cs


                                    sslStream.Write(
                                        Encoding.UTF8.GetBytes(
                                            "HTTP/1.0 200 OK\r\nConnection: close\r\n\r\n<h1>hello world</h1>"
                                        )
                                    );

                                    // i wonder could we send over a delegate as a jsc app? :D

                                    //sslStream.Write(
                                    //    delegate
                                    //{
                                    //    // jsc would have to serialize this. AOT 

                                    //    new ScriptCoreLib.JavaScript.DOM.HTML.IHTMLPre { "hello world" }.AttachToDocument();
                                    //}
                                    //);

                                };

                        }

                        y();


                        //Debugger.Break();

                    }

                };


            // Wait for a client to connect on TCP port 1300
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
