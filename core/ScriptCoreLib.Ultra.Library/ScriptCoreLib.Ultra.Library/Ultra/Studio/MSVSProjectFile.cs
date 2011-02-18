using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using System.IO;

namespace jsc.meta.Library
{

	public class MSVSProjectFile
	{
        // Microsoft Visual Studio Project File

		public string[] HintPaths;

        public string DefaultNamespace;

        public FileInfo[] NoneFiles;

		public static MSVSProjectFile FromFile(string filepath)
		{
            var ProjectFileName = new FileInfo(filepath);

			var csproj = XDocument.Load(filepath);

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
            var nsLink = ns + "Link";

			#endregion


			var HintPaths = csproj.Root.Elements(nsItemGroup).Elements(nsReference).Elements(nsHintPath).Select(k => k.Value).ToArray();

            var DefaultNamespace = Enumerable.First(
                 from PropertyGroup in csproj.Root.Elements(nsPropertyGroup)
                 //from RootNamespace in PropertyGroup.Elements(nsRootNamespace)
                 //select RootNamespace.Value

                 from __AssemblyName in PropertyGroup.Elements(nsAssemblyName)
                 select __AssemblyName.Value
            );

            var NoneFiles =

               from ItemGroup in csproj.Root.Elements(nsItemGroup)

               from None in ItemGroup.Elements(nsNone)

               let Link = None.Element(nsLink)

               let Include = None.Attribute("Include").Value

               // Directory In Project
               let Directory = Path.GetDirectoryName(Link != null ? Link.Value : Include).Replace("\\", "/")

               let File = new FileInfo(Link != null ? Include : Path.Combine(ProjectFileName.Directory.FullName, Include))

               select File;


			return new MSVSProjectFile
			{
				HintPaths = HintPaths,
                DefaultNamespace = DefaultNamespace,
                NoneFiles = NoneFiles.ToArray()
			};
		}

        public static implicit operator MSVSProjectFile(FileInfo f)
        {
            return MSVSProjectFile.FromFile(f.FullName);
        }
	}
}
