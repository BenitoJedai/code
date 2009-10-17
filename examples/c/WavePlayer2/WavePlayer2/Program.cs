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
			MySounds1.Default.dispenser.PlaySync();
			MySounds1.Default.dispenser.PlaySync();
			MySounds1.Default.dispenser.PlaySync();
			MySounds1.Default.doorclose.PlaySync();
			MySounds1.Default.doorclose.PlaySync();
			MySounds1.Default.doorclose.PlaySync();


		}
	}


}
