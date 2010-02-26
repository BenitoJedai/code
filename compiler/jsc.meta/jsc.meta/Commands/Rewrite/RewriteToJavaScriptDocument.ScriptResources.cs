using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using jsc.Library;
using System.Diagnostics;
using ScriptCoreLib.CSharp.Extensions;
using ScriptCoreLib.Shared;
using jsc.meta.Library;

namespace jsc.meta.Commands.Rewrite
{
	partial class RewriteToJavaScriptDocument
	{
		public class ScriptResourcesByAssembly
		{
			public readonly Assembly SourceAssembly;
			public readonly string[] ManifestResourceNames;

			public readonly string[] Assets;
			public readonly ScriptResourcesAttribute[] Folders;


			public ScriptResourcesByAssembly(Assembly SourceAssembly)
			{
				this.SourceAssembly = SourceAssembly;

				this.ManifestResourceNames = SourceAssembly.GetManifestResourceNames();

				this.Folders = this.SourceAssembly.GetCustomAttributes<ScriptResourcesAttribute>();

				var Name = SourceAssembly.GetName().Name;

				// see: http://sites.google.com/a/jsc-solutions.net/wiki/home/documentation/knowledge-base/scriptresourceattribute

				var AssetPrefix = Name + ".web.";

				this.Assets = Enumerable.ToArray(
					from k in this.ManifestResourceNames
					where k.StartsWith(AssetPrefix)
					let n = k.Substring(AssetPrefix.Length)

					let Folder = Folders.First(f => n.StartsWith(f.Value.Replace("/", ".")))
					let File = n.Substring(Folder.Value.Length + 1)

					select Folder + "/" + File
				);
			}

			public void AddWhenResource(
				jsc.meta.Library.ScriptResourceWriter w,
				string p
				)
			{
				var i = Array.IndexOf<string>(this.Assets, p);

				if (i < 0)
					return;

				var s = this.SourceAssembly.GetManifestResourceStream(
					this.ManifestResourceNames[i]
				);

				var Bytes = s.ToBytes();

				w.Add(p, Bytes);

				// Slot used! We should not add the asset twice!
				this.Assets[i] = null;
			}
		}

		public class ScriptResources
		{
			public readonly VirtualDictionary<Assembly, ScriptResourcesByAssembly> Cache = new VirtualDictionary<Assembly, ScriptResourcesByAssembly>();

			public ScriptResources()
			{
				Cache.Resolve +=
					SourceAssembly =>
					{
						Cache[SourceAssembly] = new ScriptResourcesByAssembly(SourceAssembly);
					};
			}


		}
	}
}
