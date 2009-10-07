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
		[Script(IsNative = true)]
		public delegate void StaticAction<T>(T t);

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

			StaticAction<string> f = logo;

			f("hi");

			Console.WriteLine("got music?");



			WaveExampleType.ExampleSquareWave.PlaySound(68);
			WaveExampleType.ExampleSquareWave.PlaySound(38);
			WaveExampleType.ExampleSquareWave.PlaySound(168);

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
