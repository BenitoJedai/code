using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PortCloner.Library;
using System.Runtime.InteropServices;
using ScriptCoreLib.Reflection.Options;
using System.IO;
using System.Net.Sockets;
// using zproxy;

namespace PortCloner
{
	sealed class Clone
	{
		public delegate void CloneAction(Clone e);

		public int ServerPort = 80;

		public string TargetHost =
			"localhost";
		//"example.com";
		public int TargetPort =
			62864;
		//80;



		public CloneAction AfterInvoke;

		public void Invoke()
		{
			if (AfterInvoke != null)
				AfterInvoke(this);
		}
	}

	public partial class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine(@"
usage: 
	PortCloner.exe Clone /TargetHost:localhost /TargetPort:80
	PortClonerMetaScript.jar.bat Clone /TargetHost:localhost /TargetPort:80
");

			args.AsParametersTo(
				new Clone
				{
					AfterInvoke = Invoke
				}.Invoke
			);

		}

		private static void Invoke(Clone c)
		{
			Console.WriteLine("listening to port " + c.ServerPort);

			var counter__ = 0;
			var single = new object();

			c.ServerPort.ToListener(
				s =>
				{
					var counter = 0;
					lock (single)
					{
						counter__++;
						counter = counter__;

					}


					Console.WriteLine(counter + " connected");


					var target = new TcpClient();

					target.Connect(c.TargetHost, c.TargetPort);

					var target_s = target.GetStream();

					var file_RequestStream = new MemoryStream();
					var file_ResponseStream = new MemoryStream();


					#region reader
					var reader = new Action(
						delegate
						{
							var sleep = 10;

							try
							{
								while (true)
								{
									s.ReadTimeout = 500;

									var x = s.ReadByte();

									if (x < 0)
									{
										sleep--;
										if (sleep < 0)
										{
											Console.WriteLine(counter + " reader exits");
											break;
										}

										Thread.Sleep(300);
										continue;
									}

									file_RequestStream.WriteByte((byte)x);
									target_s.WriteByte((byte)x);
								}
							}
							catch
							{
								Console.WriteLine(counter + " reader failed");
							}
						}
					).TryInvokeInBackground();
					#endregion
					reader.Name = "reader." + counter;

					#region writer
					var writer = new Action(
						delegate
						{
							var sleep = 10;

							try
							{
								while (true)
								{
									var x = target_s.ReadByte();

									if (x < 0)
									{
										sleep--;
										if (sleep < 0)
										{
											Console.WriteLine(counter + " writer exits");
											break;
										}

										Thread.Sleep(300);
										continue;
									}


									s.WriteByte((byte)x);
									file_ResponseStream.WriteByte((byte)x);
								}

							}
							catch
							{
								Console.WriteLine(counter + " writer failed");
							}
						}
					).TryInvokeInBackground();
					#endregion
					writer.Name = "writer." + counter;
					Console.WriteLine(counter + " joining reader...");
					reader.Join();
					Console.WriteLine(counter + " joining writer...");
					writer.Join();


					Console.WriteLine(counter + " saving...");

					File.WriteAllBytes(counter + ".in.txt", file_RequestStream.ToArray());
					File.WriteAllBytes(counter + ".out.txt", file_ResponseStream.ToArray());

					Console.WriteLine(counter + " saved!");

					s.Close();
					target.Close();

				}
			).Join();
		}
	}
}
