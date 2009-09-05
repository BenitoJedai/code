using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ScriptCoreLib.CompilerServices
{
	public class PrimitiveServerBuilder
	{
		// classes in this namespace are to be used to build
		// alternative versions additionally to user written assemblies

		// types built within this assembly can reference this assembly for previously 
		// written functionality

		// also not that this assembly depends on ScriptCoreLib but not on ScriptCoreLibJava. 
		// if we were to build a server with java in mind we should add a reference to ScriptCoreLibJava
		// to do that we should be referencing ScriptCoreLibJava at some point... but not within this assembly.

		// this assembly shall support flash network providers like nonoba and kongregate

		// the functionality from jsc.server could also be included within this assembly

		// some functionality shall be refactored to ScriptCoreLib.Net.Server assembly
		// to enable java support via [Optimization("script")]
		

		public const int DefaultPort = 2000;

		public static void ConnectToRouter(Action<Stream> Writer, Action<Stream> Reader)
		{
			var c = new TcpClient();

			c.Connect(IPAddress.Loopback, DefaultPort);

			var s = c.GetStream();

			Writer(s);

			0.AtDelay(
				() => Reader(s)
			);


		}



	
	}
}
