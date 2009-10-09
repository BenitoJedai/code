using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using System.Threading;
using System.IO;
using WavePlayer.WaveLibrary;
using System.Media;
using WavePlayer.Library;

namespace WavePlayer
{
	public class ColoredText
	{
		public ConsoleColor Color;
		public string Text;
	}

	public static class ColoredTextExtensions
	{
		public static void ToConsole(this ColoredText e)
		{
			Console.ForegroundColor = e.Color;
			Console.WriteLine(e.Text);
		}
	}


	public class Program
	{

		public static void logo(string text)
		{
			Console.WriteLine("logo here");
			Console.WriteLine(text);
		}


		public static void Main()
		{
			Console.WriteLine("other projects: ");
			Console.WriteLine("# http://naudio.codeplex.com/sourcecontrol/changeset/view/28884?projectName=naudio#");

			// you really should not use headphones with PC speakers

			TestMemoryStream();


			Console.ForegroundColor = ConsoleColor.Yellow;

			Action<string> f = logo;

			f("hi");

			Console.WriteLine("got music?");



			var a = WaveExampleType.ExampleSquareWave.ToSoundPlayer(68);
			var b = WaveExampleType.ExampleSawtoothWave.ToSoundPlayer(115);
			var c = WaveExampleType.ExampleSquareWave.ToSoundPlayer(168);

			a.PlaySync();
			a.Stream.WriteTo("a.wav");


			b.PlaySync();
			c.PlaySync();

			//b.Stream.WriteTo("b.wav");
			//c.Stream.WriteTo("c.wav");

			//return 0;
		}

		private static void TestMemoryStream()
		{
			var m = new MemoryStream();

			m.WriteByte(5);
			m.WriteByte(0xfe);

			var arr = m.ToArray();

			Console.Write("memory: ");
			Console.Write((int)m.Length);
			Console.WriteLine();

			for (int i = 0; i < m.Length; i++)
			{
				Console.Write((int)arr[i]);
				Console.WriteLine();
			}
		}


	}


}
