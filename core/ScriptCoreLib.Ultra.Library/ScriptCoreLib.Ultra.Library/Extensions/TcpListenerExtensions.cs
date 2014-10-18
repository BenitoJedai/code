using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Net.Security;

namespace ScriptCoreLib.Extensions
{
    public static class TcpListenerExtensions
    {
        //static void BridgeStreamTo(this NetworkStream x, NetworkStream y, int ClientCounter, string prefix = "#")
        static void BridgeStreamTo(this Stream x, Stream y, int ClientCounter, string prefix = "#")
        {
            new Thread(
               delegate ()
            {
                var buffer = new byte[0x100000];

                while (true)
                {
                    //
                    try
                    {

                        var c = x.Read(buffer, 0, buffer.Length);

                        if (c <= 0)
                            return;


                        Console.WriteLine(prefix + ClientCounter.ToString("x4") + " 0x" + c.ToString("x4") + " bytes");

                        if (prefix.StartsWith("?"))
                            Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, c));

                        y.Write(buffer, 0, c);

                        Thread.Sleep(1);
                    }
                    catch
                    {
                        //Console.WriteLine("#" + ClientCounter + " error");

                        return;
                    }
                }
            }
           )
            {
                Name = "BridgeStreamTo",
                IsBackground = true,
                Priority = ThreadPriority.Lowest
            }.Start();
        }

        static void BridgeConnectionTo(this TcpClient x, TcpClient y, int ClientCounter, string rx, string tx)
        {
            x.GetStream().BridgeStreamTo(y.GetStream(), ClientCounter, rx);
            y.GetStream().BridgeStreamTo(x.GetStream(), ClientCounter, tx);
        }

        public static void BridgeConnectionToPort(this TcpListener x, int port)
        {
            BridgeConnectionToPort(x, port, "> ", "< ");
        }

        public static void BridgeConnectionToPort(this TcpListener x, int port, string rx, string tx)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\TcpListenerExtensions.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201410/20141018-ssl
            // X:\jsc.svn\examples\java\hybrid\JVMCLRTCPMultiplex\JVMCLRTCPMultiplex\Program.cs

            // Error: There is no matching certificate in the issuer's Root cert store
            var makecert = @"C:\Program Files (x86)\Windows Kits\8.0\bin\x64\makecert.exe";


            var CN = "device SSL authority for developers";

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

                                    //Console.WriteLine(new { item.Subject, linkvalue });

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

                    // are we slowing down checking certs at each connection?
                    // are we spamming the cert store?
                    var n = CertificateFromCurrentUser();

                    if (n == null)
                    {
                        // http://stackoverflow.com/questions/13332569/how-to-create-certificate-authority-certificate-with-makecert
                        // http://www.jayway.com/2014/09/03/creating-self-signed-certificates-with-makecert-exe-for-development/
                        // http://stackoverflow.com/questions/4095297/self-signed-certificates-performance-in-wcf-scenarios
                        Console.WriteLine(
                            new { makecert }
                            );

                        // logical store name
                        Process.Start(
                            new ProcessStartInfo(
                            makecert,
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


            #region authority
            var r = CertificateRootFromCurrentUser();

            if (r == null)
            {
                Console.WriteLine(new { makecert });

                Process.Start(
                    new ProcessStartInfo(
                        makecert,
                        // this cert is constant
                        "-r -cy authority -a SHA1 -n \"CN=" + CN + "\"  -len 2048 -m 72 -ss Root -sr currentuser"
                    )
                {
                    UseShellExecute = false
                }

                ).WaitForExit();
            }
            #endregion

            x.Start();

            var ClientCounter = 0;

            Action<TcpClient> yield =
                clientSocket =>
                {
                    var p = new Library.Eugene.PeekableStream(clientSocket.GetStream(), 1);


                    var zbuffer = new byte[1];
                    var z = p.Peek(zbuffer, 0, 1);

                    if (zbuffer[0] == 0x16)
                    {
                        Console.WriteLine("enter https");
                        //using (
                        SslStream sslStream = new SslStream(
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
                            //Console.WriteLine(
                            //    new { certificate }
                            //    );

                            return true;
                        }
                               ),
                           encryptionPolicy: EncryptionPolicy.RequireEncryption
                           );
                        //)
                        {

                            try
                            {
                                // AuthenticateAsServer
                                // can this hang? if we use the wrong stream!

                                var enabledSslProtocols = System.Security.Authentication.SslProtocols.Default;

                                if (typeof(System.Security.Authentication.SslProtocols).GetField("Tls12") != null)
                                {
                                    // even if we link as 4.0 running in 4.5 we have tls1.2
                                    enabledSslProtocols = (System.Security.Authentication.SslProtocols)3072;
                                }

                                //Console.WriteLine(
                                //    new { enabledSslProtocols }
                                //);

                                sslStream.AuthenticateAsServer(
                                    serverCertificate: CertificateFromCurrentUserByLocalEndPoint((IPEndPoint)clientSocket.Client.LocalEndPoint),
                                clientCertificateRequired: false,
                                //clientCertificateRequired: true,
                                // Tls12 = 3072
                                //enabledSslProtocols: System.Security.Authentication.SslProtocols.Tls12,
                                enabledSslProtocols: enabledSslProtocols,
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

                            //Console.WriteLine("read " + sslStream.GetHashCode());


                            var y = new TcpClient();
                            y.Connect(new System.Net.IPEndPoint(IPAddress.Loopback, port));

                            sslStream.BridgeStreamTo(y.GetStream(), ClientCounter, rx);
                            y.GetStream().BridgeStreamTo(sslStream, ClientCounter, tx);

                            //sslStream.Close();
                        }
                        //Console.WriteLine("exit https");
                        return;
                    }

                    {
                        var y = new TcpClient();
                        y.Connect(new System.Net.IPEndPoint(IPAddress.Loopback, port));
                        clientSocket.BridgeConnectionTo(y, ClientCounter, rx, tx);
                    }

                };


            new Thread(
               delegate ()
            {
                while (true)
                {
                    var clientSocket = x.AcceptTcpClient();
                    ClientCounter++;

                    //Console.WriteLine("#" + ClientCounter + " BridgeConnectionToPort");


                    yield(clientSocket);
                }


            }
           )
            {
                IsBackground = true,
                Name = "BridgeConnectionToPort"
            }.Start();
        }
    }
}
