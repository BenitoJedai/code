using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.Extensions
{
	[Script]
	public class KnownEmbeddedResources 
	{
		public readonly List<Converter<string, Class>> Handlers = new List<Converter<string, Class>>();

		/// <summary>
		/// Walks through all the Handlers and returns the first matching Class 
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public Class this[string e]
		{
			get
			{
				var c = default(Class);

				foreach (var h in Handlers)
				{
					c = h(e);

					if (c != null)
						break;

				}

				return c;
			}
		}

		public static readonly KnownEmbeddedResources Default = new KnownEmbeddedResources();
	}
}
