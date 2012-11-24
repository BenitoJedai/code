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
            this.Description = "Write JavaScript, Adobe Flash and Oracle Java Applets within a single .NET project. http://jsc-solutions.net";
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

        #region References + AssetsLibrary
        public XElement[] References
        {
            get
            {
                var a = new List<XElement>();

                a.AddRange(
                    VisualStudioTemplates.VisualCSharpProjectReferences.Elements().Select(k => new XElement(k))
                );




                var AssetsLibrary = new XElement("Reference",
                    new XAttribute("Include", Name + ".AssetsLibrary"),
                    new XElement("HintPath", @"bin\staging.AssetsLibrary\" + Name + ".AssetsLibrary.dll")
                );

                a.Add(AssetsLibrary);

                return a.ToArray();
            }
        }
        #endregion

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

            Console.WriteLine("Selecting project template by language");

            if (this.Language is VisualCSharpLanguage)
            {
                Console.WriteLine("Selecting VisualCSharpLanguage");
                proj_Content = VisualStudioTemplates.VisualCSharpProject.Clone();
                //proj_Content = VisualStudioTemplates.VisualCSharpProjectReferences;
            }

            if (this.Language is VisualBasicLanguage)
            {
                Console.WriteLine("Selecting VisualBasicLanguage");
                proj_Content = VisualStudioTemplates.VisualBasicProject.Clone();
            }

            if (this.Language is VisualFSharpLanguage)
            {
                Console.WriteLine("Selecting VisualFSharpLanguage");
                proj_Content = VisualStudioTemplates.VisualFSharpProject.Clone();
            }


            Console.WriteLine(proj_Content.ToString());

            proj_Content.Elements("PropertyGroup").Elements("ProjectGuid").ReplaceContentWith(proj_Identifier);
            proj_Content.Elements("PropertyGroup").Elements("RootNamespace").ReplaceContentWith(Name);
            proj_Content.Elements("PropertyGroup").Elements("AssemblyName").ReplaceContentWith(Name);

            // how many item groups are there that have references?
            Console.WriteLine("Looking for ItemGroup for Referenes...");
            var ItemGroupReferenes = proj_Content.Elements("ItemGroup").Where(k => k.Elements("Reference").Any()).Single();
            Console.WriteLine("ItemGroupReferenes...");
            UpdateReferences(ItemGroupReferenes);

            // 
            var ItemGroupForCompile = proj_Content.Elements("ItemGroup").Where(k => k.Elements("Compile").Any()).Single();
            ItemGroupForCompile.RemoveAll();

            // new operator is the new call opcode? :)
            new StockUltraApplicationBuilder(AddFile, this, ItemGroupForCompile,
                StartupType =>
                {
                    proj_Content.Elements("PropertyGroup").Elements("StartupObject").ReplaceContentWith(

                        StartupType.FullName
                    );
                }
            );


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
