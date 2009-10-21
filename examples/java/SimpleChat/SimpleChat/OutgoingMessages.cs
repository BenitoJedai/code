using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using SimpleChat.Library;
using System.Net.Sockets;

namespace SimpleChat
{
	public partial class OutgoingMessages : Component
	{
		public OutgoingMessages()
		{
			InitializeComponent();
		}

		public OutgoingMessages(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		public string LocalConfigurationFile { get; set; }

		public void SendCommand(MessageEndpoint[] Targets, object Command)
		{
			// we defenetly need a peer2peer name to ip discovery
			// component

			foreach (var item in Targets)
			{
				// can we find this name?
				// if not we could fire an event
				// that this target was not found

				SendCommand(item, Command);
			}

		}

		public event MessageEndpointAction NotFound;

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

		public void SendCommand(MessageEndpoint Target, object Command)
		{
			Action TrySend =
				delegate
				{
					var Host = Target.Host;

					if (string.IsNullOrEmpty( Host ))
					{
						// name does not embed host name
						// do we have a discovery service?

						RaiseNotFound(Target);

						return;
					}


					new TrivialWebRequest
					{
						Port = Target.Port,
						Referer = "http://example.com",
						Target = new Uri("http://" + Host + ":" + Target.Port + "/" + CommandToString(Command))
					}.Invoke();
				};

			TrySend.TryInvokeInBackground();
		}

		public void RaiseNotFound(MessageEndpoint Target)
		{
			if (NotFound != null)
				NotFound(Target);
		}
	}
}
