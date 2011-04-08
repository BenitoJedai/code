﻿using System;
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
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Extensions;

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

				this.Folders = this.SourceAssembly.GetCustomAttributes<ScriptResourcesAttribute>().OrderByDescending(k => k.Value.Length).Concat(
					new[] 
					{
						new ScriptResourcesAttribute(
							"assets/" + SourceAssembly.GetName().Name
						)
					}
				).ToArray();

				var Name = SourceAssembly.GetName().Name;

				// see: http://sites.google.com/a/jsc-solutions.net/wiki/home/documentation/knowledge-base/scriptresourceattribute

				var AssetPrefix = Name + ".web.";

				this.Assets = Enumerable.ToArray(
					from k in this.ManifestResourceNames
					where k.StartsWith(AssetPrefix)
					let n = k.Substring(AssetPrefix.Length)

					let Folder = Folders.FirstOrDefault(f => n.StartsWith(f.Value.Replace("/", ".")))

                    where Folder != null

					let File = n.Substring(Folder.Value.Length + 1)

					select Folder + "/" + File
				);
			}

			public void AddWhenResource(
				jsc.meta.Library.ScriptResourceWriter w,
				// ActionScript EmbedAttribute uses aboslute path
				string value
				)
			{
                if (value.StartsWith("/"))
                    value = value.Substring(1);


				var i = Array.IndexOf<string>(
					this.Assets,
					value
				);

				if (i < 0)
					return;

				var s = this.SourceAssembly.GetManifestResourceStream(
					this.ManifestResourceNames[i]
				);

				var Bytes = s.ToBytes();

				w.Add(value, Bytes);

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
