using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Collections;
using System.Xml.Linq;
using System.IO;
using jsc.Languages.IL;

namespace jsc.meta.Library
{
	public class ScriptResourceWriter : IEnumerable
	{
		public readonly AssemblyBuilder Assembly;
		public readonly ModuleBuilder Module;
		public readonly ILTranslationContext context;

		public ScriptResourceWriter(AssemblyBuilder Assembly, ModuleBuilder Module, ILTranslationContext context)
		{
			this.Assembly = Assembly;
			this.Module = Module;
			this.context = context;
		}

		readonly List<string> AddScriptResources = new List<string>();

		public string Add(string name, XElement value)
		{
			return Add(name, Encoding.UTF8.GetBytes(value.ToString()));
		}

		/// <summary>
		/// Long paths are not good. ASP.NET will fault.
		/// <example> var AssetPath = "assets/" + DefaultNamespace + "/" + name + Extension;</example>
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public string Add(string name, byte[] value)
		{
			var ScriptResources = name.Substring(0, name.LastIndexOf("/"));


			if (!AddScriptResources.Contains(ScriptResources))
			{
				AddScriptResources.Add(ScriptResources);

				var sra = new ScriptCoreLib.Shared.ScriptResourcesAttribute { Value = ScriptResources };

				this.Assembly.SetCustomAttribute(sra.ToCustomAttributeBuilder()(context));


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
