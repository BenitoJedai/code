using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.Studio.StockMethods;
using ScriptCoreLib.Ultra.Studio.StockAttributes;

namespace ScriptCoreLib.Ultra.Studio.StockBuilders
{
	public class StockUltraApplicationBuilder
	{
		public StockUltraApplicationBuilder(
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
				Link = new Uri("http://studio.jsc-solutions.net"),
				MarginBottom = 1
			};

			var FileComments =
				new[]
					{
						FileHeader,
						Context.Interactive.ToVisualBasicLanguage,
						Context.Interactive.ToVisualCSharpLanguage,
						Context.Interactive.ToVisualFSharpLanguage
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

			DefaultPage.WriteHTMLElement(DefaultPageElement);


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
				var ApplicationWebServiceType = Context.Interactive.ApplicationWebServiceType;
				
				ApplicationWebServiceType.Namespace = Context.Name;
				ApplicationWebServiceType.Comments = FileComments;



				Context.Language.WriteType(ApplicationWebService, ApplicationWebServiceType, Context);
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
				Comments = FileComments
			};

			ApplicationType.UsingNamespaces.Add("System");
			ApplicationType.UsingNamespaces.Add("System.Text");
			ApplicationType.UsingNamespaces.Add("System.Linq");
			ApplicationType.UsingNamespaces.Add("System.Xml.Linq");
			ApplicationType.UsingNamespaces.Add("ScriptCoreLib");
			ApplicationType.UsingNamespaces.Add("ScriptCoreLib.JavaScript");
			ApplicationType.UsingNamespaces.Add("ScriptCoreLib.JavaScript.DOM");
			ApplicationType.UsingNamespaces.Add("ScriptCoreLib.JavaScript.DOM.HTML");
			ApplicationType.UsingNamespaces.Add("ScriptCoreLib.JavaScript.Components");
			ApplicationType.UsingNamespaces.Add("ScriptCoreLib.JavaScript.Extensions");
			ApplicationType.UsingNamespaces.Add("ScriptCoreLib.Extensions");
			ApplicationType.UsingNamespaces.Add(Context.Name + ".HTML.Pages");

			ApplicationType.Properties.Add(
				new SolutionProjectLanguageProperty
				{
					Name = "Property1",
					PropertyType = new SolutionProjectLanguageType
					{
						Name = "string"
					},
					GetMethod = new SolutionProjectLanguageMethod(),
					SetMethod = new SolutionProjectLanguageMethod()
				}
			);

			ApplicationType.Methods.Add(new StockMethodApplication(ApplicationType, Context.Interactive));


			Context.Language.WriteType(Application, ApplicationType, Context);

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
				AssemblyInfo.Write(Context.Language, Context, FileComments);

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
							),
							Context
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
					new StockAttributeObfuscation(),
					Context
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
					Comments = FileComments
				};

				ProgramType.UsingNamespaces.Add("System");
				ProgramType.UsingNamespaces.Add("jsc.meta.Commands.Rewrite.RewriteToUltraApplication");
				ProgramType.Methods.Add(new StockMethodMain(ApplicationType));

				Context.Language.WriteType(Program, ProgramType, Context);
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
