using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ViaAssemblyBuilder2
{
	public class Program
	{
		public static Thread StartWork(object context, int delay, string name)
		{
			var t = new Thread(
				delegate()
				{
					Console.WriteLine("Ready for " + name + " in " + delay + "ms");

					Thread.Sleep(delay);

					lock (context)
					{
						Console.Write("Working for " + name + " ");

						for (int i = 0; i < 10; i++)
						{
							Console.Write(".");
							Thread.Sleep(100);
						}

						Console.WriteLine(" done.");
					}
				}
			)
			{
				IsBackground = true
			};

			t.Start();

			return t;
		}

		public static void Main(string[] args)
		{
			// 
			// turn off optimize code
			Console.WriteLine("This console application can run at .net and java virtual machine!");


			Console.WriteLine("running at: " + Environment.CurrentDirectory);

			var context = new object();

			// we should see work reordering t1 t3 t2 in context

			var t1 = StartWork(context, 500, "t1");
			var t2 = StartWork(context, 2000, "t2");
			var t3 = StartWork(context, 800, "t3");


			t1.Join();
			t2.Join();
			t3.Join();
		}
	}
}
