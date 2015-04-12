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
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRUDPReceiveAsync
{

	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{
			System.Console.WriteLine(
			   typeof(object).AssemblyQualifiedName
			);


			new { }.With(
				async delegate
				{
					var port = 40804;

					//var u = new UdpClient("127.0.0.1", port);
					var u = new UdpClient(port);


					Console.WriteLine("ReceiveAsync...");

					while (true)
					{
						var x = await u.ReceiveAsync();

						Console.WriteLine(new { x.Buffer.Length });
					}
				}
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

//script: error JSC1000: Java : valuetype ScriptCoreLib.Shared.BCLImplementation.System.Net.Sockets.__UdpReceiveResult - ScriptCoreLib.Shared.BCLImplementation.System.Net.Sockets.__UdpReceiveResult must define a default .ctor
//internal compiler error at method
// assembly: Y:\staging\JVMCLRUDPReceiveAsync__i__d.jvm.exe at C:\Users\Arvo\AppData\Local\Temp\q4flwv54.z0g\staging.jvm
// type: JVMCLRUDPReceiveAsync.Program+<>c+<<Main>b__0_0>d+<MoveNext>06000024, JVMCLRUDPReceiveAsync__i__d.jvm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// method: <>forwardref
// Java : valuetype ScriptCoreLib.Shared.BCLImplementation.System.Net.Sockets.__UdpReceiveResult - ScriptCoreLib.Shared.BCLImplementation.System.Net.Sockets.__UdpReceiveResult must define a default .ctor