using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using System;
using ScriptCoreLib.Shared.BCLImplementation.System.Linq;

namespace ScriptCoreLib.Shared.Query
{

    internal  static partial class __Enumerable
    {
		public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
		{
			var r = default(TSource);
			var i = -1;
			foreach (var item in source.AsEnumerable())
			{
				i++;
				if (i == index)
				{
					r = item;
					break;
				}
			}
			return r;
		}
    }
}
