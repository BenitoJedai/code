using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Extensions
{
	public static class XLinqExtensions
	{
		public static IEnumerable<XElement> Add(this IEnumerable<XElement> source, params object[] content)
		{
			return source.WithEach(item => item.Add(content));
		}

		public static IEnumerable<XElement> ReplaceContentWith(this IEnumerable<XElement> source, params object[] content)
		{
			foreach (var item in source.ToArray())
			{
				item.RemoveAll();
				item.Add(content);
			}

			return source;
		}
	}
}
