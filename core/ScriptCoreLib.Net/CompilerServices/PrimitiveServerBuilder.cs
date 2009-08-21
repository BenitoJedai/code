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
		public static string GetVersionInformation()
		{
			return "powered by ScriptCoreLib.Net (server for .net)";
		}

		public class RemoteEndpointIdentity
		{
			public int Index;

			public Action<byte> WriteByte;

			public Func<int> ReadByte;

			public RemoteEndpointIdentity Others;

		}


		public static Action StartRouter(Type t)
		{
			return StartRouter(t,
				k =>
				{
					//// watch out, new player
					//k.Others.WriteByte((byte)'!');

					//// accepted to the playground
					//k.WriteByte((byte)'@');

					// route single to multiple
					while (true)
					{
						var r = k.ReadByte();

						if (r < 0)
							break;

						k.Others.WriteByte((byte)r);
					}
				}
			);
		}

		public static Action StartRouter(Type t, Action<RemoteEndpointIdentity> Handler)
		{
			// .net only touter
			// java router - should use only BCL classes and Obfuscate(feature = "script")
			// Nonoba router
			// ActionScript peer router/host

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("will start router: " + t.FullName);
			Console.ForegroundColor = ConsoleColor.Gray;

			var Clients = new List<RemoteEndpointIdentity>();

			var thread = DefaultPort.ToListener(
				s =>
				{
					Console.WriteLine("router: client connected");

					// we need a lock here and everywhere
					// java needs to support ThreadMonitor.Enter
					var Identity = new RemoteEndpointIdentity 
					{
						Index = Clients.Count + 1,
						WriteByte = s.WriteByte, 
						ReadByte = s.ReadByte
					};
					
					Identity.Others = new RemoteEndpointIdentity();
					Identity.Others.WriteByte =
						k =>
						{
							foreach (var r in Clients)
								if (r != Identity)
									r.WriteByte(k);
						};

					Clients.Add(Identity);

					Handler(Identity);
				}
			);
			

			return delegate
			{
				// we ought to kill the active connections and clients too
				thread.Abort();
			};
		}

		public const int DefaultPort = 33333;

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

		public static void EmitVersionInformation(ILGenerator il)
		{
			// kind of funny as this assembly will have parts where the IL will be compiled to target languages like actionscript
			// and also this assemlby contains code to build some of the IL which might end up being retranslated to that language

			Func<string> _GetVersionInformation = GetVersionInformation;

			var _a = il.DeclareLocal(typeof(string));

			il.EmitCall(OpCodes.Call, _GetVersionInformation.Method, null);
			il.Emit(OpCodes.Stloc, _a);
			il.EmitWriteLine(_a);
		}

	
	}
}
