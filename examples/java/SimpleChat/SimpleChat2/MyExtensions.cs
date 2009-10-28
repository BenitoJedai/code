using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace SimpleChat2
{
	static class MyExtensions
	{
		public static Action AsOnce(this Action e)
		{
			var b = false;

			return delegate
			{
				if (b)
					return;

				b = true;

				e();
			};
		}

		public static string GetLocalAddressByConnecting(this string Target)
		{
			var r = "";
			var u = new Uri("http://" + Target);

			try
			{
				var c = new TcpClient();

				c.Connect(u.Host, u.Port);

				r = ((IPEndPoint)c.Client.LocalEndPoint).Address.ToString();

				c.Close();
			}
			catch
			{
				Console.WriteLine("GetLocalAddressByConnecting failed for " + u.Host + ":" + u.Port);
			}

			return r;
		}

		public static void TryInvokeInBackground(this Action e)
		{
			new Thread(
				delegate()
				{
					try
					{
						e();
					}
					catch
					{
						Console.WriteLine("Oops! TryInvokeInBackground failed");
					}
				}
			)
			{
				IsBackground = true
			}.Start();

		}

		public static void WriteWebContent(this Stream s, string content, string type)
		{
			var w = new BinaryWriter(s);
			var ww = new StringBuilder();

			// default response

			ww.AppendLine("HTTP/1.0 200 OK");
			ww.AppendLine("Content-Type: " + type);
			ww.AppendLine();
			ww.Append(content);

			w.Write(Encoding.UTF8.GetBytes(ww.ToString()));
		}

		public static void WriteWebContent(this Stream s, string content)
		{
			var w = new BinaryWriter(s);
			var ww = new StringBuilder();

			// default response

			ww.AppendLine("HTTP/1.0 200 OK");
			ww.AppendLine("Content-Type: text/html; charset=utf-8");
			ww.AppendLine();
			ww.Append(content);

			w.Write(Encoding.UTF8.GetBytes(ww.ToString()));
		}

		public static void AppendTextLine(this TextBox t, string e)
		{
			t.AppendText(e + Environment.NewLine);
		}
		public static KeyValuePair[] GetParameters(this string Query)
		{
			var a = new ArrayList();

			var q = Query.Split('&');

			foreach (var item in q)
			{
				var e = item.IndexOf("=");

				if (e > 0)
				{
					a.Add(
						new KeyValuePair
						{
							Key = item.Substring(0, e),
							// decode?
							Value = item.Substring(e + 1).Replace("%20", " ")
						}
					);
				}
			}

			return (KeyValuePair[])a.ToArray(typeof(KeyValuePair));
		}

		public class KeyValuePair
		{
			public string Key;
			public string Value;
		}

		public static string[] GetArguments(this string PathAndQuery)
		{
			var Query = "";
			var Path = PathAndQuery;
			if (PathAndQuery.IndexOf('?') > 0)
			{
				Query = PathAndQuery.Substring(PathAndQuery.IndexOf('?') + 1);
				Path = PathAndQuery.Substring(0, PathAndQuery.IndexOf('?'));
			}

			var Parameters = Query.GetParameters();

			var s = new string[Parameters.Length + 1];

			s[0] = Path.Substring(1);

			for (int i = 0; i < Parameters.Length; i++)
			{
				var item = Parameters[i];

				s[i + 1] = "/" + item.Key + ":" + item.Value;
			}


			return s;
		}

		public static string Chop(this string e, string prefix)
		{
			if (e.StartsWith(prefix))
				return e.Substring(prefix.Length);

			return e;
		}

		public static void TryStart(this TcpListener l, Action h)
		{
			try
			{
				l.Start();
				h();
			}
			catch
			{

				// lets be informational
				Console.WriteLine("Oops! TryStart failed");
			}
		}

		public delegate void StreamAction(Stream s);

		public class ToThreadedTcpListenerInfo
		{
			public bool IsDisposed;

			public Thread Thread;

			public TcpListener Listener;
		}

		public static ToThreadedTcpListenerInfo ToThreadedTcpListener(this int port, StreamAction handler)
		{
			var ret = new ToThreadedTcpListenerInfo();

			var t = new Thread(
				delegate()
				{
					var r = new TcpListener(IPAddress.Loopback, port);

					ret.Listener = r;

					r.TryStart(
						delegate
						{


							while (true)
							{
								// http://stackoverflow.com/questions/365370/proper-way-to-stop-tcplistener
								// +		$exception	{"A blocking operation was interrupted by a call to WSACancelBlockingCall"}	System.Exception {System.Net.Sockets.SocketException}

								var c = r.AcceptTcpClient();

								// we wont be able to stop
								// this loop with current implementation

								var s = c.GetStream();

								new Thread(
									delegate()
									{
										handler(s);
									}
								)
								{
									IsBackground = true,
								}.Start();
							}
						}
					);
				}
			)
			{
				IsBackground = true,
			};

			t.Start();

			ret.Thread = t;

			return ret;
		}

		public static Thread ToListener(this int port, StreamAction handler)
		{
			var t = new Thread(
				delegate()
				{
					var r = new TcpListener(IPAddress.Loopback, port);

					r.TryStart(
						delegate
						{


							while (true)
							{
								// http://stackoverflow.com/questions/365370/proper-way-to-stop-tcplistener
								var c = r.AcceptTcpClient();

								// we wont be able to stop
								// this loop with current implementation

								var s = c.GetStream();

								new Thread(
									delegate()
									{
										handler(s);
									}
								)
								{
									IsBackground = true,
								}.Start();
							}
						}
					);
				}
			)
			{
				IsBackground = true,
			};

			t.Start();
			return t;
		}

		public static bool IsNumeric(this string e)
		{
			var n = "0123456789";
			var r = true;
			foreach (var i in e)
			{
				if (!n.Contains(new string(new[] { i })))
				{
					r = false;
					break;
				}
			}
			return r;
		}

		public static int ParseToInt32OrDefault(this string e, int d)
		{
			if (string.IsNullOrEmpty(e))
				return d;
			if (e.IsNumeric())
				return int.Parse(e);

			return d;
		}


		public static int[] ToInt32Array(this string t)
		{
			var a = t.Split(';');
			var n = new int[a.Length];

			for (int i = 0; i < a.Length; i++)
			{
				n[i] = a[i].ParseToInt32OrDefault(0);
			}

			return n;
		}
	}
}
