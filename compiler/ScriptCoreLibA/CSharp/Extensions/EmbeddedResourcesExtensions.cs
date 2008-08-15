using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace ScriptCoreLib.CSharp.Extensions
{
	public static class EmbeddedResourcesExtensions
	{

		public class ManifestResourceEntry
		{
			public ScriptResourcesAttribute VirtualPath;

			public string File;

			public Stream Stream;
		}

		public static ManifestResourceEntry ToManifestResourceStream(this string e)
		{
			// what we know: assets/ConsoleApplication3/3.png
			// what we want: ConsoleApplication3.web.assets.ConsoleApplication3.3.png
			// how we map folders: [ScriptResourcesAttribute]
			// each assembly gets a default resource directory at ~.web.assets.~
			// we do not know which assembly it is defined in

			var Candidates = from assembly in SharedHelper.LoadReferencedAssemblies(Assembly.GetEntryAssembly(), true)
							 let name = new AssemblyName(assembly.FullName)
							 let defaultpath = "assets/" + name.Name
							 let folders = assembly.GetCustomAttributes(typeof(ScriptResourcesAttribute), false).Cast<ScriptResourcesAttribute>().Concat(new[] { new ScriptResourcesAttribute(defaultpath) }).OrderByDescending(k => k.Value.Length).ToArray()
							 let prefix = name.Name + ".web."
							 from resource in assembly.GetManifestResourceNames()
							 where resource.StartsWith(prefix)
							 let prefixless = resource.Substring(prefix.Length)
							 from folder in folders
							 let folderprefix = folder.Value.Replace("/", ".")
							 where prefixless.StartsWith(folderprefix)
							 let request = e.Replace("/", ".")
							 where request == prefixless
							 let file = prefixless.Substring(folderprefix.Length + 1)
							 let stream = assembly.GetManifestResourceStream(resource)
							 select new ManifestResourceEntry
							 {
								 Stream = stream,
								 File = file,
								 VirtualPath = folder
							 };



			return Candidates.Single();


		}
	}
}
