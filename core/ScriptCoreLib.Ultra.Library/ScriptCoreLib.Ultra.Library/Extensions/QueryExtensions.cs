using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{


	public static class QueryExtensions
	{
        public static List<T> WhenAny<T>(this List<T> that)
        {
            if (that == null)
                return null;

            if (that.Count == 0)
                return null;

            return that;
        }

		public static List<T> ToEmptyList<T>(this T template)
		{
			return new List<T>();
		}
	}
}
