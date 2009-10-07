using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Threading;
using System.IO;
using WavePlayer.WaveLibrary;
using System.Media;

namespace WavePlayer
{
	[Script]
	public class ColoredText
	{
		public ConsoleColor Color;
		public string Text;
	}

	[Script]
	public static class ColoredTextExtensions
	{
		public static void ToConsole(this ColoredText e)
		{
			Console.ForegroundColor = e.Color;
			Console.WriteLine(e.Text);
		}
	}


	[Script]
	public unsafe class NativeClass1
	{

		[Script(NoDecoration = true)]
		public static int main()
		{
			// you really should not use headphones with PC speakers

			Console.ForegroundColor = ConsoleColor.Yellow;


			Console.WriteLine("got music?");


			for (int i = 40; i < 140; i += 30)
			{
				Console.Write("Frequency: ");
				Console.Write(i);

				WaveExampleType.ExampleSquareWave.ToSoundPlayer(80 + i).PlaySync();
				Console.WriteLine();
			}

			return 0;
		}


	}

	class Program
	{
		static void Main(string[] args)
		{
			NativeClass1.main();
		}
	}
}
