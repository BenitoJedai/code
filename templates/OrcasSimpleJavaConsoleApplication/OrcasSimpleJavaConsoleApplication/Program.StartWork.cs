using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OrcasSimpleJavaConsoleApplication.Library;

namespace OrcasSimpleJavaConsoleApplication
{
	partial class Program
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

							100.Sleep();

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
	}
}
