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
            // Error	1	Referenced assembly 'ScriptCoreLibA, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null' does not have a strong name.	X:\jsc.svn\examples\java\hybrid\JVMCLRSSLTCPListener\JVMCLRSSLTCPListener\CSC	JVMCLRSSLTCPListener


            // will this work on android?

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            // http://stackoverflow.com/questions/19958829/where-can-i-find-makecert-exe-visual-studio-ultimate-2012

            // "C:\Program Files (x86)\Windows Kits\8.0\bin\x64\makecert.exe"

            Process.Start(@"C:\Program Files (x86)\Windows Kits\8.0\bin\x64\makecert.exe", "-r -pe -n \"CN=localhost\" -m 12 -sky exchange -ss my serverCert.cer").WaitForExit();


            // http://www.dib0.nl/code/343-using-ssl-over-tcp-as-client-and-server-with-c
            // http://msdn.microsoft.com/en-us/library/system.net.security.sslstream.aspx

            TcpListener listener = new TcpListener(IPAddress.Any, 1300);
            listener.Start();

            // Wait for a client to connect on TCP port 1300
            TcpClient clientSocket = listener.AcceptTcpClient();

            //makecert -r -pe -n "CN=localhost" -m 12 -sky exchange -ss my serverCert.cer.  This command created a self-signed certificate with "localhost" for the certificate subject and it makes the certificate valid for 12 months.

            // jsc, when was the last time we used makecert?
            // where is makecert?



            // Additional information: The specified network password is not correct.
            X509Certificate certificate = new X509Certificate("serverCert.cer");

            // can we use async ?

            // Create a stream to decrypt the data
            using (SslStream sslStream = new SslStream(clientSocket.GetStream()))
            {
                // http://blogs.msdn.com/b/joncole/archive/2007/06/13/sample-asynchronous-sslstream-client-server-implementation.aspx
                // http://stackoverflow.com/questions/6356070/c-sslstream-and-local-proxy

                // Additional information: The handshake failed due to an unexpected packet format.
                // Additional information: The handshake failed due to an unexpected packet format.

                // !!!
                // https://localhost:1300/
                sslStream.AuthenticateAsServer(certificate);
                // ... Send and read data over the stream

                // NET::ERR_CERT_AUTHORITY_INVALID
            }



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
