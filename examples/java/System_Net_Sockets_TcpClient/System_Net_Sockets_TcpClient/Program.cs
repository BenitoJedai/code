using System.Threading;
using System;

using ScriptCoreLib;
using System.Net.Sockets;
using System.IO;


namespace System_Net_Sockets_TcpClient
{

	[Script]
	public class Program
	{
		public static void Main(string[] args)
		{
			// Use Release Build to use jsc to generate java program
			// Use Debug Build to develop on .net

			foreach (var b in "127.0.0.1".GetBytesFromPort(30123, 0x40))
			{
				Console.Write(" " + b.ToString("x2"));
			}

			Console.WriteLine();
		}

	
	}


}
