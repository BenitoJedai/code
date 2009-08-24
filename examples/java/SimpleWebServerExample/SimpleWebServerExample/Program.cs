using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SimpleWebServerExample.Library;
using System.IO;

namespace SimpleWebServerExample
{
	class Program
	{
		public static void Main(string[] args)
		{
			// Notes:
			// 1. All referenced assemblies shall
			//    define [assembly:Obfuscation(feature = "script")]
			// 2. Turn off "optimize code" option in release build
			// 3. All used .net APIs must be defined by ScriptCoreLibJava
			// 4. Generics are not supported.
			// 5. Check post build event
			// 6. Build in releas build configuration for java version

			Console.WriteLine("This console application can run at .net and java virtual machine!");

			var port = 18080;

			Console.WriteLine("http://127.0.0.1:" + port);


			port.ToListener(
				s =>
				{
					var request = new byte[0x1000];
					var requestc = s.Read(request, 0, request.Length);

					// StreamReader would be nice huh
					// ScriptCoreLibJava does not have it yet

					Console.WriteLine("client request " + requestc);

					// if we get the file name wrong we get a bad error message to stdout
					var data = File.ReadAllBytes("HTMLPage1.htm");
					Console.WriteLine("data length " + data.Length);

					var m = new MemoryStream();

					m.WriteLineASCII("HTTP/1.1 200 OK");
					m.WriteLineASCII("Content-Type:	text/html; charset=utf-8");
					m.WriteLineASCII("Content-Length: " + data.Length);
					m.WriteLineASCII("Connection: close");


					m.WriteLineASCII("");
					m.WriteLineASCII("");
					Console.WriteLine("headers written");

					m.WriteBytes(data);
					Console.WriteLine("data written");

					var Text = Encoding.ASCII.GetString(m.ToArray());
					m.WriteTo(s);
					Console.WriteLine("flush");

					s.Flush();
					s.Close();
				}
			);

			Console.WriteLine("press enter to exit!");
			Console.ReadLine();
		}
	}
}
