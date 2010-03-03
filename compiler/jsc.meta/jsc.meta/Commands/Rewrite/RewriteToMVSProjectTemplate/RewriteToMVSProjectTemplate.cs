using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Diagnostics;
using jsc.meta.Library;

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

			var Assembly = System.Reflection.Assembly.LoadFile(this.Assembly.FullName);

			var Attributes = new
			{
				Assembly.GetCustomAttributes<AssemblyDescriptionAttribute>().First().Description,
				Assembly.GetCustomAttributes<AssemblyTitleAttribute>().First().Title,
				Assembly.GetCustomAttributes<AssemblyCompanyAttribute>().First().Company
			};

			var MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var ProjectTemplates = new DirectoryInfo(
				this.UserProjectTemplates.Contains(":") ? this.UserProjectTemplates : Path.Combine(MyDocuments, this.UserProjectTemplates)
			);

			Console.WriteLine(Attributes.Title);
			//Console.WriteLine(ProjectTemplates);
			//Console.WriteLine(ProjectTemplates);

			var ProjectType = default(string);

			if (this.ProjectFileName.Extension == ".csproj")
			{
				ProjectType = "CSharp";
				ProjectTemplates = ProjectTemplates.CreateSubdirectory("Visual C#");
				SDKProjectTemplates = SDKProjectTemplates.CreateSubdirectory("Visual C#");
			}
			else
				throw new NotSupportedException();
			// VB, F#, Others ?

			Console.WriteLine("Company: " + Attributes.Company);

			ProjectTemplates = ProjectTemplates.CreateSubdirectory(Attributes.Company);
			SDKProjectTemplates = SDKProjectTemplates.CreateSubdirectory(Attributes.Company);

			const string _safeprojectname = "$safeprojectname$";

			var csproj = XDocument.Load(this.ProjectFileName.FullName);

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

			#endregion


			var ProjectFiles =
			  from ItemGroup in csproj.Root.Elements(nsItemGroup)
			  from Item in ItemGroup.Elements(nsNone).Concat(ItemGroup.Elements(nsContent)).Concat(ItemGroup.Elements(nsCompile))
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
			ProjectContent = ProjectContent.Replace(@"c:\util\jsc\bin\jsc.meta.exe RewriteToMVSProjectTemplate", @"rem c:\util\jsc\bin\jsc.meta.exe RewriteToMVSProjectTemplate");


			zip[this.ProjectFileName.Name].Bytes = Encoding.UTF8.GetBytes(ProjectContent);

			foreach (var f in ProjectFiles.SelectMany(k => k))
			{
				if (f.Item.Name == nsCompile ||

					f.Item.Name == nsContent && f.ItemFile.Extension == ".htm")
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

			zip["__TemplateIcon.ico"].Bytes = Assembly.GetExecutingAssembly().GetManifestResourceStream("jsc.meta.Documents.__TemplateIcon_CS_JSC.ico").ToBytes();
			zip["__PreviewImage.png"].Bytes = Assembly.GetExecutingAssembly().GetManifestResourceStream("jsc.meta.Documents.jsc.png").ToBytes();

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
