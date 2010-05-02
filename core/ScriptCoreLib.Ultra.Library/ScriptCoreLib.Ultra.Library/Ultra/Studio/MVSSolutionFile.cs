using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.Ultra.Studio;

namespace jsc.meta.Library
{
	public class MVSSolutionFile
	{
		// http://msdn.microsoft.com/en-us/library/hb23x61k(VS.80).aspx
		// http://peterberkenbosch.wordpress.com/2006/07/13/structure-of-an-vs2005-solution/

		public class ProjectElement
		{
			public string Kind;

			public string Name;

			public string ProjectFile;

			public string Identifier;
		}

		public readonly List<ProjectElement> Projects = new List<ProjectElement>();


		public MVSSolutionFile(FileInfo Solution)
			: this(new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(Solution.FullName)))))
		{
		}
		public MVSSolutionFile(StreamReader data)
		{
			data.ReadLine();

			var Header = data.ReadLine();

			var Project = data.ReadLine();
			while (Project != null)
			{
				if (Project.StartsWith("Project"))
				{
					var r = ToValueReader(Project);
					var p = new ProjectElement
					{
						Kind = r(0),
						Name = r(1),
						ProjectFile = r(2),
						Identifier = r(3),
					};

					this.Projects.Add(p);
				}
				Project = data.ReadLine();
			}


		}

		public static Func<int, string> ToValueReader(string e)
		{
			var z = GetIndecies(e, "\"");

			return i => e.Substring(z[i * 2] + "\"".Length, z[i * 2 + 1] - z[i * 2] - "\"".Length);
		}

		public static int[] GetIndecies(string e, string x)
		{
			var a = new List<int>();

			var i = 0;
			var p = e.IndexOf(x, i);

			while (p >= 0)
			{
				a.Add(p);

				i = p + x.Length;
				p = e.IndexOf(x, i);
			}

			return a.ToArray();
		}

		public override string ToString()
		{
			var Projects = this.Projects.ToArray();

			var w = Projects.ToSolutionFile();

			return w.ToString();
		}

	}
}
