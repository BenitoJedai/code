using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
	public static class UltraLambdaExtensions
	{
		public static T With<T>(this T e, Action<T> h) where T : class
		{
			h(e);

			return e;
		}

	}
}
