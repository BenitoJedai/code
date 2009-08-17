using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;


namespace CreateStubTypes
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("This application will help you port an actionscript application to c#.");

			// fixme: this should be a parameter instead
			var project = @"C:\work\code.google\dodging-basenji\examples\actionscript\doomedonline\Doomedonline\Doomedonline.csproj";

			XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

			var doc = XDocument.Load(
				project
			);

			const string prefix = @"web\";
			const string suffix = ".as";

			var SourceFiles =
				from ItemGroup in doc.Root.Elements(ns + "ItemGroup")
				from Compile in ItemGroup.Elements(ns + "Compile")
				let Include = Compile.Attribute("Include").Value
				where Include.EndsWith(".cs")
				select new { ItemGroup, Include };

			var DefaultSourceFileGroup = SourceFiles.First();

			foreach (var h in
			  from ItemGroup in doc.Root.Elements(ns + "ItemGroup")
			  from File in ItemGroup.Elements(ns + "None")
			  let Include = File.Attribute("Include").Value
			  where Include.EndsWith(suffix)
			  where Include.StartsWith(prefix)
			  let FullName = Include.Substring(prefix.Length, Include.Length - prefix.Length - suffix.Length)
			  select new { FullName }
				)
			{
				var Segments = h.FullName.Split('\\');

				var dir = new FileInfo(project).Directory;
				var Namespace = "";

				foreach (var s in Segments.Take(Segments.Length - 1))
				{
					if (Namespace != "")
						Namespace += ".";

					Namespace += s;

					dir = dir.CreateSubdirectory(s);
				}

				var TypeName = Segments.Last();

				var Writer = new StringWriter();

				Writer.WriteLine(@"
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;
");

				if (Namespace != "")
				{
					Writer.WriteLine(@"
namespace " + Namespace + @"
{
");
				}

				Writer.WriteLine(@"
	[Script(IsNative = true)]
	public class " + TypeName + @"
	{
		// This class is a stub. You should implement it as it is currently implemented by actionscript.
	}
");

				if (Namespace != "")
				{
					Writer.WriteLine(@"
}
");
				}

				var TargetFile = Path.Combine(dir.FullName, TypeName + ".cs");

				if (!File.Exists(TargetFile))
					File.WriteAllText(TargetFile, Writer.ToString());

				var TargetSourceFile = SourceFiles.FirstOrDefault(k => k.Include == h.FullName + ".cs");
				if (TargetSourceFile != null)
				{
				}
				else
				{
					DefaultSourceFileGroup.ItemGroup.Add(
						new XElement(ns + "Compile", new XAttribute("Include", h.FullName + ".cs"))
					);
				}

				Console.WriteLine(h);

			}

			doc.Save(project);
		}
	}
}
