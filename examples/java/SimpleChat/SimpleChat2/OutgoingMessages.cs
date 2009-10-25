using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using SimpleChat.Library;
using System.Net.Sockets;

namespace SimpleChat2
{
	public partial class OutgoingMessages : Component
	{
		public string PathPrefix { get; set; }

		public OutgoingMessages()
		{
			InitializeComponent();
		}

		public OutgoingMessages(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

	
		public static string CommandToString(object Command)
		{
			var t = Command.GetType();
			var w = new StringBuilder();

			w.Append(t.Name + "?");
			var c = 0;
			foreach (var f in t.GetFields())
			{
				if (f.FieldType.Equals(typeof(string)))
				{
					if (c > 0)
						w.Append("&");

					c++;

					var value = (string)f.GetValue(Command);

					// URLEncode?
					// http://www.blooberry.com/indexdot/html/topics/urlencoding.htm
					// http://stackoverflow.com/questions/575440/url-encoding-using-c

					value = value.Replace(" ", "%20");

					w.Append(f.Name + "=" + value);
				}
			}

			return w.ToString();
		}

		public void SendCommand(string EndPoint, object Command)
		{
			// host:port



			Action TrySend =
				delegate
				{


					var TargetUri = "http://" + EndPoint + this.PathPrefix + "/" + CommandToString(Command);
					var Target = new Uri(TargetUri);

					new TrivialWebRequest
					{
						Port = Target.Port,
						Referer = "http://example.com",
						Target = Target
					}.Invoke();
				};

			TrySend.TryInvokeInBackground();
		}

	
	}
}
