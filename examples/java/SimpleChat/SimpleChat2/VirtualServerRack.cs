using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleChat.Library;
using System.IO;

namespace SimpleChat2
{
	public class VirtualServerRack
	{

		public int[] Ports;

	

		public Action Stop;

		public delegate void CommandRequestDelegate(Stream s, string path);

		public event CommandRequestDelegate CommandRequest;

		public void Start()
		{
			Action Stop =
				delegate
				{
					this.Stop = null;
				};

			foreach (var Port in Ports)
			{
				var t = Port.ToThreadedTcpListener(
					s =>
					{
						var hr = new HeaderReader();
						var hr_Method_path = "";

						hr.Method +=
							(method, path) =>
							{
								hr_Method_path = path;
							};

						hr.Header +=
							(key, value) =>
							{
								//Console.WriteLine(key + ": " + value);
							};

						hr.Read(s);

						if (!string.IsNullOrEmpty(hr_Method_path))
						{
							Console.WriteLine(">> " + hr_Method_path);

							this.CommandRequest(s, hr_Method_path);

						}
						s.Close();
					}
				);

				Stop +=
					delegate
					{
						t.IsDisposed = true;
						t.Listener.Server.Close();
						t.Thread.Join();
					};
			}

			this.Stop = Stop;
		}
	}
}
