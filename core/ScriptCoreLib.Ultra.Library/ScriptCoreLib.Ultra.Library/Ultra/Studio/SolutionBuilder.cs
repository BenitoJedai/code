﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio
{
	/// <summary>
	/// The SolutionBuilder type will build a project in languages like
	/// CSharp, FSharp and Visual Basic
	/// </summary>
	public partial class SolutionBuilder
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

				var ApplicationWebService =
					new SolutionFile
					{
						Name = ToProjectFile("ApplicationWebService" + Context.Language.CodeFileExtension),
					};


				{
					var ApplicationWebServiceType = new SolutionProjectLanguageType
					{
						Namespace = Context.Name,
						Name = "ApplicationWebService",
						Summary = "This type can be used from javascript. The method calls will seamlessly be proxied to the server.",
						Comment = "Visit http://studio.jsc-solutions.net for more information!"
					};

					ApplicationWebServiceType.UsingNamespaces.Add("System");
					ApplicationWebServiceType.UsingNamespaces.Add("System.Linq");
					ApplicationWebServiceType.UsingNamespaces.Add("ScriptCoreLib");
					ApplicationWebServiceType.Methods.Add(WebMethod2("WebMethod2"));
					ApplicationWebServiceType.Methods.Add(WebMethod2("WebMethod3"));

					Context.Language.WriteType(ApplicationWebService, ApplicationWebServiceType);
				}

				AddFile(ApplicationWebService);


				var Application =
					new SolutionFile
					{
						Name = ToProjectFile("Application" + Context.Language.CodeFileExtension),
					};


				{
					var ApplicationType = new SolutionProjectLanguageType
					{
						Namespace = Context.Name,
						Name = "Application",
						Summary = "This type can be used from javascript. The method calls will seamlessly be proxied to the server.",
						Comment = "Visit http://studio.jsc-solutions.net for more information!"
					};

					ApplicationType.UsingNamespaces.Add("System");
					ApplicationType.UsingNamespaces.Add("System.Linq");
					ApplicationType.UsingNamespaces.Add("ScriptCoreLib");
					//ApplicationType.Methods.Add(WebMethod2("WebMethod2"));
					//ApplicationType.Methods.Add(WebMethod2("WebMethod3"));

					Context.Language.WriteType(Application, ApplicationType);
				}

				AddFile(Application);


				AddProjectFile("Properties/AssemblyInfo" + Context.Language.CodeFileExtension, "// hello");

				#region Program
				var Program =
					new SolutionFile
					{
						Name = ToProjectFile("Program" + Context.Language.CodeFileExtension),
						DependsOn = ApplicationWebService
					};


				{
					var ProgramType = new SolutionProjectLanguageType
					{
						IsStatic = true,
						Namespace = Context.Name,
						Name = "Program",
						Summary = "This type can be used from javascript. The method calls will seamlessly be proxied to the server.",
						Comment = "Visit http://studio.jsc-solutions.net for more information!"
					};

					ProgramType.UsingNamespaces.Add("System");
					ProgramType.Methods.Add(CreateMain());

					Context.Language.WriteType(Program, ProgramType);
				}

				AddFile(Program);
				#endregion

			}

		}
	}
}
