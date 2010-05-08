using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library;

namespace ScriptCoreLib.Ultra.Studio
{
	public static class SolutionFileExtensions
	{
		public static void Region(this SolutionFile File, Action y)
		{
			File.Write(new SolutionFileWriteArguments.BeginRegion());

			y();

			File.Write(new SolutionFileWriteArguments.EndRegion());
		}

		public static StringBuilder ToSolutionFile(this jsc.meta.Library.MVSSolutionFile.ProjectElement[] Projects)
		{
			var w = new StringBuilder();

			w.AppendLine();
			w.AppendLine("Microsoft Visual Studio Solution File, Format Version 11.00");
			w.AppendLine("# Visual Studio 2010");

			foreach (var item in Projects)
			{
				w.AppendLine("Project(\"" + item.Kind + "\") = \"" + item.Name + "\", \"" + item.ProjectFile + "\", \"" + item.Identifier + "\"");
				w.AppendLine("EndProject");
			}

			w.AppendLine("Global");

			w.AppendLine("	GlobalSection(SolutionConfigurationPlatforms) = preSolution");
			w.AppendLine("		Debug|Any CPU = Debug|Any CPU");
			w.AppendLine("		Release|Any CPU = Release|Any CPU");
			w.AppendLine("	EndGlobalSection");
			w.AppendLine("	GlobalSection(ProjectConfigurationPlatforms) = postSolution");

			foreach (var item in Projects)
			{
				w.AppendLine("		" + item.Identifier + ".Debug|Any CPU.ActiveCfg = Debug|Any CPU");
				w.AppendLine("		" + item.Identifier + ".Debug|Any CPU.Build.0 = Debug|Any CPU");
				w.AppendLine("		" + item.Identifier + ".Release|Any CPU.ActiveCfg = Release|Any CPU");
				w.AppendLine("		" + item.Identifier + ".Release|Any CPU.Build.0 = Release|Any CPU");
			}

			w.AppendLine("	EndGlobalSection");
			w.AppendLine("	GlobalSection(SolutionProperties) = preSolution");
			w.AppendLine("		HideSolutionNode = FALSE");
			w.AppendLine("	EndGlobalSection");

			w.AppendLine("EndGlobal");
			return w;
		}

	}
}
