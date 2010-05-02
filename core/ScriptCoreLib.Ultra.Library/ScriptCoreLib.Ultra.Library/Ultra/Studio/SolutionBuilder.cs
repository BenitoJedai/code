﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionBuilder
	{
		public string Name { get; set; }

		public XElement ApplicationPage { get; set; }

		public SolutionProjectLanguage Language { get; set; }

		public SolutionBuilder()
		{
			this.Name = "UltraApplication1";
			this.ApplicationPage = new XElement("body", "hello world");
			this.Language = new Languages.VisualCSharpLanguage();
		}

		public string SolutionFileName
		{
			get
			{
				return Name + "/" + Name + ".sln";
			}
		}

		public string SolutionProjectFileNameRelativeToSolution
		{
			get
			{
				return Name + "/" + Name + this.Language.ProjectFileExtension;
			}
		}

		public string SolutionProjectFileName
		{
			get
			{
				return Name + "/" + SolutionProjectFileNameRelativeToSolution;
			}
		}



		public void WriteTo(Action<string, string> AddFile)
		{
			var proj = new jsc.meta.Library.MVSSolutionFile.ProjectElement
			{
				ProjectFile = SolutionProjectFileNameRelativeToSolution,
				Name = Name,
				Kind = this.Language.Kind,
				Identifier = "{" + Guid.NewGuid().ToString() + "}"
			};

			AddFile(SolutionFileName, new[] { proj }.ToSolutionFile().ToString());

			AddFile(SolutionProjectFileName,
				VisualStudioTemplates.VisualCSharpProject.ToString()
			);

			Action<string, string> AddProjectFile =
				(f, t) => AddFile(Name + "/" + Name + "/" + f, t);

			AddProjectFile("Design/ApplicationPage.htm",
				new XElement("html",
					new XElement("head",
						new XElement("title", "ApplicationPage")
					),
					this.ApplicationPage
				).ToString()
			);

			AddProjectFile("Application" + this.Language.CodeFileExtension, "// hello");
			AddProjectFile("Program" + this.Language.CodeFileExtension, "// hello");
			AddProjectFile("ApplicationWebService" + this.Language.CodeFileExtension, "// hello");
		}
	}
}
