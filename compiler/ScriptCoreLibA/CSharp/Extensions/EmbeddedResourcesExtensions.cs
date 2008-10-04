using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using ScriptCoreLib.Shared;

namespace ScriptCoreLib.CSharp.Extensions
{
	public static class EmbeddedResourcesExtensions
	{

		public class ManifestResourceEntry
		{
			public ScriptResourcesAttribute VirtualPath;

			public string File;

			public string ResourceName;

			public Stream Stream;

			public string PrefixlessResourceName;
		}

		internal class BooleanProperty
		{
			public bool Value;
		}

		public static readonly Dictionary<string, ManifestResourceEntry> ToManifestResourceStream_Cache = new Dictionary<string, ManifestResourceEntry>();

		public static ManifestResourceEntry ToManifestResourceStream(this string e)
		{
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

			var Candidates = from assembly in SharedHelper.LoadReferencedAssemblies(Assembly.GetEntryAssembly(), true)
							 let name = new AssemblyName(assembly.FullName)
							 let assembly_resources = GetScriptResourcesAttributes(assembly)
							 let folders = assembly_resources.Concat(GetDefaultPaths(name)).OrderByDescending(k => k.Value.Length).ToArray()
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
							 //let request = e.Replace("/", ".")
							 //where request == prefixless
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
			

			foreach (var v in a)
			{
				if (ToManifestResourceStream_Cache.ContainsKey(v.PrefixlessResourceName))
					continue;

				ToManifestResourceStream_Cache[v.PrefixlessResourceName] = v;
			}

			return  a.Where(k => k.PrefixlessResourceName == request).Single();
		}



		public static void ExtractEmbeddedResources(DirectoryInfo dir, Assembly e)
		{
			ExtractEmbeddedResources(dir, e,
				 (v, tf, Path, File) =>
					  CopyStream(e.GetManifestResourceStream(v), tf.OpenWrite())
			);


		}

		public static ScriptResourcesAttribute[] GetScriptResourcesAttributes(Assembly assembly)
		{
			var assembly_types = 
				from assembly_type in assembly.GetTypes()
				let assembly_type_attribute = assembly_type.GetCustomAttributes(typeof(ScriptResourcesAttribute), false).Cast<ScriptResourcesAttribute>().SingleOrDefault()
				where assembly_type_attribute != null
				select assembly_type;

			return assembly.GetCustomAttributes(typeof(ScriptResourcesAttribute), false).Cast<ScriptResourcesAttribute>().Concat(
								from assembly_type in assembly_types
								from assembly_type_constant in assembly_type.GetFields()
								where assembly_type_constant.IsLiteral
								where assembly_type_constant.IsStatic
								where assembly_type_constant.FieldType == typeof(string)
								select new ScriptResourcesAttribute((string)assembly_type_constant.GetValue(assembly_type))
							 ).ToArray();
		}

		public static void ExtractEmbeddedResources(DirectoryInfo dir, Assembly e, Action<string, FileInfo, string, string> handler)
		{
			var DefaultResources = new ScriptResourcesAttribute("assets/" + e.GetName().Name);

			ScriptResourcesAttribute[] a =
				GetScriptResourcesAttributes(e)
				.Concat(
				new[] { 
                    // default
                    DefaultResources
                }).OrderByDescending(i => i.Value.Length).ToArray();

			string[] r = e.GetManifestResourceNames();

			AssemblyName n = new AssemblyName(e.FullName);

			var dir_Name = dir == null ? "web" : dir.Name;

			var prefix1 = n.Name + "." + dir_Name;

			Func<AssemblyName, string> GetTopMostNamespace =
				an =>
				{
					var x = an.Name;
					var i = x.IndexOf(".");

					if (i == -1)
						return x;

					return x.Substring(0, i);
				};

			var prefix2 = GetTopMostNamespace(n) + "." + dir_Name;

			// fixme: empty directory or overlapping directories with files containing "." .
			// fixme: 
			// "ScriptCoreLib.web.assets.Controls.NatureBoy.dude6.274.png"
			// "ScriptCoreLib.Controls.NatureBoy.web"

			Func<string, string, string> EnsureStartsWith =
				(_prefix, _subject) =>
				{
					if (_subject.StartsWith(_prefix))
						return _subject;

					return _prefix + _subject;
				};

			Func<string, string, string> EnsureNotStartsWith =
			   (_prefix, _subject) =>
			   {
				   if (_subject.StartsWith(_prefix))
					   return _subject.Substring(_prefix.Length);

				   return _subject;
			   };

			var prefixes = new[] { prefix1, prefix2 };

			var query = from v in r
						from prefix in prefixes.Distinct()
						where v.StartsWith(prefix)
						let z = (
									from av in a
									let ap = prefix + EnsureStartsWith(".", av.Value.Replace('/', '.'))
									where v.StartsWith(ap)
									select new { ap, av }
								  ).FirstOrDefault()
						where z != null
						let NewSubDir = EnsureNotStartsWith("/", z.av.Value)
						let t = (string.IsNullOrEmpty(NewSubDir) || dir == null) ? dir : dir.CreateSubdirectory(NewSubDir)
						let f = string.IsNullOrEmpty(NewSubDir) ? v.Substring(z.ap.Length) : v.Substring(z.ap.Length + 1)
						let tf = new FileInfo((t == null ? "" : t.FullName) + "/" + f)
						select new { v, tf, File = f, Path = z.av.Value };

			foreach (var p in query)
			{
				handler(p.v, p.tf, p.Path, p.File);

				//CopyStream(e.GetManifestResourceStream(p.v), p.tf.OpenWrite());
			}
		}

		public static void CopyStream(Stream FromStream, Stream ToStream)
		{

			try
			{
				//Creat a file to save to


				//use the binary reader & writer because
				//they can work with all formats
				//i.e images, text files ,avi,mp3..



				BinaryReader br = new BinaryReader(FromStream);


				BinaryWriter bw = new BinaryWriter(ToStream);


				//copy data from the FromStream to the outStream
				//convert from long to int 
				bw.Write(br.ReadBytes((int)FromStream.Length));
				//save
				bw.Flush();
				//clean up 
				bw.Close();
				br.Close();
			}

			 //use Exception e as it can handle any exception 
			catch (Exception)
			{
				//code if u like 
			}
		}

		public static string[] GetEmbeddedResources(DirectoryInfo web, Assembly e)
		{
			var a = new List<string>();

			#region fields
			EmbeddedResourcesExtensions.ExtractEmbeddedResources(web, e,
				(v, tf, Path, File) =>
				{
					var source = Path + "/" + File;

					a.Add(source);
				}
			);
			#endregion

			return a.ToArray();
		}

	}
}
