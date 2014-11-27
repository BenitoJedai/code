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
using ScriptCoreLib.Extensions;

namespace jsc.meta.Library
{
    [Obsolete("move into a nuget?")]
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

            public override string ToString()
            {
                return new { File, NamespaceDirectory }.ToString();
            }
        }


        public static readonly XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
        public static readonly XName nsItemGroup = ns + "ItemGroup";
        public static readonly XName nsRootNamespace = ns + "RootNamespace";
        public static readonly XName nsPropertyGroup = ns + "PropertyGroup";
        public static readonly XName nsNone = ns + "None";
        public static readonly XName nsContent = ns + "Content";
        public static readonly XName nsCompile = ns + "Compile";
        public static readonly XName nsDependentUpon = ns + "DependentUpon";
        public static readonly XName nsProjectReference = ns + "ProjectReference";
        public static readonly XName nsReference = ns + "Reference";
        public static readonly XName nsHintPath = ns + "HintPath";
        public static readonly XName nsAssemblyName = ns + "AssemblyName";
        public static readonly XName nsLink = ns + "Link";

        public XDocument Document;

        public static MSVSProjectFile FromFile(string filepath)
        {
            var ProjectFileName = new FileInfo(filepath);

            var Document = XDocument.Load(filepath);


            var HintPaths = Document.Root.Elements(nsItemGroup).Elements(nsReference).Elements(nsHintPath).Select(k => k.Value).ToArray();

            var DefaultNamespace = Enumerable.First(
                 from PropertyGroup in Document.Root.Elements(nsPropertyGroup)
                 //from RootNamespace in PropertyGroup.Elements(nsRootNamespace)
                 //select RootNamespace.Value

                 from __AssemblyName in PropertyGroup.Elements(nsAssemblyName)
                 select __AssemblyName.Value
            );


            // emulate $safeprojectname$
            DefaultNamespace = DefaultNamespace.Replace(" ", "_");

            #region GetFilesByType
            Func<XName, IEnumerable<ProjectFileInfo>> GetFilesByType =
                FileType =>
                      from ItemGroup in Document.Root.Elements(nsItemGroup)

                      from None in ItemGroup.Elements(FileType)

                      // <Link>%(RecursiveDir)%(FileName)%(Extension)</Link>
                      let Link0 = None.Element(nsLink)

                      // Include = "Y:\\opensource\\github\\elastic-droid\\src\\**\\*.*"
                      let Include0 = None.Attribute("Include").Value

                      // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20141127
                      // what about *.java ?
                      let Wildcard = @"**\*.*"
                      let IsWildcard = Include0.EndsWith(Wildcard)

                      let IncludeWildcard = Include0.TakeUntilLastOrNull(Wildcard)

                      let Include1 = !IsWildcard ?
                        new[] { Include0 } :
                        Directory.EnumerateFiles(IncludeWildcard, "*.*", SearchOption.AllDirectories)


                      from Include in Include1

                      let IncludeDirectory = Path.GetDirectoryName(Include)

                      let Replace = new
                      {
                          Extension = Path.GetExtension(Include),
                          FileName = Path.GetFileNameWithoutExtension(Include),
                          RecursiveDir = IncludeDirectory.SkipUntilOrEmpty(IncludeWildcard),
                      }

                      //let Include = Include0
                      let Link = Link0 == null ? null : new
                      {
                          Value =
                              Link0.Value
                              .Replace("%(Extension)", Replace.Extension)
                              .Replace("%(FileName)", Replace.FileName)
                              .Replace("%(RecursiveDir)", Replace.RecursiveDir == "" ? "" : Replace.RecursiveDir + "\\")
                      }

                      // Directory In Project
                      let Directory = Path.GetDirectoryName(Link != null ? Link.Value : Include).Replace("\\", "/")

                      let File = new FileInfo(
                          Link != null ? 
                          Include : 
                          Path.Combine(ProjectFileName.Directory.FullName, Include)
                          )

                      select new ProjectFileInfo { File = File, NamespaceDirectory = Directory };
            #endregion





            #region HasReference
            Func<FileInfo, bool> HasReference =
              AssemblyFile =>
              {
                  var TargetHintPath = GetRelativePath(
                      ProjectFileName.Directory.FullName,
                      AssemblyFile.FullName
                      );


                  return Enumerable.Any(
                       from ItemGroup in Document.Root.Elements(nsItemGroup)
                       from Reference in ItemGroup.Elements(nsReference)
                       from HintPath in Reference.Elements(nsHintPath)
                       where TargetHintPath == HintPath.Value
                       select new { HintPath, Reference, ItemGroup }
                  );
              };
            #endregion

            #region GetAssemblyReferences
            Func<FileInfo[]> GetAssemblyReferences =
               delegate
               {
                   var a =
                       from ItemGroup in Document.Root.Elements(nsItemGroup)
                       from Reference in ItemGroup.Elements(nsReference)
                       from HintPath in Reference.Elements(nsHintPath)
                       let AssemblyReferencePath = Path.Combine(new FileInfo(filepath).Directory.FullName, HintPath.Value)
                       select new FileInfo(AssemblyReferencePath);


                   //            <Reference Include="My.Solutions.Pages.OpCode.AssetsLibrary">
                   //  <HintPath>bin\staging.AssetsLibrary\My.Solutions.Pages.OpCode.AssetsLibrary.dll</HintPath>
                   //</Reference>

                   return a.ToArray();

               };
            #endregion

            #region GetProjectReferences
            Func<MSVSProjectFile[]> GetProjectReferences =
                delegate
                {
                    var a =
                       from ItemGroup in Document.Root.Elements(nsItemGroup)
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
            #endregion


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

                    if (!AssemblyFile.Exists)
                        return;

                    //var TargetHintPath = AssemblyFile.FullName.Substring(ProjectFileName.Directory.FullName.Length + 1);
                    var TargetHintPath = GetRelativePath(
                        ProjectFileName.Directory.FullName,
                        AssemblyFile.FullName
                        );

                    // sanity check
                    if (!HasReference(AssemblyFile))
                    {
                        var TargetItemGroup = Enumerable.First(
                            from ItemGroup in Document.Root.Elements(nsItemGroup)
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
                Document.Save(filepath);
            };

            return __this = new MSVSProjectFile
            {
                Document = Document,

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

            var v = path.ToString();

            if (v.StartsWith(@".\"))
                v = v.Substring(@".\".Length);

            return v;
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
