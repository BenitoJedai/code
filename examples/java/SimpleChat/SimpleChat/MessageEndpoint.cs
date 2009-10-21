using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using SimpleChat.Library;
using System.Net;
using ScriptCoreLib.JSON;

namespace SimpleChat
{
	public delegate void MessageEndpointAction(MessageEndpoint e);

	public class MessageEndpoint
	{
		public string Name;

		public int Port;

		public string Nickname
		{
			get
			{
				var a = Name.IndexOf("@");

				if (a < 0)
					return Name;


				return Name.Substring(0, a);
			}
		}

		public string Host
		{
			get
			{
				var a = Name.IndexOf("@");

				if (a < 0)
					return "0.0.0.0";

				var host = Name.Substring(a + 1);

				return host;
			}
		}

		public override string ToString()
		{
			return this.Name + ":" + this.Port;
		}
	}

	public static class MessageEndpointExtensions
	{
		public static MessageEndpoint[] ToMessageFromArray(this string e)
		{
			var a = new ArrayList();
			var q = e.Split(';');

			foreach (var i in q)
			{
				var n = i.Split(':');

				var _Name = n[0];

				if (_Name != "")
				{
					var _Port = 80;

					if (n.Length == 2)
					{
						_Port = n[1].ParseToInt32OrDefault(_Port);
					}


					a.Add(
						new MessageEndpoint
						{
							Name = _Name,
							Port = _Port
						}
					);
				}

			}

			return (MessageEndpoint[])a.ToArray(typeof(MessageEndpoint));
		}

		public static MessageEndpoint[] Concat(this MessageEndpoint[] f, MessageEndpoint v)
		{
			if (f == null)
				return new[] { v };

			var a = new MessageEndpoint[f.Length + 1];

			Array.Copy(f, a, f.Length);

			a[f.Length] = v;

			return a;
		}


		public static WebServer[] ToWebServers(this MessageEndpoint[] f)
		{
			// linq group by would be awesome!
			var a = new ArrayList();

			foreach (var kf in f)
			{
				var s = default(WebServer);

				#region get server
				foreach (WebServer item in a)
				{
					if (item.Port == kf.Port)
					{
						s = item;
						break;
					}
				}
				#endregion

				if (s == null)
					s = new WebServer { Port = kf.Port };

				s.Locals = s.Locals.Concat(kf);

				a.Add(s);
			}

			return (WebServer[])a.ToArray(typeof(WebServer));
		}

		public static string ToJSON(this MessageEndpoint[] e)
		{
			return ScriptCoreLib.JSON.JSONDocument.ToString(
				delegate
				{
					var i = -1;

					


					return delegate
					{
						i++;

						if (i < e.Length)
						{
							// we are in the business
							var k = e[i];

							return new[] { k.Nickname, k.Host + ":" + k.Port };
						}

						return null;

					};

				}	
				
			);
		}

	}
}
