using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;

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
			WriteTo(k => AddFile(k.Name, k.Content));
		}

		public void WriteTo(Action<SolutionFile> AddFile)
		{
			var guid = Guid.NewGuid();

			var proj = new jsc.meta.Library.MVSSolutionFile.ProjectElement
			{
				ProjectFile = SolutionProjectFileNameRelativeToSolution,
				Name = Name,
				Kind = this.Language.Kind,
				Identifier = "{" + guid.ToString() + "}"
			};

			var projects = new[] { proj };

			AddFile(
				new SolutionFile
				{
					Name = SolutionFileName,
					Content = projects.ToSolutionFile().ToString(),
					Context = this
				}
			);

			AddFile(
				new SolutionFile
				{
					Name = SolutionProjectFileName,
					Content = VisualStudioTemplates.VisualCSharpProject.ToString(),
					Context = this
				}
			);

			// new operator is the new call opcode? :)
			new UltraApplicationBuilder(AddFile, this);
		}

		class UltraApplicationBuilder
		{
			public UltraApplicationBuilder(Action<SolutionFile> AddFile, SolutionBuilder Context)
			{
				Func<string, string> ToProjectFile =
					f => Context.Name + "/" + Context.Name + "/" + f;

				Func<string, string, SolutionFile> AddProjectFile =
					(f, t) =>
					{
						var r = new SolutionFile
						{
							Name = ToProjectFile(f),
							Content = t,
							Context = Context
						};

						AddFile(
							r
						);

						return r;
					};


				var ApplicationPage =
					new XElement("html",
						new XElement("head",
							new XElement("title", "ApplicationPage")
						),
						Context.ApplicationPage
					);


				AddProjectFile("Design/ApplicationPage.htm", ApplicationPage.ToString());

				// http://thevalerios.net/matt/2009/01/assembly-information-for-f-console-applications/

				AddFile(
					new SolutionFile
					{
						Name = ToProjectFile("ApplicationWebService" + Context.Language.CodeFileExtension),

					}.With(
						ApplicationWebService =>
						{
							// can we do XML comments? :)
							Context.Language.WriteCommentLine(ApplicationWebService, " hello world");

							#region using namespaces
							ApplicationWebService.Write(SolutionFileTextFragment.Keyword, "using");
							ApplicationWebService.WriteLine(SolutionFileTextFragment.None, " System;");

							ApplicationWebService.Write(SolutionFileTextFragment.Keyword, "using");
							ApplicationWebService.WriteLine(SolutionFileTextFragment.None, " System.Linq;");
							#endregion

							ApplicationWebService.WriteLine();

							ApplicationWebService.Write(SolutionFileTextFragment.Keyword, "namespace");
							ApplicationWebService.Write(SolutionFileTextFragment.None, " ");
							ApplicationWebService.Write(SolutionFileTextFragment.None, Context.Name);
							ApplicationWebService.WriteLine(SolutionFileTextFragment.None, "");
							ApplicationWebService.WriteLine(SolutionFileTextFragment.None, "{");
							ApplicationWebService.CurrentIndent++;


							Context.Language.WriteComment(
								ApplicationWebService,
								"hello world"
							);

							Context.Language.WriteIndent(ApplicationWebService);
							ApplicationWebService.Write(SolutionFileTextFragment.Keyword, "public");
							ApplicationWebService.Write(SolutionFileTextFragment.None, " ");
							ApplicationWebService.Write(SolutionFileTextFragment.Keyword, "class");
							ApplicationWebService.Write(SolutionFileTextFragment.None, " ");
							ApplicationWebService.Write(SolutionFileTextFragment.Type, "ApplicationWebService");
							ApplicationWebService.WriteLine(SolutionFileTextFragment.None, "");

							Context.Language.WriteIndent(ApplicationWebService);
							ApplicationWebService.WriteLine(SolutionFileTextFragment.None, "{");
							ApplicationWebService.CurrentIndent++;

							Context.Language.WriteIndent(ApplicationWebService);
							ApplicationWebService.Write(SolutionFileTextFragment.Keyword, "public");
							ApplicationWebService.Write(SolutionFileTextFragment.None, " ");
							ApplicationWebService.Write(SolutionFileTextFragment.Keyword, "void");
							ApplicationWebService.Write(SolutionFileTextFragment.None, " ");
							ApplicationWebService.Write(SolutionFileTextFragment.None, "AtServerMethod1");
							ApplicationWebService.Write(SolutionFileTextFragment.None, "(");

							ApplicationWebService.Write(SolutionFileTextFragment.Type, "XElement");
							ApplicationWebService.Write(SolutionFileTextFragment.None, " ");
							ApplicationWebService.Write(SolutionFileTextFragment.None, "e");

							ApplicationWebService.Write(SolutionFileTextFragment.None, ", ");

							ApplicationWebService.Write(SolutionFileTextFragment.Type, "Action");
							ApplicationWebService.Write(SolutionFileTextFragment.None, "<");
							ApplicationWebService.Write(SolutionFileTextFragment.Type, "XElement");
							ApplicationWebService.Write(SolutionFileTextFragment.None, ">");
							ApplicationWebService.Write(SolutionFileTextFragment.None, " ");
							ApplicationWebService.Write(SolutionFileTextFragment.None, "y");


							ApplicationWebService.Write(SolutionFileTextFragment.None, ")");
							ApplicationWebService.WriteLine();

							Context.Language.WriteIndent(ApplicationWebService);
							ApplicationWebService.WriteLine(SolutionFileTextFragment.None, "{");
							ApplicationWebService.CurrentIndent++;


							ApplicationWebService.CurrentIndent--;
							Context.Language.WriteIndent(ApplicationWebService);
							ApplicationWebService.WriteLine(SolutionFileTextFragment.None, "}");

							ApplicationWebService.CurrentIndent--;
							Context.Language.WriteIndent(ApplicationWebService);
							ApplicationWebService.WriteLine(SolutionFileTextFragment.None, "}");

							ApplicationWebService.CurrentIndent--;
							ApplicationWebService.WriteLine(SolutionFileTextFragment.None, "}");
						}
					)
				);

				AddProjectFile("Application" + Context.Language.CodeFileExtension, "// hello");

				// -->

				AddProjectFile("Properties/AssemblyInfo" + Context.Language.CodeFileExtension, "// hello");
				AddProjectFile("Program" + Context.Language.CodeFileExtension, "// hello");
			}
		}
	}
}
