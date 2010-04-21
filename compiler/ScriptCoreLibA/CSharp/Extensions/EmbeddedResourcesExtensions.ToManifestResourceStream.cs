using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using ScriptCoreLib.Shared;

namespace ScriptCoreLib.CSharp.Extensions
{
	public static partial class EmbeddedResourcesExtensions
	{

		public static readonly Dictionary<string, ManifestResourceEntry> ToManifestResourceStream_Cache = new Dictionary<string, ManifestResourceEntry>();

	

		public static ManifestResourceEntry ToManifestResourceStream(this string e, Assembly context)
		{
			return InternalToMRS(e, context);

		}

		private static ManifestResourceEntry InternalToMRS(string e, Assembly context)
		{
			// fixme: this implementation needs cleanup and a rewrite

			Func<string, string> Diagnostics =
				eee =>
				{
					//Console.WriteLine("ToManifestResourceStream: " + eee);

					return eee;
				};

			Diagnostics("e: " + e);

			var request = e.Replace("/", ".");

			if (ToManifestResourceStream_Cache.ContainsKey(request))
				return ToManifestResourceStream_Cache[request];

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


			// to support partial trust - this workaround should be implemented:
			//Assembly assembly = Assembly.GetEntryAssembly();
			//AssemblyName[] names = assembly.GetReferencedAssemblies();
			//foreach (AssemblyName name in names)
			//{
			//    MessageBox.Show(name.Name + " " + name.Version.ToString());
			//}


			Diagnostics("Candidates");

			var References = Enumerable.Concat(
				
				
				SharedHelper.LoadReferencedAssemblies(context, true),
				
				
				new [] { context }
				
			).Distinct().ToArray();


			Diagnostics("References: " + References.Length);

			var Candidates = from assembly in References

							 let assembly__ = Diagnostics("assembly: " + assembly.FullName)
							 let name = new AssemblyName(assembly.FullName)
							 let assembly_resources = GetScriptResourcesAttributes(assembly)
							 let folders = assembly_resources.Concat(GetDefaultPaths(name)).OrderByDescending(k => k.Value.Length).ToArray()
							 from resource in assembly.GetManifestResourceNames()
							 from halfprefix in GetPrefixes(name)
							 let prefix = halfprefix + ".web."


							 let resource__ = Diagnostics("resource: " + resource)
							 where resource.StartsWith(prefix)

							 let prefixless = resource.Substring(prefix.Length)
							 let folderfound = new BooleanProperty()
							 from folder in folders

							 let folder__ = Diagnostics("folder: " + folder)

							 where !folderfound.Value
							 let folderprefix = folder.Value.Replace("/", ".")

							 let prefixless__ = Diagnostics("prefixless: " + prefixless + ", folderprefix" + folderprefix)
							 where prefixless.StartsWith(folderprefix)

							 let file = prefixless.Substring(folderprefix.Length + 1)
							 let stream = assembly.GetManifestResourceStream(resource)
							 let folderfound_value = folderfound.Value = true
							 select new ManifestResourceEntry
							 {
								 Stream = stream,
								 File = file,
								 ResourceName = resource,
								 VirtualPath = folder,
								 PrefixlessResourceName = prefixless
							 };

			var a = Candidates.ToArray();

			Diagnostics("ToManifestResourceStream_Cache");

			foreach (var v in a)
			{
				if (ToManifestResourceStream_Cache.ContainsKey(v.PrefixlessResourceName))
					continue;

				ToManifestResourceStream_Cache[v.PrefixlessResourceName] = v;
			}

			// did you embed the resource?
			// we may have multiple resources with the same name?
			var result = a.Where(k => k.PrefixlessResourceName == request).FirstOrDefault();

			if (result == null)
				throw new ArgumentOutOfRangeException(request);

			return result;
		}



	}
}
