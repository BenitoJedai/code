using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using jsc.meta.Commands.Rewrite;
using System.IO;
using System.Xml.XPath;
using ScriptCoreLib.Archive.ZIP;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Reflection;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource
{
	partial class ReferenceUltraSource
	{
		[Description("Convert assemblies (.dll, .xml) from zip files into documentation.")]
		public class DefineDocumentation
		{
			public string DefaultNamespace;
			public XElement BodyElement;
			public RewriteToAssembly r;
			public Func<string, FileInfo> GetLocalResource;


			public void Define()
			{
				var Assemblies = Enumerable.ToArray(
					from a in this.BodyElement.XPathSelectElements("//a")

					let href = a.Attribute("href")
					where href != null


					let Archive = href.Value
					where Archive.EndsWith(".zip")

					let ArchiveTitleAttribute = a.Attribute("title")

					let ArchiveName = Archive.SkipUntilLastIfAny("/").TakeUntilLastIfAny(".")

					let ArchiveTitle = ArchiveTitleAttribute == null ? ArchiveName : ArchiveTitleAttribute.Value


					let res = GetLocalResource(Archive)
					let zip = res.ToZIPFile()
					from AssemblyEntry in zip.Entries
					let IsApplication = AssemblyEntry.FileName.ToLower().EndsWith(".exe")
					let IsLibrary = AssemblyEntry.FileName.ToLower().EndsWith(".dll")
					where IsApplication || IsLibrary

					let AssemblyDocumentationFile = AssemblyEntry.FileName.SkipUntilLastIfAny(".") + ".xml"
					let AssemblyDocumentation = zip.Entries.FirstOrDefault(k => k.FileName.ToLower() == AssemblyDocumentationFile)


					let DocumentationOrDefault = AssemblyDocumentation == null ? null : XDocument.Parse(AssemblyDocumentation.Text)

					let Assembly = Assembly.Load(AssemblyEntry.Bytes)

					let AssemblyName = Assembly.GetName().Name

					//let AssemblyName = AssemblyEntry.FileName.SkipUntilLastIfAny("/").TakeUntilLastIfAny(".")

					group new { ArchiveTitle, AssemblyName, DocumentationOrDefault, Assembly } by new { ArchiveTitle, AssemblyName }
				);

				{
					// http://www.google.com/search?sourceid=chrome&ie=UTF-8&q=define:+compilation
					// something that is compiled (as into a single book or file) 
					var t = this.r.RewriteArguments.Module.DefineType(
						this.DefaultNamespace + ".Documentation.Compilation", TypeAttributes.Public
					);

					t.CreateType();
				}

				foreach (var aa in Assemblies)
				{
					var a = aa.First();

					{
						var t = this.r.RewriteArguments.Module.DefineType(
							this.DefaultNamespace + ".Documentation." + a.ArchiveTitle + "." + a.AssemblyName + ".Assembly"
						);

						t.CreateType();
					}


					foreach (var at in a.Assembly.GetExportedTypes())
					{
						{
							var t = this.r.RewriteArguments.Module.DefineType(
								this.DefaultNamespace + ".Documentation." + a.ArchiveTitle + "." + a.AssemblyName + "." + at.FullName.SkipUntilIfAny(a.AssemblyName + ".")
							);

							t.CreateType();
						}
					}
				}
			}
		}
	}
}
