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

namespace JVMCLRLINQOrderByLastWriteTime
{

	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{
			// http://bouncy-castle.1462172.n4.nabble.com/FW-RSA-ECB-OAEP-Encryption-From-JAVA-to-NET-td1463697.html
			// http://stackoverflow.com/questions/17110217/is-rsa-pkcs1-oaep-padding-supported-in-bouncycastle
			// http://www.java2s.com/Tutorial/Java/0490__Security/RSAexamplewithOAEPPaddingandrandomkeygeneration.htm

			// X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

			System.Console.WriteLine(
			   typeof(object).AssemblyQualifiedName
			);

			try
			{

				var aa = Enumerable.ToArray(
					from fname in System.IO.Directory.GetFiles(Environment.CurrentDirectory)
					let ff = new System.IO.FileInfo(fname)
					let LastWriteTime = ff.LastWriteTime
					orderby LastWriteTime
					select new { ff.Name, LastWriteTime }
				);

				// X:\jsc.svn\examples\java\hybrid\JVMCLRCryptoKeyExport\JVMCLRCryptoKeyExport\Program.cs
				aa.WithEach(
					a =>
					{
						Console.WriteLine(a);
					}
				);

				//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
				//{ Name = ScriptCoreLibA.xml, LastWriteTime = 2015-02-04 3:42:33 PM }
				//{ Name = ScriptCoreLib.Ultra.VisualBasic.dll, LastWriteTime = 2015-03-25 3:13:26 PM }
				//{ Name = ScriptCoreLib.Ultra.VisualBasic.pdb, LastWriteTime = 2015-03-25 3:13:26 PM }
				//{ Name = ScriptCoreLib.Ultra.VisualBasic.xml, LastWriteTime = 2015-03-25 3:13:26 PM }
				//{ Name = ScriptCoreLibA.pdb, LastWriteTime = 2015-03-25 3:13:26 PM }
				//{ Name = ScriptCoreLibA.dll, LastWriteTime = 2015-03-25 3:13:26 PM }
				//{ Name = ScriptCoreLib.Ultra.Library.pdb, LastWriteTime = 2015-03-25 3:13:27 PM }
				//{ Name = ScriptCoreLib.Ultra.Library.dll, LastWriteTime = 2015-03-25 3:13:27 PM }
				//{ Name = ScriptCoreLibJava.xml, LastWriteTime = 2015-03-28 9:18:32 AM }
				//{ Name = ScriptCoreLibJava.pdb, LastWriteTime = 2015-03-28 9:18:33 AM }
				//{ Name = ScriptCoreLibJava.dll, LastWriteTime = 2015-03-28 9:18:36 AM }
				//{ Name = ScriptCoreLib.xml, LastWriteTime = 2015-03-28 9:22:46 AM }
				//{ Name = ScriptCoreLib.pdb, LastWriteTime = 2015-03-28 9:22:46 AM }
				//{ Name = ScriptCoreLib.dll, LastWriteTime = 2015-03-28 9:22:46 AM }
				//{ Name = JVMCLRLINQOrderByLastWriteTime.exe.config, LastWriteTime = 2015-03-28 9:25:49 AM }
				//{ Name = JVMCLRLINQOrderByLastWriteTime.exe, LastWriteTime = 2015-03-28 9:30:08 AM }
				//{ Name = JVMCLRLINQOrderByLastWriteTime.pdb, LastWriteTime = 2015-03-28 9:30:08 AM }

				//java.lang.Object, rt
				//{{ Name = ScriptCoreLibA.xml, LastWriteTime = 04.02.2015 15:42:33 }}
				//{{ Name = ScriptCoreLib.Ultra.VisualBasic.dll, LastWriteTime = 25.03.2015 15:13:26 }}
				//{{ Name = ScriptCoreLib.Ultra.VisualBasic.pdb, LastWriteTime = 25.03.2015 15:13:26 }}
				//{{ Name = ScriptCoreLib.Ultra.VisualBasic.xml, LastWriteTime = 25.03.2015 15:13:26 }}
				//{{ Name = ScriptCoreLibA.pdb, LastWriteTime = 25.03.2015 15:13:26 }}
				//{{ Name = ScriptCoreLibA.dll, LastWriteTime = 25.03.2015 15:13:26 }}
				//{{ Name = ScriptCoreLib.Ultra.Library.pdb, LastWriteTime = 25.03.2015 15:13:27 }}
				//{{ Name = ScriptCoreLib.Ultra.Library.dll, LastWriteTime = 25.03.2015 15:13:27 }}
				//{{ Name = ScriptCoreLib.xml, LastWriteTime = 28.03.2015 09:22:46 }}
				//{{ Name = ScriptCoreLib.pdb, LastWriteTime = 28.03.2015 09:22:46 }}
				//{{ Name = ScriptCoreLib.dll, LastWriteTime = 28.03.2015 09:22:46 }}
				//{{ Name = JVMCLRLINQOrderByLastWriteTime.exe.config, LastWriteTime = 28.03.2015 09:25:49 }}
				//{{ Name = ScriptCoreLibJava.xml, LastWriteTime = 28.03.2015 09:40:48 }}
				//{{ Name = ScriptCoreLibJava.pdb, LastWriteTime = 28.03.2015 09:40:49 }}
				//{{ Name = ScriptCoreLibJava.dll, LastWriteTime = 28.03.2015 09:40:49 }}
				//{{ Name = JVMCLRLINQOrderByLastWriteTime.pdb, LastWriteTime = 28.03.2015 09:41:13 }}
				//{{ Name = JVMCLRLINQOrderByLastWriteTime.exe, LastWriteTime = 28.03.2015 09:41:36 }}
				//{{ Name = JVMCLRLINQOrderByLastWriteTime.exports, LastWriteTime = 28.03.2015 09:41:36 }}






			}
			catch (Exception ex)
			{

				Console.WriteLine(new { ex.Message, ex.StackTrace });

			}

			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150323
			Thread.Sleep(10000);
		}
	}

	// hard switch, could we do a soft hop to?
	[SwitchToCLRContext]
	static class CLRProgram
	{
		[STAThread]
		public static byte[] CLRMain(
			byte[] e,
			byte[] m
				)
		{

			return null;
		}
	}



}
