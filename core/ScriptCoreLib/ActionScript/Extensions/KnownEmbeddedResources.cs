using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ScriptCoreLib.ActionScript.Extensions
{
	[Script]
	[EmbedResources]
	public class KnownEmbeddedResources
	{
		public readonly List<Converter<string, Class>> Handlers = new List<Converter<string, Class>>();

		internal readonly Dictionary<string, Class> Cache = new Dictionary<string, Class>();

		/// <summary>
		/// Walks through all the Handlers and returns the first matching Class 
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public Class this[string e]
		{
			get
			{
				if (Cache.ContainsKey(e))
					return Cache[e];

				var c = default(Class);

				foreach (var h in Handlers)
				{
					if (h == null)
						throw new ArgumentNullException();

					c = h(e);

					if (c != null)
						break;

				}

				return c;
			}
			set
			{
				Cache[e] = value;
			}
		}

		public static readonly KnownEmbeddedResources Default = new KnownEmbeddedResources();
	}
}
