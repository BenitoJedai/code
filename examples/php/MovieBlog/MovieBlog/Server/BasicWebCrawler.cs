using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;

namespace MovieBlog.Server
{
	[Script]
	public class BasicWebCrawler
	{
		public event Action<string> HeaderReceived;
		public event Action<string> DataReceived;

		public readonly string Host;
		public readonly int Port;

		public BasicWebCrawler(string host, int port)
		{
			this.Host = host;
			this.Port = port;
		}

		public void Crawl(string path)
		{
			
			var t = new TcpClient();

			t.Connect(this.Host, this.Port);

			var w = new StreamWriter(t.GetStream());

			w.WriteLine("GET " + path + " HTTP/1.0");
			w.WriteLine("Host: " + this.Host);
			w.WriteLine("Connection: Close");
			w.WriteLine();
			w.Flush();

			var r = new StreamReader(t.GetStream());

			var ReadingHeaders = true;

			while (ReadingHeaders)
			{
				var Header = r.ReadLine();

				if (string.IsNullOrEmpty(Header))
					ReadingHeaders = false;
				else
					if (this.HeaderReceived != null)
						this.HeaderReceived(Header);
			}

			var Data = r.ReadToEnd();

			t.Close();

			if (DataReceived != null)
				DataReceived(Data);
		}
	}
}
