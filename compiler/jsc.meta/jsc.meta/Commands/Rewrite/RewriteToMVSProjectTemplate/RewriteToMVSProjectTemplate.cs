using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using jsc.meta.Library;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Ultra.Studio.Languages;
using ScriptCoreLib.Extensions;

namespace jsc.meta.Commands.Rewrite.RewriteToVSProjectTemplate
{
    [Description(Description)]
    public partial class RewriteToMVSProjectTemplate : CommandBase
    {
        public const string Description = "Create project templates with ease!";

        public override void Invoke()
        {

            Console.WriteLine(Description);

            if (this.AttachDebugger)
                Debugger.Launch();

            if (this.DefaultToOrcas)
            {
                Console.WriteLine("DefaultToOrcas");

                this.UserProjectTemplates = ProjectTemplatesOrcas;
            }

            if (this.SDKProjectTemplates == null)
            {
                this.SDKProjectTemplates = new DirectoryInfo(
                    @"c:\util\jsc\templates\" + this.UserProjectTemplates
                );
            }


            var Attributes = this.AssemblyAttributes;

            if (this.Assembly != null)
            {
                var Assembly = System.Reflection.Assembly.LoadFile(this.Assembly.FullName);

                if (Attributes.Description == null)
                    Attributes.Description =
                        (
                            Assembly.GetCustomAttributes<AssemblyDescriptionAttribute>().FirstOrDefault()
                        )
                        .Description;

                if (Attributes.Title == null)
                    Attributes.Title =
                        (
                            Assembly.GetCustomAttributes<AssemblyTitleAttribute>().FirstOrDefault()
                        ).Title;

                if (Attributes.Company == null)
                    Attributes.Company =
                        (
                            Assembly.GetCustomAttributes<AssemblyCompanyAttribute>().FirstOrDefault()
                        ).Company;
            }

            var MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var ProjectTemplates = new DirectoryInfo(
                this.UserProjectTemplates.Contains(":") ? this.UserProjectTemplates : Path.Combine(MyDocuments, this.UserProjectTemplates)
            );

            Console.WriteLine(Attributes.Title);
            //Console.WriteLine(ProjectTemplates);
            //Console.WriteLine(ProjectTemplates);

            // http://msdn.microsoft.com/en-us/library/5we0w25d(VS.100).aspx
            var ProjectType = default(string);
            //
            if (this.ProjectFileName.Extension == KnownLanguages.VisualCSharp.ProjectFileExtension)
            {
                ProjectType = "CSharp";
                ProjectTemplates = ProjectTemplates.CreateSubdirectory("Visual C#");
                SDKProjectTemplates = SDKProjectTemplates.CreateSubdirectory("Visual C#");
            }
            else if (this.ProjectFileName.Extension == KnownLanguages.VisualFSharp.ProjectFileExtension)
            {
                ProjectType = "FSharp";
                ProjectTemplates = ProjectTemplates.CreateSubdirectory("Visual F#");
                SDKProjectTemplates = SDKProjectTemplates.CreateSubdirectory("Visual F#");
            }
            else if (this.ProjectFileName.Extension == KnownLanguages.VisualBasic.ProjectFileExtension)
            {
                ProjectType = "VisualBasic";
                ProjectTemplates = ProjectTemplates.CreateSubdirectory("Visual Basic");
                SDKProjectTemplates = SDKProjectTemplates.CreateSubdirectory("Visual Basic");
            }
            else
                throw new NotSupportedException();
            // VB, F#, Others ?

            Console.WriteLine("Company: " + Attributes.Company);

            ProjectTemplates = ProjectTemplates.CreateSubdirectory(Attributes.Company);
            SDKProjectTemplates = SDKProjectTemplates.CreateSubdirectory(Attributes.Company);

            const string _safeprojectname = "$safeprojectname$";

            var proj = XDocument.Load(this.ProjectFileName.FullName);

            #region ns
            XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
            var nsItemGroup = ns + "ItemGroup";
            var nsRootNamespace = ns + "RootNamespace";
            var nsPropertyGroup = ns + "PropertyGroup";
            var nsNone = ns + "None";
            var nsContent = ns + "Content";
            var nsCompile = ns + "Compile";
            var nsDependentUpon = ns + "DependentUpon";
            var nsReference = ns + "Reference";
            var nsHintPath = ns + "HintPath";
            var nsAssemblyName = ns + "AssemblyName";
            var nsEmbeddedResource = ns + "EmbeddedResource";
            var nsTargetFrameworkVersion = ns + "TargetFrameworkVersion";

            #endregion

            (
                from pg in proj.Root.Elements(nsPropertyGroup)
                from f in pg.Elements(nsTargetFrameworkVersion)
                select (Action)(() => f.Value = DefaultToOrcas ? "v3.5" : "v4.0")
            ).Invoke();

            var ProjectFiles =
              from ItemGroup in proj.Root.Elements(nsItemGroup)
              from Item in ItemGroup.Elements(nsNone)
                  .Concat(ItemGroup.Elements(nsContent))
                  .Concat(ItemGroup.Elements(nsCompile))
                  .Concat(ItemGroup.Elements(nsEmbeddedResource))
              let Include = Item.Attribute("Include").Value
              let Directory = Path.GetDirectoryName(Include)


              let ItemFile = new FileInfo(Path.Combine(ProjectFileName.Directory.FullName, Include))

              group new { ItemGroup, Item, Include, ItemFile, Directory } by Directory
            ;

            var zip = new ScriptCoreLib.Archive.ZIP.ZIPFile();

            XNamespace ns_vstemplate = "http://schemas.microsoft.com/developer/vstemplate/2005";

            zip["MyTemplate.vstemplate"].Text = new XElement(ns_vstemplate + "VSTemplate",
                new XAttribute("Type", "Project"),
                new XAttribute("Version", "3.0.0"),
                new XElement(ns_vstemplate + "TemplateData",
                    new XElement(ns_vstemplate + "Name", Attributes.Title),
                    new XElement(ns_vstemplate + "Description", Attributes.Description),
                    new XElement(ns_vstemplate + "ProjectType", ProjectType),
                    new XElement(ns_vstemplate + "ProjectSubType"),
                    new XElement(ns_vstemplate + "SortOrder", "1000"),
                    new XElement(ns_vstemplate + "CreateNewFolder", "true"),
                    new XElement(ns_vstemplate + "DefaultName", Attributes.Title.ToCamelCase()),
                    new XElement(ns_vstemplate + "ProvideDefaultName", "true"),
                    new XElement(ns_vstemplate + "LocationField", "Enabled"),
                    new XElement(ns_vstemplate + "EnableLocationBrowseButton", "true"),
                    new XElement(ns_vstemplate + "Icon", "__TemplateIcon.ico"),
                    new XElement(ns_vstemplate + "PreviewImage", "__PreviewImage.png")
                ),
                new XElement(ns_vstemplate + "TemplateContent",
                    new XElement(ns_vstemplate + "Project",
                        new XObject[] {
							new XAttribute("TargetFileName", this.ProjectFileName.Name),
							new XAttribute("File", this.ProjectFileName.Name),
							new XAttribute("ReplaceParameters", "true")
						}.Concat(
                            from g in ProjectFiles
                            where g.Key != ""
                            select (XObject)new XElement(ns_vstemplate + "Folder",
                                new XObject[] {
									new XAttribute("Name", g.Key),
									new XAttribute("TargetFolderName", g.Key),
								}.Concat(
                                    from f in g
                                    select (XObject)new XElement(ns_vstemplate + "ProjectItem",
                                        new XAttribute("ReplaceParameters", ShouldReplaceParameters(f.ItemFile.Extension)),
                                        new XAttribute("TargetFileName", f.ItemFile.Name),
                                        new XText(f.ItemFile.Name)
                                    )
                                )
                            )
                        ).Concat(
                            from g in ProjectFiles
                            where g.Key == ""
                            from f in g
                            select (XObject)new XElement(ns_vstemplate + "ProjectItem",
                                new XAttribute("ReplaceParameters", ShouldReplaceParameters(f.ItemFile.Extension)),
                                new XAttribute("TargetFileName", f.ItemFile.Name),
                                new XText(f.ItemFile.Name)
                            )
                        )

                    )
                )
            ).ToString();

            var ProjectContent = File.ReadAllText(this.ProjectFileName.FullName);

            ProjectContent = ProjectContent.Replace(Path.GetFileNameWithoutExtension(this.ProjectFileName.Name), _safeprojectname);

            ProjectContent = ProjectContent.Replace(@"c:\util\jsc\bin\jsc.meta.exe RewriteToUltraLibrary", @"rem c:\util\jsc\bin\jsc.meta.exe RewriteToUltraLibrary");
            ProjectContent = ProjectContent.Replace(@"c:\util\jsc\bin\jsc.meta.exe RewriteToMVSProjectTemplate", @"rem c:\util\jsc\bin\jsc.meta.exe RewriteToMVSProjectTemplate");
            ProjectContent = ProjectContent.Replace(@"c:\util\jsc\bin\jsc.meta.v4.0.30128.exe RewriteToMVSProjectTemplate", @"rem c:\util\jsc\bin\jsc.meta.exe RewriteToMVSProjectTemplate");


            zip[this.ProjectFileName.Name].Bytes = Encoding.UTF8.GetBytes(ProjectContent);

            foreach (var f in ProjectFiles.SelectMany(k => k))
            {
                if (f.Item.Name == nsCompile ||

                    f.Item.Name == nsContent &&
                        new[] { ".aspx", ".asax", ".htm" }.Contains(f.ItemFile.Extension)
                    )
                {
                    var Content = File.ReadAllText(f.ItemFile.FullName);

                    Content = Content.Replace(Path.GetFileNameWithoutExtension(this.ProjectFileName.Name), _safeprojectname);

                    zip[f.Include].Bytes = Encoding.UTF8.GetBytes(Content);
                }
                else
                {
                    zip[f.Include].Bytes = File.ReadAllBytes(f.ItemFile.FullName);
                }
            }

            zip["__TemplateIcon.ico"].Bytes = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("jsc.meta.Documents.__TemplateIcon_CS_JSC.ico").ToBytes();

            var PreviewFiles =
                from ItemGroup in proj.Root.Elements(nsItemGroup)
                from Item in ItemGroup.Elements(nsContent)
                let Include = Item.Attribute("Include").Value
                let Directory = Path.GetDirectoryName(Include)

                let ItemFile = new FileInfo(Path.Combine(ProjectFileName.Directory.FullName, Include))

                where ItemFile.Name == "Preview.png"

                select new { ItemGroup, Item, Include, ItemFile, Directory }
              ;

            var Preview = PreviewFiles.FirstOrDefault();

            if (Preview == null)
            {
                zip["__PreviewImage.png"].Bytes = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("jsc.meta.Documents.jsc.png").ToBytes();
            }
            else
            {
                zip["__PreviewImage.png"].Bytes = File.ReadAllBytes(Preview.ItemFile.FullName);
            }

            var ProjectTemplateFile = Path.Combine(ProjectTemplates.FullName, Attributes.Title + ".zip");
            Console.WriteLine(ProjectTemplateFile);
            File.WriteAllBytes(ProjectTemplateFile, zip.ToBytes());

            SDKProjectTemplates.Create();
            File.WriteAllBytes(Path.Combine(SDKProjectTemplates.FullName, Attributes.Title + ".zip"), zip.ToBytes());

        }


        public bool ShouldReplaceParameters(string extension)
        {
            if (extension == ".png")
                return false;

            if (extension == ".mp3")
                return false;

            return true;
        }
    }
}
