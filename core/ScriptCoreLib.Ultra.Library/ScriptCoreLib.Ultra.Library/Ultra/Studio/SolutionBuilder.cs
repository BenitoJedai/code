using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Studio.StockMethods;
using ScriptCoreLib.Ultra.Studio.StockPages;
using System.Diagnostics;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.StockAttributes;
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
			this.ApplicationPage = StockPageDefault.Element;
			this.Name = "VisualCSharpProject1";
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

		public IEnumerable<SolutionFile> ToFiles()
		{
			var a = new List<SolutionFile>();

			WriteTo(a.Add);

			return a;
		}

		public IEnumerable<XElement> References
		{
			get
			{
				var a = new List<XElement>();

				VisualStudioTemplates.VisualCSharpProjectReferences.Elements().WithEach(a.Add);

				var Reference =
					new XElement("Reference",
						new XAttribute("Include", Name + ".UltraSource"),
						new XElement("HintPath", @"bin\staging.UltraSource\" + Name + ".UltraSource.dll")
					);

				a.Add(Reference);

				return a;
			}
		}

		public void WriteTo(Action<SolutionFile> AddFile)
		{
			var guid = Guid.NewGuid();
			var proj_Identifier = "{" + guid.ToString() + "}";

			var proj = new jsc.meta.Library.MVSSolutionFile.ProjectElement
			{
				ProjectFile = SolutionProjectFileNameRelativeToSolution,
				Name = Name,
				Kind = this.Language.Kind,
				Identifier = proj_Identifier
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

			#region first project in current solution
			var proj_Content = VisualStudioTemplates.VisualCSharpProject;




			proj_Content.Elements("PropertyGroup").Elements("ProjectGuid").ReplaceContentWith(proj_Identifier);
			proj_Content.Elements("PropertyGroup").Elements("RootNamespace").ReplaceContentWith(Name);
			proj_Content.Elements("PropertyGroup").Elements("AssemblyName").ReplaceContentWith(Name);

			var ItemGroupReferenes = proj_Content.Elements("ItemGroup").Where(k => k.Elements("Reference").Any()).Single();


			UpdateReferences(ItemGroupReferenes);


			var ItemGroupForCompile = proj_Content.Elements("ItemGroup").Where(k => k.Elements("Compile").Any()).Single();


			ItemGroupForCompile.RemoveAll();

			// new operator is the new call opcode? :)
			new UltraApplicationBuilder(AddFile, this, ItemGroupForCompile);


			// The default XML namespace of the project must be the MSBuild XML namespace. 
			// If the project is authored in the MSBuild 2003 format, 
			// please add xmlns="http://schemas.microsoft.com/developer/msbuild/2003" 
			// to the <Project> element. 
			// If the project has been authored in the old 1.0 or 1.2 format, 
			// please convert it to MSBuild 2003 format.




			AddFile(
				new SolutionFile
				{
					Name = SolutionProjectFileName,


					Content = proj_Content.ToString().Replace(
						// dirty little hack
						// http://stackoverflow.com/questions/461251/add-xml-namespace-attribute-to-3rd-party-xml

						"<Project ToolsVersion=\"3.5\" DefaultTargets=\"Build\">",
						"<Project ToolsVersion=\"4.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\" >"
					),
					Context = this
				}
			);


			#endregion

		}

		private void UpdateReferences(XElement ItemGroupReferenes)
		{
			ItemGroupReferenes.RemoveAll();
			var _R = this.References.ToArray();
			ItemGroupReferenes.Add(_R);
		}

		class UltraApplicationBuilder
		{
			public UltraApplicationBuilder(
				Action<SolutionFile> AddFile,
				SolutionBuilder Context,
				XElement ItemGroupForCompile)
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

				var FileHeader = new SolutionFileComment
				{
					Comment = "For more information visit:",
					Link = new Uri("http://studio.jsc-solutions.net")
				};

				#region DefaultPage
				var DefaultPageElement =
					new XElement("html",
						new XElement("head",
							new XElement("title", "DefaultPage")
						),
						Context.ApplicationPage
					);

				var DefaultPage =
					new SolutionFile
					{
						Name = ToProjectFile("Design/Default.htm"),
					};

				DefaultPage.Write(DefaultPageElement);
				

				ItemGroupForCompile.Add(
					new XElement("Content",
						new XAttribute("Include",
							@"Design\Default.htm"
						)
					)
				);

				AddFile(DefaultPage);

				#endregion


				// http://thevalerios.net/matt/2009/01/assembly-information-for-f-console-applications/

				#region ApplicationWebService
				var ApplicationWebService =
					new SolutionFile
					{
						Name = ToProjectFile("ApplicationWebService" + Context.Language.CodeFileExtension),
					};

				{
					var ApplicationWebServiceType = new SolutionProjectLanguageType
					{
						IsSealed = true,
						Namespace = Context.Name,
						Name = "ApplicationWebService",
						Summary = "This type can be used from javascript. The method calls will seamlessly be proxied to the server.",
						Header = FileHeader
					};

					ApplicationWebServiceType.UsingNamespaces.Add("System");
					ApplicationWebServiceType.UsingNamespaces.Add("System.Linq");
					ApplicationWebServiceType.UsingNamespaces.Add("System.Xml.Linq");
					ApplicationWebServiceType.UsingNamespaces.Add("ScriptCoreLib");
					ApplicationWebServiceType.Methods.Add(new StockMethodWebMethod("WebMethod2"));
					ApplicationWebServiceType.Methods.Add(new StockMethodWebMethod("WebMethod3"));

					Context.Language.WriteType(ApplicationWebService, ApplicationWebServiceType);
				}

				ItemGroupForCompile.Add(
					new XElement("Compile",
						new XAttribute("Include",
							"ApplicationWebService" + Context.Language.CodeFileExtension
						)
					)
				);

				AddFile(ApplicationWebService);
				#endregion

				#region Application
				var Application =
					new SolutionFile
					{
						Name = ToProjectFile("Application" + Context.Language.CodeFileExtension),
					};


				var ApplicationType = new SolutionProjectLanguageType
				{
					IsSealed = true,

					Namespace = Context.Name,
					Name = "Application",
					Summary = "This type can be used from javascript. The method calls will seamlessly be proxied to the server.",
					Header = FileHeader
				};

				ApplicationType.UsingNamespaces.Add("System");
				ApplicationType.UsingNamespaces.Add("System.Linq");
				ApplicationType.UsingNamespaces.Add("System.Xml.Linq");
				ApplicationType.UsingNamespaces.Add("ScriptCoreLib");
				ApplicationType.UsingNamespaces.Add("ScriptCoreLib.JavaScript");
				ApplicationType.UsingNamespaces.Add("ScriptCoreLib.JavaScript.DOM");
				ApplicationType.UsingNamespaces.Add("ScriptCoreLib.JavaScript.Extensions");
				ApplicationType.UsingNamespaces.Add("ScriptCoreLib.Extensions");
				ApplicationType.UsingNamespaces.Add(Context.Name + ".HTML.Pages");


				ApplicationType.Methods.Add(new StockMethodApplication(ApplicationType));

				Context.Language.WriteType(Application, ApplicationType);

				ItemGroupForCompile.Add(
					new XElement("Compile",
						new XAttribute("Include",
							"Application" + Context.Language.CodeFileExtension
						)
					)
				);


				AddFile(Application);
				#endregion

				#region AssemblyInfo
				var AssemblyInfo =
					new SolutionFile
					{
						Name = ToProjectFile("Properties/AssemblyInfo" + Context.Language.CodeFileExtension),
					};


				{
					FileHeader.WriteTo(AssemblyInfo, Context.Language);

					AssemblyInfo.WriteLine();

					Context.Language.WriteUsingNamespace(AssemblyInfo, "System.Reflection");

					AssemblyInfo.WriteLine();

					// language write assembly attribute

					Action<string, string> WriteGeneralInformation =
						(TypeName, Constant) =>
						{
							Context.Language.WriteAssemblyAttribute(
								AssemblyInfo,
								new StockAttributeGeneralInformation(
									new SolutionProjectLanguageType { Name = TypeName, Namespace = "System.Reflection" },
									Constant
								)
							);
						};

					Context.Language.WriteIndentedComment(AssemblyInfo,
@"General Information about an assembly is controlled through the following 
set of attributes. Change these attribute values to modify the information
associated with an assembly."
					);

					WriteGeneralInformation("AssemblyTitle", Context.Name);
					WriteGeneralInformation("AssemblyDescription", "Hello!");
					WriteGeneralInformation("AssemblyCompany", "ACME");
					WriteGeneralInformation("AssemblyProduct", Context.Name);
					WriteGeneralInformation("AssemblyCopyright", "Copyright 2010");
					WriteGeneralInformation("AssemblyVersion", "1.0.0.0");
					WriteGeneralInformation("AssemblyFileVersion", "1.0.0.0");

					Context.Language.WriteAssemblyAttribute(
						AssemblyInfo,
						new StockAttributeObfuscation()
					);

				}

				ItemGroupForCompile.Add(
					new XElement("Compile",
						new XAttribute("Include",
							@"Properties\AssemblyInfo" + Context.Language.CodeFileExtension
						)
					)
				);


				AddFile(AssemblyInfo);
				#endregion


				#region Program
				var Program =
					new SolutionFile
					{
						Name = ToProjectFile("Program" + Context.Language.CodeFileExtension),
						DependentUpon = Application
					};


				{
					var ProgramType = new SolutionProjectLanguageType
					{
						IsStatic = true,
						Namespace = Context.Name,
						Name = "Program",
						Summary = "This type can be used from javascript. The method calls will seamlessly be proxied to the server.",
						Header = FileHeader
					};

					ProgramType.UsingNamespaces.Add("System");
					ProgramType.UsingNamespaces.Add("jsc.meta.Commands.Rewrite.RewriteToUltraApplication");
					ProgramType.Methods.Add(new StockMethodMain(ApplicationType));

					Context.Language.WriteType(Program, ProgramType);
				}

				ItemGroupForCompile.Add(
					new XElement("Compile",
						new XAttribute("Include",
							"Program" + Context.Language.CodeFileExtension
						),
						new XElement("DependentUpon", "Application" + Context.Language.CodeFileExtension)
					)
				);


				AddFile(Program);
				#endregion

			}

		}
	}
}
