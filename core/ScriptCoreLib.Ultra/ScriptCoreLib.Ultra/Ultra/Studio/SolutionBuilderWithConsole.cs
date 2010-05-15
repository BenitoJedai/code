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
				{ SolutionFileTextFragment.XMLAttributeName, ConsoleColor.Red },
				{ SolutionFileTextFragment.XMLAttributeValue, ConsoleColor.Blue },
				{ SolutionFileTextFragment.XMLComment, ConsoleColor.Green},
				{ SolutionFileTextFragment.XMLKeyword, ConsoleColor.Blue},
				{ SolutionFileTextFragment.Type, ConsoleColor.Yellow },

			};

			var zip = new ZIPFile();

			that.WriteTo(
				SolutionFile =>
				{
					Console.BackgroundColor = ConsoleColor.Blue;
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine(" " + SolutionFile.Name + " ");

					//if (SolutionFile.WriteHistory.Count > 1)
					foreach (var item in SolutionFile.WriteHistory)
					{
						if (item.Fragment == SolutionFileTextFragment.Indent)
							Console.BackgroundColor = ConsoleColor.DarkGray;
						else if (item.Fragment == SolutionFileTextFragment.XMLText)
							Console.BackgroundColor = ConsoleColor.DarkCyan;
						else
							Console.BackgroundColor = ConsoleColor.Black;

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
