using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SimpleConsoleMaze.Library;
using System.Runtime.InteropServices;
using ScriptCoreLib.Shared.Maze;
using ScriptCoreLib.Archive.ZIP;
using System.IO;

namespace SimpleConsoleMaze
{
	public partial class Program
	{


		public static void Main(string[] args)
		{

			var _w = 24;
			var _h = 16;


			var str = GetMaze(_w, _h);

			Console.WriteLine(str.ToString());

			var zip = new ZIPFile();

			zip.Add("24x16/maze.txt", str.ToString());
			zip.Add("16x12/maze.txt", GetMaze(16, 12).ToString());
			zip.Add("12x8/maze.txt", GetMaze(12, 8).ToString());

			var file = new FileInfo("maze.zip");

			Console.WriteLine("writing to '" + file.FullName + "'...");

			var zzm = new MemoryStream();
			using (var w = new BinaryWriter(zzm))
			{
				zip.WriteTo(w);
			}

			File.WriteAllBytes(file.FullName, zzm.ToArray());
		}

		private static StringBuilder GetMaze(int _w, int _h)
		{

			var str = new StringBuilder();

			str.AppendLine("Here is a maze for you: " + _w + "x" + _h);
			str.AppendLine();


			var maze = new MazeGenerator(_w, _h, null);



			var w = new BlockMaze(maze);

			for (var y = 0; y < w.Height; y++)
			{
				str.Append(new string(' ', 8));

				for (var x = 0; x < w.Width; x++)
				{
					var v = w.Walls[x][y];

					if (v)
					{
						str.Append("©");
					}
					else
					{
						str.Append(" ");
					}
				}

				str.AppendLine();
			}
			return str;
		}


	}
}
