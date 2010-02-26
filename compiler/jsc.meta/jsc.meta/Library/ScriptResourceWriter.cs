using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Collections;
using System.Xml.Linq;
using System.IO;

namespace jsc.meta.Library
{
	public class ScriptResourceWriter : IEnumerable
	{
		public readonly AssemblyBuilder Assembly;
		public readonly ModuleBuilder Module;

		public ScriptResourceWriter(AssemblyBuilder Assembly, ModuleBuilder Module)
		{
			this.Assembly = Assembly;
			this.Module = Module;
		}

		readonly List<string> AddScriptResources = new List<string>();

		public string Add(string name, XElement value)
		{
			var ScriptResources = name.Substring(0, name.LastIndexOf("/"));

			if (!AddScriptResources.Contains(ScriptResources))
			{
				AddScriptResources.Add(ScriptResources);

				this.Assembly.DefineAttribute<ScriptCoreLib.Shared.ScriptResourcesAttribute>(
					new { Value = ScriptResources }
				);
			}

			var n = this.Assembly.GetName().Name + ".web." + name.Replace("/", ".");

			this.Module.DefineManifestResource(n, value);

			return n;
		}

		public string Add(string name, byte[] value)
		{
			var ScriptResources = name.Substring(0, name.LastIndexOf("/"));

			if (!AddScriptResources.Contains(ScriptResources))
			{
				AddScriptResources.Add(ScriptResources);

				this.Assembly.DefineAttribute<ScriptCoreLib.Shared.ScriptResourcesAttribute>(
					new { Value = ScriptResources }
				);
			}

			System.Reflection.Assembly a = this.Assembly;

			var n = GetScriptResourcePath(name, a);

			this.Module.DefineManifestResource(n, new MemoryStream(value), System.Reflection.ResourceAttributes.Public);

			return n;
		}

		public static string GetScriptResourcePath(string name, System.Reflection.Assembly a)
		{
			var n = a.GetName().Name + ".web." + name.Replace("/", ".");
			return n;
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
