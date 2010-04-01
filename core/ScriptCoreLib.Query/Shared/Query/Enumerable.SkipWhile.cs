using ScriptCoreLib;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using System;
using ScriptCoreLib.Shared.BCLImplementation.System.Linq;

namespace ScriptCoreLib.Shared.Query
{

	internal static partial class __Enumerable
	{


		public static IEnumerable<T> SkipWhile<T>(this IEnumerable<T> source, global::System.Func<T, bool> predicate)
		{
			var _Where = false;

			return source.Where(
				k =>
				{
					var r = _Where;

					if (!_Where)
						if (predicate(k))
							_Where = true;

					return r;
				}
			);
		}

	}
}
