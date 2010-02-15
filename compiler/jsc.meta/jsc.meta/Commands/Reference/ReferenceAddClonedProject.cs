using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;

namespace jsc.meta.Commands.Reference
{
	public class ReferenceAddClonedProject : CommandBase
	{
		public FileInfo Solution;

		public override void Invoke()
		{
			//Console.WriteLine(Solution.FullName);

			var s = new MVSSolutionFile(Solution);

			var p_old_Name = s.Projects.Last().Name;
			var p_Name = s.Projects.First().Name + s.Projects.Count.ToString("00");

			Console.WriteLine("Creating: " + p_Name);

			var p = new MVSSolutionFile.ProjectElement
			{
				Kind = s.Projects.Last().Kind,
				Name = p_Name,
				ProjectFile = s.Projects.Last().ProjectFile.Replace(p_old_Name, p_Name),
				Identifier = "{" + Guid.NewGuid().ToString().ToUpper() + "}"
			};

			var source_dir = new FileInfo(Path.Combine(Solution.Directory.FullName, s.Projects.Last().ProjectFile)).Directory;
			var source_files = source_dir.GetAllFilesByPattern("*.cs", "*.csproj");

			foreach (var item in source_files)
			{
				var old_name = item.FullName.Substring(Solution.Directory.FullName.Length + 1);
				var new_name = new FileInfo(Path.Combine(Solution.Directory.FullName, old_name.Replace(p_old_Name, p_Name)));

				Console.WriteLine(old_name + " -> " + new_name.FullName);

				new_name.Directory.Create();

				var content = File.ReadAllText(item.FullName);

				content = content.Replace(s.Projects.Last().Identifier, p.Identifier);
				content = content.Replace(p_old_Name, p_Name);

				File.WriteAllText(new_name.FullName, content);
			}

			s.Projects.Add(p);

			var sln = s.ToString();

			if (File.ReadAllText(Solution.FullName) != sln)
			{
				s.ToConsole();
				File.WriteAllText(Solution.FullName, sln);

			}
		}

	}
}
