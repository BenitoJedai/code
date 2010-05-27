using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.StockAttributes;
using ScriptCoreLib.Ultra.Studio.StockMethods;
using ScriptCoreLib.Ultra.Studio.StockPages;
using ScriptCoreLib.Ultra.Studio.StockBuilders;
using ScriptCoreLib.Ultra.Studio.Languages;

namespace ScriptCoreLib.Ultra.Studio
{
	/// <summary>
	/// The SolutionBuilder type will build a project in languages like
	/// CSharp, FSharp and Visual Basic
	/// </summary>
	public partial class SolutionBuilder
	{
        public string Description { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }

		public XElement ApplicationPage { get; set; }

		public SolutionProjectLanguage Language { get; set; }

		public SolutionBuilderInteractive Interactive { get; set; }

		public SolutionBuilder()
		{
			this.Interactive = new SolutionBuilderInteractive();
			this.ApplicationPage = StockPageDefault.Element;
			this.Language = new Languages.VisualCSharpLanguage();
			this.Name = "VisualCSharpProject1";
            this.Description = "Write javascript, flash and java applets within a C# project. http://jsc-solutions.net";
            this.Company = "jsc-solutions.net";
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



	

		public IEnumerable<SolutionFile> ToFiles()
		{
			var a = new List<SolutionFile>();

			WriteTo(a.Add);

			return a;
		}

		public XElement[] References
		{
			get
			{
				var a = new List<XElement>();

				a.AddRange(
					VisualStudioTemplates.VisualCSharpProjectReferences.Elements().Select(k => new XElement(k))
				);

				var Reference =
					new XElement("Reference",
						new XAttribute("Include", Name + ".UltraSource"),
						new XElement("HintPath", @"bin\staging.UltraSource\" + Name + ".UltraSource.dll")
					);

				a.Add(Reference);

				return a.ToArray();
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
            var proj_Content = default(XElement);

            if (this.Language is VisualCSharpLanguage)
            {
                proj_Content = VisualStudioTemplates.VisualCSharpProject;
            }

            if (this.Language is VisualBasicLanguage)
            {
                proj_Content = VisualStudioTemplates.VisualBasicProject;
            }

            if (this.Language is VisualFSharpLanguage)
            {
                proj_Content = VisualStudioTemplates.VisualFSharpProject;
            }




			proj_Content.Elements("PropertyGroup").Elements("ProjectGuid").ReplaceContentWith(proj_Identifier);
			proj_Content.Elements("PropertyGroup").Elements("RootNamespace").ReplaceContentWith(Name);
			proj_Content.Elements("PropertyGroup").Elements("AssemblyName").ReplaceContentWith(Name);

			var ItemGroupReferenes = proj_Content.Elements("ItemGroup").Where(k => k.Elements("Reference").Any()).Single();


			UpdateReferences(ItemGroupReferenes);


			var ItemGroupForCompile = proj_Content.Elements("ItemGroup").Where(k => k.Elements("Compile").Any()).Single();


			ItemGroupForCompile.RemoveAll();

			// new operator is the new call opcode? :)
			new StockUltraApplicationBuilder(AddFile, this, ItemGroupForCompile);


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
			ItemGroupReferenes.ReplaceAll(
				this.References
			);
		}

	}
}
