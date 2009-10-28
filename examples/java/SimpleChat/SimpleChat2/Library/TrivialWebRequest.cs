using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace SimpleChat.Library
{
	public class TrivialWebRequest
	{
		// contracts/statements:
		// this class can be merged to any other assembly
		// this class can be used on JVM

		public Uri Target;
		public string Referer;
		public int Port;

		public bool ContentExpected;
		public string Content;

		public void Invoke()
		{
			var uri = this.Target;
			var t = new TcpClient();

			t.Connect(uri.Host, this.Port);

			//var w = new StreamWriter(t.GetStream());

			var w = new StringBuilder();

			w.AppendLine("GET" + " " + uri.PathAndQuery + " HTTP/1.1");
			w.AppendLine("Host: " + uri.Host);
			w.AppendLine("Connection: Close");

			// http://www.botsvsbrowsers.com/
			w.Append(@"User-Agent: jsc
Referer: " + this.Referer + @"
Accept:  */*
Accept-Encoding:  gzip,deflate
Accept-Language:  et-EE,et;q=0.8,en-US;q=0.6,en;q=0.4
Accept-Charset:  windows-1257,utf-8;q=0.7,*;q=0.3
");
			w.AppendLine();

			var data = Encoding.UTF8.GetBytes(w.ToString());

			t.GetStream().Write(data, 0, data.Length);

			if (ContentExpected)
			{
				var hr = new HeaderReader();

				hr.Read(t.GetStream());

				this.Content = hr.Reader.ReadToEnd();
			}

			// it will take up to a minute to show up
			t.Close();
		}
	}

}
