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
using System.Security.Cryptography;
using System.Diagnostics;

namespace JVMCLRRSADuplex
{
	class RSADuplex
	{
		public RSAParameters PublicKey;

		public Action<byte[]> AtMessage;

		//public static int dwKeySizeFromMaxData(int MaxData) => MaxData = (dwKeySize - 384) / 8 + 7;
		//public static int dwKeySizeFromMaxData(int MaxData) => MaxData - 7 = (dwKeySize - 384) / 8;
		public static int dwKeySizeFromMaxData(int MaxData) => 384 + 8 * (MaxData - 7);


		public RSADuplex(
			// MaxData = 471
			//int dwKeySize = (0x100 + 0x100) * 8
			int dwKeySize = (0x100 + 0x100) * 3
			, Action<RSADuplex> ready = null)
		{
			// we will be running on multiple vms.
			// the callbacks will be encrypted.

			//var MaxData = (RSA.KeySize - 384) / 8 + 37;
			var MaxData = (dwKeySize - 384) / 8 + 7;

			Console.WriteLine(
			   typeof(object).AssemblyQualifiedName + " enter RSADuplex " + new { dwKeySize, MaxData }
			);

			var sw = Stopwatch.StartNew();

			var RSA = new RSACryptoServiceProvider(dwKeySize: dwKeySize, parameters: new CspParameters { });

			this.PublicKey = RSA.ExportParameters(includePrivateParameters: false);

			Console.WriteLine(
			   typeof(object).AssemblyQualifiedName + " ready RSADuplex " + new { sw.Elapsed, e = this.PublicKey.Exponent.Length, m = this.PublicKey.Modulus.Length }
			);

			this.AtMessage =
				EncryptedHelloString =>
				{
					Console.WriteLine(
					   typeof(object).AssemblyQualifiedName + " enter RSADuplex.AtMessage " + new { EncryptedHelloString = EncryptedHelloString.Length }
					);

					//foreach (var item in EncryptedHelloString)

					for (int i = 0; i < EncryptedHelloString.Length; i++)
					{
						var item = EncryptedHelloString[i];

                        Console.Write(item.ToString("x2"));

					}

					Console.WriteLine();


					// X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyImport\TestWebCryptoKeyImport\ApplicationWebService.cs
					var xdata = RSA.Decrypt(
					 //ebytes, fOAEP: false
					 EncryptedHelloString, fOAEP: true
					 );

					var xstring = Encoding.UTF8.GetString(xdata);

					Console.WriteLine(
						typeof(object).AssemblyQualifiedName + " at RSADuplex.AtMessage: " + new { xstring }
					);
				};

			if (ready != null)
				ready(this);

			Console.WriteLine(
			   typeof(object).AssemblyQualifiedName + " exit RSADuplex"
			);

		}
	}

	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{
			// if only jsc ssl would start exposing the e and m to client?

			// http://bouncy-castle.1462172.n4.nabble.com/FW-RSA-ECB-OAEP-Encryption-From-JAVA-to-NET-td1463697.html

			// http://stackoverflow.com/questions/17110217/is-rsa-pkcs1-oaep-padding-supported-in-bouncycastle
			// http://www.java2s.com/Tutorial/Java/0490__Security/RSAexamplewithOAEPPaddingandrandomkeygeneration.htm

			// X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs
			// X:\jsc.svn\examples\java\hybrid\JVMCLRCryptoKeyExport\JVMCLRCryptoKeyExport\Program.cs

			try
			{
				new RSADuplex(
					//dwKeySize: RSADuplex.dwKeySizeFromMaxData(MaxData: 8),
					dwKeySize: 512,

					ready: RSADuplex =>
					{

						// switch to clr

						var data = CLRProgram.CLRMain(
								new CLRData
								{
									e = RSADuplex.PublicKey.Exponent,
									m = RSADuplex.PublicKey.Modulus

									// no message, as we do not yet know the future public key
								}
							);

						if (data.EncryptedHelloString != null)
						{
							RSADuplex.AtMessage(data.EncryptedHelloString);
						}

						// we should now be able to relply


						var data2 = CLRProgram.CLRMain(
								new CLRData
								{
									//e = RSADuplex.PublicKey.Exponent,
									//m = RSADuplex.PublicKey.Modulus,

									EncryptedHelloString = new EncryptedString(data.e, data.m,
										//text: typeof(object).AssemblyQualifiedName + " hello from Main"
										text: "54321"
									)
								}
							);
					}
				);


				//java.lang.Object, rt enter RSADuplex {{ dwKeySize = 1536, MaxData = 151 }}
				//RSACryptoServiceProvider before generateKeyPair { dwKeySize = 1536 }
				//RSACryptoServiceProvider after generateKeyPair { ElapsedMilliseconds = 1935 }
				//java.lang.Object, rt ready RSADuplex {{ Elapsed = 00:00:01.1940.0, e = 3, m = 192 }}
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 enter RSADuplex { dwKeySize = 1536, MaxData = 151 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 ready RSADuplex { Elapsed = 00:00:00.6692472, e = 3, m = 192 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 exit RSADuplex
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 will prepare reply { m = 192, bytes = 109 }
				//java.lang.Object, rt enter RSADuplex.AtMessage {{ EncryptedHelloString = 192 }}
				//java.lang.Object, rt at RSADuplex.AtMessage: {{ xstring = System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 hello from CLRMain }}
				//java.lang.Object, rt will prepare reply {{ m = 192, bytes = 36 }}
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 enter RSADuplex.AtMessage { EncryptedHelloString = 192 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 at RSADuplex.AtMessage: { xstring = java.lang.Object, rt hello from Main }
				//java.lang.Object, rt exit RSADuplex


				//java.lang.Object, rt enter RSADuplex {{ dwKeySize = 392, MaxData = 8 }}
				//RSACryptoServiceProvider before generateKeyPair { dwKeySize = 392 }
				//{{ err = java.lang.RuntimeException: RSA keys must be at least 512 bits long, Message = RSA keys must be at least 512 bits long, StackTrace = java.lang.RuntimeException: RSA keys must be at least 512 bits long
				//        at ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography.__RSACryptoServiceProvider.<init>(__RSACryptoServiceProvider.java:61)
				//        at JVMCLRRSADuplex.RSADuplex.<init>(RSADuplex.java:51)
				//        at JVMCLRRSADuplex.Program.main(Program.java:28)
				//Caused by: java.security.InvalidParameterException: RSA keys must be at least 512 bits long
				//        at sun.security.rsa.RSAKeyPairGenerator.initialize(Unknown Source)
				//        at java.security.KeyPairGenerator$Delegate.initialize(Unknown Source)
				//        at java.security.KeyPairGenerator.initialize(Unknown Source)
				//        at ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography.__RSACryptoServiceProvider.<init>(__RSACryptoServiceProvider.java:54)
				//        ... 2 more
				// }}

				//java.lang.Object, rt enter RSADuplex {{ dwKeySize = 512, MaxData = 23 }}
				//RSACryptoServiceProvider before generateKeyPair { dwKeySize = 512 }
				//RSACryptoServiceProvider after generateKeyPair { ElapsedMilliseconds = 299 }
				//java.lang.Object, rt ready RSADuplex {{ Elapsed = 00:00:00.307.0, e = 3, m = 64 }}
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 enter RSADuplex { dwKeySize = 512, MaxData = 23 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 ready RSADuplex { Elapsed = 00:00:00.0077127, e = 3, m = 64 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 exit RSADuplex
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 will prepare reply { m = 64, bytes = 5 }
				//java.lang.Object, rt enter RSADuplex.AtMessage {{ EncryptedHelloString = 64 }}
				//java.lang.Object, rt at RSADuplex.AtMessage: {{ xstring = 12345 }}
				//java.lang.Object, rt will prepare reply {{ m = 64, bytes = 5 }}
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 enter RSADuplex.AtMessage { EncryptedHelloString = 64 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 at RSADuplex.AtMessage: { xstring = 54321 }
				//java.lang.Object, rt exit RSADuplex








				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 enter RSADuplex { dwKeySize = 392, MaxData = 8 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 ready RSADuplex { Elapsed = 00:00:00.0130751, e = 3, m = 49 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 enter RSADuplex { dwKeySize = 392, MaxData = 8 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 ready RSADuplex { Elapsed = 00:00:00.0046702, e = 3, m = 49 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 exit RSADuplex
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 will prepare reply { m = 49, bytes = 5 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 enter RSADuplex.AtMessage { EncryptedHelloString = 49 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 at RSADuplex.AtMessage: { xstring = 12345 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 will prepare reply { m = 49, bytes = 5 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 enter RSADuplex.AtMessage { EncryptedHelloString = 49 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 at RSADuplex.AtMessage: { xstring = 54321 }
				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 exit RSADuplex


			}
			catch (Exception err)
			{

				Console.WriteLine(new { err, err.Message, err.StackTrace });
			}

			Thread.Sleep(25000);

			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150323
		}


	}



	public class EncryptedString
	{
		// Y:\staging\web\java\JVMCLRRSADuplex\EncryptedString.java:49: error: variable EncryptedBytes might already have been assigned
		// Y:\staging\web\java\JVMCLRRSADuplex\EncryptedString.java:53: error: variable EncryptedBytes might not have been initialized
		//public readonly byte[] EncryptedBytes = null;
		public byte[] EncryptedBytes = null;

		public EncryptedString(byte[] e, byte[] m, string text)
		{

			if (e == null)
				return;

			if (m == null)
				return;


			var bytes = Encoding.UTF8.GetBytes(text);

			//Caused by: java.lang.RuntimeException: Message is larger than modulus

			Console.WriteLine(
			   typeof(object).AssemblyQualifiedName + " will prepare reply " + new { m = m.Length, bytes = bytes.Length }
			);


			#region EncryptedHelloString
			var n = new RSACryptoServiceProvider();

			n.ImportParameters(
				new RSAParameters { Exponent = e, Modulus = m }
			);

			// http://stackoverflow.com/questions/9839274/rsa-encryption-by-supplying-modulus-and-exponent
			//javax.crypto.IllegalBlockSizeException: Data must not be longer than 256 bytes

			//var text = typeof(object).AssemblyQualifiedName + " hello from CLRMain";

			this.EncryptedBytes = n.Encrypt(
				bytes, fOAEP: true
			);
			#endregion
		}

		public static implicit operator byte[] (EncryptedString e)
		{
			return e.EncryptedBytes;
		}
	}

	class CLRData
	{
		public byte[] e;
		public byte[] m;

		public byte[] EncryptedHelloString;
	}

	[SwitchToCLRContext]
	static class CLRProgram
	{
		// java.lang.Object, rt enter RSADuplex {{ dwKeySize = 64, MaxData = -33 }}
		//		RSACryptoServiceProvider before generateKeyPair { dwKeySize = 64 }
		//{{ err = java.lang.RuntimeException: RSA keys must be at least 512 bits long, Message = RSA keys must be at least 512 bits long, StackTrace = java.lang.RuntimeException: RSA keys must be at least 512 bits long
		//		at ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography.__RSACryptoServiceProvider.<init>(__RSACryptoServiceProvider.java:61)
		//        at JVMCLRRSADuplex.RSADuplex.<init>(RSADuplex.java:51)
		//        at JVMCLRRSADuplex.Program.main(Program.java:28)
		//Caused by: java.security.InvalidParameterException: RSA keys must be at least 512 bits long
		//		at sun.security.rsa.RSAKeyPairGenerator.initialize(Unknown Source)
		//        at java.security.KeyPairGenerator$Delegate.initialize(Unknown Source)
		//        at java.security.KeyPairGenerator.initialize(Unknown Source)
		//        at ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography.__RSACryptoServiceProvider.<init>(__RSACryptoServiceProvider.java:54)
		//        ... 2 more
		//}}


		// will this CLR load before JVM?
		private static RSADuplex RSADuplex = new RSADuplex(
					//dwKeySize: RSADuplex.dwKeySizeFromMaxData(MaxData: 8)
					dwKeySize: 512
			);

		[STAThread]
		public static CLRData CLRMain(CLRData data)
		{
			// x:\jsc.svn\examples\javascript\test\testwebcryptokeyexport\testwebcryptokeyexport\applicationwebservice.cs

			if (data.EncryptedHelloString != null)
			{
				RSADuplex.AtMessage(data.EncryptedHelloString);
			}



			return new CLRData
			{
				e = RSADuplex.PublicKey.Exponent,
				m = RSADuplex.PublicKey.Modulus,


				EncryptedHelloString = new EncryptedString(data.e, data.m,
					//text: typeof(object).AssemblyQualifiedName + " hello from CLRMain"
					text: "12345"
				)
			};


		}
	}



}
