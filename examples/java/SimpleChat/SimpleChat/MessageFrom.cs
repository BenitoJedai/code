using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using SimpleChat.Library;

namespace SimpleChat
{
	public class MessageFrom
	{
		public string Name;

		public int Port;

	}

	public static class MessageFromExtensions
	{
		public static MessageFrom[] ToMessageFromArray(this string e)
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
						new MessageFrom
						{
							Name = _Name,
							Port = _Port
						}
					);
				}

			}

			return (MessageFrom[])a.ToArray(typeof(MessageFrom));
		}

		public static MessageFrom[] Concat(this MessageFrom[] f, MessageFrom v)
		{
			if (f == null)
				return new[] { v };

			var a = new MessageFrom[f.Length + 1];

			Array.Copy(f, a, f.Length);

			a[f.Length] = v;

			return a;
		}


		public static WebServer[] ToWebServers(this MessageFrom[] f)
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
	}
}
