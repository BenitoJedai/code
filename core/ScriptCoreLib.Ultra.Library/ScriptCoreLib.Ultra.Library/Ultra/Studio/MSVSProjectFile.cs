using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace jsc.meta.Library
{

    public class MSVSProjectFile
    {
        // Microsoft Visual Studio Project File

        public string[] HintPaths;

        public string DefaultNamespace;

        public ProjectFileInfo[] NoneFiles;
        public ProjectFileInfo[] ContentFiles;

        public Func<FileInfo, bool> HasReference;

        public event Action ReferenceAdded;

        public Action<FileInfo, AssemblyName> AddReference;

        public Func<FileInfo[]> GetAssemblyReferences;
        public Func<MSVSProjectFile[]> GetProjectReferences;

        public Action Save;

        public void RaiseReferenceAdded()
        {
            if (ReferenceAdded != null)
                ReferenceAdded();
        }

        public class ProjectFileInfo
        {
            public FileInfo File;

            public static implicit operator FileInfo(ProjectFileInfo f)
            {
                return f.File;
            }

            public string NamespaceDirectory;
        }

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

            var nsProjectReference = ns + "ProjectReference";
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


            // emulate $safeprojectname$
            DefaultNamespace = DefaultNamespace.Replace(" ", "_");


            Func<XName, IEnumerable<ProjectFileInfo>> GetFilesByType =
                FileType =>
                      from ItemGroup in csproj.Root.Elements(nsItemGroup)

                      from None in ItemGroup.Elements(FileType)

                      let Link = None.Element(nsLink)

                      let Include = None.Attribute("Include").Value

                      // Directory In Project
                      let Directory = Path.GetDirectoryName(Link != null ? Link.Value : Include).Replace("\\", "/")

                      let File = new FileInfo(Link != null ? Include : Path.Combine(ProjectFileName.Directory.FullName, Include))

                      select new ProjectFileInfo { File = File, NamespaceDirectory = Directory };




            #region HasReference
            Func<FileInfo, bool> HasReference =
              AssemblyFile =>
              {
                  var TargetHintPath = AssemblyFile.FullName.Substring(ProjectFileName.Directory.FullName.Length + 1);

                  return Enumerable.Any(
                       from ItemGroup in csproj.Root.Elements(nsItemGroup)
                       from Reference in ItemGroup.Elements(nsReference)
                       from HintPath in Reference.Elements(nsHintPath)
                       where TargetHintPath == HintPath.Value
                       select new { HintPath, Reference, ItemGroup }
                  );
              };
            #endregion

            Func<FileInfo[]> GetAssemblyReferences =
               delegate
               {
                   var a =
                       from ItemGroup in csproj.Root.Elements(nsItemGroup)
                       from Reference in ItemGroup.Elements(nsReference)
                       from HintPath in Reference.Elements(nsHintPath)
                       let AssemblyReferencePath = Path.Combine(new FileInfo(filepath).Directory.FullName, HintPath.Value)
                       select new FileInfo(AssemblyReferencePath);


                   //            <Reference Include="My.Solutions.Pages.OpCode.AssetsLibrary">
                   //  <HintPath>bin\staging.AssetsLibrary\My.Solutions.Pages.OpCode.AssetsLibrary.dll</HintPath>
                   //</Reference>

                   return a.ToArray();

               };

            Func<MSVSProjectFile[]> GetProjectReferences =
                delegate
                {
                    var a =
                       from ItemGroup in csproj.Root.Elements(nsItemGroup)
                       from ProjectReference in ItemGroup.Elements(nsProjectReference)
                       let Include = ProjectReference.Attribute("Include")
                       let ProjectReferencePath = Path.Combine(new FileInfo(filepath).Directory.FullName, Include.Value)
                       where File.Exists(ProjectReferencePath)
                       select (MSVSProjectFile)new FileInfo(ProjectReferencePath);

                    //<ItemGroup>
                    //  <ProjectReference Include="..\..\My.Solutions.Pages.OpCode\My.Solutions.Pages.OpCode\My.Solutions.Pages.OpCode.csproj">
                    //    <Project>{5823d538-ea39-4d2a-9c15-f3bce76c9781}</Project>
                    //    <Name>My.Solutions.Pages.OpCode</Name>
                    //  </ProjectReference>



                    return a.ToArray();
                };

            MSVSProjectFile __this = null;

            #region AddReference
            Action<FileInfo, AssemblyName> AddReference =
                (AssemblyFile, Name) =>
                {

                    /* add reference
<Reference Include="AutoGeneratedReferences.Components.JohDoe.TextComponent, Version=0.0.0.0, Culture=neutral, processorArchitecture=MSIL">
  <SpecificVersion>False</SpecificVersion>
  <HintPath>bin\staging\AutoGeneratedReferences.Components.JohDoe.TextComponent.dll</HintPath>
</Reference>
                    */

                    //var TargetHintPath = AssemblyFile.FullName.Substring(ProjectFileName.Directory.FullName.Length + 1);
                    var TargetHintPath = GetRelativePath(
                        ProjectFileName.Directory.FullName,
                        AssemblyFile.FullName
                        );

                    // sanity check
                    if (!HasReference(AssemblyFile))
                    {
                        var TargetItemGroup = Enumerable.First(
                            from ItemGroup in csproj.Root.Elements(nsItemGroup)
                            from Reference in ItemGroup.Elements(nsReference)
                            select ItemGroup
                        );

                        TargetItemGroup.Add(
                            new XElement(nsReference,
                                new XAttribute("Include", Name.ToString()),
                                new XElement(nsHintPath, TargetHintPath)
                            )
                        );

                        __this.RaiseReferenceAdded();
                    }
                };
            #endregion

            Action Save = delegate
            {
                csproj.Save(filepath);
            };

            return __this = new MSVSProjectFile
            {
                HintPaths = HintPaths,
                DefaultNamespace = DefaultNamespace,
                NoneFiles = GetFilesByType(nsNone).ToArray(),
                ContentFiles = GetFilesByType(nsContent).ToArray(),
                HasReference = HasReference,
                AddReference = AddReference,
                Save = Save,
                GetProjectReferences = GetProjectReferences,
                GetAssemblyReferences = GetAssemblyReferences
            };
        }

        public static implicit operator MSVSProjectFile(FileInfo f)
        {
            return MSVSProjectFile.FromFile(f.FullName);
        }


        #region http://stackoverflow.com/questions/275689/how-to-get-relative-path-from-absolute-path
        public static string GetRelativePath(string fromPath, string toPath)
        {
            int fromAttr = GetPathAttribute(fromPath);
            int toAttr = GetPathAttribute(toPath);

            StringBuilder path = new StringBuilder(260); // MAX_PATH
            if (PathRelativePathTo(
                path,
                fromPath,
                fromAttr,
                toPath,
                toAttr) == 0)
            {
                throw new ArgumentException("Paths must have a common prefix");
            }
            return path.ToString();
        }

        private static int GetPathAttribute(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists)
            {
                return FILE_ATTRIBUTE_DIRECTORY;
            }

            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                return FILE_ATTRIBUTE_NORMAL;
            }

            throw new FileNotFoundException();
        }

        private const int FILE_ATTRIBUTE_DIRECTORY = 0x10;
        private const int FILE_ATTRIBUTE_NORMAL = 0x80;

        [DllImport("shlwapi.dll", SetLastError = true)]
        private static extern int PathRelativePathTo(StringBuilder pszPath,
            string pszFrom, int dwAttrFrom, string pszTo, int dwAttrTo);
        #endregion

    }
}
