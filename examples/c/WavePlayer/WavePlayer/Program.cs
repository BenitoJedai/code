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

		public class PropertyToConsoleTag
		{
			public ColoredText Name;
			public ColoredText Value;

		}

		public static void ToConsole(this PropertyToConsoleTag e)
		{
			e.Name.ToConsole();

			Console.Write("    ");

			e.Value.ToConsole();
		}
		public static PropertyToConsoleTag PropertyToConsole(this string name, string value)
		{
			return new PropertyToConsoleTag
			{
				Name = new ColoredText { Color = ConsoleColor.Green, Text = name },
				Value = new ColoredText { Color = ConsoleColor.Yellow, Text = value }
			};

		}

	}


	public static class Program
	{

		public static void logo(string text)
		{
			Console.WriteLine("logo here");
			Console.WriteLine(text);
		}

		public static void ShowProperties(MyContent.WriteToArguments a)
		{
			a.Name.PropertyToConsole(a.Value).ToConsole();
		}


		public static void Main()
		{
			Console.WriteLine("other projects: ");
			Console.WriteLine("# http://naudio.codeplex.com/sourcecontrol/changeset/view/28884?projectName=naudio#");

			Console.WriteLine();

			var MyContent = new MyContent();


			MyContent.WriteTo((sender, args) => ShowProperties((MyContent.WriteToArguments)args));

			Console.WriteLine();
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
