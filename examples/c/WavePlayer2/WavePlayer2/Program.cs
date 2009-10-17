using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Media;

namespace WavePlayer2
{

	public static partial class Program
	{
		public static void Main()
		{
			Console.WriteLine("hear the sounds!");

			Action dispenser = delegate
			{
				Console.WriteLine("dispenser");
				MySounds1.Default.dispenser.PlaySync();
			};

			Action jscisawesome = delegate
			{
				Console.WriteLine("jscisawesome");
				MySounds1.Default.jscisawesome.PlaySync();
			};



			Action doorclose = delegate
			{
				Console.WriteLine("doorclose");
				MySounds1.Default.doorclose.PlaySync();
			};


			dispenser();
			jscisawesome();
			doorclose();


		}
	}


}
