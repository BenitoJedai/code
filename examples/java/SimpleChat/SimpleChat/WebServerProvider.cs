using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SimpleChat.Library;
using System.IO;

namespace SimpleChat
{
	public class WebServerProvider : WebServer
	{
		public WebServerComponent Context;

		public delegate void StringAction(string e);

		public class IncomingDataArguments
		{
			public string QueryAndPath;

			public string Text;

			public StringAction SetLogText;
		}

		public delegate void IncomingDataDelegate(WebServerProvider sender, IncomingDataArguments a);

		public event IncomingDataDelegate IncomingData;

		public void RaiseIncomingData(WebServerProvider sender, WebServerProvider.IncomingDataArguments args)
		{
			// jsc needs to take care of this on its own!
			// as we cannot call local event from an anonymous delegate it seems

			if (IncomingData != null)
				IncomingData(sender, args);
		}

		public void Shutdown()
		{
			if (InternalThread != null)
				InternalThread.Abort();
		}

		Thread InternalThread;

		public void Start()
		{
			InternalThread = this.Port.ToListener(ForkClient);
		}

		private void ForkClient(Stream s)
		{
			// http://en.wikipedia.org/wiki/List_of_HTTP_headers

			// somebody connected to us
			// this is a new thread

			var hr = new HeaderReader();

			var LogText = "";

			hr.Method +=
				(method, path) =>
				{
					Console.WriteLine(path);

					RaiseIncomingData(this,
						new IncomingDataArguments
						{
							// we should ask for a specific r
							QueryAndPath = path,
							SetLogText = k => LogText = k
						}
					);
				};

			hr.Header +=
				(key, value) =>
				{
					//Console.WriteLine(key + ": " + value);
				};

			hr.Read(s);

			var w = new BinaryWriter(s);
			var ww = new StringBuilder();

			// default response

			ww.AppendLine("HTTP/1.0 200 OK");
			ww.AppendLine("Content-Type: text/html; charset=utf-8");
			ww.AppendLine();


			foreach (var k in this.Locals)
			{
				ww.AppendLine("<div>at this location you can talk to <b>" + k.Name + "</b></div>");
			}

			// show remote users?

			ww.AppendLine("<hr />");

			ww.AppendLine("<pre>");

			// escape html entities?
			ww.AppendLine(LogText);

			ww.AppendLine("</pre>");

			w.Write(Encoding.ASCII.GetBytes(ww.ToString()));


			s.Close();
		}
	}
}

