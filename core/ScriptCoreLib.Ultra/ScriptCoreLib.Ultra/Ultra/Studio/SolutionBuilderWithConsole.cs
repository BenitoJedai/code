using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Archive.ZIP;
using System.IO;

namespace ScriptCoreLib.Ultra.Studio
{
	public static class SolutionBuilderWithConsole
	{
		// to be used only on .net

		public static void WriteToConsole(this SolutionBuilder that)
		{
			var Lookup = new Dictionary<SolutionFileTextFragment, ConsoleColor>
			{
				{ SolutionFileTextFragment.Comment, ConsoleColor.Green },
				{ SolutionFileTextFragment.Keyword, ConsoleColor.Cyan },

				{ SolutionFileTextFragment.None, ConsoleColor.Gray },

				{ SolutionFileTextFragment.String, ConsoleColor.Red },
				{ SolutionFileTextFragment.Type, ConsoleColor.Yellow },

			};

			var zip = new ZIPFile();

			that.WriteTo(
				SolutionFile =>
				{
					Console.WriteLine(SolutionFile.Name);

					//if (SolutionFile.WriteHistory.Count > 1)
					foreach (var item in SolutionFile.WriteHistory)
					{
						if (Lookup.ContainsKey(item.Fragment))
							Console.ForegroundColor = Lookup[item.Fragment];
						else
							Console.ForegroundColor = ConsoleColor.Gray;
						Console.Write(item.Text);

					}

					Console.WriteLine();

					zip.Add(SolutionFile.Name, SolutionFile.Content);
				}
			);

			File.WriteAllBytes(new FileInfo(that.Name).FullName + ".zip", zip.ToBytes());
		}
	}
}
