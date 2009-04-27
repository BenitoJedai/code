using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Threading;
using System.IO;

namespace OrcasNativeApplication
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
	public class MusicalTabs
	{
		public MusicalNotes Notes;
		public readonly List<Tab> Tabs = new List<Tab>();

		[Script]
		public class Tab
		{
			public MusicalNotes.Notes Note;
			public string Stream;
			public int OctaveOffset;
		}

		public string this[MusicalNotes.Notes e, int octaveoffset]
		{
			set
			{
				Tabs.Add(
					new Tab
					{
						Note = e,
						Stream = value,
						OctaveOffset = octaveoffset
					}
				);
			}
		}

		public void Play()
		{
			var firsttab = this.Tabs[0];
			var length = firsttab.Stream.Length;

			for (int i = 0; i < length; i++)
			{
				for (int x = 0; x < this.Tabs.Count; x++)
				{
					var t = this.Tabs[x];
					var n = t.Stream[i];

					Play(t.Note, n, t.OctaveOffset);
				}
			}
		}

		public void Play(MusicalNotes.Notes note, char octave, int octaveoffset)
		{
			var n = 0;

			if (octave == '0') n = 0;
			else if (octave == '1') n = 1;
			else if (octave == '2') n = 2;
			else if (octave == '3') n = 3;
			else if (octave == '4') n = 4;
			else if (octave == '5') n = 5;
			else if (octave == '6') n = 6;
			else if (octave == '7') n = 7;
			else return;

			this.Notes.Beep(note, n + octaveoffset);
		}
	}

	[Script]
	public class MusicalNotes
	{
		internal readonly List<int> Frequencies;

		public enum Notes
		{
			C,
			Cs,
			D,
			Ds,
			E,
			F,
			Fs,
			G,
			Gs,
			A,
			As,
			B
		}

		// http://fly.cc.fer.hr/GDM/articles/sndmus/speaker1.html
		/*
	 Octave 0    1    2    3    4    5    6    7
	Note
	 C     16   33   65  131  262  523 1046 2093
	 C#    17   35   69  139  277  554 1109 2217
	 D     18   37   73  147  294  587 1175 2349
	 D#    19   39   78  155  311  622 1244 2489
	 E     21   41   82  165  330  659 1328 2637
	 F     22   44   87  175  349  698 1397 2794
	 F#    23   46   92  185  370  740 1480 2960
	 G     24   49   98  196  392  784 1568 3136
	 G#    26   52  104  208  415  831 1661 3322
	 A     27   55  110  220  440  880 1760 3520
	 A#    29   58  116  233  466  932 1865 3729
	 B     31   62  123  245  494  988 1975 3951
		 */


		public MusicalNotes()
		{
			this.Frequencies = new List<int>
			{
				16,   33,   65,  131,  262,  523, 1046, 2093,
				17,   35,   69,  139,  277,  554, 1109, 2217,
				18,   37,   73,  147,  294,  587, 1175, 2349,
				19,   39,   78,  155,  311,  622, 1244, 2489,
				21,   41,   82,  165,  330,  659, 1328, 2637,
				22,   44,   87,  175,  349,  698, 1397, 2794,
				23,   46,   92,  185,  370,  740, 1480, 2960,
				24,   49,   98,  196,  392,  784, 1568, 3136,
				26,   52,  104,  208,  415,  831, 1661, 3322,
				27,   55,  110,  220,  440,  880, 1760, 3520,
				29,   58,  116,  233,  466,  932, 1865, 3729,
				31,   62,  123,  245,  494,  988, 1975, 3951,
			};
		}

		public int GetFrequency(Notes note, int octave)
		{
			return this.Frequencies[((int)note) * 8 + octave];
		}

		public void Beep(Notes note, int octave)
		{
			Console.Beep(GetFrequency(note, octave) - 16 + 37, 100);
			Thread.Sleep(100);
		}
	}

	[Script]
	public class GuitarTabs : MusicalTabs
	{
		public GuitarTabs(MusicalNotes Notes,
			string E_,
			string B,
			string G,
			string D,
			string A,
			string E)
		{

			this.Notes = Notes;

			this[MusicalNotes.Notes.E, 1] = E_;
			this[MusicalNotes.Notes.B, 0] = B;
			this[MusicalNotes.Notes.G, 0] = G;
			this[MusicalNotes.Notes.D, 0] = D;
			this[MusicalNotes.Notes.A, 0] = A;
			this[MusicalNotes.Notes.E, 0] = E;

		}
	}

	[Script]
	public unsafe class NativeClass1
	{

		[Script(NoDecoration = true)]
		public static int main()
		{
			// you really should not use headphones with PC speakers
			Console.Beep();

			var m = new MusicalNotes();

			var intro1 = new GuitarTabs(m,
			 "---------0-------]---------0-------]---------0-------]---------0-------]",
			 "-----------0-----]-----------3-----]-----3-----3---3-]-----3-----3---3-]",
			 "-------0-----0---]-------0-----0---]---2---2-----2---]---2---2-----2---]",
			 "-----2---------0-]-----0---------0-]-0---------------]-0---------------]",
			 "---2-------------]---2-------------]-----------------]-----------------]",
			 "-0---------------]-3---------------]-----------------]-----------------]"
			);

			var intro2 = new GuitarTabs(m,
				"---------0-------]---------0-------]---------0-------]---------0-------]",
				"-----3-----3---3-]-----3-----3---3-]-----0-----0---0-]-----0-----0---0-]",
				"-------0-----0---]-------0-----0---]-------2-----2---]-------2-----2---]",
				"---2-------------]---2-------------]---2-------------]---2-------------]",
				"-3---------------]-3---------------]-0---------------]-0---------------]",
				"-----------------]-----------------]-----------------]-----------------]"
			);


			// http://www.guitaretab.com/l/limp-bizkit/25879.html

			for (int i = 0; i < 100; i++)
			{
				var x = i * Math.PI / 100;
				int y = (int)(Math.Sin(x) * 400);

				Console.Beep(800 + y, 30);
			}


			intro1.Play();
			intro2.Play();


			new ColoredText
			{
				Color = ConsoleColor.Green,
				Text = "look, it is an object"
			}.ToConsole();
			// this object wont be garbage collected, so be careful for what you wish for.

			Console.ForegroundColor = ConsoleColor.Cyan;

			var pi = Math.PI;

			Console.Write(pi);
			Console.WriteLine();

			var param = 30;
			var pisin = Math.Sin(param * Math.PI / 180);

			Console.Write(pisin);
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("[NativeClass1]");
			Console.ForegroundColor = ConsoleColor.Yellow;

			File.WriteAllText("log.txt", "this is the result");

			// we are safe to use the list until our 
			// value type is not larger than the pointer
			var music = new List<int>
			{
				100,
				600,
				300,
				700,
				100,
				200,
				500,
				50,
				50,
				600
			};




			foreach (char c in "hello world. c# code has been converted to c code. and you are running it!")
			{
				Console.Beep(37 + c, 10);
				Console.Write(c);

				if (c == '.')
					Thread.Sleep(100);
				else
					Thread.Sleep(20);
			}

			Console.WriteLine();

			for (int r = 0; r < 2; r++)
			{
				for (int i = 0; i < music.Count; i++)
				{
					var x = (int)music[i];

					Console.Write(x);
					Console.Write(" ");

					Console.Beep(x, 50);
					Thread.Sleep(20);
				}
			}



			Console.WriteLine("got music?");


			for (int i = 37; i < 1200; i += 100)
			{
				Console.Beep(i, 50);
				Thread.Sleep(20);
			}


			Console.Write('j');
			Console.Write('s');
			Console.Write('c');

			Console.Beep(1200, 200);

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
