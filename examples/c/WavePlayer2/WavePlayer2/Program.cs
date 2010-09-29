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
        static MySounds1 Sounds;

		public static void Main()
		{
			Console.WriteLine("hear the sounds!");

            Sounds = new MySounds1();

			Action dispenser = delegate
			{
				Console.WriteLine("dispenser");
                Sounds.dispenser.PlaySync();
			};

			Action jscisawesome = delegate
			{
				Console.WriteLine("jscisawesome");
                Sounds.jscisawesome.PlaySync();
			};



			Action doorclose = delegate
			{
				Console.WriteLine("doorclose");
                Sounds.doorclose.PlaySync();
			};
			
		
			dispenser();
			jscisawesome();
			doorclose();


		}
	}


}
