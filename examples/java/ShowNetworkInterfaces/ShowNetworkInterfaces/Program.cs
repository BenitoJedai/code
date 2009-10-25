using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ShowNetworkInterfaces
{
	public partial class Program
	{


		public static void Main(string[] args)
		{
			var t = new TcpClient();

			t.Connect("www.google.ee", 80);
			var LocalEndPoint = t.Client.LocalEndPoint;

			Console.WriteLine("LocalEndPoint: " + LocalEndPoint);
			// http://www.rgagnon.com/javadetails/java-0390.html
			var ii = NetworkInterface.GetAllNetworkInterfaces();

			
			foreach (var j in ii)
			{


				Console.WriteLine(j.Name + " - " + j.Description);

				
			}


		}


	}
}
