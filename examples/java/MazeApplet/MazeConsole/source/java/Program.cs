using System.Threading;
using System;

using ScriptCoreLib;
using ScriptCoreLib.Shared.Maze;


namespace MazeConsole.source.java
{
	[Script]
	public class Program
	{
		public static string Text;

		[Script]
		class Feedback : MazeGenerator.IFeedback
		{
			public void Invoke(string e)
			{
				Console.WriteLine(e);
			}

		}
		public static void main(string[] args)
		{
			// doubleclicking on the jar will not show the console

			Console.WriteLine("maze...");

			var m = new MazeGenerator(20, 20, new Feedback());

		}
	}
}
