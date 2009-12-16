using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleChat2.Network;
using System.Net;
using System.Threading;

namespace SimpleChat2.ClientProvider
{
	public static class ClientProviderExtensions
	{
		public delegate void SendToComplete(string e);

		public static void SendTo(this IDefaultRequestPath e, Uri server, SendToComplete done)
		{
			object x = e;
			//Console.WriteLine("SendTo");
			var t = x.GetType();
			//Console.WriteLine("GetType: " + t.FullName);
			var f = t.GetFields();
			//Console.WriteLine("GetFields: " + f.Length);

			var w = new StringBuilder();

			w.Append(server.ToString());

			var DefaultRequestPath = e.DefaultRequestPath;
			if (DefaultRequestPath.StartsWith("/"))
				DefaultRequestPath = DefaultRequestPath.Substring(1);

			w.Append(DefaultRequestPath);
			w.Append("?");

			var i = 0;
			foreach (var k in f)
			{
				var v = (string)k.GetValue(x);

				if (v != null)
				{
					if (i > 0)
						w.Append("&");

					w.Append(k.Name);
					w.Append("=");


					w.Append(v.Replace(" ", "+"));

					i++;
				}

				//Console.WriteLine(k.Name);
			}

			new Uri(w.ToString()).SendTo(done);
		}

		public static void SendTo(this Uri e, SendToComplete done)
		{
			var content = new WebClient().DownloadString(e);

			if (done != null)
				done(content);
		}

		public static void ThreadedSendTo(this IDefaultRequestPath e, Uri server, SendToComplete done)
		{
			new Thread(
				delegate()
				{
					e.SendTo(server, done);
				}
			)
			{
				IsBackground = true
			}.Start();
		}

		public static void ThreadedSendTo(this IDefaultRequestPath e, Uri server)
		{
			e.ThreadedSendTo(server, null);
		}
	}
}
