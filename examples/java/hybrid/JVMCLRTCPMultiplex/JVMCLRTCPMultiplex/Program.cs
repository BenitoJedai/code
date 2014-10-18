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
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Cryptography;

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
            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\TcpListenerExtensions.css
            // X:\jsc.svn\examples\javascript\Test\TestTCPMultiplex\TestTCPMultiplex\Application.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201410/20141018-ssl
            // http://msdn.microsoft.com/en-us/library/ms733813.aspx
            // http://stackoverflow.com/questions/4095297/self-signed-certificates-performance-in-wcf-scenarios

            var CN = "device SSL authority for developers";


            #region CertificateRootFromCurrentUser
            Func<X509Certificate> CertificateRootFromCurrentUser =
                delegate
            {
                X509Store store = new X509Store(
                            StoreName.Root,
                    StoreLocation.CurrentUser);
                // https://syfuhs.net/2011/05/12/making-the-x509store-more-friendly/
                // http://ftp.icpdas.com/pub/beta_version/VHM/wince600/at91sam9g45m10ek_armv4i/cesysgen/sdk/inc/wintrust.h

                // Policy Information:
                //URL = http://127.0.0.5:10500

                try
                {

                    store.Open(OpenFlags.ReadOnly);

                    var item = store.Certificates.Find(X509FindType.FindBySubjectName, CN, true);

                    if (item.Count > 0)
                        return item[0];

                }
                finally
                {

                    store.Close();
                }

                return null;

            };
            #endregion

            // Error: There is no matching certificate in the issuer's Root cert store

            var r = CertificateRootFromCurrentUser();

            if (r == null)
            {
                Process.Start(
                                          new ProcessStartInfo(
                                          @"C:\Program Files (x86)\Windows Kits\8.0\bin\x64\makecert.exe",

                           // this cert is constant
                           "-r -cy authority -a SHA1 -n \"CN=" + CN + "\"  -len 2048 -m 72 -ss Root -sr currentuser"
                                          )

                {
                    UseShellExecute = false

                }

                ).WaitForExit();
            }

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


            //  http://stackoverflow.com/questions/463657/makecert-is-it-possible-to-change-the-key-size

            // The certificate has to be generated with "client authentication" option
            // http://stackoverflow.com/questions/18942848/authenticate-user-via-client-signed-ssl-certificate-in-asp-net-application
            // https://github.com/mono/mono/blob/master/mcs/tools/security/makecert.cs

            //X509CertificateBuilder
            // jsc can you build a cert anywhere?

            var port = new Random().Next(8000, 12000);

            // -l <link> Link to the policy information (such as a URL)


            // http://www.michael-thomas.com/tech/msiis/ssl_self_generating_certificates_for_iis_makecert.htm
            // -nscp Include netscape client auth extension
            // http://stackoverflow.com/questions/650017/what-does-subject-mean-in-certificate
            // http://technet.microsoft.com/en-us/library/aa998840.aspx

            // https://access.redhat.com/documentation/en-US/Red_Hat_Certificate_System/8.0/html/Admin_Guide/Managing_Subject_Names_and_Subject_Alternative_Names.html


            // http://blogs.technet.com/b/jhoward/archive/2005/02/02/365323.aspx
            // http://certificate.fyicenter.com/439_Windows__makecert.exe_-in_-eku__Certificate_for_Server_Aut.html

            // http://www.forumeasy.com/forums/archive/ldappro/201211/p135257621115.html


            //'-eku 1.3.6.1.5.5.7.3.1' specifies the new certificate is for "Server Authentication" purpose only.
            // http://stackoverflow.com/questions/12120630/how-do-i-identify-my-server-name-for-server-authentication-by-client-in-c-sharp
            // http://stackoverflow.com/questions/17477279/client-authentication-1-3-6-1-5-5-7-3-2-oid-in-server-certificates
            // http://security.stackexchange.com/questions/36932/what-is-the-difference-between-ssl-and-x-509-certificates
            // http://msdn.microsoft.com/en-us/library/windows/desktop/aa378132(v=vs.85).aspx


            //            Server Authentication (1.3.6.1.5.5.7.3.1)
            //Client Authentication (1.3.6.1.5.5.7.3.2)
            // http://msdn.microsoft.com/en-us/library/windows/desktop/aa386968(v=vs.85).aspx
            // http://www.wilsonmar.com/1certs.htm
            // http://forums.iis.net/t/1180823.aspx

            // http://stackoverflow.com/questions/13806299/how-to-create-a-self-signed-certificate-using-c
            // https://clrsecurity.svn.codeplex.com/svn/Security.Cryptography/src/CngKeyExtensionMethods.cs




            //                ---------------------------
            //Security Warning
            //---------------------------
            //You are about to install a certificate from a certification authority (CA) claiming to represent:
            //127.0.0.101
            //Windows cannot validate that the certificate is actually from "127.0.0.101". You should confirm its origin by contacting "127.0.0.101". The following number will assist you in this process:
            //Thumbprint (sha1): 8B8942FB DEB64552 7BBDAD27 24B78664 A6D85D7E
            //Warning:
            //If you install this root certificate, Windows will automatically trust any certificate issued by this CA. Installing a certificate with an unconfirmed thumbprint is a security risk. If you click "Yes" you acknowledge this risk.
            //Do you want to install this certificate?
            //---------------------------
            //Yes   No   
            //---------------------------

            // http://msdn.microsoft.com/en-us/library/ms733813.aspx


            #region CertificateFromCurrentUserByLocalEndPoint
            Func<IPEndPoint, X509Certificate> CertificateFromCurrentUserByLocalEndPoint =
                LocalEndPoint =>
                {
                    var host = LocalEndPoint.Address.ToString();
                    var link = "http://" + host + ":" + LocalEndPoint.Port;


                    #region CertificateFromCurrentUser
                    Func<X509Certificate> CertificateFromCurrentUser =
                        delegate
                    {
                        X509Store store = new X509Store(
                            //StoreName.Root,
                            StoreName.My,
                            StoreLocation.CurrentUser);
                        // https://syfuhs.net/2011/05/12/making-the-x509store-more-friendly/
                        // http://ftp.icpdas.com/pub/beta_version/VHM/wince600/at91sam9g45m10ek_armv4i/cesysgen/sdk/inc/wintrust.h

                        // Policy Information:
                        //URL = http://127.0.0.5:10500

                        try
                        {

                            store.Open(OpenFlags.ReadOnly);
                            // Additional information: The OID value was invalid.
                            X509Certificate2Collection cers = store.Certificates;


                            foreach (var item in cers)
                            {
                                // http://comments.gmane.org/gmane.comp.emulators.wine.devel/86862
                                var SPC_SP_AGENCY_INFO_OBJID = "1.3.6.1.4.1.311.2.1.10";

                                // // spcSpAgencyInfo private extension

                                var elink = item.Extensions[SPC_SP_AGENCY_INFO_OBJID];
                                if (elink != null)
                                {
                                    var prefix = 6;
                                    var linkvalue = Encoding.UTF8.GetString(elink.RawData, prefix, elink.RawData.Length - prefix);

                                    Console.WriteLine(new { item.Subject, linkvalue });

                                    if (linkvalue == link)
                                        return item;
                                }
                            }
                        }
                        finally
                        {

                            store.Close();
                        }

                        return null;

                    };
                    #endregion

                    var n = CertificateFromCurrentUser();

                    if (n == null)
                    {
                        // http://stackoverflow.com/questions/13332569/how-to-create-certificate-authority-certificate-with-makecert
                        // http://www.jayway.com/2014/09/03/creating-self-signed-certificates-with-makecert-exe-for-development/
                        // http://stackoverflow.com/questions/4095297/self-signed-certificates-performance-in-wcf-scenarios

                        // logical store name
                        Process.Start(
                            new ProcessStartInfo(
                            @"C:\Program Files (x86)\Windows Kits\8.0\bin\x64\makecert.exe",
                //"-r  -n \"CN=localhost\" -m 12 -sky exchange -sv serverCert.pvk -pe -ss my serverCert.cer"
                //"-r  -n \"CN=localhost\" -m 12 -sky exchange -pe -ss my serverCert.cer -sr localMachine"
                //"-r  -n \"CN=localhost\" -m 12 -sky exchange -pe -ss my serverCert.cer -sr currentuser"
                //"-r  -a SHA1 -n \"CN=" + host + "\"  -len 2048 -m 1 -sky exchange -pe -ss my -sr currentuser -l " + link
                //"-r -cy authority -eku 1.3.6.1.5.5.7.3.1,1.3.6.1.5.5.7.3.2 -a SHA512 -n \"CN=" + host + "\"  -len 2048 -m 1 -sky exchange  -ss Root -sr currentuser -l " + link

                // chrome wont like SHA512
                // https://code.google.com/p/chromium/issues/detail?id=342230
                // http://serverfault.com/questions/407006/godaddy-ssl-certificate-shows-domain-name-instead-of-full-company-name
                // The certificate's O attribute in the subject (organization), along with the C attribute (country) determine what is displayed. If they are absent, it will simply display the primary subject domain name from the certificate.

                //"-r -cy authority -eku 1.3.6.1.5.5.7.3.1,1.3.6.1.5.5.7.3.2 -a SHA1 -n \"CN=" + host + ",O=JVMCLRTCPMultiplex\"  -len 2048 -m 1 -sky exchange  -ss Root -sr currentuser -l " + link
                //" -eku 1.3.6.1.5.5.7.3.1,1.3.6.1.5.5.7.3.2 -a SHA1 -n \"CN=" + host + "\"  -len 2048 -m 1 -sky exchange  -ss MY -sr currentuser -is Root -in \"" + CN + "\" -l " + link
                " -eku 1.3.6.1.5.5.7.3.1 -a SHA1 -n \"CN=" + host + "\"  -len 2048 -m 1 -sky exchange  -ss MY -sr currentuser -is Root -in \"" + CN + "\" -l " + link
                            )

                        {
                            UseShellExecute = false

                        }

                            ).WaitForExit();

                        n = CertificateFromCurrentUser();
                    }

                    return n;
                };
            #endregion




            //store.Open(OpenFlags.

            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            Process.Start(@"http://" + "127.0.0.101" + ":" + port); //.WaitForExit();
            //Process.Start(@"http://localhost:" + port); //.WaitForExit();

            // "X:\jsc.svn\examples\java\hybrid\JVMCLRSSLTCPListener\JVMCLRSSLTCPListener\bin\Debug\serverCert.cer.pfx"

            // http://stackoverflow.com/questions/11749036/networkstream-doesnt-support-seek-operations

            Action<TcpClient> yield =
                clientSocket =>
                {
                    var LocalEndPoint = (IPEndPoint)clientSocket.Client.LocalEndPoint;
                    var host = LocalEndPoint.Address.ToString();
                    //var host = LocalEndPoint.Address.ToString();

                    //clientSocket.Client.
                    // why cannot i peek?

                    var p = new Eugene.PeekableStream(clientSocket.GetStream(), 1);

                    var zbuffer = new byte[1];
                    var z = p.Peek(zbuffer, 0, 1);

                    // 47 HTTP
                    // 16 HTTPS
                    Console.WriteLine(zbuffer[0].ToString("x2"));

                    #region 200
                    Action<Stream> x200 =
                        s =>
                    {
                        var rx = new StreamReader(s);
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

                                    // +		clientSocket.Client.LocalEndPoint	{127.0.0.11:10033}	System.Net.EndPoint {System.Net.IPEndPoint}


                                    var bytes = Encoding.UTF8.GetBytes(
                                            "HTTP/1.0 200 OK\r\nConnection: close\r\n\r\n<h1>hello world " + clientSocket.Client.LocalEndPoint + "</h1><a href='https://" + host + ":" + port + "'>https</a> <a href='http://" + host + ":" + port + "'>http</a>"
                                        );

                                    s.Write(bytes, 0, bytes.Length);

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
                    };
                    #endregion

                    if (zbuffer[0] == 0x47)
                    {
                        Console.WriteLine("enter http");
                        x200(p);
                        p.Close();
                        Console.WriteLine("exit http");
                        return;
                    }

                    if (zbuffer[0] == 0x16)
                    {
                        Console.WriteLine("enter https");
                        X509Certificate2 xcertificate = new X509Certificate2(@"X:\jsc.svn\examples\java\hybrid\JVMCLRSSLTCPListener\JVMCLRSSLTCPListener\bin\Debug\serverCert.cer.pfx", "xxx");


                        using (SslStream sslStream = new SslStream(
                            innerStream: p,
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
                            Console.WriteLine(
                                new { certificate }
                                );

                            return true;
                        }
                                ),
                            encryptionPolicy: EncryptionPolicy.RequireEncryption

                            ))
                        {

                            try
                            {
                                // AuthenticateAsServer
                                // can this hang? if we use the wrong stream!

                                sslStream.AuthenticateAsServer(
                                    serverCertificate: CertificateFromCurrentUserByLocalEndPoint((IPEndPoint)clientSocket.Client.LocalEndPoint),
                                //clientCertificateRequired: false,
                                clientCertificateRequired: true,
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

                            Console.WriteLine("read " + sslStream.GetHashCode());

                            x200(sslStream);
                            sslStream.Close();
                        }
                        Console.WriteLine("exit https");
                        return;
                    }


                    Console.WriteLine("exit other");
                    p.Close();

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
