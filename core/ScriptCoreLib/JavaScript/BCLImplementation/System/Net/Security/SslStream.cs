using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Net.Security
{
	// http://referencesource.microsoft.com/#System/net/System/Net/SecureProtocols/_SslStream.cs
	// http://msdn.microsoft.com/en-us/library/system.net.security.sslstream.aspx
	// http://referencesource.microsoft.com/#System/net/System/Net/SecureProtocols/SslStream.cs
	// https://github.com/mono/mono/tree/master/mcs/class/System/System.Net.Security/SslStream.cs

	[Script(Implements = typeof(global::System.Net.Security.SslStream))]
    internal class __SslStream : __AuthenticatedStream
    {
		// "R:\opensource\unmonitored\seclib-1.0\seclib-1.0.0\seclib\Security\Ssl\Ssl3\Ssl3ServerHandshakeLayer.cs"

		// https://github.com/dart-lang/chromedeveditor/blob/master/chrome-app-net.dart/lib/server.dart

		// could we do it on chrome yet?
		// use workers for encryption?

		// 0x16
		// 20141019
		// jsc now has ssl port rewriter in place for CLR


		// http://www.herongyang.com/JDK/SSL-Socket-Server-Example-SslReverseEchoer.html
		// http://www.jguru.com/faq/view.jsp?EID=32388
		// http://exampledepot.8waytrips.com/egs/javax.net.ssl/Server.html

		// https://www.simple-talk.com/dotnet/.net-framework/tlsssl-and-.net-framework-4.0/
		// http://stackoverflow.com/questions/6720275/c-sharp-ssl-tcplistener-tcpclient
		// http://www.dib0.nl/code/343-using-ssl-over-tcp-as-client-and-server-with-c
		// pfx

		// MonoCipherAlgorithmType.Rijndael
		// CipherAlgorithmType.Aes256
		// HashAlgorithmType.Sha1;
		// SslProtocols.Tls


		// PKI and SSL needs to be obsoleted and replaced. 
		// http://jim.com/security/

		// DTLS

		// http://jim.com/security/replacing_TCP.html
		// http://security.stackexchange.com/questions/51590/how-do-ssl-authenticated-users-prove-authenticity-through-udp-packets

		// see
		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140831
		// "X:\jsc.svn\core\ScriptCoreLibJava\javax\net\ssl\SSLServerSocket.cs"

		// can the webserivce return sslstream?

		// http://stackoverflow.com/questions/8086790/streaming-image-over-ssl-socket-doesnt-come-across-correctly

		// how would CLR or Android send out a custom stream and have it signed?

		public virtual X509Certificate LocalCertificate { get; set; }

		// nfc?
        public virtual X509Certificate RemoteCertificate { get; set; }

        // every web request shall expect a callback against its new key
        public override bool IsMutuallyAuthenticated { get; set; }

		// X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\SubtleCrypto.cs

		// X:\opensource\codeplex\webserver\HttpServer\SecureHttpContext.cs
		// when can we use it?
		// when can we do SSL web servers?

		// what about MAC stream?
		// X:\opensource\codeplex\webserver\HttpServer\SecureHttpContext.cs

		// https://support.globalsign.com/customer/portal/articles/1216536-securing-a-public-ip-address---ssl-certificates
		// http://stackoverflow.com/questions/9726802/ssl-socket-between-net-and-java-with-client-authentication
		// X:\opensource\googlecode\dotnetasyncsocket\AsyncSocket\AsyncSocket.cs

		public virtual void AuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
		}

	}
}
