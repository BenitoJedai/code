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
		public string[] HintPaths;

		public static MSVSProjectFile FromFile(string filepath)
		{
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

			#endregion


			var HintPaths = csproj.Root.Elements(nsItemGroup).Elements(nsReference).Elements(nsHintPath).Select(k => k.Value).ToArray();


			return new MSVSProjectFile
			{
				HintPaths = HintPaths
			};
		}
	}
}
