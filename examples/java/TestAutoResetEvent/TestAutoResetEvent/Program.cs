using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TestAutoResetEvent.Library;
using System.Runtime.InteropServices;

namespace TestAutoResetEvent
{
	public partial class Program
	{


		public static void Main(string[] args)
		{
			//var r = new AutoResetEvent(false);
			//var r = new ManualResetEvent(false);
			var r = new EventWaitHandle(false, EventResetMode.ManualReset);

			new Thread(
				delegate()
				{
					Console.WriteLine("working...");

					Thread.Sleep(5000);

					Console.WriteLine("signaling...");

					r.Set();
				}
			).Start();

			Console.WriteLine("waiting...");
			r.WaitOne();
			Console.WriteLine("done!");

		}


	}
}
