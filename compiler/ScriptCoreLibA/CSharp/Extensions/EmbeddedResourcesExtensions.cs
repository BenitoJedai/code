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

		internal class BooleanProperty
		{
			public bool Value;
		}

		public static ManifestResourceEntry ToManifestResourceStream(this string e)
		{
			// what we know: assets/ConsoleApplication3/3.png
			// what we want: ConsoleApplication3.web.assets.ConsoleApplication3.3.png
			// how we map folders: [ScriptResourcesAttribute]
			// each assembly gets a default resource directory at ~.web.assets.~
			// we do not know which assembly it is defined in

			Func<AssemblyName, string> GetTopMostNamespace =
				an =>
				{
					var x = an.Name;
					var i = x.IndexOf(".");

					if (i == -1)
						return x;

					return x.Substring(0, i);
				};

			Func<AssemblyName, IEnumerable<string>> GetPrefixes =
				an =>
				{
					return new[]
					{
						an.Name,
						GetTopMostNamespace(an)
					}.Distinct();
				};

			Func<AssemblyName, IEnumerable<ScriptResourcesAttribute>> GetDefaultPaths =
				an =>
				{
					return GetPrefixes(an).Select(k => new ScriptResourcesAttribute("assets/" + k));
				};

			var Candidates = from assembly in SharedHelper.LoadReferencedAssemblies(Assembly.GetEntryAssembly(), true)
							 let name = new AssemblyName(assembly.FullName)
							 let folders = assembly.GetCustomAttributes(typeof(ScriptResourcesAttribute), false).Cast<ScriptResourcesAttribute>().Concat(GetDefaultPaths(name)).OrderByDescending(k => k.Value.Length).ToArray()
							 from resource in assembly.GetManifestResourceNames()
							 from halfprefix in GetPrefixes(name)
							 let prefix = halfprefix + ".web."
							 where resource.StartsWith(prefix)
							 let prefixless = resource.Substring(prefix.Length)
							 let folderfound = new BooleanProperty()
							 from folder in folders
							 where !folderfound.Value 
							 let folderprefix = folder.Value.Replace("/", ".")
							 where prefixless.StartsWith(folderprefix)
							 let request = e.Replace("/", ".")
							 where request == prefixless
							 let file = prefixless.Substring(folderprefix.Length + 1)
							 let stream = assembly.GetManifestResourceStream(resource)
							 let folderfound_value = folderfound.Value = true
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
