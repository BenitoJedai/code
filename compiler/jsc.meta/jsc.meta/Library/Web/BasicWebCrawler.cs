using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Diagnostics;

namespace jsc.meta.Library.Web
{
	internal class BasicWebCrawler
	{
		// All HTTP/1.1 applications MUST be able to receive and decode the "chunked" transfer-coding, and MUST ignore chunk-extension extensions they do not understand. 
		// http://www.httpwatch.com/httpgallery/chunked/
		// http://www.w3.org/Protocols/rfc2616/rfc2616-sec3.html#sec3.6.1

		public event Action<string> HeaderReceived;

		public event Action<StreamWriter> HeaderWriter;
		public event Action<StreamWriter> StreamWriter;

		public event Action AllHeadersReceived;
		public event Action AllHeadersSent;

		public event Action<byte[]> BinaryDataReceived;
		public event Action<byte[], TimeSpan> BinaryDataReceivedWithTimeSpan;

		public event Action<string> DataReceived;
		public event Action<string, TimeSpan> DataReceivedWithTimeSpan;

		public string Host { get; set; }
		public int Port { get; set; }

		/// <summary>
		/// http://www.w3.org/Protocols/HTTP/1.0/draft-ietf-http-spec.html#Method
		/// </summary>
		public string Method;

		/// <summary>
		/// http://www.coralcdn.org/
		/// </summary>
		public bool CoralEnabled;

		public byte[] Buffer;

		public event Action<Stream> StreamReader;

		public BasicWebCrawler(string host, int port)
		{
			this.Host = host;
			this.Port = port;

			this.Method = "GET";
		}

		public class RangeHeader
		{
			public int From;
			public int To;

			public override string ToString()
			{
				// http://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.35

				return "Range: bytes=" + From + "-" + To;
			}

			public int MaxCount = -1;

			public int Count
			{
				get
				{
					return To - From + 1;
				}
				set
				{
					To = From + value - 1;

					if (MaxCount >= 0)
					{
						if (To > MaxCount - 1)
						{
							To = MaxCount - 1;
						}
					}
				}
			}
		}

		public RangeHeader Range;

		public event Action<string> Diagnostics;

		public void Crawl(string path)
		{

			var t = new TcpClient();

			var Host = this.Host;

			const string CoralSuffix = ".nyud.net";

			if (this.CoralEnabled)
				if (!Host.EndsWith(CoralSuffix))
				{
					Host += CoralSuffix;
				}

			t.Connect(Host, this.Port);

			var w = new StreamWriter(t.GetStream());

			w.WriteLine(this.Method + " " + path + " HTTP/1.1");
			w.WriteLine("Host: " + Host);
			w.WriteLine("Connection: Close");

			this.Range.Apply(k => w.WriteLine(k.ToString()));

			if (this.HeaderWriter != null)
				this.HeaderWriter(w);

			w.WriteLine();
			w.Flush();

			if (this.AllHeadersSent != null)
				this.AllHeadersSent();

			if (this.StreamWriter != null)
			{
				this.StreamWriter(w);
				w.Flush();
			}

			if (DiscardResponse)
			{
				t.Close();
				return;
			}

			// http://bytes.com/groups/net-c/266557-problem-accessing-streamreaders-basestream

			var r = new SmartStreamReader(t.GetStream());

			var TransferEncoding = default(string);

			this.ForHeaderReceived("transfer-encoding:", value => TransferEncoding = value);

			#region ReadingHeaders
			var ReadingHeaders = true;

			while (ReadingHeaders)
			{
				var Header = r.ReadLine();

				//Console.WriteLine("-- Header:" + Header);

				if (string.IsNullOrEmpty(Header))
					ReadingHeaders = false;
				else
					if (this.HeaderReceived != null)
						this.HeaderReceived(Header);
			}
			#endregion

			if (Diagnostics != null)
			{
				Diagnostics("SmartStreamReader AllHeadersReceived");
			}

			if (AllHeadersReceived != null)
				AllHeadersReceived();

			string Data = null;
			var BufferSize = -1;
			var s = new Stopwatch();

			if (!DiscardData)
			{
				s.Start();
				if (this.StreamReader != null)
				{
					this.StreamReader(r);
				}
				else if (this.Buffer != null)
				{
					//throw new NotSupportedException("");

					BufferSize = r.Read(this.Buffer, 0, this.Buffer.Length);

				}
				else
				{
					if (string.IsNullOrEmpty(TransferEncoding))
					{
						Data = r.ReadToEnd();
					}
					else
					{
						if (TransferEncoding == "chunked")
						{
							var ww = new StringBuilder();
							var ChunkedLengthString = r.ReadLine();
							var ChunkedLength = int.Parse(ChunkedLengthString, System.Globalization.NumberStyles.HexNumber);

							while (ChunkedLength > 0)
							{
								r.ReadBlockTo(ChunkedLength, ww);
								var Separator = r.ReadLine();
								ChunkedLengthString = r.ReadLine();

								if (ChunkedLengthString == "")
									ChunkedLength = 0;
								else
									ChunkedLength = int.Parse(ChunkedLengthString, System.Globalization.NumberStyles.HexNumber);
							}

							Data = ww.ToString();
						}
					}

				}
				s.Stop();
			}

			t.Close();

			if (!DiscardData)
			{
				if (DataReceived != null)
					DataReceived(Data);

				if (DataReceivedWithTimeSpan != null)
					DataReceivedWithTimeSpan(Data, s.Elapsed);
			}

			if (BufferSize > 0)
			{
				// http://drupal.org/node/195642

				var u = new byte[BufferSize];

				for (int i = 0; i < BufferSize; i++)
				{
					u[i] = this.Buffer[i];
				}

				if (this.BinaryDataReceived != null)
					this.BinaryDataReceived(u);

				if (this.BinaryDataReceivedWithTimeSpan != null)
					this.BinaryDataReceivedWithTimeSpan(u, s.Elapsed);
			}
		}

		public bool DiscardResponse;
		public bool DiscardData;

	
		public void ForHeaderReceived(string header, Action<string> value)
		{
			var x = header.ToLower();

			this.HeaderReceived +=
				newheader =>
				{
					var h = newheader.ToLower();
					var z = h.StartsWith(x);

					//Console.WriteLine(new { h, x, z });

					if (z)
					{
						var v = newheader.Substring(x.Length).Trim();

						//Console.WriteLine("HeaderReceived: " + Location);

						value(v);
					}
				};
		}


		public event Action<string> LocationReceived
		{
			add
			{
				ForHeaderReceived("Location:", value);

			}
			remove
			{
				throw new NotSupportedException("LocationReceived.remove");
			}
		}



		public event Action<string> ContentLengthReceived
		{
			add
			{
				if (value == null)
					return;

				const string ContentLength = "Content-Length:";

				var x = ContentLength.ToLower();

				this.HeaderReceived +=
					header =>
					{
						var h = header.ToLower();
						var z = h.StartsWith(x);

						//Console.WriteLine(new { h, x, z });

						if (z)
						{
							var Location = header.Substring(ContentLength.Length).Trim();

							//Console.WriteLine("HeaderReceived: " + Location);

							value(Location);
						}
					};
			}
			remove
			{
				throw new NotSupportedException("ContentLength.remove");
			}
		}

		public void Crawl(Uri source)
		{
			this.Host = source.Host;
			this.Crawl(source.PathAndQuery);
		}

		public static byte[] ToBytes(Uri source)
		{
			var c = new BasicWebCrawler(source.Host, 80);

			c.ContentLengthReceived += y => c.Buffer = new byte[int.Parse(y)];

			c.Crawl(source.PathAndQuery);

			return c.Buffer;
		}
	}

}
